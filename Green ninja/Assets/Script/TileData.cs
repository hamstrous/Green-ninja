using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/Tile Data")]
public class TileData : ScriptableObject
{
    public State facing;

    public void Init(State facing)
    {
        this.facing = facing;
    }

    public static TileData CreateInstance(State state)
    {
        var data = ScriptableObject.CreateInstance<TileData>();
        data.Init(state);
        return data;
    }
}
