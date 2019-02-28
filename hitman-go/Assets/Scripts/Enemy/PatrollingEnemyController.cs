﻿using UnityEngine;
using PathSystem;
using GameState;
using Common;
using System.Collections;

namespace Enemy
{
    public class PatrollingEnemyController : EnemyController
    {


        public PatrollingEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {


        }

        protected override void MoveToNextNode(int nodeID)
        {
            currentEnemyView.GetGameObject().transform.localPosition = pathService.GetNodeLocation(nodeID);
            currentNodeID = nodeID;
        }

    }
}