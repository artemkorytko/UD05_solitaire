namespace Solitaire.Signals
{
    public struct ExcludeFromDeckSignal
    {
        public readonly PlayingCard PlayingCard;

        public ExcludeFromDeckSignal(PlayingCard playingCard)
        {
            PlayingCard = playingCard;
        }
    }
}