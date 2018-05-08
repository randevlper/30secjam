using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
	[SerializeField] int points;
	public Gold.Delegates.ValueChange<int> OnPointChange;

	public int Points {
		get { return points; }
		set {
			points = value;
			if (OnPointChange != null) {
				OnPointChange (points);
			}
		}
	}

	public float fireSpeedMult = 1.0f;
	public float damageMult = 1.0f;
	public float healthMult = 1.0f;

	public bool CanBuy (int value) {
		if (value >= points) {
			return true;
		} else {
			return false;
		}

	}
}