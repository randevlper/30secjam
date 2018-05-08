using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {

    public static ServiceLocator instance;

    public Camera cam;
    public Canvas canvas;
    public AudioSpawner audioManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
