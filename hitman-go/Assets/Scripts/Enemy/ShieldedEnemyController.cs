﻿using UnityEngine;
using PathSystem;
using Common;
using System.Collections;

namespace Enemy
{
    public class ShieldedEnemyController : EnemyController
    {


        public ShieldedEnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject) : base(_enemyService, _pathService, _spawnLocation, _enemyScriptableObject)
        {


        }

    }
}