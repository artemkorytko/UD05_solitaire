using Solitaire.Signals;
using Zenject;

namespace Solitaire
{
    public class SignalInstaller : Installer<SignalInstaller>

    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<OnElementClickSignal>();
            Container.DeclareSignal<OnBoardMatchSignal>();
            Container.DeclareSignal<AddCoinsSignal>();  
            Container.DeclareSignal<RestartButtonSignal>();  
            Container.DeclareSignal<StartGameSignal>();  
        }
    }
}