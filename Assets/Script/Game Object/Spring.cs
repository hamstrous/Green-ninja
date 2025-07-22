using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spring : EffectTile
{
    // Start is called before the first frame update
    bool _bounce = false;    

    void Start()
    {
        ID = Object.SPRING;
        Begin();
        Rotate();
	UpdateAnimation();
        TurnManager.Instance._effectTile.Add(this);
        UpdateTile();
    }

    override protected void ThisBlock(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction;
        List<Tile> _tile = GetTileSet(targetCell);
	_bounce = false;
        if (_tile != null)
        {
            foreach (Tile tile in _tile)
            {
                Object id = tile.GetID();
                if ((id == Object.PLAYER || id == Object.ENEMY1 || id == Object.ENEMY2 || id == Object.BARREL)) 
                {
                    MusicManager.Instance.PlaySoundSpring();
                    tile.push(_face);
		    _bounce = true;
                }
            }
        }
    }

    // Update is called once per frame
    public override void Update1()
    {
        update = true;
        switch (_face)
        {
            case State.UP:
                ThisBlock(Vector3Int.up); break;
            case State.DOWN:
                ThisBlock(Vector3Int.down); break;
            case State.RIGHT:
                ThisBlock(Vector3Int.right); break;
            case State.LEFT:
                ThisBlock(Vector3Int.left); break;

        }
	UpdateAnimation();
    }
    
    public override void UpdateAnimation()
    {
        _animator.SetBool("bounce", _bounce);
    }
}
