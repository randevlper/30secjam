using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public GameObject player;
	public Text timerText;
	public Text waveCountText;
	Timer timer;
	public float waveTime = 30.0f;
	int wave = 1;

	public int currentNumEnemies = 6;
	public float enemyWaveMuliplier = 1.0f;
	public GameObject enemyPrefab;

	public Transform[] spawnpoints;

	ObjectPool enemies;

	// Use this for initialization
	void Start () {
		enemies = new ObjectPool (enemyPrefab, currentNumEnemies, true);
		timer = new Timer (NextWave, waveTime, true);
		timer.Start ();
		SetWaveText ();
		SpawnWave();
	}

	void NextWave () {
		wave++;
		SetWaveText ();
		currentNumEnemies = (int) (currentNumEnemies * enemyWaveMuliplier);
		SpawnWave();
	}

	void SpawnWave () {
		for (int i = 0; i < currentNumEnemies; i++) {
			GameObject spawnedObject = enemies.Get ();
			spawnedObject.transform.position = spawnpoints[Random.Range (0, spawnpoints.Length)].position;
			spawnedObject.SetActive (true);
			EnemyAI spawnedAI = spawnedObject.GetComponent<EnemyAI>();
			spawnedAI.target = player;
		}
	}

	// Update is called once per frame
	void Update () {
		timer.Tick (Time.deltaTime);
		SetTimerText ();

	}

	void SetTimerText () {
		timerText.text = timer.RemainingTime.ToString ().Substring (0, 5);
	}

	void SetWaveText () {
		waveCountText.text = wave.ToString ();
	}
}