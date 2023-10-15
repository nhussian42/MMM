﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;

    float assignedYPos;

    SpriteRenderer _spriteRenderer;

    //bool gameOver;


    public void Init(float assignedYPos)
    {
        this.assignedYPos = assignedYPos;
    }
    
    void OnEnable()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        ///GameManager.GameOver += () => gameOver = true;
    }

    private void OnDisable()
    {
        
        _spriteRenderer.color = new Color(255f, 255f, 255f, 1f);
        //GameManager.GameOver -= () => gameOver = true;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameOver) return;
        if (!SongManager.songPlaying) return;

        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        
        if (t > 1)
        {
            // Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.right * SongManager.Instance.noteSpawnX + Vector3.up * assignedYPos, Vector3.right * SongManager.Instance.noteDespawnX + Vector3.up * assignedYPos, t); 
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // Reset Position and release Note back into the object pool
    public void ReleaseNote()
    {
        transform.localPosition = Vector3.right * SongManager.Instance.noteSpawnX + Vector3.up * assignedYPos;
        LaneManager.Instance._notePool.Release(this);
    }

    public void GreyOutNote()
    {
        _spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f);
    }
}