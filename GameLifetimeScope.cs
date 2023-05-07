using CommonProcesses;
using EventHandlers;
using Models;
using Presenters;
using VContainer;
using VContainer.Unity;
using Views;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<EnemiesLayoutsStore>(Lifetime.Singleton);
        builder.Register<EnemiesModel>(Lifetime.Singleton);
        builder.Register<GameStateModel>(Lifetime.Singleton);
        builder.Register<MainFieldModel>(Lifetime.Singleton);
        builder.Register<MainGameTimeLimitModel>(Lifetime.Singleton);
        builder.Register<ResultRankModel>(Lifetime.Singleton);
        builder.Register<ScoreModel>(Lifetime.Singleton);
        builder.Register<TanekkoExperienceModel>(Lifetime.Singleton);
        builder.Register<TanekkoModel>(Lifetime.Singleton);

        builder.RegisterComponentInHierarchy<BlackScreen>();
        builder.RegisterComponentInHierarchy<EnemyBulletFactory>();
        builder.RegisterComponentInHierarchy<EnemyFactory>();
        builder.RegisterComponentInHierarchy<GameOverScreen>();
        builder.RegisterComponentInHierarchy<Goal>();
        builder.RegisterComponentInHierarchy<InputView>();
        builder.RegisterComponentInHierarchy<MainCamera>();
        builder.RegisterComponentInHierarchy<MainField>();
        builder.RegisterComponentInHierarchy<MainGameTimeLimitView>();
        builder.RegisterComponentInHierarchy<MusicPlayer>();
        builder.RegisterComponentInHierarchy<ParamsDefiner>();
        builder.RegisterComponentInHierarchy<ResultScreen>();
        builder.RegisterComponentInHierarchy<SoundPlayer>();
        builder.RegisterComponentInHierarchy<Tanekko>();
        builder.RegisterComponentInHierarchy<TanekkoBody>();
        builder.RegisterComponentInHierarchy<TanekkoBulletFactory>();
        builder.RegisterComponentInHierarchy<TanekkoGroundDetector>();
        builder.RegisterComponentInHierarchy<TanekkoLifeCountView>();
        builder.RegisterComponentInHierarchy<TitleScreen>();

        builder.Register<DefeatEnemyProcess>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<EnemyPresenter>();
        builder.RegisterEntryPoint<GameOverScreenPresenter>();
        builder.RegisterEntryPoint<MainFieldPresenter>();
        builder.RegisterEntryPoint<MainGameTimeLimitPresenter>();
        builder.RegisterEntryPoint<ResultScreenPresenter>();
        builder.RegisterEntryPoint<TanekkoBodyPresenter>();
        builder.RegisterEntryPoint<TanekkoLifeCountPresenter>();
        builder.RegisterEntryPoint<TanekkoPresenter>();
        builder.RegisterEntryPoint<TitleScreenPresenter>();
        
        builder.RegisterEntryPoint<OnDefineParams>();
        builder.RegisterEntryPoint<OnFixedUpdatedMainGameTimeLimit>();
        builder.RegisterEntryPoint<OnFixedUpdatedTanekkoAbsorbing>();
        builder.RegisterEntryPoint<OnFixedUpdatedTanekkoAttacking>();
        builder.RegisterEntryPoint<OnFixedUpdatedTanekkoGetDamaged>();
        builder.RegisterEntryPoint<OnGameStart>();
        builder.RegisterEntryPoint<OnGoal>();
        builder.RegisterEntryPoint<OnMainGameStart>();
        builder.RegisterEntryPoint<OnTanekkoAbsorb>();
        builder.RegisterEntryPoint<OnTanekkoAttack>();
        builder.RegisterEntryPoint<OnTanekkoBulletReleased>();
        builder.RegisterEntryPoint<OnTanekkoGetDamaged>();
        builder.RegisterEntryPoint<OnTanekkoGrounded>();
        builder.RegisterEntryPoint<OnTanekkoJump>();
        builder.RegisterEntryPoint<OnTanekkoMove>();
        builder.RegisterEntryPoint<OnTanekkoPositionChanged>();
    }
}
