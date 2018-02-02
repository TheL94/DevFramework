using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.AI
{
    public abstract class AI_Action : ScriptableObject
    {
        public void DoAct(AI_Controller _controller)
        {
            if (Act(_controller))
            {
                if (OnSuccesfulEnd != null)
                    OnSuccesfulEnd(_controller);
            }
            else
            {
                if (OnFailureEnd != null)
                    OnFailureEnd(_controller);
            }
        }

        protected abstract bool Act(AI_Controller _controller);

        public delegate void OnLifeFlow(AI_Controller _controller);

        /// <summary>
        /// Called on succesful end of the Action
        /// </summary>
        public event OnLifeFlow OnSuccesfulEnd;
        /// <summary>
        /// Called on failed end of the Action
        /// </summary>
        public event OnLifeFlow OnFailureEnd;
    }
}
