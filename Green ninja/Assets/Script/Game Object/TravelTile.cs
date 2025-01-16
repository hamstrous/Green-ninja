using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TravelTile : Tile
{
    // Start is called before the first frame update
    protected int tilePerSecond = TurnManager.tilePerSecond;
    protected bool _smooth = false;
    public float GetTilePerSecond() => tilePerSecond;

    [SerializeField] protected float timer = 0;
    [SerializeField] protected State _dir = State.IDLE;
    protected State _preDir = State.IDLE;
    override public State GetDir() => _dir;

    protected bool die = false;
    protected bool deathCall = false;
    float _deathSpeed = 10f;
    

    override public void GoDie()
    {
        die = true;
    }

    void Update()
    {

    }

    virtual protected void OnDestroy()
    {
        TurnManager.Instance._moveTile.Remove(this);
    }

    public override void push(State dir)
    {
        if(ID == Object.BARREL) PlayHitRock();
        _dir = dir;
        Update1();
    }

    protected virtual void NextBlock(Vector3Int direction)
    {

    }

    protected virtual void CheckFall()
    {
        Vector3Int direction = Vector3Int.zero;
        if (OppositeDir(_face) == State.UP) 
        {
            direction = Vector3Int.up;
        }
        else if (OppositeDir(_face) == State.RIGHT)
        {
            direction = Vector3Int.right;
        }
        else if (OppositeDir(_face) == State.DOWN)
        {
            direction = Vector3Int.down;
        }
        else if (OppositeDir(_face) == State.LEFT)
        {
            direction = Vector3Int.left;
        }

        if (ID == Object.ENEMY2) return;

        Vector3Int targetCell = currentCell + direction;
        List<Tile> _tile = GetTileSet(targetCell);

        if(_tile == null)
        {
            push(State.DOWN);
        }
        else if (_tile.Count > 0)
        {

        }else push(State.DOWN);
    }

    override public void Update1()
    {
	if(deathCall) return;
        //die = false;
        update = true;
        //if (_pdir == State.IDLE && _dir == OppositeDir(_face)) _dir = State.IDLE;
        if (_dir != State.IDLE && ID == Object.PLAYER)
        {
            _face = OppositeDir(_dir);
        }
        switch (_dir)
        {
            case State.UP:
                NextBlock(Vector3Int.up); break;
            case State.DOWN:
                NextBlock(Vector3Int.down); break;
            case State.RIGHT:
                NextBlock(Vector3Int.right); break;
            case State.LEFT:
                NextBlock(Vector3Int.left); break;
            case State.IDLE:
                CheckFall(); break;

        }
        UpdateAnimation();
        if (_dir == State.IDLE && ID == Object.PLAYER)
        {
            if (GameManager.Instance.winner)
            {
                _animator.SetTrigger("win");
                deathCall = true;
            }
        }
    }

    protected bool Occupied(Vector3Int pos)
    {
        List<Tile> _tile = GetTileSet(pos);
        if (_tile == null) return false;
        foreach(var tile in _tile)
        {
            if (tile.GetID() == Object.BARREL || tile.GetID() == Object.PLAYER) return true;
        }
        return false;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public IEnumerator Move(float x, float y)
    {
        while (true)
        {
            transform.position = transform.position + new Vector3(x, y) * Time.deltaTime; 
            yield return null;
        }
    }

    float RandomSpeed()
    {
        return Random.Range(_deathSpeed, _deathSpeed + 8);
    }

    public void MoveDown()
    {
        StartCoroutine(Move(0, -RandomSpeed()));
    }

    public void MoveLeft()
    {
        StartCoroutine(Move(-RandomSpeed(), -RandomSpeed()  ));
    }
    public void MoveRight()
    {
        StartCoroutine(Move(RandomSpeed(), -RandomSpeed()));
    }

    public void DieMove()
    {
        if (_preDir == State.LEFT) MoveRight();
        else if (_preDir == State.RIGHT) MoveLeft();
        else MoveDown();
    }

    override public void Update2()
    {
        if (deathCall) return;
        RemoveTile();
        if (die)
        {
            if(ID == Object.PLAYER)
            {
                PlayFrogDie();
            }
            if(ID == Object.ENEMY1 || ID == Object.ENEMY2)
            {
                PlayEnemyDie();
                EnemyManager._enemyCount--;
            }
            if (ID == Object.MELON)
            {
                PlayMelonHit();
            }
		_animator.SetTrigger("die");
                deathCall = true;
            return;
        }

        Vector3Int targetCell = currentCell + updateCell;
        updateCell = Vector3Int.zero;
        if (currentCell != targetCell)
        {
            if (!Occupied(targetCell))
                StartCoroutine(SmoothMovement(targetCell, 1));
            else
            {
                targetCell = currentCell;
                //_dir = State.DOWN;
            }
        }
        currentCell = targetCell;
        //Rotate();
        UpdateTile();
    }

    protected IEnumerator SmoothMovement(Vector3Int targetCell, int speed)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = tilemap.GetCellCenterWorld(targetCell);

        float progress = 0f;
        while (progress <= 1f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, progress);
            progress += (float) tilePerSecond/speed * Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
    }

    protected void PlayEnemyHit()
    {
        MusicManager.Instance.PlaySoundHit();
    }

    protected void PlayBombHit()
    {
        MusicManager.Instance.PlaySoundBombHit();
    }

    protected void PlayEnemyDie()
    {
        MusicManager.Instance.PlaySoundEnemyDie();
    }
    protected void PlayFrogDie()
    {
        MusicManager.Instance.PlaySoundFrogDie();
    }
    protected void PlayMelonHit()
    {
        MusicManager.Instance.PlaySoundMelonHit();
    }
    void PlayHitRock()
    {
        MusicManager.Instance.PlaySoundHitRock();
    }

    void PlayFrogWin()
    {
        MusicManager.Instance.PlaySoundFrogWin();
    }

    void PlayGong()
    {
        MusicManager.Instance.PlaySoundGong();
    }
}
