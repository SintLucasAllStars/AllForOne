using Zenject;

public class SetupBindings : MonoInstaller
{
    public GameManager gameManager;
    public UnitManager unitManager;
    public UnitEditorManager unitEditor;
    public DefaultUiManager defaultUiManager;
    public TurnManager turnManager;
    
    public override void InstallBindings()
    {
        Container.Bind<ITurnManager>().FromInstance(turnManager).AsSingle();
        Container.Bind<IUnitEditorManager>().FromInstance(unitEditor).AsSingle();
        Container.Bind<IGameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<IUnitManager>().FromInstance(unitManager).AsSingle();
        Container.Bind<IDefaultUiManager>().FromInstance(defaultUiManager).AsSingle();
    }
}