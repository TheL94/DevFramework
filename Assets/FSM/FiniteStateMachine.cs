using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        #region Events
        public delegate void FSMEvent(string _FSMName);
        public static FSMEvent OnFSMConclusion;
        #endregion

        /// <summary>
        /// Ritorna il Nome della Macchina a Stati.
        /// </summary>
        public string Name { get { return GetType().Name; } }

        /// <summary>
        /// Ritorna il Nome dello Stato Corrente.
        /// </summary>
        public string CurrentStateName
        {
            get
            {
                if (stateStack.Count == 0)
                    return null;
                return stateStack.Peek().Name;
            }
        }
        /// <summary>
        /// Ritorna il lo Stato Corrente.
        /// </summary>
        public AbstactState CurrentState
        {
            get
            {
                if (stateStack.Count == 0)
                    return null;
                return stateStack.Peek();
            }
        }

        Stack<AbstactState> stateStack = new Stack<AbstactState>();

        #region API 
        /// <summary>
        /// Funzione che inizializza la Macchina a Stati.
        /// </summary>
        /// <param name="_initialState"></param>
        public void Initialize(AbstactState _initialState)
        {
            if (_initialState != null)
            {
                stateStack.Push(_initialState);
                CurrentState.Initialize(this);
            }
            else
                Debug.LogError("FSM - No Initial State available");
        }

        /// <summary>
        /// Funzione che innesca il passaggio di stato, se l'ID della transizione passsato risulta esse presente nella lista delle transioni dello Stato Corrente.
        /// Altimenti, se lo Stato Corrente, dopo la transione risulta essere nullo, la  Macchina a Stati termina e lancia l'evento OnFSMConclusion.
        /// </summary>
        /// <param name="_transitionID"></param>
        public void DoTransition(int _transitionID)
        {
            Transition transition = CurrentState.GetTransition(_transitionID);
            SetState(transition);
        }
        #endregion

        void SetState(Transition _transition)
        {
            if (_transition == null)
            {
                Debug.LogError("FSM " + Name + " - " + CurrentState.Name + " - No available Trasition with ID :" + _transition.ID);
                return;
            }

            switch (_transition.Type)
            {
                case Transition.TranstionType.PopItself_PushNew:
                    stateStack.Pop().Terminate();
                    if (_transition.NextState != null)
                        stateStack.Push(_transition.NextState);
                    else
                        Debug.LogError("FSM " + Name + " - " + CurrentState.Name + " - No available Next State for Trasition with ID :" + _transition.ID);
                    break;

                case Transition.TranstionType.PopItself:
                    stateStack.Pop().Terminate();
                    break;

                case Transition.TranstionType.PushNew:
                    CurrentState.Terminate();
                    if (_transition.NextState != null)
                        stateStack.Push(_transition.NextState);
                    else
                        Debug.LogError("FSM " + Name + " - " + CurrentState.Name + " - No available Next State for Trasition with ID :" + _transition.ID);
                    break;
            }

            if (CurrentState == null && OnFSMConclusion != null)
            {
                OnFSMConclusion(Name);
                return;
            }

            if (CurrentState.IsToSetup)
                CurrentState.Initialize(this);
        }

        private void Update()
        {
            if (CurrentState != null && !CurrentState.IsToSetup)
                CurrentState.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (CurrentState != null && !CurrentState.IsToSetup)
                CurrentState.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            if (CurrentState != null && !CurrentState.IsToSetup)
                CurrentState.OnLateUpdate();
        }
    }
}

