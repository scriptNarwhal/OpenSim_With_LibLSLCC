using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LibLSLCC.Compilers;
using LibLSLCC.LibraryData;
using LibLSLCC.CodeValidator.Components;
using LibLSLCC.CodeValidator.Components.Interfaces;
using LibLSLCC.CodeValidator.Nodes.Interfaces;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.ScriptEngine.Shared.CodeTools;

namespace OpenSim.Region.ScriptEngine.Shared.LibLSLCCCompiler
{
    // ReSharper disable once InconsistentNaming
    public class LibLSLCCCodeGenerator : ICodeConverter
    {
        private readonly ILSLLibraryDataProvider _libraryData;
        private readonly bool _insertCoopTerminationCalls;

        public LibLSLCCCodeGenerator()
        {

        }

        public LibLSLCCCodeGenerator(ILSLLibraryDataProvider libraryData, IScriptModuleComms comms, bool insertCoopTerminationCalls)
        {
            _libraryData = libraryData;
            _insertCoopTerminationCalls = insertCoopTerminationCalls;
        }


        public Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>> PositionMap
        {
            get { return new Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>>(); }
        }


        private class ErrorListener : LSLDefaultSyntaxErrorListener
        {
            protected override void OnError(global::LibLSLCC.CodeValidator.Primitives.LSLSourceCodeRange location, string message)
            {
                int line = MapLineNumber(location.LineStart);

                throw new Exception(string.Format("({0},{1}): ERROR, {2}", line, location.ColumnStart, message));
            }

            protected override int MapLineNumber(int oneBasedLine)
            {
                //return -1 for all line indexes, since the SecondLife viewer's LSL editor uses a 0 based index and LibLSLCC uses a 1 based index.
                return oneBasedLine - 1;
            }
        }


        private class WarningListener : LSLDefaultSyntaxWarningListener
        {
            public readonly List<string> Warnings = new List<string>();

            protected override void OnWarning(global::LibLSLCC.CodeValidator.Primitives.LSLSourceCodeRange location, string message)
            {
                int line = MapLineNumber(location.LineStart);

                Warnings.Add(string.Format("({0},{1}): WARNING, {2}", line, location.ColumnStart, message));
            }

            protected override int MapLineNumber(int oneBasedLine)
            {
                //return -1 for all line indexes, since the SecondLife viewer's LSL editor uses a 0 based index and LibLSLCC uses a 1 based index.
                return oneBasedLine - 1;
            }
        }

        private static readonly ILSLExpressionValidator ExpressionValidator = new LSLDefaultExpressionValidator();



        public string Convert(string script)
        {
            var validatorServices = new LSLCustomValidatorServiceProvider();


            var errorListener = new ErrorListener();
            var warningListener = new WarningListener();

            validatorServices.ExpressionValidator = ExpressionValidator;
            validatorServices.LibraryDataProvider = _libraryData;
            validatorServices.StringLiteralPreProcessor = new LSLDefaultStringPreProcessor();
            validatorServices.SyntaxErrorListener = errorListener;
            validatorServices.SyntaxWarningListener = warningListener;


            var validator = new LibLSLCC.CodeValidator.LSLCodeValidator(validatorServices);

            ILSLCompilationUnitNode syntaxTree;

            using (var m = new MemoryStream(Encoding.Unicode.GetBytes(script)))
            {

                syntaxTree = validator.Validate(new StreamReader(m, Encoding.Unicode));
            }

            _warnings = warningListener.Warnings;



            if (validator.HasSyntaxErrors) return "";

            var outStream = new MemoryStream();

            var compilerSettings = LSLOpenSimCSCompilerSettings.OpenSimClientUploadable(_libraryData);
            compilerSettings.InsertCoOpTerminationCalls = _insertCoopTerminationCalls;

            var compiler = new LSLOpenSimCSCompiler(compilerSettings);

            compiler.Compile(syntaxTree, new StreamWriter(outStream, Encoding.Unicode));
            

            return Encoding.Unicode.GetString(outStream.ToArray());
        }



        private List<string> _warnings;

        public string[] GetWarnings()
        {
            return _warnings.ToArray();
        }
    }
}