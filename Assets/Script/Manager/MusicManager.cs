using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    #region Singleton
    private static MusicManager _instance;
    public static MusicManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
            _instance = this;
    }

    #endregion Singleton

    public AudioSource _audioSourceBGM;
    public AudioSource _audioSourceFX;
    public AudioSource _audioSourceImpact;
    public AudioSource _audioSourceHit;
    public AudioSource _audioSourceTransition;
    public AudioSource _audioSourceEnd;
    public MusicConfig _config;

    int ran;
    public AudioClip GetClip(string fileName)
    {
        for(int i = 0; i < _config._clips.Count; i++)
        {
            if (_config._clips[i].name.Equals(fileName)) 
                return _config._clips[i];
        }
        Debug.Log("KHONG TIM THAY FILE " + fileName);
        return null;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level Selector")
        {
            _audioSourceBGM.clip = GetClip("green ninja intro");
        }
        else if (SceneManager.GetActiveScene().name == "Start Game")
        {
            return;
        }else _audioSourceBGM.clip = GetClip("green ninja main");
        _audioSourceBGM.Play();
    }

    public void PlaySoundFX(string fileName)
    {
        _audioSourceFX.clip = GetClip(fileName);
        _audioSourceFX.Play();
    }

    public void PlaySoundImpact(string fileName)
    {
        _audioSourceImpact.clip = GetClip(fileName);
        _audioSourceImpact.Play();
    }
    public void PlaySoundHit(string fileName)
    {
        _audioSourceHit.clip = GetClip(fileName);
        _audioSourceHit.Play();
    }
    public void PlaySoundTransition(string fileName)
    {
        _audioSourceTransition.clip = GetClip(fileName);
        _audioSourceTransition.Play();
    }
    public void PlaySoundEnd(string fileName)
    {
        _audioSourceBGM.clip = GetClip(fileName);
        _audioSourceBGM.Play();
    }

    public void PlaySoundGong()
    {
        PlaySoundTransition("gong");
    }
    public void PlaySoundWin()
    {
        PlaySoundEnd("win-jingle");
    }

    public void PlaySoundLose()
    {
        PlaySoundEnd("lose-jingle");
    }

    public void PlaySoundFrogCharge()
    {
        ran = Random.Range(1, 5);
        PlaySoundFX("charge" + ran);
    }

    public void PlaySoundFrogMove()
    {
        ran = Random.Range(1, 4);
        PlaySoundHit("kick" + ran);
        PlaySoundImpact("player-move" + Random.Range(1, 3));
    }

    public void PlaySoundFrogDie()
    {
        ran = Random.Range(1, 4);
        PlaySoundFX("hurt" + ran);
    }

    public void PlaySoundFrogWin()
    {
        ran = Random.Range(1, 4);
        PlaySoundHit("kick" + ran);
    }

    public void PlaySoundHit()
    {
        ran = Random.Range(1, 4);
        PlaySoundHit("hit-target" + ran);
    }

    public void PlaySoundEnemyDie()
    {
        ran = Random.Range(1, 3);
        PlaySoundFX("ninja-debris" + ran);
        if(Random.Range(1, 5) == 1)
            PlaySoundHit("ninja-die" + Random.Range(1, 5)); 
    }

    public void PlaySoundHitWall()
    {
        PlaySoundImpact("hit-wall");
    }

    public void PlaySoundHitRock()
    {
        PlaySoundImpact("hit-rock");
    }

    public void PlaySoundSpring()
    {
        ran = Random.Range(1, 3);
        PlaySoundHit("spring" + ran);
    }

    public void PlaySoundHitTeflon()
    {
        ran = Random.Range(1, 3);
        PlaySoundImpact("teflon" + ran);
    }

    public void PlaySoundMenuPunch()
    {
        ran = Random.Range(1, 3);
        PlaySoundFX("menu-punch" + ran);
    }
    public void PlaySoundFlapCreak()
    {
        PlaySoundFX("flapCreak");
    }
    public void PlaySoundBombHit()
    {
        PlaySoundHit("mine-hit");
    }
    public void PlaySoundMelonHit()
    {
        PlaySoundImpact("melon-hit");
    }
}
