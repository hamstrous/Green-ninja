using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public void PlayMenuSound()
    {
        MusicManager.Instance.PlaySoundMenuPunch();
    }
}
