using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OpcUaIntegrationTest
{
    public class OpcUaBaseIntegrationTestClass
    {
        private int stopTimeout = Timeout.Infinite;
        private bool autoAccept = false;
        private Process _opcUaServerProcess;
        private string endpointURL = "opc.tcp://localhost:62641/ViCellBlu/Server";
        protected OpcUaTestingClient OpcUaClient { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            _opcUaServerProcess = StartOpcUaServer();
            OpcUaClient = new OpcUaTestingClient(endpointURL, autoAccept, stopTimeout);
            // ToDo: For an integration test, we will need a way to create a valid user that could be used to login.
            OpcUaClient.ConnectToServer("admin_user_dummy", "dummy_password");
            //OpcUaClient.SetupSubscriptions();
        }

        [TearDown]
        public virtual void Cleanup()
        {
            Assert.True(StopOpcUaServer());
            //Assert.AreEqual(ExitCode.Ok,(int)OpcUaTestingClient.ExitCode);
        }

        protected Process StartOpcUaServer()
        {
            try
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        WorkingDirectory = TestContext.CurrentContext.TestDirectory,
                        FileName = "dotnet",
                        Arguments = "ViCellOpcUaServer.dll",
                        UseShellExecute = true
                    }
                };
                process.Start();
                return process;
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        /// <summary>
        /// Stop the ScoutX OPC/UA server
        /// </summary>
        /// <returns>true if stopped cleanly, false if kill was used.</returns>
        protected bool StopOpcUaServer()
        {
            _opcUaServerProcess?.Kill();

            /*
            try
            {
                _opcUaServerProcess.Refresh();
                _opcUaServerProcess.Close();
                _opcUaServerProcess.Refresh();
                int tries = 0;
                while (tries < 30 && !_opcUaServerProcess.HasExited)
                {
                    // Discard cached information about the process.
                    _opcUaServerProcess.Refresh();
                    // Print working set to console.
                    Thread.Sleep(2000);
                    tries++;
                }

                if (tries >= 30 && !_opcUaServerProcess.HasExited)
                {
                    _opcUaServerProcess.Kill();
                    return false;
                }
            }
            catch (Exception)
            {
                // Exception can occur if the process already exited. Bad Microsoft. Bad dog.
            }
            */

            return true;
        }
    }
}