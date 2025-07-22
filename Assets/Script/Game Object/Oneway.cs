using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oneway : EffectTile
{
    // Start is called before the first frame update
    bool _passThrough = false;
    void Start()
    {
        Begin();
        ID = Object.ONEWAY;
        Rotate();
	    UpdateAnimation();
        TurnManager.Instance._effectTile.Add(this);
        UpdateTile();
    }

    void PlayFlapCreak()
    {
        MusicManager.Instance.PlaySoundFlapCreak();
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
                if ((id == Object.PLAYER || id == Object.ENEMY1 || id == Object.ENEMY2 || id == Object.BARREL))
                {
                    if (!_passThrough)
                    {
                        PlayFlapCreak();
                        _passThrough = true;
                    }
                }
            }if(_tile.Count == 1)
            {
                _passThrough = false;
            }
        }
    }

    // Update is called once per frame
    public override void Update1()
    {
        ThisBlock(Vector3Int.zero);
	    UpdateAnimation();
    }

    public override void UpdateAnimation()
    {
        _animator.SetBool("pass", _passThrough);
    }
}
