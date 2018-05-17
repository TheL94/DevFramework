using System.Collections.Generic;

namespace UnityFramework.FSM
{
    public abstract class AbstactState
    {
        private bool _isToSetup = true;
        public bool IsToSetup
        {
            get { return _isToSetup; }
            private set { _isToSetup = value; }
        }

        protected FiniteStateMachine FSM;

        List<Transition> possibleTransitions;

        #region API 
        public void Initialize(FiniteStateMachine _FSM)
        {
            FSM = _FSM;
            possibleTransitions = SetTransitions();
            OnStart();
            IsToSetup = false;
        }

        public void Terminate()
        {
            OnEnd();
            IsToSetup = true;
        }

        public Transition GetTransition(int _transitionID)
        {
            if (possibleTransitions == null)
                return null;

            for (int i = 0; i < possibleTransitions.Count; i++)
            {
                if (possibleTransitions[i].ID == _transitionID)
                    return possibleTransitions[i];
            }

            return null;
        }
        #region Hooks
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnEnd() { }
        #endregion
        #endregion

        protected abstract List<Transition> SetTransitions();
    }
}