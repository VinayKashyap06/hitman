﻿using UnityEngine;
using Zenject;
using Player;
using Enemy;
using InputSystem;
using PathSystem;
using System.Collections;
using Common;
using GameState;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayerMoveSignal>();
        Container.DeclareSignal<PlayerSpawnSignal>();
        Container.DeclareSignal<PlayerDeathSignal>();
        Container.DeclareSignal<PlayerKillSignal>();
        Container.DeclareSignal<EnemyDeathSignal>();
        Container.DeclareSignal<GameStartSignal>();
        Container.DeclareSignal<GameOverSignal>();
        Container.DeclareSignal<StateChangeSignal>();
        Container.DeclareSignal<NewLevelLoadedSignal>();
        Container.Bind<IEnemyService>()
            .To<EnemyService>()
            .AsSingle()
            .NonLazy();
        Container.Bind<IPlayerService>()
            .To<PlayerService>()
            .AsSingle()
            .NonLazy();

        Container.Bind(typeof(IInputService),typeof(ITickable))
            .To<InputService>()
            .AsSingle()
            .NonLazy();
        


        Container.Bind<IPathService>()
            .To<PathService>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameService>()
            .AsSingle()
            .NonLazy();
    }
}
