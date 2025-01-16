using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager._enemyCount++;
        ID = Object.ENEMY2;
        Begin();
        _dir = State.IDLE;
	    UpdateAnimation();
        UpdateTile();
        TurnManager.Instance._moveTile.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
