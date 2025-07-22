using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicConfig", menuName = "Config/MusicConfig")]
public class MusicConfig : ScriptableObject
{
    public List<AudioClip> _clips;
}