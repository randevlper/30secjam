using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public class GunController : MonoBehaviour {

    Camera currentCamera;
    public GameObject bulletPrefab;

    public float bulletSpeed;
    ObjectPool bullets;

    public bool isFullAuto;

    // Use this for initialization
    void Start () {
        bullets = new ObjectPool (bulletPrefab, 5, true);
        currentCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
    }

    // Update is called once per frame
    void Update () {

        if (isFullAuto) {
            if (Input.GetButton ("Fire1")) {
                FireBullet (currentCamera.ScreenToWorldPoint (Input.mousePosition));
            }
        } else {
            if (Input.GetButtonDown ("Fire1")) {
                FireBullet (currentCamera.ScreenToWorldPoint (Input.mousePosition));
            }
        }
    }

    void FireBullet (Vector2 position) {
        GameObject spawnedObject = bullets.Get ();
        Bullet spawnedBullet = spawnedObject.GetComponent<Bullet> ();
        spawnedObject.SetActive (true);
        spawnedBullet.Fire (transform.position, (position - (Vector2) transform.position).normalized, bulletSpeed);
    }
}