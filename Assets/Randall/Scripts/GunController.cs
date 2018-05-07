using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Camera currentCamera;
    public GameObject bulletPrefab;
    ObjectPool bullets;

    // Use this for initialization
    void Start () {
        bullets = new ObjectPool (bulletPrefab, 5, true);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown ("Fire1")) {
            //Shoot at mouse position
            FireBullet(currentCamera.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Input.GetButton ("Fire1")) {

        }

        if (Input.GetButtonUp ("Fire1")) {

        }
    }

    void FireBullet (Vector2 position) {
        GameObject spawnedObject = bullets.Get();
        Bullet spawnedBullet = spawnedObject.GetComponent<Bullet> ();
        spawnedObject.SetActive(true);
        spawnedBullet.Fire (transform.position, (position - (Vector2) transform.position).normalized, 10);
    }
}