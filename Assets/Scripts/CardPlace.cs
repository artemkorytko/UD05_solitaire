using System.Collections;
using System.Collections.Generic;
using Solitaire;
using UnityEngine;

public class CardPlace : MonoBehaviour
{
    [SerializeField] protected bool isMain;

    protected int _nextCardValue = -1;
    protected CardType _nextCardType = CardType.Any;
    protected CardColor _nextCardColor = CardColor.Any;
    [SerializeField] protected Transform _cardContainer;

    protected bool _isOpen = true;

    public bool IsMain => isMain;

    public int NextCardValue => _nextCardValue;

    public CardType NextCardType => _nextCardType;

    public CardColor NextCardColor => _nextCardColor;

    public Transform CardContainer => _cardContainer;

    public bool IsOpen => _isOpen;

    public bool IsCanConnect(PlayingCard playingCard)
    {
        if (!_isOpen)
            return false;
        
        if (playingCard.Value != _nextCardValue && _nextCardValue != -1)
        { 
            return false;
        }

        if (_nextCardColor != CardColor.Any && playingCard.CardColor != _nextCardColor)
        {
            return false;
        }

        if (_nextCardType != CardType.Any && playingCard.CardType != _nextCardType)
        {
            return false;
        }
        
        //TODO: Add connect logic

        return true;

    }
}
