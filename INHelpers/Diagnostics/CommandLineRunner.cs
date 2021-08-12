using System;
using System.Diagnostics;
using System.Text;

namespace INHelpers.Diagnostics
{
    public class CommandLineRunner : ICommandLineRunner
    {
        public ICommandLineResult Run(
          string workingDirectory,
          string executable,
          string? arguments,
          int timeout = 120000)
        {
            if (workingDirectory == null)
                throw new ArgumentNullException(nameof(workingDirectory));
            if (executable == null)
                throw new ArgumentNullException(nameof(executable));
            ProcessStartInfo processStartInfo = arguments != null ? new ProcessStartInfo(executable, arguments) : throw new ArgumentNullException(nameof(arguments));
            processStartInfo.WorkingDirectory = workingDirectory;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            Process process = new Process();
            StringBuilder stringBuilder = new StringBuilder();
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit(timeout);
            string str1;
            while ((str1 = process.StandardOutput.ReadLine()) != null)
                stringBuilder.AppendLine(str1);
            string str2;
            while ((str2 = process.StandardError.ReadLine()) != null)
                stringBuilder.AppendLine(str2);
            return new CommandLineResult(stringBuilder.ToString(), process.ExitCode);
        }
    }
}
