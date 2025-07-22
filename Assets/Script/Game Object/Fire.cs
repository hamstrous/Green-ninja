using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : EffectTile
{
    bool _smother = false;
    // Start is called before the first frame update
    void Start()
    {
        Begin();
        ID = Object.FIRE;
	TurnManager.Instance._effectTile.Add(this);
        Rotate();
        UpdateAnimation();
        UpdateTile();
    }

    override protected void ThisBlock(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction;
        List<Tile> _tile = GetTileSet(targetCell);
        if (_tile != null)
        {
            foreach (Tile tile in _tile)
            {
                Object id = tile.GetID();
                if ((id == Object.MOVEBLOCK || id == Object.BARREL))
                {
                    _smother = true;
                }
            }
            if (_tile.Count == 1)
            {
                _smother = false;
            }
        }
    }

    public override void Update1()
    {
        ThisBlock(Vector3Int.zero);
        UpdateAnimation();
    }
    public override void UpdateAnimation()
    {
        _animator.SetBool("smother", _smother);
    }
}
