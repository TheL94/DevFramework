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
        protected FiniteStateMachine FSM;

        List<Transition> possibleTransitions;

        #region API 
        /// <summary>
        /// Funzione che inizializza lo stato, chiama l'hook OnStart.
        /// </summary>
        /// <param name="_FSM"></param>
        public void Initialize(FiniteStateMachine _FSM)
        {
            FSM = _FSM;
            possibleTransitions = SetTransitions();
            OnStart();
            IsToSetup = false;
        }

        /// <summary>
        /// Funzione che termina lo stato, chiama l'hook OnEnd.
        /// </summary>
        public void Terminate()
        {
            OnEnd();
            IsToSetup = true;
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
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnEnd() { }
        #endregion
        #endregion

        /// <summary>
        /// Funzione che ritorna una lista di transizioni possibili. Da usare OBBLIGATORIAMENTE per il setup delle transizioni.
        /// </summary>
        /// <returns>La lista di transizioni</returns>
        protected abstract List<Transition> SetTransitions();
    }
}