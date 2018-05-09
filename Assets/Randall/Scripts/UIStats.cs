using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour {

	public Character playerCharacter;
	public GunController gun;

	public Text ui;
	public string healthFormat;
	public string firerateFormat;
	public string damageFormat;


	int health;
	int maxHealth;
	float firerate;
	float damage;


	void Awake()
	{
		playerCharacter.onHealthChangeRaw += SetHealth;
		playerCharacter.onMaxHealthChangeRaw += SetMaxHealth;
		gun.onCooldownChage += SetFirate;
		gun.onDamageChange += SetDamage;
	}

	public void SetHealth(float value)
	{
		health = (int)value;
		BuildString();
	}

	public void SetMaxHealth(float value)
	{
		maxHealth = (int)value;
		BuildString();
	}

	public void SetFirate(float value)
	{
		firerate = value;
		BuildString();
	}

	public void SetDamage(float value)
	{
		damage = value;
		BuildString();
	}

	void BuildString()
	{
		ui.text = healthFormat + health + "/" + maxHealth + firerateFormat + firerate + damageFormat + damage;
	}
}
