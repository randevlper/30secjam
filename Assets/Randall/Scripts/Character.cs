using System.Collections;
using System.Collections.Generic;
using Gold.Delegates;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable {

	public Inform onDeath;
	public ValueChange<HitData> onHit;

	public ValueChange<float> onHealthChange;
	//public ValueChange<float> onHealthChangeRaw;

	private float health;

	public float SetPrivateHealth{
		set{health = value;}
	}

	public float Health {
		get { return health; }
		set {
			health = value;
			if (onHealthChange != null) {
				onHealthChange (health/maxHealth);
			}
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
		//Debug.Log(name + " " + hit.damage);
		Health -= hit.damage;
		if (health <= 0f) {
			if (onDeath != null) {
				onDeath ();
			}
		}
	}
}