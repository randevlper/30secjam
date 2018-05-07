using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour {

    void Awake()
    {
        if (ServiceLocator.instance.canvas == null)
        {
            ServiceLocator.instance.canvas = this.GetComponent<Canvas>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
