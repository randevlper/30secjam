using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnDisable : MonoBehaviour {

	public PlayerData playerData;
	public int points;

	void OnDisable()
	{
		playerData.Points += points;
	}
}
