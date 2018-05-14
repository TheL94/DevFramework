using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFramework.NDFSM
{
    public class Transition
    {
        public BaseState StartState { get; private set; }
        public char Symbol { get; private set; }
        public BaseState EndState { get; private set; }

        public Transition(BaseState _startState, char _symbol, BaseState _endState)
        {
            StartState = _startState;
            Symbol = _symbol;
            EndState = _endState;
        }
    }
}

