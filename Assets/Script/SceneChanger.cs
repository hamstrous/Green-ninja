using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance;

    private static SceneChanger Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SceneChanger");
                instance = go.AddComponent<SceneChanger>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void SceneChange(string sceneName)
    {
        Instance.StartCoroutine(Instance.LoadSceneAsync(sceneName));
    }

    public static void ResetToCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Instance.StartCoroutine(Instance.LoadSceneAsync(currentSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = true;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
