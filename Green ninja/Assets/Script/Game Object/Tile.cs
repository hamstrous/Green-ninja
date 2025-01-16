using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour {
    public Animator _animator;

    public bool movable;

    public string _name = "TILE";

    public bool update = false;

    [SerializeField] protected State _face = State.UP;
    public State GetFace() => _face;
    public void SetFace(State state) => _face = state;

    public Tilemap tilemap;
    
    protected int _x = 0;
    protected Object ID;

    protected Vector3Int currentCell;

    public Vector3Int GetCurrentCell() => currentCell;

    protected Vector3Int updateCell = Vector3Int.zero;



    public Object GetID() => ID;
    public void SetID(Object value) => ID = value;

    private void Start()
    {
        currentCell = tilemap.WorldToCell(transform.position);
        this.transform.position = tilemap.GetCellCenterWorld(currentCell);
    }

    public int x
    {
        get
        {
            return _x;
        }
        set
        {
            _x = value;
        }
    }
    protected int _y = 0;

    public int y
    {
        get
        {
            return _y;
        }
        set
        {
            _y = value;
        }
    }
    public void ChangePosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public virtual void push(State dir)
    {
        Debug.Log("p");
    }

    public State OppositeDir(State dir)
    {
        switch(dir)
        {
            case State.UP:
                return State.DOWN;
            case State.DOWN: 
                return State.UP;
            case State.LEFT:
                return State.RIGHT;
            case State.RIGHT:
                return State.LEFT;
        }return State.IDLE;
    }

    protected void Begin()
    {
        tilemap = GridManager.Instance.tilemap;
        currentCell = tilemap.WorldToCell(transform.position);
        this.transform.position = tilemap.GetCellCenterWorld(currentCell);
    }

    protected virtual void UpdateTile()
    {
        GridManager.Instance.UpdateTile(currentCell, this);
        //Debug.Log(ID + "got added");
    }

    public virtual void RemoveTile()
    {
        GridManager.Instance.RemoveTile(currentCell, this);
    }

    protected List<Tile> GetTileSet(Vector3Int pos)
    {
        return GridManager.Instance.GetTile(pos);
    }

    virtual public State GetDir()
    {
        return State.IDLE;
    }

    protected Vector3Int OppositeVector(Vector3Int direction)
    {
        if (direction == Vector3Int.up)
        {
            return Vector3Int.down;
        }
        else if (direction == Vector3Int.down)
        {
            return Vector3Int.up;
        }
        else if (direction == Vector3Int.left)
        {
            return Vector3Int.right;
        }
        else if (direction == Vector3Int.right)
        {
            return Vector3Int.left;
        }
        return Vector3Int.zero;
    }

    public void Lose()
    {
        GameManager.Instance.Lose();
    }

    public virtual void UpdateAnimation()
    {

    }

    protected Vector3Int StateToVector(State state)
    {
        switch (state){
            case State.UP:
                return Vector3Int.up;
            case State.DOWN:
                return Vector3Int.down;
            case State.LEFT:
                return Vector3Int.left;
            case State.RIGHT:
                return Vector3Int.right;
        }return Vector3Int.zero;
    }

    public void CheckWin()
    {
        GameManager.Instance.Win();
    }

    protected void Rotate()
    {
        switch (_face)
        {
            case State.UP:
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case State.DOWN:
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case State.LEFT:
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case State.RIGHT:
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }

    virtual public void GoDie() {}
    virtual public bool CanMove(State dir) { return true; }

    virtual public void Update1() { }
    virtual public void Update2() { }

    // Update is called once per frame
}
