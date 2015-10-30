/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */


using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using LibLSLCC.CodeValidator.Components;
using LibLSLCC.CodeValidator.Enums;
using LibLSLCC.CodeValidator.Primitives;
using LibLSLCC.LibraryData;
using LibLSLCC.LibraryData.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using OpenMetaverse;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.ScriptEngine.Interfaces;
using OpenSim.Region.ScriptEngine.Shared.CodeTools;
using OpenSim.Region.ScriptEngine.Shared.ScriptBase;
using Tools;
using liblslcc = LibLSLCC;
using Path = System.IO.Path;

namespace OpenSim.Region.ScriptEngine.Shared.LibLSLCCCompiler
{
    public class Compiler : ICompiler
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private LSLLibraryDataProvider _libLslccMainLibraryDataProvider;


        // * Uses "LSL2Converter" to convert LSL to C# if necessary.
        // * Compiles C#-code into an assembly
        // * Returns assembly name ready for AppDomain load.
        //
        // Assembly is compiled using LSL_BaseClass as base. Look at debug C# code file created when LSL script is compiled for full details.
        //

        internal enum CompileLanguage
        {
            lsl = 0,
            cs = 1,
            vb = 2
        }


        private CompileLanguage DefaultCompileLanguage;
        private bool WriteScriptSourceToDebugFile;
        private bool CompileWithDebugInformation;

        private readonly Dictionary<string, bool> AllowedCompilers =
            new Dictionary<string, bool>(StringComparer.CurrentCultureIgnoreCase);

        private readonly Dictionary<string, CompileLanguage> LanguageMapping =
            new Dictionary<string, CompileLanguage>(StringComparer.CurrentCultureIgnoreCase);

        private bool m_insertCoopTerminationCalls;

        private string FilePrefix;

        private readonly string ScriptEnginesPath = null;


        private ICodeConverter LSL_Converter;

        private List<string> m_warnings = new List<string>();


        private static CSharpCodeProvider CScodeProvider = new CSharpCodeProvider();
        private static VBCodeProvider VBcodeProvider = new VBCodeProvider();


        private readonly IScriptEngine m_scriptEngine;

        private bool in_startup = true;


        public Compiler(IScriptEngine scriptEngine)
        {
            m_scriptEngine = scriptEngine;
            ScriptEnginesPath = scriptEngine.ScriptEnginePath;

            InitLibraryData();
            ReadConfig();
        }

        private class ConstantValueStringConverter : ILSLValueStringConverter
        {

            public bool Convert(LSLType constantType, object fieldValue, out string valueString)
            {
                Type objType = fieldValue.GetType();

                if (objType == typeof(LSL_Types.list))
                {
                    throw new ArgumentException(
                        "LSL_Type.list is not supported for use with constants defined in script modules", "obj");
                }

                //every other mapped type's ToString() value can successfully be parsed by LibLSLCC's LSLLibraryConstantSignature
                valueString = fieldValue.ToString();
                return true;
            }
        }



        private void InitLibraryData()
        {
            var comms = m_scriptEngine.World.RequestModuleInterface<IScriptModuleComms>();


            _libLslccMainLibraryDataProvider = new LSLLibraryDataProvider(new[] {"os-lsl"}, false);
            _libLslccMainLibraryDataProvider.AddSubsetDescription(new LSLLibrarySubsetDescription("os-lsl",
                "All OpenSim Modules"));

            _libLslccMainLibraryDataProvider.AddSubsetDescription(new LSLLibrarySubsetDescription("lsl",
                "LSL, Required for event definition the hack at the end of this function."));


            var reflectionSerializer = new LSLLibraryDataReflectionSerializer()
            {
                AttributedConstantsOnly = false,
                AttributedMethodsOnly = false,
            };

            var paramTypeMapping = new LSLTypeConverterMap();
            reflectionSerializer.ReturnTypeConverter = paramTypeMapping;
            

            paramTypeMapping.Add(typeof (void), LSLType.Void);

            paramTypeMapping.Add(typeof (LSL_Types.Vector3), LSLType.Vector);
            paramTypeMapping.Add(typeof (LSL_Types.LSLFloat), LSLType.Float);
            paramTypeMapping.Add(typeof (LSL_Types.LSLInteger), LSLType.Integer);
            paramTypeMapping.Add(typeof (LSL_Types.LSLString), LSLType.String);
            paramTypeMapping.Add(typeof (LSL_Types.Quaternion), LSLType.Rotation);
            paramTypeMapping.Add(typeof (LSL_Types.key), LSLType.Key);


            paramTypeMapping.Add(typeof (LSL_Types.list), LSLType.List);
            paramTypeMapping.Add(typeof (object[]), LSLType.List);
            paramTypeMapping.Add(typeof (string[]), LSLType.List);

            paramTypeMapping.Add(typeof (string), LSLType.String);

            paramTypeMapping.Add(typeof (bool), LSLType.Integer);
            paramTypeMapping.Add(typeof (sbyte), LSLType.Integer);
            paramTypeMapping.Add(typeof (byte), LSLType.Integer);
            paramTypeMapping.Add(typeof (char), LSLType.Integer);
            paramTypeMapping.Add(typeof (short), LSLType.Integer);
            paramTypeMapping.Add(typeof (ushort), LSLType.Integer);
            paramTypeMapping.Add(typeof (int), LSLType.Integer);
            paramTypeMapping.Add(typeof (uint), LSLType.Integer);
            paramTypeMapping.Add(typeof (long), LSLType.Integer);
            paramTypeMapping.Add(typeof (ulong), LSLType.Integer);

            paramTypeMapping.Add(typeof (float), LSLType.Float);
            paramTypeMapping.Add(typeof (double), LSLType.Float);
            paramTypeMapping.Add(typeof (decimal), LSLType.Float);

            paramTypeMapping.Add(typeof (UUID), LSLType.Key);

            //not sure if these are necessary, but just in case?
            paramTypeMapping.Add(typeof (Vector3), LSLType.Vector);
            paramTypeMapping.Add(typeof (Vector3d), LSLType.Vector);
            paramTypeMapping.Add(typeof (Vector4), LSLType.Rotation);
            paramTypeMapping.Add(typeof (Quaternion), LSLType.Rotation);


            //clone the dictionary
            var constantMapping = paramTypeMapping.Clone();

            reflectionSerializer.ConstantTypeConverter = constantMapping;

            constantMapping.Remove(typeof (void));


            var paramMapping = constantMapping.Clone();

            reflectionSerializer.ParamTypeConverter = paramMapping;

            //for variadic void parameters only, we need to filter out
            //method info that contains parameters with 'object' parameters that are not variadic
            //because only a variadic parameters base type is used for the mapping, so actual 'object'
            //parameters can get declared as LSLType.Void.
            paramMapping.Add(typeof (object), LSLType.Void);


            


            var methodFilter = new LSLLambdaMethodFilter()
            {
                PreFilterFunction = (serializer, info) =>
                {
                    //we don't want non variadic 'object' parameters.
                    //variadic object parameters will have the parameter type typeof(object[]) and have a variadic
                    //attribute.  We don't even need to look for the attribute because if its not an array type then
                    //the possibility of it being variadic is ruled out immediately.
                    return info.GetParameters().Any(x => x.ParameterType == typeof (object));
                }
            };


            reflectionSerializer.MethodFilter = methodFilter;

            reflectionSerializer.FieldBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                                     BindingFlags.Instance | BindingFlags.DeclaredOnly;

            reflectionSerializer.PropertyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                        BindingFlags.Static | BindingFlags.Instance |
                                                        BindingFlags.DeclaredOnly;

            reflectionSerializer.MethodBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
                                                      BindingFlags.Instance | BindingFlags.DeclaredOnly;


            reflectionSerializer.ValueStringConverter = new ConstantValueStringConverter();

            foreach (var method in reflectionSerializer.DeSerializeMethods(typeof (ScriptBaseClass)))
            {
                method.AddSubsets("os-lsl");
                _libLslccMainLibraryDataProvider.DefineFunction(method);
            }

            foreach (var method in reflectionSerializer.DeSerializeConstants(typeof (ScriptBaseClass)))
            {
                method.AddSubsets("os-lsl");
                _libLslccMainLibraryDataProvider.DefineConstant(method);
            }


            foreach (var method in comms.GetScriptInvocationList().Select(x => x.Method))
            {
                var def = reflectionSerializer.DeSerializeMethod(method);
                if (def != null)
                {
                    //make sure it does not get filtered when you add it by giving it the all subset
                    def.AddSubsets("os-lsl");
                    def.ModInvoke = true;
                    _libLslccMainLibraryDataProvider.DefineFunction(def);
                }
                else
                {
                    throw new Exception("LibLSLCC failed to reflect OpenSim runtime types!");
                }
            }


            foreach (var constant in comms.GetConstants())
            {
                Type valueType = constant.Value.GetType();

                LSLType t;

                //the Field type mappings an Property type mappings are the same in our case.
                if (reflectionSerializer.ConstantTypeConverter.Convert(valueType, out t))
                {
                    string valueString;
                    reflectionSerializer.ValueStringConverter.Convert(t, constant.Value, out valueString);

                    //the value needs to be expanded by the compiler
                    var cdef = new LSLLibraryConstantSignature(t, constant.Key,valueString) {Expand = true};

                    cdef.AddSubsets("os-lsl");
                    _libLslccMainLibraryDataProvider.DefineConstant(cdef);
                }
                else
                {
                    throw new Exception(
                        "LibLSLCC could not define constant found in script module, constants definition type was un-mapped.  Update mappings!");
                }
            }

            var ev = new LSLDefaultLibraryDataProvider(new [] {"os-lsl"}, false);
            _libLslccMainLibraryDataProvider.DefineEventHandlers(ev.SupportedEventHandlers);
        }



        public void ReadConfig()
        {
            // Get some config

            WriteScriptSourceToDebugFile = m_scriptEngine.Config.GetBoolean("WriteScriptSourceToDebugFile", false);
            CompileWithDebugInformation = m_scriptEngine.Config.GetBoolean("CompileWithDebugInformation", true);
            bool deleteScriptsOnStartup = m_scriptEngine.Config.GetBoolean("DeleteScriptsOnStartup", true);
            m_insertCoopTerminationCalls = m_scriptEngine.Config.GetString("ScriptStopStrategy", "abort") == "co-op";

            // Get file prefix from script-engine name and make it file system safe:
            FilePrefix = "CommonCompiler";
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                FilePrefix = FilePrefix.Replace(c, '_');
            }

            if (in_startup)
            {
                in_startup = false;
                CheckOrCreateScriptsDirectory();

                // First time we start? Delete old files
                if (deleteScriptsOnStartup)
                    DeleteOldFiles();
            }

            // Map name and enum type of our supported languages
            LanguageMapping.Add(CompileLanguage.cs.ToString(), CompileLanguage.cs);
            LanguageMapping.Add(CompileLanguage.vb.ToString(), CompileLanguage.vb);
            LanguageMapping.Add(CompileLanguage.lsl.ToString(), CompileLanguage.lsl);


            // Allowed compilers
            string allowComp = m_scriptEngine.Config.GetString("AllowedCompilers", "lsl");
            AllowedCompilers.Clear();


#if DEBUG
            m_log.Debug("[Compiler]: Allowed languages: " + allowComp);
#endif


            foreach (string strl in allowComp.Split(','))
            {
                string strlan = strl.Trim(" \t".ToCharArray()).ToLower();
                if (!LanguageMapping.ContainsKey(strlan))
                {
                    m_log.Error("[Compiler]: Config error. Compiler is unable to recognize language type \"" + strlan +
                                "\" specified in \"AllowedCompilers\".");
                }
                else
                {
#if DEBUG
                    //m_log.Debug("[Compiler]: Config OK. Compiler recognized language type \"" + strlan + "\" specified in \"AllowedCompilers\".");
#endif
                }
                AllowedCompilers.Add(strlan, true);
            }


            if (AllowedCompilers.Count == 0)
            {
                m_log.Error(
                    "[Compiler]: Config error. Compiler could not recognize any language in \"AllowedCompilers\". Scripts will not be executed!");
            }

            // Default language
            string defaultCompileLanguage = m_scriptEngine.Config.GetString("DefaultCompileLanguage", "lsl").ToLower();

            // Is this language recognized at all?
            if (!LanguageMapping.ContainsKey(defaultCompileLanguage))
            {
                m_log.Error("[Compiler]: " +
                            "Config error. Default language \"" + defaultCompileLanguage +
                            "\" specified in \"DefaultCompileLanguage\" is not recognized as a valid language. Changing default to: \"lsl\".");
                defaultCompileLanguage = "lsl";
            }

            // Is this language in allow-list?
            if (!AllowedCompilers.ContainsKey(defaultCompileLanguage))
            {
                m_log.Error("[Compiler]: " +
                            "Config error. Default language \"" + defaultCompileLanguage +
                            "\"specified in \"DefaultCompileLanguage\" is not in list of \"AllowedCompilers\". Scripts may not be executed!");
            }
            else
            {
#if DEBUG
                //                m_log.Debug("[Compiler]: " +
                //                                            "Config OK. Default language \"" + defaultCompileLanguage + "\" specified in \"DefaultCompileLanguage\" is recognized as a valid language.");
#endif
                // LANGUAGE IS IN ALLOW-LIST
                DefaultCompileLanguage = LanguageMapping[defaultCompileLanguage];
            }

            // We now have an allow-list, a mapping list, and a default language
        }


        /// <summary>
        /// Create the directory where compiled scripts are stored if it does not already exist.
        /// </summary>
        private void CheckOrCreateScriptsDirectory()
        {
            if (!Directory.Exists(ScriptEnginesPath))
            {
                try
                {
                    Directory.CreateDirectory(ScriptEnginesPath);
                }
                catch (Exception ex)
                {
                    m_log.Error("[Compiler]: Exception trying to create ScriptEngine directory \"" + ScriptEnginesPath +
                                "\": " + ex.ToString());
                }
            }

            if (!Directory.Exists(Path.Combine(ScriptEnginesPath,
                m_scriptEngine.World.RegionInfo.RegionID.ToString())))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(ScriptEnginesPath,
                        m_scriptEngine.World.RegionInfo.RegionID.ToString()));
                }
                catch (Exception ex)
                {
                    m_log.Error("[Compiler]: Exception trying to create ScriptEngine directory \"" +
                                Path.Combine(ScriptEnginesPath,
                                    m_scriptEngine.World.RegionInfo.RegionID.ToString()) + "\": " + ex.ToString());
                }
            }
        }


        /// <summary>
        /// Delete old script files
        /// </summary>
        private void DeleteOldFiles()
        {
            foreach (string file in Directory.GetFiles(Path.Combine(ScriptEnginesPath,
                m_scriptEngine.World.RegionInfo.RegionID.ToString()), FilePrefix + "_compiled*"))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    m_log.Error("[Compiler]: Exception trying delete old script file \"" + file + "\": " + ex.ToString());
                }
            }
            foreach (string file in Directory.GetFiles(Path.Combine(ScriptEnginesPath,
                m_scriptEngine.World.RegionInfo.RegionID.ToString()), FilePrefix + "_source*"))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    m_log.Error("[Compiler]: Exception trying delete old script file \"" + file + "\": " + ex.ToString());
                }
            }
        }


        public string GetCompilerOutput(string assetID)
        {
            return Path.Combine(ScriptEnginesPath, Path.Combine(
                m_scriptEngine.World.RegionInfo.RegionID.ToString(),
                FilePrefix + "_compiled_" + assetID + ".dll"));
        }



        public void PerformScriptCompile(
            string source, string asset, UUID ownerUUID,
            out string assembly, out Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>> linemap)
        {
            PerformScriptCompile(source, asset, ownerUUID, false, out assembly, out linemap);
        }



        public void PerformScriptCompile(
            string source, string asset, UUID ownerUUID, bool alwaysRecompile,
            out string assembly, out Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>> linemap)
        {
            //            m_log.DebugFormat("[Compiler]: Checking script for asset {0} in {1}\n{2}", asset, m_scriptEngine.World.Name, source);

            IScriptModuleComms comms = m_scriptEngine.World.RequestModuleInterface<IScriptModuleComms>();

            linemap = null;
            m_warnings.Clear();

            assembly = GetCompilerOutput(asset);

            //            m_log.DebugFormat("[Compiler]: Retrieved assembly {0} for asset {1} in {2}", assembly, asset, m_scriptEngine.World.Name);

            CheckOrCreateScriptsDirectory();

            // Don't recompile if we're not forced to and we already have it
            // Performing 2 file exists tests for every script can still be slow

            //LibLSLCC does not need error map files, all its syntax checking is done on the front end.
            if (!alwaysRecompile && File.Exists(assembly) && File.Exists(assembly + ".text"))
            {
                //m_log.DebugFormat("[Compiler]: Found existing assembly {0} for asset {1} in {2}", assembly, asset, m_scriptEngine.World.Name);

                //no more line map junk!
                return;
            }

            //            m_log.DebugFormat("[Compiler]: Compiling assembly {0} for asset {1} in {2}", assembly, asset, m_scriptEngine.World.Name);

            if (source == string.Empty)
            {
                throw new Exception("Cannot find script assembly and no script text present");
            }


            CompileLanguage language = DefaultCompileLanguage;

            if (source.StartsWith("//c#", true, CultureInfo.InvariantCulture))
            {
                language = CompileLanguage.cs;
            }
            if (source.StartsWith("//vb", true, CultureInfo.InvariantCulture))
            {
                language = CompileLanguage.vb;
                // We need to remove //vb, it won't compile with that

                source = source.Substring(4, source.Length - 4);
            }
            if (source.StartsWith("//lsl", true, CultureInfo.InvariantCulture))
            {
                language = CompileLanguage.lsl;
            }


            //            m_log.DebugFormat("[Compiler]: Compile language is {0}", language);

            if (!AllowedCompilers.ContainsKey(language.ToString()))
            {
                // Not allowed to compile to this language!
                string errtext = string.Empty;
                errtext += "The compiler for language \"" + language.ToString() +
                           "\" is not in list of allowed compilers. Script will not be executed!";
                throw new Exception(errtext);
            }

            if (m_scriptEngine.World.Permissions.CanCompileScript(ownerUUID, (int) language) == false)
            {
                // Not allowed to compile to this language!
                string errtext = string.Empty;
                errtext += ownerUUID +
                           " is not in list of allowed users for this scripting language. Script will not be executed!";
                throw new Exception(errtext);
            }

            string compileScript = source;

            if (language == CompileLanguage.lsl)
            {
                // Its LSL, convert it to C#
                LSL_Converter = new LibLSLCCCodeGenerator(_libLslccMainLibraryDataProvider, comms,
                    m_insertCoopTerminationCalls);
                compileScript = LSL_Converter.Convert(source);

                // copy converter warnings into our warnings.
                foreach (string warning in LSL_Converter.GetWarnings())
                {
                    AddWarning(warning);
                }

                //no more line map junk!
            }

            switch (language)
            {
                case CompileLanguage.cs:
                case CompileLanguage.lsl:
                    compileScript = CreateCSCompilerScript(
                        compileScript,
                        m_scriptEngine.ScriptClassName,
                        m_scriptEngine.ScriptBaseClassName,
                        m_scriptEngine.ScriptBaseClassParameters);
                    break;
                case CompileLanguage.vb:
                    compileScript = CreateVBCompilerScript(
                        compileScript, m_scriptEngine.ScriptClassName, m_scriptEngine.ScriptBaseClassName);
                    break;
            }

            assembly = CompileFromDotNetText(compileScript, language, asset, assembly);
        }


        public string[] GetWarnings()
        {
            return m_warnings.ToArray();
        }


        private void AddWarning(string warning)
        {
            if (!m_warnings.Contains(warning))
            {
                m_warnings.Add(warning);
            }
        }


        private static string CreateCSCompilerScript(
            string compileScript, string className, string baseClassName, ParameterInfo[] constructorParameters)
        {
            compileScript = string.Format(
                @"using OpenSim.Region.ScriptEngine.Shared; 
using System.Collections.Generic;

namespace SecondLife 
{{ 
    public class {0} : {1} 
    {{
        public {0}({2}) : base({3}) {{}}
{4}
    }}
}}",
                className,
                baseClassName,
                constructorParameters != null
                    ? string.Join(", ",
                        Array.ConvertAll<ParameterInfo, string>(constructorParameters, pi => pi.ToString()))
                    : "",
                constructorParameters != null
                    ? string.Join(", ", Array.ConvertAll<ParameterInfo, string>(constructorParameters, pi => pi.Name))
                    : "",
                compileScript);

            return compileScript;
        }


        private static string CreateVBCompilerScript(string compileScript, string className, string baseClassName)
        {
            compileScript = string.Empty +
                            "Imports OpenSim.Region.ScriptEngine.Shared: Imports System.Collections.Generic: " +
                            string.Empty + "NameSpace SecondLife:" +
                            string.Empty + "Public Class " + className + ": Inherits " + baseClassName +
                            "\r\nPublic Sub New()\r\nEnd Sub: " +
                            compileScript +
                            ":End Class :End Namespace\r\n";

            return compileScript;
        }

        /// <summary>
        /// Compile .NET script to .Net assembly (.dll)
        /// </summary>
        /// <param name="script">CS script</param>
        /// <returns>Filename to .dll assembly</returns>
        internal string CompileFromDotNetText(string script, CompileLanguage lang, string asset, string assembly)
        {
            //            m_log.DebugFormat("[Compiler]: Compiling to assembly\n{0}", Script);

            string ext = "." + lang.ToString();

            // Output assembly name
            try
            {
                if (File.Exists(assembly))
                {
                    File.SetAttributes(assembly, FileAttributes.Normal);
                    File.Delete(assembly);
                }
            }
            catch (Exception e) // NOTLEGIT - Should be just FileIOException
            {
                throw new Exception("Unable to delete old existing " +
                                    "script-file before writing new. Compile aborted: " +
                                    e.ToString());
            }

            // DEBUG - write source to disk
            if (WriteScriptSourceToDebugFile)
            {
                string srcFileName = FilePrefix + "_source_" +
                                     Path.GetFileNameWithoutExtension(assembly) + ext;
                try
                {
                    File.WriteAllText(Path.Combine(Path.Combine(
                        ScriptEnginesPath,
                        m_scriptEngine.World.RegionInfo.RegionID.ToString()),
                        srcFileName), script);
                }
                catch (Exception ex) //NOTLEGIT - Should be just FileIOException
                {
                    m_log.Error("[Compiler]: Exception while " +
                                "trying to write script source to file \"" +
                                srcFileName + "\": " + ex.ToString());
                }
            }

            // Do actual compile
            CompilerParameters parameters = new CompilerParameters();

            parameters.IncludeDebugInformation = true;

            string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                "OpenSim.Region.ScriptEngine.Shared.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                "OpenSim.Region.ScriptEngine.Shared.Api.Runtime.dll"));
            parameters.ReferencedAssemblies.Add(Path.Combine(rootPath,
                "OpenMetaverseTypes.dll"));

            if (m_scriptEngine.ScriptReferencedAssemblies != null)
                Array.ForEach<string>(
                    m_scriptEngine.ScriptReferencedAssemblies,
                    a => parameters.ReferencedAssemblies.Add(Path.Combine(rootPath, a)));

            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = assembly;
            parameters.IncludeDebugInformation = CompileWithDebugInformation;
            //parameters.WarningLevel = 1; // Should be 4?
            parameters.TreatWarningsAsErrors = false;

            CompilerResults results;
            switch (lang)
            {
                case CompileLanguage.vb:
                    results = VBcodeProvider.CompileAssemblyFromSource(
                        parameters, script);
                    break;
                case CompileLanguage.cs:
                case CompileLanguage.lsl:
                    bool complete = false;
                    bool retried = false;
                    do
                    {
                        lock (CScodeProvider)
                        {
                            results = CScodeProvider.CompileAssemblyFromSource(
                                parameters, script);
                        }

                        // Deal with an occasional segv in the compiler.
                        // Rarely, if ever, occurs twice in succession.
                        // Line # == 0 and no file name are indications that
                        // this is a native stack trace rather than a normal
                        // error log.
                        if (results.Errors.Count > 0)
                        {
                            if (!retried && string.IsNullOrEmpty(results.Errors[0].FileName) &&
                                results.Errors[0].Line == 0)
                            {
                                // System.Console.WriteLine("retrying failed compilation");
                                retried = true;
                            }
                            else
                            {
                                complete = true;
                            }
                        }
                        else
                        {
                            complete = true;
                        }
                    } while (!complete);
                    break;
                default:
                    throw new Exception("Compiler is not able to recognize " +
                                        "language type \"" + lang.ToString() + "\"");
            }

            //
            // WARNINGS AND ERRORS
            //
            bool hadErrors = false;
            string errtext = string.Empty;
            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    string severity = CompErr.IsWarning ? "Warning" : "Error";

                    KeyValuePair<int, int> errorPos;

                    // Show 5 errors max, but check entire list for errors

                    if (severity == "Error")
                    {
                        string text = CompErr.ErrorText;

                        // The Second Life viewer's script editor begins
                        // counting lines and columns at 0, so we subtract 1.
                        errtext += string.Format("({0},{1}): {4} {2}: {3}\n",
                            CompErr.Line - 1, CompErr.Column - 1,
                            CompErr.ErrorNumber, text, severity);
                        hadErrors = true;
                    }
                }
            }

            if (hadErrors)
            {
                throw new Exception(errtext);
            }

            //  On today's highly asynchronous systems, the result of
            //  the compile may not be immediately apparent. Wait a 
            //  reasonable amount of time before giving up on it.

            if (!File.Exists(assembly))
            {
                for (int i = 0; i < 20 && !File.Exists(assembly); i++)
                {
                    System.Threading.Thread.Sleep(250);
                }
                // One final chance...
                if (!File.Exists(assembly))
                {
                    errtext = string.Empty;
                    errtext += "No compile error. But not able to locate compiled file.";
                    throw new Exception(errtext);
                }
            }

            //            m_log.DebugFormat("[Compiler] Compiled new assembly "+
            //                    "for {0}", asset);

            // Because windows likes to perform exclusive locks, we simply
            // write out a textual representation of the file here
            //
            // Read the binary file into a buffer
            //
            FileInfo fi = new FileInfo(assembly);

            //I don't know what this is about so I am leaving it?
            if (fi == null)
            {
                errtext = string.Empty;
                errtext += "No compile error. But not able to stat file.";
                throw new Exception(errtext);
            }

            byte[] data = new byte[fi.Length];

            try
            {
                using (FileStream fs = File.Open(assembly, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(data, 0, data.Length);
                }
            }
            catch (Exception)
            {
                errtext = string.Empty;
                errtext += "No compile error. But not able to open file.";
                throw new Exception(errtext);
            }

            // Convert to base64
            //
            string filetext = Convert.ToBase64String(data);

            byte[] buf = Encoding.ASCII.GetBytes(filetext);

            using (FileStream sfs = File.Create(assembly + ".text"))
                sfs.Write(buf, 0, buf.Length);

            return assembly;
        }
    }
}