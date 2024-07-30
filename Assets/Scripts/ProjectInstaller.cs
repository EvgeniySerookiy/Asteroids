using Scenes;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameSceneManager>().AsSingle();
    }
}