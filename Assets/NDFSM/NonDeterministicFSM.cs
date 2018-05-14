using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.NDFSM
{
    public class NonDeterministicFSM : MonoBehaviour
    {
        private readonly List<BaseState> States = new List<BaseState>();
        private readonly List<char> Symbols = new List<char>();
        private readonly List<Transition> Transitions = new List<Transition>();

        private readonly BaseState InitialState;
        private readonly List<BaseState> FinalStates = new List<BaseState>();

        public void Setup()
        {

        }

        void Update()
        {

        }
    }
}

