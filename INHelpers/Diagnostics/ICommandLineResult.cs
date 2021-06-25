namespace INHelpers.Diagnostics
{

    /// <summary>
    /// Represents the result of a process being invoked on the command line and running to completion
    /// </summary>
    public interface ICommandLineResult
    {

        /// <summary>
        /// The textual output of the process
        /// </summary>
        string Output { get; set; }

        /// <summary>
        /// The exit code of the process
        /// </summary>
        int ExitCode { get; set; }

        /// <summary>
        /// Called to validate that the exit code of the process is an expected one. Typically
        /// throws exception if the exit code is unexpected.
        /// </summary>
        void ExpectExitCode(params int[] validExitCodes);
    }
}
