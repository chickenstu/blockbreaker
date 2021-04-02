using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    // params
    [SerializeField] int breakableBlocks; // serialized for debugging
    
    // cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void RemoveBreakableBlock()
    {
        breakableBlocks--;
    }

    private void Update()
    {
        //Debug.Log("Blocks: " + breakableBlocks);
        if(breakableBlocks == 0)
        {
            //Debug.Log("All Blocks Destroyed!!");
            sceneLoader.LoadNextScene();
        }
    }
}
