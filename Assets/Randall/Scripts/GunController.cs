using System.Collections;
using System.Collections.Generic;
using Gold;
using Gold.Delegates;
using UnityEngine;

public class GunController : MonoBehaviour {

    Camera currentCamera;
    public GameObject bulletPrefab;
    public PlayerData playerData;

    public float cooldownAutoBase;
    float cooldownAuto;
    bool canShoot = true;
    Timer cooldownTimer;

    public float bulletSpeed;
    ObjectPool bullets;

    public float damage;

    public bool isFullAuto;

    public ValueChange<float> onDamageChange;
    public ValueChange<float> onCooldownChage;

    // Use this for initialization
    void Awake () {
        playerData.onDamageMultChange += SetDamageMult;
        playerData.onFireSpeedMultChange += SetFirerateMult;
    }

    void Start () {
        cooldownAuto = cooldownAutoBase;
        cooldownTimer = new Timer (CanShoot, cooldownAuto);
        bullets = new ObjectPool (bulletPrefab, 5, true);
        currentCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
    }

    void CanShoot () {
        canShoot = true;
    }

    void Cooldown () {
        canShoot = false;
        cooldownTimer.length = cooldownAuto;
        cooldownTimer.Start ();
    }

    void SetDamageMult (float value) {
        damage *= value;
        if (onDamageChange != null) {
            onDamageChange (damage);
        }
    }

    void SetFirerateMult (float value) {
        cooldownAuto = cooldownAutoBase * value;
        if (onCooldownChage != null) {
            onCooldownChage (cooldownAuto);
        }
    }

    // Update is called once per frame
    void Update () {
        cooldownTimer.Tick (Time.deltaTime);
        if (isFullAuto && canShoot) {
            //cooldownTimer.length = cooldownAuto;
            if (Input.GetButton ("Fire1")) {
                FireBullet (currentCamera.ScreenToWorldPoint (Input.mousePosition));
                Cooldown ();
            }
        }
    }

    void FireBullet (Vector2 position) {
        if (!canShoot) { return; }
        GameObject spawnedObject = bullets.Get ();
        Bullet spawnedBullet = spawnedObject.GetComponent<Bullet> ();
        spawnedBullet.damage = damage * playerData.DamageMult;
        spawnedObject.SetActive (true);
        spawnedBullet.Fire (transform.position, (position - (Vector2) transform.position).normalized, bulletSpeed);
        canShoot = false;
    }
}