using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region singleton
    private static TurnManager _instance;
    public static TurnManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public static int tilePerSecond = 8;
    float timer = 0, BombTimer = 0;
    public List<TravelTile> _moveTile = new List<TravelTile>();
    public List<EffectTile> _effectTile = new List<EffectTile>();
    // Update is called once per frame
    void Update()
    {
        bool DoBomb = false;

        if (timer >= (float)1 / tilePerSecond)
        {
            //first update 
            if (BombTimer >= (float)1 / (tilePerSecond / 2)) DoBomb = true;
            foreach (var tile in _moveTile)
            {
                if (tile.GetDir() == State.IDLE) continue;

                if (tile.GetID() == Object.BOMB && !DoBomb) continue;

                /*if (tile.update)
                {
                    tile.update = false;
                    continue;
                }*/
                tile.Update1();
                tile.update = false;
            }

            foreach (var tile in _moveTile)
            {
                if (tile.GetDir() != State.IDLE) continue;

                if (tile.GetID() == Object.BOMB && !DoBomb) continue;

                /*if (tile.update)
                {
                    tile.update = false;
                    continue;S
                }*/
                tile.Update1();
                tile.update = false;
            }

            foreach (var tile in _effectTile)
            {
                if (tile.update)
                {
                    tile.update = false;
                    continue;
                }
                tile.Update1();
                tile.update = false;
            }

            for (int i = _moveTile.Count - 1; i >= 0; i--)
            {
                var tile = _moveTile[i];

                if (tile.GetID() == Object.BOMB) continue;

                if (tile != null)
                {
                    tile.Update2();
                }
            }

            if (DoBomb)
                for (int i = _moveTile.Count - 1; i >= 0; i--)
                {
                    var tile = _moveTile[i];

                    if (tile.GetID() != Object.BOMB) continue;

                    if (tile != null)
                    {
                        tile.Update2();
                    }
                }
           
            timer = 0;
            if (DoBomb) BombTimer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            BombTimer += Time.deltaTime;
        }
    }
}
