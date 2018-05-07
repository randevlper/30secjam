using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
	public Slider slider;
	public Character character;

	private void Awake() {
		character.onHealthChange = UpdateSliderValue;
	}
	void UpdateSliderValue(float percentHealth)
	{
		slider.value = percentHealth;
	}
}
