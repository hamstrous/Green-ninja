using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    #region singleton
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

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
    public Sprite _locked, _unlocked, _beated;
    UserData _user;
    [SerializeField] List<UnityEngine.UI.Button> _button = new List<UnityEngine.UI.Button>();
    string DATA_KEY = GameManager.DATA_KEY;
    Dictionary<string, List<string>> _nextLevel = new Dictionary<string, List<string>>
    {
        { "Level 1",new List<string> {"Level 2"} },
        { "Level 2",new List<string> {"Level 3"} },
        { "Level 3",new List<string> {"Level 4", "Level 5"} },
        { "Level 4",new List<string> {"Level 6"} },
        { "Level 5",new List<string> {"Level 6"} },
        { "Level 6",new List<string> {"Level 7"} },
        { "Level 7",new List<string> {"Level 8", "Level 9"} },
        { "Level 8",new List<string> {"Level 10"} },
        { "Level 9",new List<string> {"Level 10"} },
        { "Level 10",new List<string> {"Level 11"} },
        { "Level 11",new List<string> {"Level 12"} },
        { "Level 12",new List<string>()}
    };

    UnityEngine.UI.Button FindButtonWithName(string name)
    {
        foreach(var button in _button)
        {
            if(button.name == name) return button;
        }
        return null;
    }

    void Start()
    {
        
        if (PlayerPrefs.HasKey(DATA_KEY))
        {
            string savedJSON = PlayerPrefs.GetString(DATA_KEY);
            _user = JsonUtility.FromJson<UserData>(savedJSON);
        }
        else
        {
            _user = new UserData();
        }
        foreach (var _beat in _user._beatenLevel)
        {
            foreach(var _next in _nextLevel[_beat])
            {
                UnityEngine.UI.Button _bt = FindButtonWithName(_next);
                if (_bt != null)
                {
                    _bt.interactable = true;
                    _bt.image.sprite = _unlocked;
                    TextMeshProUGUI tmp = _bt.GetComponentInChildren<TextMeshProUGUI>();
                    if (tmp != null) tmp.enabled = true;
                }
            }
        }
        foreach (var button in _button)
        {
            if (_user._beatenLevel.Contains(button.name))
            {
                button.image.sprite = _beated;
                button.interactable = true;
                TextMeshProUGUI tmp = button.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null) tmp.enabled = true;
            }
        }
    }

    private void SaveData()
    {
        //JSON h¨®a data clas
        string dataJSON = JsonUtility.ToJson(this._user);
        Debug.Log("DATA " + dataJSON);
        //save JSON string
        PlayerPrefs.SetString(DATA_KEY, dataJSON);
    }
}
