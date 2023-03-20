using System;
using System.Collections;
using System.Collections.Generic;
using Solitaire;
using Solitaire.Signals;
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
        _signalBus.Subscribe<ExcludeFromDeckSignal>(ExcludeFromDeck);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<ExcludeFromDeckSignal>(ExcludeFromDeck);
    }

    private void ExcludeFromDeck(ExcludeFromDeckSignal signal)
    {
        _cards[_currentShown].IsInDeck = false;
        _cards.RemoveAt(_currentShown);

        if (_currentShown + 1 < _cards.Count)
        {
            _currentShown++;
        }
        else
        {
            _meshRenderer.enabled = true;
            _currentShown = -1;
        }
    }

    private void OnMouseUpAsButton()
    {
        ShowNext();
    }

    private void ShowNext()
    {
        if (_currentShown >= 0)
        {
            _cards[_currentShown].gameObject.SetActive(false);
            _cards[_currentShown].Close();
        }

        _currentShown++;

        if (_currentShown == _cards.Count - 1 && _meshRenderer.enabled)
        {
            _meshRenderer.enabled = false;
            _cards[_currentShown].gameObject.SetActive(true);
            _cards[_currentShown].Open();
            return;
        }

        if (_currentShown >= _cards.Count)
        {
            _currentShown = -1;
            _meshRenderer.enabled = true;
        }
        else
        {
            _cards[_currentShown].gameObject.SetActive(true);
            _cards[_currentShown].Open();
        }
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
            card.transform.SetParent(showContainer);
            card.transform.localPosition = Vector3.zero;
            card.gameObject.SetActive(false);
            card.Close();
            _cards.Add(card);
            _allCards.Add(card);
        }
    }

    public void RandomizeDeck()
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

    public void Reset()
    {
        _cards.Clear();
        foreach (var card in _allCards)
        {
            card.transform.SetParent(null);
        }

        foreach (var card in _allCards)
        {
            card.transform.SetParent(showContainer);
            card.Reset();
            card.IsInDeck = true;
            card.gameObject.SetActive(false);
        }

        _cards.AddRange(_allCards);
    }
}