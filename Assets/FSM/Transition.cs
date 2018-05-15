namespace UnityFramework.FSM
{
    public class Transition
    {
        public int ID { get; private set; }
        public AbstactState NextState { get; private set; }
        public TranstionType Type { get; private set; }

        public Transition(int _id, AbstactState _nextState, TranstionType _type = TranstionType.PopItself_PushNew)
        {
            ID = _id;
            NextState = _nextState;
            Type = _type;
        }

        public enum TranstionType
        {
            PopItself_PushNew,
            PopItself,
            PushNew
        }
    }
}

