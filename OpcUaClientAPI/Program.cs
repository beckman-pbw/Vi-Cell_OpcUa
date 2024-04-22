using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using OpcUaClientAPI.Model;
using Console = System.Console;

namespace OpcUaClientAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessBatchOperations(args);
            Console.WriteLine("Completed");
        }

        private static void ProcessBatchOperations(string[] jsonFiles)
        {
            try
            {
                foreach (var file in jsonFiles)
                {
                    if (!File.Exists(file))
                    {
                        Console.WriteLine($"ProcessBatchOperations :: FileNotFound :: File: {file}");
                        continue;
                    }

                    using (var r = new StreamReader(file))
                    {
                        var json = r.ReadToEnd();
                        var result = JsonConvert.DeserializeObject<List<JsonOperationModel>>(json);
                        ProcessOperations(result);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ProcessBatchOperations :: Exception");
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ProcessOperations(List<JsonOperationModel> operations)
        {
            var session = new OpcUaClientWrapper();

            foreach (var op in operations)
            {
                var operation = op.operation;
                var arguments = op.parameters;

                var formattedArgs = arguments.Aggregate(string.Empty, (current, arg) => current + (arg + ", "));
                if (!string.IsNullOrEmpty(formattedArgs))
                    formattedArgs = formattedArgs.Remove(formattedArgs.Length - 2, 2);

                Console.WriteLine($"Operation='{operation}' Arguments='{formattedArgs}'");

                if (string.IsNullOrEmpty(operation))
                    continue;

                if (operation.Contains("RequestLock"))
                    session.RequestLock(arguments);
                else if (operation.Contains("ReleaseLock"))
                    session.ReleaseLock(arguments);
                else if (operation.Contains("CreateCellType"))
                    session.CreateCellType(arguments);
                else if (operation.Contains("CreateQualityControl"))
                    session.CreateQualityControl(arguments);
                else if (operation.Contains("DeleteCellType"))
                    session.DeleteCellType(arguments);
                else if (operation.Contains("DeleteSampleResults"))
                    session.DeleteSampleResults(arguments);
                else if (operation.Contains("EjectStage"))
                    session.EjectStage();
                else if (operation.Contains("ExportConfig"))
                    session.ExportConfig(arguments);
                else if (operation.Contains("RetrieveSampleExport"))
                    session.RetrieveSampleExport(arguments);
                else if (operation.Contains("GetSampleResults"))
                    session.GetSampleResults(arguments);
                else if (operation.Contains("ImportConfig"))
                    session.ImportConfig(arguments);
                else if (operation.Contains("Pause"))
                    session.Pause();
                else if (operation.Contains("Resume"))
                    session.Resume();
                else if (operation.Contains("StartSampleSet"))
                    session.StartSampleSet(arguments);
                else if (operation.Contains("StartSample"))
                    session.StartSample(arguments);
                else if (operation.Contains("Stop"))
                    session.Stop();
            }
        }
    }
}
