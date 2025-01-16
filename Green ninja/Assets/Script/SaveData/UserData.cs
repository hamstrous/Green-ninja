using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public List<string> _beatenLevel = new List<string>();
    public string _preLevel = "Level 1";

    public void WinLevel(string level)
    {
        if (!_beatenLevel.Contains(level))
        {
            _beatenLevel.Add(level);
        }
        _preLevel = level;
    }
}
