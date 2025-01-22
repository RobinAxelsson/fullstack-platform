using System.Diagnostics;
using System.Reflection;

namespace TensStar.IntegrationTests
{
    public class WebApiFixture : IDisposable
    {
        private const int PORT = 5555;
        internal static readonly string BASE_URL = "http://localhost:" + PORT;
        private Process? _webApiProcess = null;
        private static readonly string OUTPUT_DIR = Path.Combine(".", "UserWebApi");
        private static readonly string DLL_PATH = Path.Combine(OUTPUT_DIR, "TenStar.UserWebApi.dll");

        public WebApiFixture()
        {
            Build();
            _webApiProcess = Run();
            WaitForConnection();
        }

        public void Dispose()
        {
            _webApiProcess?.Kill();
        }

        private static void Build()
        {
            string solutionPath = GetSolutionDir();
            string projectPath = Path.Combine(solutionPath, "src", "TenStar.UserWebApi", "TenStar.UserWebApi.csproj");

            if (!File.Exists(projectPath))
            {
                throw new FileNotFoundException("Project file not found.");
            }

            Environment.SetEnvironmentVariable("USE_INMEMORY_DB", "TRUE");

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"build \"{projectPath}\" -o {OUTPUT_DIR}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = processStartInfo };
            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                throw new InvalidOperationException($"Build failed with exit code {process.ExitCode}. Output: {output}. Error: {error}");
            }
        }

        private static Process Run()
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"{DLL_PATH} --urls http://localhost:5555",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var process = new Process { StartInfo = processStartInfo };
            process.Start();

            return process;
        }

        private static void WaitForConnection()
        {
            Thread.Sleep(5000);
        }

        private static string GetSolutionDir()
        {
            string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new ArgumentException("Location: " + Assembly.GetExecutingAssembly().Location);

            // Traverse upwards to find the solution file
            while (!string.IsNullOrEmpty(currentDirectory))
            {
                string[] solutionFiles = Directory.GetFiles(currentDirectory, "*.sln", SearchOption.TopDirectoryOnly);

                if (solutionFiles.Length > 0)
                {
                    return currentDirectory;
                }

                currentDirectory = Directory.GetParent(currentDirectory)?.FullName ?? throw new ArgumentNullException(Directory.GetParent(currentDirectory)?.FullName);
            }

            throw new FileNotFoundException("No solution file (*.sln) found in the directory hierarchy.");
        }
    }
}
