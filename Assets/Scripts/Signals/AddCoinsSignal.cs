namespace Solitaire.Signals
{
    public struct AddCoinsSignal
    {
        public readonly int Value;

        public AddCoinsSignal(int value)
        {
            Value = value;
        }
    }
}