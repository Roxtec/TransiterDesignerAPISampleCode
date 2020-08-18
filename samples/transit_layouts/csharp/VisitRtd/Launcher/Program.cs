using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using RtdApiCodeSamples;

namespace Launcher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var browserWrapperExePath = FindBrowserWrapperExe();

            var options = Common.ParseCommandLine(args);

            var rand = new Random().Next(1000, 100000);
            var transitName = $"Visit RTD test transit {rand}";

            Console.WriteLine($"Project ID: {options.ProjectId}");
            Console.WriteLine($"Project API key: {options.ProjectApiKey}");
            Console.WriteLine($"Transit name: {transitName}");

            // Create a client and set to authenticate using the API key
            var client = Common.CreateClient(options);

            // Create a transit with 12 cables with diameter 33 mm.
            // This should give 12 RM 60 modules by default (though it depends on project settings),
            // which with a S 6x1 frame is too much.
            var cables = Enumerable
                .Range(1, 12)
                .Select(i => new Cable { Id = $"a-{i}", Diameter = 33})
                .ToList();
            // Build a document with settings for the new transit
            var createDocument = new SingleTransitLayoutCreateUpdateDocument
            {
                Data = new TransitLayoutCreateUpdateResource
                {
                    Type = Common.TransitLayoutsType,
                    Attributes = new Attributes
                    {
                        Name = transitName,
                        Cables = cables,
                        Frame = new Frame
                        {
                            PartNumber = "S006000000121" // S 6x1 AISI316
                        }
                    },
                    Relationships = new Relationships
                    {
                        Project = Common.CreateProjectRelationship(options.ProjectId)
                    }
                }
            };

            Console.WriteLine("");
            Console.WriteLine("Creating transit ...");
            var transitBefore = await client.CreateTransitLayoutAsync(options.ProjectId, createDocument);
            var transitId = Guid.Parse(transitBefore.Data.Id);

            Console.WriteLine("");
            Console.WriteLine($"The transit is {completeness(transitBefore)} with a fill rate of {fillRatePct(transitBefore)}");

            // Extract the URL of the Transit Designer packing page for the transit.
            var packingPageUrl = transitBefore.Data.Relationships.PackingPage.Links.Related;

            // With the browser open, the user can modify the transit. For this sample, it is
            // recommended to change RM 60 to RM 40 for all cables and then let Transit Designer
            // autopack the result.
            Console.WriteLine("");
            Console.WriteLine($"Starting {browserWrapperExePath} with URL {packingPageUrl}, and waiting for it to exit ...");
            await StartBrowserWrapperAndWaitForExit(browserWrapperExePath, packingPageUrl);

            Console.WriteLine("");
            Console.WriteLine("Fetching transit layout again...");
            var transitAfter = await client.GetTransitLayoutAsync(options.ProjectId, transitId);

            Console.WriteLine("");
            Console.WriteLine($"The transit is {completeness(transitAfter)} with a fill rate of {fillRatePct(transitAfter)}");

            Console.WriteLine("");
            Console.WriteLine("Exiting.");

            static string fillRatePct(SingleTransitLayoutDocument document)
            {
                var rate = document.Data.Attributes.PackingResult.FillRate * 100;
                var rateStr = rate.ToString("F2", CultureInfo.InvariantCulture);
                return $"{rateStr} %";
            }

            static string completeness(SingleTransitLayoutDocument document)
            {
                var isComplete = document.Data.Attributes.PackingResult.IsComplete;
                return isComplete ? "COMPLETE" : "INCOMPLETE";
            }
        }

        private static Task StartBrowserWrapperAndWaitForExit(string exe, string arguments)
        {
            // Use a TaskCompletionSource together with the Exited event (enabled by EnableRaisingEvents)
            // to be able to wait for the process to exist asynchronously.
            var tcs = new TaskCompletionSource<bool>();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = arguments
                },
                EnableRaisingEvents = true
            };
            process.Exited += (sender, ev) => tcs.SetResult(true);
            process.Start();

            return tcs.Task;
        }

        private static string FindBrowserWrapperExe()
        {
            // CodeBase is typically a file:// URI, so let the Uri class parse it.
            var currentDllUri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var currentDllPath = currentDllUri.AbsolutePath;
            var currentExePath = currentDllPath.Replace(".dll", ".exe");

            var browserWrapperExePath = currentExePath.Replace("Launcher", "BrowserWrapper");

            if (!File.Exists(browserWrapperExePath))
            {
                throw new ArgumentException($"Cannot find {browserWrapperExePath}");
            }

            return browserWrapperExePath;
        }
    }
}
