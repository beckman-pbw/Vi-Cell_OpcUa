using ViCellBlu;
using Moq;
using Ninject;
using NUnit.Framework;
using System.Linq;
using ViCellBluOpcUaModelDesign.OpcUa;
using BrowseNames = ViCellBlu.BrowseNames;

namespace OpcUaIntegrationTest
{
    /// <summary>
    /// Tests streaming of events over gRPC, by registering for those events, changing the lock state, and then invoking the INodeService when receiving those events.
    /// </summary>
    [TestFixture]
    public class LockManagerTest : OpcUaBaseTestClass
    {
        private TestLockManager _grpcLockManager;
        private BecNodeManager _nodeManager;

        public override void Setup()
        {
            base.Setup();
            _grpcLockManager = Kernel.Get<TestLockManager>();
            _nodeManager = Kernel.Get<BecNodeManager>();
        }

        public void LockManagerEventSucceeds()
        {
            var lockEventChangedNodeState = _nodeManager.FindNode(ViCellBlu.ObjectTypes.LockStateChangedEvent);
            MockNodeService.SetupGet(n => n.RootFolderState).Returns(_nodeManager.RootFolderState);
            var initSequence = new MockSequence();
            MockNodeService.InSequence(initSequence).Setup(e => e.InitEventState(It.IsAny<BecEventState>(), lockEventChangedNodeState,
                "AutomationLockStateChanged", "The automation lock has been set to locked", 501));
            MockNodeService.InSequence(initSequence).Setup(e => e.InitEventState(It.IsAny<BecEventState>(), lockEventChangedNodeState,
                "AutomationLockStateChanged", "The automation lock has been set to unlocked", 501));
            MockNodeService.InSequence(initSequence).Setup(e => e.InitEventState(It.IsAny<BecEventState>(), lockEventChangedNodeState,
                "AutomationLockStateChanged", "The automation lock has been set to locked", 501));
            var reportSequence = new MockSequence();
            MockNodeService.InSequence(reportSequence).Setup(e => e.ReportEvent(It.IsAny<BecEventState>()));
            MockNodeService.InSequence(reportSequence).Setup(e => e.ReportEvent(It.IsAny<BecEventState>()));
            MockNodeService.InSequence(reportSequence).Setup(e => e.ReportEvent(It.IsAny<BecEventState>()));

            // Start test
            _grpcLockManager.SetLockState(true);
            _grpcLockManager.SetLockState(false);
            _grpcLockManager.SetLockState(true);

            MockNodeService.Verify();
        }

        /// <summary>
        /// Initial test to verify communication and node tree - needs to be enhanced.
        /// </summary>
        public void SimpleLockUnlock()
        {
//            Thread.Sleep(5000);
            // System.Diagnostics.Debug.Write(OpcUaClient.DumpNodes());
            var scoutXrefs = OpcUaClient.Browse(out var continuePoint);
            var viCellBlu = scoutXrefs.First(n => n.BrowseName.Name.Equals(BrowseNames.ViCellBluState));
            var viCellBluRefs = OpcUaClient.Browse(out var viCellBluContinuePoint, viCellBlu.NodeId);
            var automationLockStateRef = viCellBluRefs.First(n => n.BrowseName.Name.Equals(BrowseNames.LockState));
            Assert.IsNotNull(automationLockStateRef);

            // Read and verify LockState variable is unlocked
            // Call Request Automation Lock
            // Read and verify LockState variable is locked
            // Call Release Automation Lock
            // Read and verify LockState variable is unlocked
        }

        [Test, Ignore("Not implemented")]
        public void LockUiUnlockCheckEvent()
        {
            // Use OPC/UA to request an Automation Lock
            // Use Winium.Cruciatus to click the Titlebar Automation lock
            // Verify receiving the lock manager event
            // Read the OPC/UA variable and verify it is showing unlocked.
            Assert.True(true);
        }
    }
}