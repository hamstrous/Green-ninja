using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bomb : TravelTile
{
    // Start is called before the first frame update

    Vector3Int _previousDir;

    void Start()
    {
        Begin();
        ID = Object.BOMB;
        Rotate();
        UpdateTile();
        TurnManager.Instance._moveTile.Add(this);
    }

    bool IsBombZone(Vector3Int target)
    {
        List<Tile> _tile = GridManager.Instance.GetTile(target);
        bool ok = false;
        if (_tile != null){
            foreach(Tile tile in _tile)
                if (tile.GetID() == Object.BOMBZONE) ok = true;
        }
        return ok;
    }

    override public void Update1()
    {
        if (deathCall) return;
        //die = false;
        update = true;
        Vector3Int targetCell;
        foreach (Vector3Int direction in new Vector3Int[] {Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right }){
            if (direction == _previousDir) continue;
             targetCell = currentCell + direction;
            if (IsBombZone(targetCell))
            {
                updateCell = direction;
            }
        }
        if(updateCell == Vector3Int.zero) {
            targetCell = currentCell + _previousDir;
            if (IsBombZone(targetCell))
            {
                updateCell = _previousDir;
            }
        }
        targetCell = currentCell + updateCell;
        List<Tile> _tile = GridManager.Instance.GetTile(targetCell);
        if (_tile != null)
        {
            foreach (Tile tile in _tile){
                Object id = tile.GetID();
                if (id == Object.BOMB) continue;
                if (id == Object.PLAYER || id == Object.ENEMY1 || id == Object.ENEMY2 || id == Object.MELON) 
                {
                    die = true;
                    tile.GoDie();
                }
                else if(id != Object.MOVEBLOCKZONE && id != Object.BOMBZONE)
                {
                    die = true;
                }
            }
        }
    }

    override public void Update2()
    {
        RemoveTile();
        if (die)
        {
            PlayBombHit();
            _animator.SetBool("die", true);
            deathCall = true;
            return;
        }
        Vector3Int targetCell = currentCell + updateCell;
        _previousDir = OppositeVector(updateCell);
        updateCell = Vector3Int.zero;
        if (currentCell != targetCell)
        {
            if (!Occupied(targetCell))
                StartCoroutine(SmoothMovement(targetCell, 2));
            else
            {
                targetCell = currentCell;
                //_dir = State.DOWN;
            }
        }
        currentCell = targetCell;
        UpdateTile();
    }
}
