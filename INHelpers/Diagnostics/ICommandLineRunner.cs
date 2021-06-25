namespace INHelpers.Diagnostics
{

    /// <summary>
    /// Used to initiate finite command line processes
    /// </summary>
    public interface ICommandLineRunner
    {

        /// <summary>
        /// Invokes a command line process and waits for its exit
        /// </summary>
        /// <param name="workingDirectory">The path to the directory to use as the working directory</param>
        /// <param name="executable">Path to the executable to invoke on the command line</param>
        /// <param name="arguments">Arguments to pass to the command line process</param>
        /// <param name="timeout">Time to wait for the process too run to completion</param>
        /// <returns>The result of the invocation</returns>
        ICommandLineResult Run(
          string workingDirectory,
          string executable,
          string arguments,
          int timeout = 120000);
    }
}
