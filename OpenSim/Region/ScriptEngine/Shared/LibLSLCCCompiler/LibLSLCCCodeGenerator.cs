using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LibLSLCC.CodeValidator;
using LibLSLCC.LibraryData;
using LibLSLCC.Compilers;
using OpenSim.Region.ScriptEngine.Shared.CodeTools;

namespace OpenSim.Region.ScriptEngine.Shared.LibLSLCCCompiler
{
    // ReSharper disable once InconsistentNaming
    public class LibLSLCCCodeGenerator : ICodeConverter
    {
        private readonly ILSLLibraryDataProvider _libraryData;


        public LibLSLCCCodeGenerator(ILSLLibraryDataProvider libraryDataProvider, LSLOpenSimCompilerSettings compilerSettings)
        {
            _libraryData = libraryDataProvider;
            CompilerSettings = compilerSettings;

        }


        public bool EmitCompilerWarnings { get; set; }


        private class ErrorListener : LSLDefaultSyntaxErrorListener
        {
            private LibLSLCCCodeGenerator _libLslccCodeGenerator;

            public ErrorListener(LibLSLCCCodeGenerator libLslccCodeGenerator)
            {
                _libLslccCodeGenerator = libLslccCodeGenerator;
            }

            protected override void OnError(LSLSourceCodeRange location, string message)
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
            private readonly LibLSLCCCodeGenerator _libLslccCodeGenerator;

            public WarningListener(LibLSLCCCodeGenerator libLslccCodeGenerator)
            {
                _libLslccCodeGenerator = libLslccCodeGenerator;
            }

            protected override void OnWarning(LSLSourceCodeRange location, string message)
            {
                if (!_libLslccCodeGenerator.EmitCompilerWarnings) return;


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



        public LSLOpenSimCompilerSettings CompilerSettings { get; private set; }



        public string Convert(string script)
        {
            var validatorServices = new LSLCodeValidatorStrategies();


            var errorListener = new ErrorListener(this);
            var warningListener = new WarningListener(this);

            validatorServices.ExpressionValidator = ExpressionValidator;
            validatorServices.LibraryDataProvider = _libraryData;
            validatorServices.StringLiteralPreProcessor = new LSLDefaultStringPreProcessor();
            validatorServices.SyntaxErrorListener = errorListener;
            validatorServices.SyntaxWarningListener = warningListener;


            var validator = new LSLCodeValidator(validatorServices);

            ILSLCompilationUnitNode syntaxTree;

            using (var m = new MemoryStream(Encoding.Unicode.GetBytes(script)))
            {

                syntaxTree = validator.Validate(new StreamReader(m, Encoding.Unicode));
            }

            _warnings = warningListener.Warnings;



            if (validator.HasSyntaxErrors) return "";

            var outStream = new MemoryStream();

            var compiler = new LSLOpenSimCompiler(_libraryData, CompilerSettings);

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