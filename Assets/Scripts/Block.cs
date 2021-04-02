using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // init
    [SerializeField] AudioClip destroyBlock;
    [SerializeField] GameObject destroyParticles;
    [SerializeField] Sprite[] hitSprites;
    // One method to access Level script:
    //[SerializeField] Level level;

    // cached reference to objects
    Level level;
    GameSession gameSession;

    // state variables
    [SerializeField] int timesHit; // TODO only serialized for debugging

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

        AddBlockToCount();
    }

    private void AddBlockToCount()
    {
        level = FindObjectOfType<Level>();
        // only add block to block count if breakable
        if (tag == "breakable")
        {
            level.CountBreakableBlocks();
        }
    }

    // 'collision' is the parameter for this method of type 'Collision2D'
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "breakable")
        {
            HandleHit();
        }
    }

    // if breakable, increment timesHit and destroy if maxHits reached
    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= hitSprites.Length + 1)
        {
            DestroyBlock();
        }
        else 
        {
            ShowNextHitsSprite();
        }
    }

    private void ShowNextHitsSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Index '" + spriteIndex + "' not found in block: " + gameObject.name); 
        }
    }

    private void DestroyBlock()
    {
        Vector3 camPos = Camera.main.transform.position;

        // trigger particles
        TriggerParticles(); 

        // block is being destroyed, remove it from level script
        level.RemoveBreakableBlock();

        // update and increment score
        gameSession.UpdateScore();

        // this allowed for the clip to play even though its source was destroyed
        AudioSource.PlayClipAtPoint(destroyBlock, camPos);
        //Debug.Log("collided with" + collision.gameObject.name);
        //https://docs.unity3d.com/ScriptReference/Object.Destroy.html
        //'gameObject' is the current object that executes the code
        Destroy(gameObject);
    }

    private void TriggerParticles()
    {
        GameObject sparkles = Instantiate(destroyParticles, transform.position, transform.rotation);
        // Destroy object after a timer of 1f
        Destroy(sparkles, 1f);
    }
}
