using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public void PlayCharge()
    {
        MusicManager.Instance.PlaySoundFrogCharge();
    }
    public void PlayMove()
    {
        MusicManager.Instance.PlaySoundFrogMove();
    }
}
