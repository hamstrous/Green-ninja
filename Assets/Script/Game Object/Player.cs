using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : TravelTile
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    void Start()
    {
        _dir = State.IDLE;
        _preDir = OppositeDir(_face);
        ID = Object.PLAYER;
        Begin();
        UpdateAnimation();
        UpdateTile();
        TurnManager.Instance._moveTile.Add(this);
    }

    void PlayFrogCharge()
    {
        MusicManager.Instance.PlaySoundFrogCharge();
    }

    void PlayFrogMove()
    {
        MusicManager.Instance.PlaySoundFrogMove();
    }

    void PlayHitWall()
    {
        MusicManager.Instance.PlaySoundHitWall();
    }
    void PlayHitTeflon()
    {
        MusicManager.Instance.PlaySoundHitTeflon();
    }

    bool charging = false;
    void Move()
    {
        if (deathCall) return;
        State newDir = State.IDLE;

        if (_dir == State.IDLE) 
        {
            //KEYBOARD
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
            {
                if (!charging)
                {
                    PlayFrogCharge();
                    charging = true;
                }

            }
            else charging = false;
            if (!charging)
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    newDir = State.LEFT;
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    newDir = State.RIGHT;
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    newDir = State.DOWN;
                }
                else if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    newDir = State.UP;
                }
                if (newDir != State.IDLE && newDir != _preDir) 
                {
                    PlayFrogMove();
                    _dir = newDir;
                }
            }
            //TOUCHSCREEN
            if (Input.touches.Length > 0)
            {
                
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    PlayFrogCharge();
                    //save began touch 2d point
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }
                if (t.phase == TouchPhase.Ended)
                {
                    //save ended touch 2d point
                    secondPressPos = new Vector2(t.position.x, t.position.y);

                    //create vector from the two points
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    //normalize the 2d vector
                    currentSwipe.Normalize();

                    //swipe upwards
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        newDir = State.UP;
                    }
                    //swipe down
                    if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        newDir = State.DOWN;
                    }
                    //swipe left
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        newDir = State.LEFT;
                    }
                    //swipe right
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        newDir = State.RIGHT;
                    }

                    if (newDir != State.IDLE && newDir != _preDir)
                    {
                        PlayFrogMove();
                        _dir = newDir;
                    }
                }
            }
        }
    }

    protected override void NextBlock(Vector3Int direction)
    {
        Vector3Int targetCell = currentCell + direction;
        List<Tile> _tile = GetTileSet(targetCell);
        bool move = false;
        bool stick = false;

        bool gonnaDie = true;

        if (_tile == null)
        {
            gonnaDie = true;
            move = true;
        }
        else
        {

            if (_tile.Count == 0)
            {
                move = true; 
                gonnaDie = false;
            }
            else
                foreach (Tile tile in _tile)
                {
                    Object id = tile.GetID();
                    if (id == Object.WALL)
                    {
                        PlayHitWall();
                        gonnaDie = false;
                        move = false;
                        stick = true;
                    }
                    else if (id == Object.ENEMY1 || id == Object.ENEMY2)
                    {
                        gonnaDie = false;
                        tile.push(_dir);
                        move = true;
                    }
                    else if (id == Object.ONEWAY)
                    {
                        gonnaDie = false;
                        if (tile.GetFace() == OppositeDir(_dir))
                        {
                            move = false;
                            stick = true;
                        }
                        else
                        {
                            move = true;
                        }
                    }
                    else if (id == Object.METAL || id == Object.SPRING)
                    {
                        if (id == Object.METAL) PlayHitTeflon();
                        move = false;
                        stick = false;
                        gonnaDie = false;
                    }
                    else if (id == Object.BARREL)
                    {
                        gonnaDie = false;
                        move = false;
                        stick = false;
                        if (tile.GetDir() == OppositeDir(_dir))
                        {
                            tile.push(State.DOWN);
                        }
                        else
                        {
                            tile.push(_dir);
                        }
                    }
                    else if (id == Object.MELON)
                    {
                        gonnaDie = false;
                        move = false;
                        stick = false;
                        tile.push(_dir);
                    }
                    else if (id == Object.FIRE)
                    {
                        move = false;
                    }
                    else if (id == Object.BOMB)
                    {
                        gonnaDie = true;
                        move = false;
                        tile.GoDie();
                    }else if(id == Object.BOMBZONE)
                    {
                        gonnaDie = false;
                        move = true;
                    }else if(id == Object.MOVEBLOCK)
                    {
                        stick = true;
                        gonnaDie = false;
                        if (tile.CanMove(_dir))
                        {
                            move = true;
                        }
                        else
                        {
                            PlayHitWall();
                            move = false;
                        }
                    }
                    else if(id == Object.MOVEBLOCKZONE)
                    {
                        gonnaDie = false;
                        move = true;
                    }
                }
        }
        if (move)
        {
            gonnaDie = false;
            updateCell = direction;
        }
        else if(!stick && _dir != State.DOWN)
        {
            push(State.DOWN);
        }
        else
        {

            _dir = State.IDLE;
        }
        if (die) return;
        die = gonnaDie;
    }

    private void Update()
    {
        Move();
	    _preDir = OppositeDir(_face);
    }

    public override void UpdateAnimation()
    {
        _animator.SetInteger("idle", (int)_face);
        _animator.SetInteger("direction", (int)_dir) ;
    }

}
