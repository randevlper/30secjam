using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HitData
{
	float damage;
	GameObject other;

	public HitData(GameObject you, float dam)
	{
		damage = dam;
		other = you;
	}
}
public interface IDamageable
{
	void Damage(HitData hit);
}
