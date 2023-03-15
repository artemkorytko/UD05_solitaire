using System;
using System.Collections;
using System.Collections.Generic;
using Solitaire;
using UnityEngine;
using Zenject;

public class PlayingCard : CardPlace
{
    public class Factory : PlaceholderFactory<CardData, PlayingCard>
    {
    }

    [SerializeField] private Transform openCardContainer;
    [SerializeField] private Transform closeCardContainer;

    private CardPlace _parent;

    public int Value { get; private set; }
    public CardType CardType { get; private set; }
    public CardColor CardColor { get; private set; }

    public bool IsInDeck { get; set; } = true;
    public CardPlace CurrentPlace => _parent;

    private CardData _cardData;

    [Inject]
    public void Construct(CardData cardData)
    {
        _cardData = cardData;
    }

    private void Awake()
    {
        Value = _cardData.Value;
        CardType = _cardData.CardType;
        CardColor = _cardData.CardColor;
        GetComponent<MeshRenderer>().sharedMaterial = _cardData.Material;

        _nextCardValue = Value - 1;
        _nextCardColor = CardColor == CardColor.Black ? CardColor.Red : CardColor.Black;
        _nextCardType = CardType.Any;
    }

    public void Open()
    {
        if (_isOpen) return;

        _isOpen = true;
        _cardContainer = openCardContainer;
        transform.Rotate(Vector3.forward * 180, Space.Self);
    }

    public void Close()
    {
        if (!_isOpen) return;

        _isOpen = false;
        _cardContainer = closeCardContainer;
        transform.Rotate(Vector3.forward * -180, Space.Self);
    }

    public void SetParent(CardPlace parent = null)
    {
        if (parent == null)
        {
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(parent.CardContainer);
            transform.localPosition = Vector3.zero;

            SetAtMain(parent.IsMain);

            if (_parent is PlayingCard card)
            {
                card.Open();
            }

            _parent = parent;
        }
    }

    private void SetAtMain(bool isMain)
    {
        if (isMain)
        {
            _nextCardColor = CardColor;
            _nextCardType = CardType;
            _nextCardValue = Value + 1;
        }
        else
        {
            _nextCardColor = CardColor == CardColor.Red ? CardColor.Black : CardColor.Red;
            _nextCardValue = Value - 1;
            _nextCardType = CardType.Any;
        }

        this.isMain = isMain;
    }
}