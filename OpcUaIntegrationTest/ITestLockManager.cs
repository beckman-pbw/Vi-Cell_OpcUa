using System;

namespace OpcUaIntegrationTest
{
    /// <summary>
    /// gRPC server side interface for testing purposes
    /// </summary>
    public interface ITestLockManager
    {
        bool IsLocked();

        IObservable<LockTestEnum> SubscribeStateChanges();
    }
}