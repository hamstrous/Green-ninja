using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombZone : Zone
{
    // Start is called before the first frame update
    void Start()
    {
        Begin();
        ID = Object.BOMBZONE;
        Rotate();
        UpdateTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
