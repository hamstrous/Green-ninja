using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : TravelTile
{
    void Start()
    {
        ID = Object.MELON;
        Begin();
        Rotate();
        UpdateTile();
        UpdateAnimation();
        TurnManager.Instance._moveTile.Add(this);
    }


    protected override void NextBlock(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction;
        List<Tile> _tile = GetTileSet(targetCell);
        bool move = true;

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
                            _dir = State.IDLE;
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
                        tile.push(_dir);
                    }
                    else if (id == Object.FIRE)
                    {
                        move = true;
                    }
                    else if (id == Object.BOMB)
                    {
                        die = true;
                        move = false;
                        tile.GoDie();
                    }
                }
        }
        if (move)
        {
            updateCell = direction;
        }
        else
        {
            die = true;
        }
        return;
    }
	
    public override void UpdateAnimation(){
	if(_dir == State.IDLE) _animator.speed = 0f;
	    else _animator.speed = 0.4f;  
    }
}
