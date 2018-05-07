using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    void Start()
    {
        if (ServiceLocator.instance.cam == null)
        {
            ServiceLocator.instance.cam = this.GetComponent<Camera>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
