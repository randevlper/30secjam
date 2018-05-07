using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject target;
	Vector3 currentVelocity;
	public float smoothTime = 0.1f;
	public float maxSpeed = 5f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () { }

	private void LateUpdate () {
		Vector3 newPos = Vector3.SmoothDamp (transform.position, target.transform.position, ref currentVelocity, smoothTime, maxSpeed);
		newPos.z = transform.position.z;
		transform.position = newPos;
	}
}