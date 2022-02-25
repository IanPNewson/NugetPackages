namespace INHelpers.Diagnostics
{
    public class CommandLineResult : ICommandLineResult
    {
        public string? Output { get; set; }

        public int ExitCode { get; set; }

        public CommandLineResult(int exitCode)
          : this(null, exitCode)
        {
        }

        public CommandLineResult(string? output)
          : this(output, 0)
        {
        }

        public CommandLineResult(string? output, int exitCode)
        {
            Output = output;
            ExitCode = exitCode;
        }

        public void ExpectExitCode(params int[] validExitCodes)
        {
            if (!((IEnumerable<int>)validExitCodes).Contains(ExitCode))
                throw new InvalidOperationException(string.Format("Expected one of the exit codes '{0}' but instead got '{1}'\r\nOutput:\r\n{2}", string.Join(", ", validExitCodes), ExitCode, Output));
        }
    }
}
