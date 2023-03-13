using Solitaire;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
{
    [SerializeField] private GameSettings gameSettings;

    public override void InstallBindings()
    {
        Container.BindInstances(gameSettings);
    }
}