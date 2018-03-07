using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.AI
{
    /// <summary>
    /// Class the add some property to the normal Action in order to make it easy manageable by the Unity Editor and State class.
    /// </summary>
    [System.Serializable]
    public class ActionStructureForState
    {
        public float Delay;
        private float timer;
        public bool Loop;
        private bool runtOnce;
        public AI_Action Action;
        public AI_State OnSuccess;
        public AI_State OnFailure;

        private AI_Action originalAction;

        public void Init(bool toSetUp)
        {
            if (toSetUp)
            {
                if (originalAction == null)
                    originalAction = Action;

                Action = GameObject.Instantiate(originalAction);
            }

            if (!Loop)
                runtOnce = false;
            if (Delay > 0)
                timer = Delay;

            Action.OnSuccesfulEnd += HandleOnSuccess;
            Action.OnFailureEnd += HandleOnFailure;
        }

        public void Clean()
        {
            Action.OnSuccesfulEnd -= HandleOnSuccess;
            Action.OnFailureEnd -= HandleOnFailure;
        }

        public void Run(AI_Controller _controller)
        {
            if (timer <= 0)
            {
                if (Loop || (runtOnce == false))
                {
                    Action.DoAct(_controller);
                    runtOnce = true;
                }
            }
            else
                timer -= Time.deltaTime;
        }

        void HandleOnSuccess(AI_Controller _controller)
        {
            if (OnSuccess != null)
                _controller.CurrentState = OnSuccess;
        }
        void HandleOnFailure(AI_Controller _controller)
        {
            if (OnFailure != null)
                _controller.CurrentState = OnFailure;
        }
    }
}
