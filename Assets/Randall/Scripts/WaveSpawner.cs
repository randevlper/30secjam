using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gold;

public class WaveSpawner : MonoBehaviour {

	public Text timerText;
	public Text waveCountText;
	Timer timer;
	public float waveTime = 30.0f;
	int wave = 1;

	// Use this for initialization
	void Start () {
		timer = new Timer(NextWave, waveTime, true);
		timer.Start();
		SetWaveText();
	}

	void NextWave()
	{
		wave++;
		SetWaveText();
		
	}
	
	// Update is called once per frame
	void Update () {
		timer.Tick(Time.deltaTime);
		SetTimerText();
		
	}

	void SetTimerText()
	{
		timerText.text = timer.RemainingTime.ToString().Substring(0,5);
	}

	void SetWaveText()
	{
		waveCountText.text = wave.ToString();
	}
}
