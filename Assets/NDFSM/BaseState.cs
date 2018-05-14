using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.NDFSM
{
    public abstract class BaseState
    {
        NonDeterministicFSM nDFSM;

        public void OnInit(NonDeterministicFSM _nDFSM)
        {
            nDFSM = _nDFSM;
        }

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnEnd() { }
    }
}