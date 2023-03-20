 using Solitaire;
using Zenject;

public class GameMonoInstaller : MonoInstaller
{
    [Inject] private GameSettings _gameSettings;
    
    public override void InstallBindings()
    {
        SignalInstaller.Install(Container);
        
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        Container.BindFactory<CardData, PlayingCard, PlayingCard.Factory>()
            .FromComponentInNewPrefab(_gameSettings.PlayingCardPrefab);
    }
}