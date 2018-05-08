using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gold;

public class AudioSpawner : MonoBehaviour {

    //Prefabs
    public GameObject SoundPrefab;
    ObjectPool pool;
    public int objectPoolSize;

    void Awake()
    {
        if (ServiceLocator.instance.audioManager == null)
        {
            ServiceLocator.instance.audioManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        pool = new ObjectPool(SoundPrefab, objectPoolSize, false);
    }

    public void Play(AudioClip clip, Vector3 pos)
    {
        GameObject temp = pool.Get();
        temp.transform.parent = gameObject.transform;
        temp.SetActive(true);
        AudioSource audio = temp.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.clip = clip;
            temp.transform.position = pos;
            audio.Play();
        }
    }

}
