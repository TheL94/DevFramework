namespace UnityFramework.FSM
{
    public class Transition
    {
        public int ID { get; private set; }
        public AbstactState NextState { get; private set; }
        public TranstionType Type { get; private set; }

        public Transition(int _id, AbstactState _nextState)
        {
            ID = _id;
            NextState = _nextState;
        }

        public enum TranstionType
        {
            PopItself_PushNew,
            PopItself,
            PushNew
        }
    }
}

