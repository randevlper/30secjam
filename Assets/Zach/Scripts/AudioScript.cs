using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    AudioSource myAudio;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!myAudio.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
