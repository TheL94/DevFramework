using System.Collections.Generic;

namespace UnityFramework.FSM
{
    public abstract class AbstactState
    {
        /// <summary>
        /// Ritorna il nome dello stato
        /// </summary>
        public string Name { get { return GetType().Name; } }

        private bool _isToSetup = true;
        /// <summary>
        /// Ritorna vero se lo stato non è mai stato inizializzato, altrimenti falso.
        /// </summary>
        public bool IsToSetup
        {
            get { return _isToSetup; }
            private set { _isToSetup = value; }
        }

        /// <summary>
        /// Riferimento della macchina a stati a da cui è stato inizializzato.
        /// </summary>
        FiniteStateMachine fsm;

        List<Transition> possibleTransitions;

        #region Event
        protected delegate void StateTransitionEvent(int _transitionID);
        protected StateTransitionEvent triggerTransition;
        #endregion

        #region API 
        /// <summary>
        /// Funzione che inizializza lo stato, chiama l'hook OnStart.
        /// </summary>
        /// <param name="_fsm"></param>
        public void Initialize(FiniteStateMachine _fsm)
        {
            fsm = _fsm;
            possibleTransitions = SetTransitions();
            triggerTransition += fsm.DoTransition;
            IsToSetup = false;
            Start();
        }

        /// <summary>
        /// Funzione che termina lo stato, chiama l'hook OnEnd.
        /// </summary>
        public void Terminate()
        {
            End();
            IsToSetup = true;
            triggerTransition -= fsm.DoTransition;
        }

        /// <summary>
        /// Funzione che ritorna la transizione che corrisponde all'ID passato, se esiste. Altrimenti ritorna null.
        /// </summary>
        /// <param name="_transitionID">ID della transizione</param>
        /// <returns>Transizione corrispondente all'ID passato</returns>
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
        protected virtual void Start() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void LateUpdate() { }
        protected virtual void End() { }
        #endregion
        #endregion

        /// <summary>
        /// Funzione che ritorna una lista di transizioni possibili. Da usare OBBLIGATORIAMENTE per il setup delle transizioni.
        /// </summary>
        /// <returns>La lista di transizioni</returns>
        protected abstract List<Transition> SetTransitions();
    }
}