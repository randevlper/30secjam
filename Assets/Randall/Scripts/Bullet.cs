using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;

public class Bullet : MonoBehaviour {

	float damage;
	public Rigidbody2D rb2D;
	public Timer timer;
	public float life;

	void Awake () {
		timer = new Timer (Disable, life);
	}

	void Disable () {
		gameObject.SetActive (false);
		timer.Stop ();
	}

	private void Update () {
		timer.Tick (Time.deltaTime);
	}

	public void Fire (Vector2 pos, Vector2 dir, float speed) {
		transform.position = pos;
		rb2D.velocity = dir * speed;
		timer.Start ();
	}

	void OnCollisionEnter2D (Collision2D collisionInfo) {
		IDamageable damageable = collisionInfo.gameObject.GetComponent<IDamageable> ();
		if (damageable != null) {
			damageable.Damage (new HitData (gameObject, damage));
		}
	}
}