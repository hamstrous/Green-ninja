using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{

    private void Awake()
    {
        
    }

    void Start()
    {
        EnemyManager._enemyCount++;
        ID = Object.ENEMY1;
        Begin();
        _dir = State.IDLE;
        UpdateAnimation();
        UpdateTile();
        TurnManager.Instance._moveTile.Add(this);
    }

    
}
