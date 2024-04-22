using System;
using System.Reactive.Subjects;

namespace OpcUaIntegrationTest
{
    public class TestLockManager : ITestLockManager
    {
        private bool _isLocked = false;
        private readonly Subject<LockTestEnum> _lockStateChangeSubject;

        public LockTestEnum LockState => _isLocked ? LockTestEnum.Locked : LockTestEnum.Unlocked;

        public TestLockManager()
        {
            _lockStateChangeSubject = new Subject<LockTestEnum>();
        }

        public void SetLockState(bool newLockState)
        {
            if (_isLocked != newLockState)
            {
                _isLocked = newLockState;
                _lockStateChangeSubject.OnNext(LockState);
            }
        }

        public bool IsLocked()
        {
            return _isLocked;
        }

        public IObservable<LockTestEnum> SubscribeStateChanges()
        {
            return _lockStateChangeSubject;
        }
    }
}