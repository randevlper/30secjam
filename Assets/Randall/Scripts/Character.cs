using System.Collections;
using System.Collections.Generic;
using Gold.Delegates;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable {

	public Inform onDeath;
	public ValueChange<HitData> onHit;

	public ValueChange<float> onHealthChange;
	public ValueChange<float> onHealthChangeRaw;
	public ValueChange<float> onMaxHealthChangeRaw;

	private float health;

	public float SetPrivateHealth {
		set { health = value; }
	}

	public float Health {
		get { return health; }
		set {
			health = value;
			if (onHealthChange != null) {
				onHealthChange (health / maxHealth);
			}
			if (onHealthChangeRaw != null) {
				onHealthChangeRaw (health);
			}
		}
	}

	public float MaxHealth{
		get{return maxHealth;}
		set{
			maxHealth = value;
			if(onMaxHealthChangeRaw != null){
				onMaxHealthChangeRaw(maxHealth);
			}
		}
	}

	float maxHealth = 100f;
	public float baseMaxHealth;

	private void Start () {
		MaxHealth = baseMaxHealth;
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