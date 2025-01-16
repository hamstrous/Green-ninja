using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : Tile
{
    // Start is called before the first frame update
    void Start()
    {
        ID = Object.METAL;
        Begin();
        Rotate();
        UpdateTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
