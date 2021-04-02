using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    // This is a simpleton method
    // Awake() is being called before start
    // https://docs.unity3d.com/Manual/ExecutionOrder.html
    private void Awake()
    {
        GameObject[] bgMusicObjs = GameObject.FindGameObjectsWithTag("bgmusic");
        if (bgMusicObjs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
