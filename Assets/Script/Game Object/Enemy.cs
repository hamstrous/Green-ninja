using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TravelTile
{
    // Start is called before the first frame update
    int travel = 0;
    protected override void OnDestroy()
    {
        TurnManager.Instance._moveTile.Remove(this);
    }

    protected override void NextBlock(Vector3Int direction)
    {
        _preDir = _dir;
        Vector3Int targetCell = currentCell + direction;
        List<Tile> _tile = GridManager.Instance.GetTile(targetCell);
        bool move = false;

        if (_tile == null)
        {
            move = true;
        }
        else
        {
            if (_tile.Count == 0) move = true;
            foreach (Tile tile in _tile)
            {
                Object id = tile.GetID();
                if (id == Object.WALL || id == Object.BARREL || id == Object.MELON || id == Object.METAL || id == Object.FIRE || id == Object.SPRING)
                {
                    move = false;
                    die = true;
                }
                else if (id == Object.ENEMY1 || id == Object.ENEMY2)
                {
                    tile.push(_dir);
                    move = true;
                }
                else if (id == Object.ONEWAY)
                {
                    if (tile.GetFace() == OppositeDir(_dir))
                    {
                        move = false;
                        die = true;
                    }
                    else
                    {
                        move = true;
                    }
                }
                else if (id == Object.FIRE)
                {
                    move = false;
                    die = true;
                }
                else if (id == Object.BOMB)
                {
                    die = true;
                    move = false;
                    tile.GoDie();
                }
                else if(id == Object.BOMBZONE)
                {
                    move = true;
                }
                else if (id == Object.MOVEBLOCKZONE)
                {
                    move = true;
                }
                else if (id == Object.MOVEBLOCK)
                {
                    move = false;
                    die = true;
                }
            }
        }
        if (move)
        {
            travel++;
            updateCell = direction;
        }
        else
        {
            //_dir = State.IDLE;
            die = true;
        }
        if(travel == 1)
        {
            PlayEnemyHit();
        }
    }

    public override void UpdateAnimation()
    {
        _animator.SetInteger("idle", (int)_face);
        _animator.SetInteger("direction", (int)_dir);
    }
}
