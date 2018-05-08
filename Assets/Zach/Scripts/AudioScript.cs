using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioSource myAudio;

    void Update()
    {
        if (!myAudio.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
