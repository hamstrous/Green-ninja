using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region singleton
    private static EnemyManager _instance;
    public static EnemyManager Instance => _instance;

    private void Awake()
    {
	_enemyCount = 0;
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
    public bool start = false;
    public static int _enemyCount = 0;

    // Update is called once per frame
    void Update()
    {
        if(start && _enemyCount == 0) {
            start = false;
            GameManager.Instance.Winner();
        }
    }
}
