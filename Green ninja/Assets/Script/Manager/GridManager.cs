using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum State
{
    UP, DOWN, LEFT, RIGHT, IDLE
}

public enum Object
{
       PLAYER, WALL, ENEMY1, ENEMY2, ONEWAY, METAL, SPRING, BARREL, MELON, FIRE, BOMB, MOVEBLOCK, BOMBZONE, MOVEBLOCKZONE
}

public class GridManager : MonoBehaviour
{
    #region singleton
    private static GridManager _instance;
    public static GridManager Instance => _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    [SerializeField] private Wall _wall;
    [SerializeField] Transform _camera;
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy1;
    [SerializeField] private Oneway _oneway;
    [SerializeField] private Metal _metal;
    [SerializeField] private Spring _spring;
    [SerializeField] private MoveBlockZone _moveBlockZone;
    [SerializeField] private BombZone _bombZone;
     private Barrel _barrel;
    public Tilemap tilemap;

    public List<Tilemap> tilemapList = new List<Tilemap> ();
    public Dictionary<Vector3Int, List<Tile>> _tile = new Dictionary<Vector3Int, List<Tile>>();

    private void Init()
    {

    }

    void SetTileMap()
    {
        Wall wall = Instantiate(_wall, new Vector3(-10, -10), Quaternion.identity);
        MoveBlockZone mbzone = Instantiate(_moveBlockZone, new Vector3(-10, -10), Quaternion.identity);
        BombZone bzone = Instantiate(_bombZone, new Vector3(-10, -10), Quaternion.identity);

        List<Tile> tile = new List<Tile>();
        tile.Add(wall);
        tile.Add(bzone);
        tile.Add(mbzone);
        

        for (int i = 0; i < tilemapList.Count; i++) 
        {
            for(int x = tilemapList[i].cellBounds.xMin; x < tilemapList[i].cellBounds.xMax; x++)
            {
                for (int y = tilemapList[i].cellBounds.yMin; y < tilemapList[i].cellBounds.yMax; y++)
                {
                    if (tilemapList[i].HasTile(new Vector3Int(x, y)))   
                    {
                        UpdateTile(new Vector3Int(x,y), tile[i]);
                    }
                }
            }
        }
    }

    void Start()
    {
        Init();
        SetTileMap();
        EnemyManager.Instance.start = true;
    }
    public void RemoveTile(Vector3Int pos, Tile tile)
    {
        _tile[pos].Remove(tile);
    }

    public List<Tile> GetTile(Vector3Int pos)
    {
        if(!_tile.TryGetValue(pos, out List<Tile> value)) return null;
        return value;
    }

    public void UpdateTile(Vector3Int pos, Tile tile)
    {
        if(tile == null)
        {
            return;
        }
        if (!_tile.ContainsKey(pos))
            _tile[pos] = new List<Tile>();
        _tile[pos].Add(tile);
    }
}
