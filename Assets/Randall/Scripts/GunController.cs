using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public class GunController : MonoBehaviour {

    Camera currentCamera;
    public GameObject bulletPrefab;
    public PlayerData playerData;

    public float cooldownAuto;
    bool canShoot = true;
    Timer cooldownTimer;

    public float bulletSpeed;
    ObjectPool bullets;

    public float damage;

    public bool isFullAuto;

    // Use this for initialization
    void Start () {
        cooldownTimer = new Timer(CanShoot,cooldownAuto);
        bullets = new ObjectPool (bulletPrefab, 5, true);
        currentCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
    }

    void CanShoot()
    {
        canShoot = true;
    }

    void Cooldown()
    {
        canShoot = false;
        cooldownTimer.length = cooldownAuto;
        cooldownTimer.Start();
    }

    // Update is called once per frame
    void Update () {
        cooldownTimer.Tick(Time.deltaTime);
        if (isFullAuto && canShoot) {
            if (Input.GetButton ("Fire1")) {
                FireBullet (currentCamera.ScreenToWorldPoint (Input.mousePosition));
                Cooldown();
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
        spawnedBullet.damage = damage * playerData.damageMult;
        spawnedObject.SetActive (true);
        spawnedBullet.Fire (transform.position, (position - (Vector2) transform.position).normalized, bulletSpeed);
    }
}