using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        public AbstactState CurrentState { get { return stateStack.Peek(); } }

        Stack<AbstactState> stateStack = new Stack<AbstactState>();

        #region API 
        public void Initialize(AbstactState _initialState)
        {
            if(_initialState != null)
            {
                stateStack.Push(_initialState);
                CurrentState.Initialize(this);
            }
            else
                Debug.LogError("FSM - No Initial State Available");
        }

        public void DoTransition(int _transitionID)
        {
            Transition transition = CurrentState.GetTransition(_transitionID);
            SetState(transition);
        }
        #endregion

        void SetState(Transition _transition)
        {
            if(_transition == null)
            {
                Debug.LogError("FSM - " + CurrentState.GetType().Name + " - No Available Trasition With ID :" + _transition.ID);
                return;
            }

            switch (_transition.Type)
            {
                case Transition.TranstionType.PopItself_PushNew:
                    stateStack.Pop().Terminate();
                    stateStack.Push(_transition.NextState);            
                    break;
                case Transition.TranstionType.PopItself:
                    stateStack.Pop().Terminate();
                    break;
                case Transition.TranstionType.PushNew:
                    CurrentState.Terminate();
                    stateStack.Push(_transition.NextState);
                    break;
            }

            if (CurrentState.IsToSetup)
                CurrentState.Initialize(this);
        }

        private void Update()
        {
            if(!CurrentState.IsToSetup)
                CurrentState.OnUpdate();
        }

        private void LateUpdate()
        {
            if (!CurrentState.IsToSetup)
                CurrentState.OnLateUpdate();
        }
    }
}

