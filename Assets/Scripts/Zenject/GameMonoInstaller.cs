using Solitaire;
using UnityEngine;
using Zenject;

public class GameMonoInstaller : MonoInstaller
{
    [Inject] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
        SignalInstaller.Install(Container);
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();

        Container.BindFactory<PlayingCard, PlayingCard.Factory>().FromComponentInNewPrefab(_gameSettings.PlayingCardPrefab);
    }
}