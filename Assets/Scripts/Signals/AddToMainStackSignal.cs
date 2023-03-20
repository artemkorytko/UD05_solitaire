namespace Solitaire.Signals
{
    public struct AddToMainStackSignal
    {
        public readonly CardType Type;
        public readonly int Value;

        public AddToMainStackSignal(CardType type, int value)
        {
            Type = type;
            Value = value;
        }
    }
}