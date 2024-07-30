using Scenes;
using Zenject;

public class StartSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<StartScene>().FromComponentInHierarchy().AsSingle();
    }
}