using System;
using System.Collections.Generic;
using Solitaire.Signals;
using UnityEngine;
using Zenject;

namespace Solitaire
{
    public class GameController : MonoBehaviour
    {
        private int COMPLETE_VALUE = 78;

        [SerializeField] private CardPlace[] cardPlaces;

        private CardDeck _cardDeck;
        private SignalBus _signalBus;

        private readonly Dictionary<CardType, int> _mainPlaceInfo = new Dictionary<CardType, int>();

        [Inject]
        public void Construct(CardDeck cardDeck, SignalBus signalBus)
        {
            _cardDeck = cardDeck;
            _signalBus = signalBus;
        }

        private void Start()
        {
            FillGamePlaces();

            _signalBus.Subscribe<AddToMainStackSignal>(OnAddToMain);
            _signalBus.Subscribe<RemoveFromMainStackSignal>(OnRemoveFromMain);
            _signalBus.Subscribe<RestartButtonSignal>(Restart);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<AddToMainStackSignal>(OnAddToMain);
            _signalBus.Unsubscribe<RemoveFromMainStackSignal>(OnRemoveFromMain);
            _signalBus.Unsubscribe<RestartButtonSignal>(Restart);
        }

        private void Restart()
        {
            _cardDeck.Reset();
            _cardDeck.RandomizeDeck();
            FillGamePlaces();
            _mainPlaceInfo.Clear();
        }

        private void OnAddToMain(AddToMainStackSignal signal)
        {
            if (_mainPlaceInfo.ContainsKey(signal.Type))
            {
                _mainPlaceInfo[signal.Type] += signal.Value;
            }
            else
            {
                _mainPlaceInfo.Add(signal.Type, signal.Value);
            }

            CheckForWin();
        }

        private void OnRemoveFromMain(RemoveFromMainStackSignal signal)
        {
            if (_mainPlaceInfo.ContainsKey(signal.Type))
            {
                _mainPlaceInfo[signal.Type] -= signal.Value;
            }
        }

        private void CheckForWin()
        {
            if (_mainPlaceInfo.Keys.Count < 4)
            {
                return;
            }

            bool isWin = true;

            foreach (var key in _mainPlaceInfo.Keys)
            {
                if (_mainPlaceInfo[key] != COMPLETE_VALUE)
                {
                    isWin = false;
                }
            }

            if (isWin)
            {
                _signalBus.Fire<SignalBus>();
            }
        }

        private void FillGamePlaces()
        {
            for (int i = 0; i < cardPlaces.Length; i++)
            {
                int counter = i;
                var cardPlace = cardPlaces[i];
                PlayingCard card = null;
                while (counter > 0)
                {
                    card = _cardDeck.GetCard();
                    card.SetParent(cardPlace);
                    cardPlace = card;
                    counter--;
                }

                card = _cardDeck.GetCard();
                card.SetParent(cardPlace);
                card.Open();
            }
        }
    }
}