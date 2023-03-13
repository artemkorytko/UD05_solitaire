using UnityEngine;

namespace Solitaire
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Configs/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private PlayingCard playingCardPrefab;

        public PlayingCard PlayingCardPrefab => playingCardPrefab;
    }
}