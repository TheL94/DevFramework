﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.AI
{
    /// <summary>
    /// It runs and controls the flow of the current State.
    /// Also manage the state change.
    /// </summary>
    public class AI_Controller : MonoBehaviour
    {
        public bool IsActive = false;
        public AI_State InitialDefaultState;
        private AI_State _currentState;
        public AI_State CurrentState
        {
            get { return _currentState; }
            set { _currentState = OnCurrentStateSet(CurrentState, value); }
        }

        public void Init(AI_State currentState)
        {
            CurrentState = currentState;
            OnInit();
        }

        protected virtual void OnInit() { }

        private void Update()
        {
            if (CurrentState == null)
            {
                if (InitialDefaultState == null)
                    IsActive = false;
                else
                    Init(InitialDefaultState);
            }

            if (IsActive)
            {
                CurrentState.Run(this);
            }
        }

        /// <summary>
        /// Called on CurrentState Set to manage the shift correctly
        /// </summary>
        /// <param name="oldState">Previus CurrentState</param>
        /// <param name="newState">Incoming State to Set</param>
        private AI_State OnCurrentStateSet(AI_State oldState, AI_State newState)
        {
            if (oldState != null)
            {
                oldState.Clean(); //Clean the old State as soon as the state change in order to prevent multiple State changes called by events
                
                //Prevent incorrect state changes
                if (newState == null || oldState.name == newState.name)
                    return oldState;
            }


            AI_State newStateInstance = AI_DataManager.GetState(this, newState);
            newStateInstance.Init();

            return newStateInstance;
        }
    }

    public static class AI_ControllerExtention
    {
        /// <summary>
        /// Used to call a state change from outside
        /// </summary>
        /// <param name="_controller"></param>
        /// <param name="_newState"></param>
        public static void SetAICurrentState(this AI_Controller _controller, AI_State _newState)
        {
            _controller.CurrentState = _newState;
        }
    }
}
