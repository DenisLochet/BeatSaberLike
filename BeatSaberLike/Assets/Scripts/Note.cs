using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public string t;
    double timeInstantiated;
    public float assignedTime;

    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.right * SongManager.Instance.noteSpawnY, Vector3.right * SongManager.Instance.noteDespawnY, t);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == t)
        {
            double timeStamp = assignedTime;
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (Math.Abs(audioTime - timeStamp) < marginOfError)
            {
                Debug.Log("Hit");
            }
            else
            {
                Debug.Log($"Hit inaccurate on note with {Math.Abs(audioTime - timeStamp)} delay");
            }

            Destroy(gameObject);
        }
    }
}