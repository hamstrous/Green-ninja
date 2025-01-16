using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : TravelTile
{
    // Start is called before the first frame update

    void Start()
    {
        ID = Object.BARREL;
        Begin();
        _dir = State.IDLE; 
	UpdateAnimation();
        Rotate();
        UpdateTile();
        TurnManager.Instance._moveTile.Add(this);
    }


    protected override void NextBlock(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction;
        List<Tile> _tile = GetTileSet(targetCell);
        bool move = false;

        if (_tile == null)
        {
            move = true;
        }
        else
        {

            if (_tile.Count == 0) move = true;
            else
                foreach (Tile tile in _tile)
                {
                    Object id = tile.GetID();
                    if (id == Object.WALL)
                    {
                        move = false;
                    }
                    else if (id == Object.ENEMY1)
                    {
                        tile.push(_dir);
                        move = true;
                    }
                    else if (id == Object.ONEWAY)
                    {
                        if (tile.GetFace() == OppositeDir(_dir))
                        {
                            move = false;
                        }
                        else
                        {
                            move = true;
                        }
                    }
                    else if (id == Object.METAL)
                    {
                        move = false;
                    }
                    else if (id == Object.PLAYER)
                    {
                        move = false;
                        tile.push(_dir);
                    }
                    else if (id == Object.BARREL || id == Object.MELON)
                    {
                        move = false;
                        if (tile.GetDir() == OppositeDir(_dir)){
                            tile.push(State.DOWN);
                        }
                        else tile.push(_dir);
                    }else if(id == Object.FIRE)
                    {
                        move = true;
                    }else if(id == Object.BOMB)
                    {
                        move = false;
                        tile.GoDie();
                    }
                    else if (id == Object.BOMBZONE)
                    { 
                        move = true;
                    }
                    else if (id == Object.MOVEBLOCK)
                    {
                        if (tile.CanMove(_dir))
                        {
                            move = true;
                        }
                        else
                        {
                            move = false;
                        }
                    }else if (id == Object.MOVEBLOCKZONE)
                    {
                        move = true;
                    }
                }
        }
        if (move)
        {
            updateCell = direction;
        }
        else
        {
            if (_dir != State.DOWN) push(State.DOWN);
            else _dir = State.IDLE;

        }
        return;
    }

    // Update is called once per frame
    public override void UpdateAnimation()
    {
    	if(_dir == State.IDLE) _animator.speed = 0f;
	    else _animator.speed = 0.4f;   
    }
}
