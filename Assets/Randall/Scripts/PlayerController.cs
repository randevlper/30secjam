using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Camera currentCamera;
    public Rigidbody2D rb2D;
    public Vector2 directionalInput;
    public float speed;

    // Use this for initialization
    void Start () {
        currentCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
    }

    // Update is called once per frame
    void Update () {
        GetInput ();
        rb2D.velocity = directionalInput * speed;
    }

    void GetInput () {
        directionalInput.x = Input.GetAxisRaw ("Horizontal");
        directionalInput.y = Input.GetAxisRaw ("Vertical");

        float lookAngle = Vector2.SignedAngle(
                Vector2.right,
                (currentCamera.ScreenToWorldPoint (Input.mousePosition)- transform.position).normalized);
        transform.rotation = Quaternion.Euler(0,0,lookAngle);
        //
    }
}