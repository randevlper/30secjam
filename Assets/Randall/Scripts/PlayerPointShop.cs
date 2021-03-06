﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gold.Delegates;

public class PlayerPointShop : MonoBehaviour {

	public Character playerCharacter;
	public PlayerData playerData;

	public int FireSpeedIncreaseCost;
	public int HealthIncreaseCost;
	public int DeamageIncreaseCost;
	public int HealCost;

	public float FireSpeedIncrease = 0.1f;
	public float HealthIncrease = 0.1f;
	public float DeamageIncrease = 0.1f;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (playerData.CanBuy (HealCost)) {
				playerCharacter.Health = playerCharacter.MaxHealth;
				playerData.Points -= (int)(playerCharacter.Health / 10);
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			//
			if (playerData.CanBuy (HealthIncreaseCost)) {
				playerData.healthMult += HealthIncrease;
				playerData.Points -= HealthIncreaseCost;
				HealthIncreaseCost *= 2;
				playerCharacter.MaxHealth = playerData.healthMult * playerCharacter.baseMaxHealth;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			if (playerData.CanBuy (DeamageIncreaseCost)) {
				playerData.DamageMult += DeamageIncrease;
				playerData.Points -= DeamageIncreaseCost;
				DeamageIncreaseCost *= 2;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			if (playerData.CanBuy (FireSpeedIncreaseCost)) {
				playerData.FireSpeedMult -= FireSpeedIncrease;
				playerData.Points -= FireSpeedIncreaseCost;
				FireSpeedIncreaseCost *= 2;
			}
		}

	}
}