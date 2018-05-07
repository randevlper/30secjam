using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Rigidbody2D rb2D;
    public Vector2 directionalInput;
    public float speed;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        GetInput ();
        rb2D.velocity = directionalInput * speed;
    }

    void GetInput () {
        directionalInput.x = Input.GetAxisRaw ("Horizontal");
        directionalInput.y = Input.GetAxisRaw ("Vertical");
    }
}