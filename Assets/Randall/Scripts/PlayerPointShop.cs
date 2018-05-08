using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointShop : MonoBehaviour {

	public Character playerCharacter;
	public PlayerData playerData;
	public int FireSpeedIncreaseCost;
	public int HealthIncreaseCost;
	public int DeamageIncreaseCost;
	public int HealCost;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			if(playerData.CanBuy(HealCost))
			{
playerCharacter.Health = playerCharacter.maxHealth;
			}
			
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			//
			if(playerData.CanBuy(HealthIncreaseCost))
			{
				//playerData.
			}
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			//
		}
	}

	void SetHealth()
	{

	}

	void SetFireSpeed()
	{

	}
}
