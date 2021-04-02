// the below are referred to as 'namespaces', they're just imports in Python
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int curSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIdx + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().DestroyOnReset();
    }

    public void QuitGame()
    {
        // this is different for iOS or Android!
        Application.Quit();
    }
}
