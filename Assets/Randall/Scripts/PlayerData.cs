using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gold.Delegates;

public class PlayerData : MonoBehaviour {
	[SerializeField] int points;
	public Gold.Delegates.ValueChange<int> OnPointChange;
	public ValueChange<float> onDamageMultChange;
	public ValueChange<float> onFireSpeedMultChange;

	public int Points {
		get { return points; }
		set {
			points = value;
			if (OnPointChange != null) {
				OnPointChange (points);
			}
		}
	}

	float fireSpeedMult = 1.0f;
	float damageMult = 1.0f;

	public float editorFireSpeedMult = 1.0f;
	public float editorDamageMult = 1.0f;
	public float healthMult = 1.0f;

	public float DamageMult{
		get {return damageMult;}
		set {damageMult = value;
			if(onDamageMultChange != null){
				onDamageMultChange(damageMult);
			}}
	}

	public float FireSpeedMult{
		get {return fireSpeedMult;}
		set {fireSpeedMult = value;
			if(onFireSpeedMultChange != null){
				onFireSpeedMultChange(fireSpeedMult);
			}}
	}

	public bool CanBuy (int value) {
		if (points >= value) {
			return true;
		} else {
			return false;
		}

	}


	void Start()
	{
		FireSpeedMult = editorFireSpeedMult;
		DamageMult = editorDamageMult;
	}
}