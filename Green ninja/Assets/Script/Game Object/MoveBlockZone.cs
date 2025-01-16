using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockZone : Zone
{
    // Start is called before the first frame update
    void Start()
    {
        ID = Object.MOVEBLOCKZONE;
        Begin();
        Rotate();
        UpdateTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
