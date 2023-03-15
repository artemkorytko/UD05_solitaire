using System;
using System.Collections;
using System.Collections.Generic;
using Solitaire;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private Transform showContainer;

    private PlayingCard.Factory _factory;
    private CardsConfig _cardsConfig;
    private SignalBus _signalBus;

    private MeshRenderer _meshRenderer;

    private int _currentShown = -1;
    public List<PlayingCard> _cards = new List<PlayingCard>();
    private List<PlayingCard> _allCards = new List<PlayingCard>();

    [Inject]
    public void Construct(PlayingCard.Factory factory, CardsConfig cardsConfig, SignalBus signalBus)
    {
        _factory = factory;
        _cardsConfig = cardsConfig;
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        GenerateCards();
        RandomizeDeck();
    }

    private void GenerateCards()
    {
        GenerateType(_cardsConfig.Diamond, CardType.Diamond, CardColor.Red);
        GenerateType(_cardsConfig.Heard, CardType.Heard, CardColor.Red);
        GenerateType(_cardsConfig.Clubs, CardType.Clubs, CardColor.Black);
        GenerateType(_cardsConfig.Spade, CardType.Spade, CardColor.Black);
    }

    private void GenerateType(Material[] materials, CardType type, CardColor color)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            var data = new CardData(i, type, color, materials[i]);
            var card = _factory.Create(data);
            card.gameObject.SetActive(false);
            card.Close();
            _cards.Add(card);
            _allCards.Add(card);
        }
    }

    private void RandomizeDeck()
    {
        var randomizesList = new List<PlayingCard>(_cards.Count);
        while (_cards.Count > 0)
        {
            var rndIndex = Random.Range(0, _cards.Count);
            randomizesList.Add(_cards[rndIndex]);
            _cards.RemoveAt(rndIndex);
        }

        _cards.AddRange(randomizesList);
    }

    public PlayingCard GetCard()
    {
        if (_cards.Count == 0) return null;
        
        var card = _cards[0];
        _cards.RemoveAt(0);
        card.gameObject.SetActive(true);
        card.IsInDeck = false;

        return card;
    }
}