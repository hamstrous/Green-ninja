using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : TravelTile
{
    // Start is called before the first frame update
    bool nextTurn = true;

    void Start()
    {
        ID = Object.MOVEBLOCK;
        Begin();
        _dir = State.IDLE;
        movable = true;
        Rotate();
        UpdateTile();
        TurnManager.Instance._moveTile.Add(this);
    }

    override public bool CanMove(State dir) { 
        if(!movable) return false;
        Vector3Int direction = StateToVector(dir);
        movable = false;
        nextTurn = false;
        Vector3Int targetCell = currentCell + direction;

        List<Tile> _tile = GetTileSet(targetCell);
        bool move = false;
        bool moveLine = false;

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
                        if (tile.GetDir() == OppositeDir(_dir))
                        {
                            tile.push(State.DOWN);
                        }
                        else tile.push(_dir);
                    }
                    else if (id == Object.FIRE)
                    {
                        move = true;
                    }
                    else if (id == Object.BOMB)
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
                        move = false;
                    }
                    else if(id == Object.MOVEBLOCKZONE)
                    {
                        move = true;
                        moveLine = true;
                    }
                }
        }
        if (moveLine&&move)
        {
            updateCell = direction;
            nextTurn = false;
            movable = false;
        }
        
        return moveLine&&move;
    }

    public override void Update1()
    {
        update = true;
    }

    public override void Update2()
    {
        RemoveTile();
        Vector3Int targetCell = currentCell + updateCell;
        updateCell = Vector3Int.zero;
        if (currentCell != targetCell) StartCoroutine(SmoothMovement(targetCell, 1));
        currentCell = targetCell;
        if (_dir != State.IDLE && ID == Object.PLAYER)
        {
            _face = OppositeDir(_dir);
        }
        Rotate();
        UpdateTile();
        if (nextTurn) movable = true;
        if (!nextTurn) nextTurn = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
