using System;
using Zenject;

namespace Solitaire
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly PlayingCard.Factory _factory;

        public GameController(PlayingCard.Factory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _factory.Create();
        }

        public void Dispose()
        {
        }
    }
}