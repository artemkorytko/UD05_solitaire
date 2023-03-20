namespace Solitaire.Signals
{
    public struct RemoveFromMainStackSignal
    {
        public readonly CardType Type;
        public readonly int Value;

        public RemoveFromMainStackSignal(CardType type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}