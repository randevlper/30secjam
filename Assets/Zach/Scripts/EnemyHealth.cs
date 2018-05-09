using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public GameObject target;
    public Camera cam;

    void Start()
    {
        cam = ServiceLocator.instance.cam;
    }

    void Update()
    {
        Vector3 temp = new Vector3(0, 50);
        transform.position = cam.WorldToScreenPoint(target.transform.position) + temp;
    }
}
