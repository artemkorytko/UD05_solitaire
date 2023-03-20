using UnityEngine;

namespace Solitaire
{
    public struct CardData
    {
        public readonly int Value;
        public readonly CardType CardType;
        public readonly CardColor CardColor;
        public readonly Material Material;

        public CardData(int value, CardType cardType, CardColor cardColor, Material material)
        {
            Value = value;
            CardType = cardType;
            CardColor = cardColor;
            Material = material;
        }
    }
}