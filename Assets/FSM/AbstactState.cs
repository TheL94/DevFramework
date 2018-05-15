using System.Collections.Generic;

namespace UnityFramework.FSM
{
    public abstract class AbstactState
    {
        FiniteStateMachine FSM;
        List<Transition> possibleTransitions;

        private bool _isToSetup = true;
        public bool IsToSetup
        {
            get { return _isToSetup; }
            private set { _isToSetup = value; }
        }

        public void Initialize(FiniteStateMachine _FSM)
        {
            FSM = _FSM;
            possibleTransitions = SetTransitions();
            OnStart();
            IsToSetup = false;
        }

        #region API
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnEnd() { }

        public void GoNext(int _transitionID)
        {
            FSM.SetState(GetTransition(_transitionID));
        }
        #endregion

        Transition GetTransition(int _transitionID)
        {
            if(possibleTransitions == null)
                return null;

            for (int i = 0; i < possibleTransitions.Count; i++)
            {
                if (possibleTransitions[i].ID == _transitionID)
                    return possibleTransitions[i];
            }

            return null; 
        }

        protected abstract List<Transition> SetTransitions();
    }
}