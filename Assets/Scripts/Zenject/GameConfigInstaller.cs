using Solitaire;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
{
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private CardsConfig cardsConfig;

    public override void InstallBindings()
    {
        Container.BindInstances(gameSettings,cardsConfig);
    }
}