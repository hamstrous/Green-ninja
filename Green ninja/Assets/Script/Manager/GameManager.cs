using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    public const string DATA_KEY = "DATA_KEY";
    public UnityEngine.Canvas win, lose;
    bool los = false, wi = false;
    UserData _user;
    public bool winner = false;

    public void Reset()
    {
        SceneChanger.ResetToCurrentScene();
    }

    public void UpdateData()
    {
        if(PlayerPrefs.HasKey(DATA_KEY))
        {
            _user = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(DATA_KEY));
        }
        else
        {
            _user = new UserData();
        }
    }

    public void Winner()
    {
        winner = true;
    }

    public void Win()
    {
        if (!wi)
        {
            UpdateData();
            _user.WinLevel(SceneManager.GetActiveScene().name);
            SaveData();
            MusicManager.Instance.PlaySoundWin();
            Instantiate(win);
            wi = true;
        }
    }

    public void Lose()
    {
        if (!los)
        {
            MusicManager.Instance.PlaySoundLose();
            Instantiate(lose);
            los = true;
        }
    }

    private void SaveData()
    {
        //JSON h¨®a data clas
        string dataJSON = JsonUtility.ToJson(this._user);
        //Debug.Log("DATA " + dataJSON);
        //save JSON string
        PlayerPrefs.SetString(DATA_KEY, dataJSON);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
