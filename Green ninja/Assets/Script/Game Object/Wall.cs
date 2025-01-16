using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Tile
{
    private void Awake()
    {
        
    }

    void Start()
    {
        ID = Object.WALL;
        Begin();
        Rotate();
        UpdateTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
