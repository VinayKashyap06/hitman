﻿using UnityEngine;
using PathSystem;
using Common;
using System.Collections;

namespace Enemy
{
    public class PatrollingEnemyController : EnemyController
    {


        public PatrollingEnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject) : base(_enemyService, _pathService, _spawnLocation, _enemyScriptableObject)
        {


        }

    }
}