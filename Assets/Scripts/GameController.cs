using System;
using UnityEngine;
using Zenject;

namespace Solitaire
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private CardPlace[] cardPlaces;

        private CardDeck _cardDeck;

        [Inject]
        private void Construct(CardDeck cardDeck)
        {
            _cardDeck = cardDeck;
        }

        private void Start()
        {
            FillGamePlaces();
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