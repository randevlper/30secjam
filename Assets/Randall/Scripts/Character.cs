using System.Collections;
using System.Collections.Generic;
using Gold.Delegates;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable {

	public Inform onDeath;
	public ValueChange<HitData> onHit;

	public ValueChange<float> onHealthChange;

	private float health;

	public float Health {
		get { return health; }
		set {
			if (onHealthChange != null) {
				onHealthChange (maxHealth / health);
			}
			health = value;
		}
	}
	public float maxHealth = 100f;

	private void Start () {
		Health = maxHealth;
	}

	public void Damage (HitData hit) {
		if (onHit != null) {
			onHit (hit);
		}
		health -= hit.damage;
		if (health <= 0f) {
			if (onDeath != null) {
				onDeath ();
			}
		}
	}
}