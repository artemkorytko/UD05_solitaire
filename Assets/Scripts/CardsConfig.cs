using UnityEngine;

namespace Solitaire
{
    [CreateAssetMenu(fileName = "CardsConfig", menuName = "Configs/CardsConfig", order = 0)]
    public class CardsConfig : ScriptableObject
    {
        [SerializeField] private Material[] diamond;
        [SerializeField] private Material[] heart;
        [SerializeField] private Material[] spade;
        [SerializeField] private Material[] clubs;

        public Material[]Diamond => diamond;

        public Material[]Heart => heart;

        public Material[]Spade => spade;

        public Material[]Clubs => clubs;
    }
}