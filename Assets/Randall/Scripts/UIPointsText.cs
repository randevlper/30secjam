using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointsText : MonoBehaviour {

	public Text text;
	public string flavor;
	public PlayerData playerData;

	private void Start() {
		playerData.OnPointChange = SetPoints;
	}

	public void SetPoints(int points)
	{
		text.text = flavor + points;
	}
}
