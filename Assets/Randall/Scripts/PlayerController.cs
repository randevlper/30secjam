using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    Camera currentCamera;
    public Rigidbody2D rb2D;
    public Character character;
    public Vector2 directionalInput;
    public float speed;

    public GameObject Audio;
    public GameObject GFX;
    public float deathTime;

    // Use this for initialization
    void Start () {
        currentCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
        character.onDeath += OnDeath;
    }

    // Update is called once per frame
    void Update () {
        GetInput ();
        rb2D.velocity = directionalInput * speed;
    }

    void GetInput () {
        directionalInput.x = Input.GetAxisRaw ("Horizontal");
        directionalInput.y = Input.GetAxisRaw ("Vertical");

        float lookAngle = Vector2.SignedAngle (
            Vector2.right,
            (currentCamera.ScreenToWorldPoint (Input.mousePosition) - transform.position).normalized);
        transform.rotation = Quaternion.Euler (0, 0, lookAngle - 90);
        //
    }

    void OnDeath () {
        GFX.SetActive (false);
        Audio.SetActive(false);
        Invoke("ResetToScreen", deathTime);
    }

    void ResetToScreen(){
        SceneManager.LoadScene(0);
    }
}