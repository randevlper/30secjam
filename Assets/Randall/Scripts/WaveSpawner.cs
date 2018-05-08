using System.Collections;
using System.Collections.Generic;
using Gold;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public GameObject player;
	public PlayerData playerData;
	public Text timerText;
	public Text waveCountText;
	Timer timer;
	public float waveTime = 30.0f;
	int wave = 1;

	public int currentNumEnemies = 6;
	public int maxEnemies = 250;
	public float enemyWaveMuliplier = 1.0f;
	public GameObject enemyPrefab;
	public float spawnRadius = 20f;

	ObjectPool enemies;
	List<GameObject> enemyObjects;

	// Use this for initialization
	void Start () {
		enemies = new ObjectPool (enemyPrefab, maxEnemies, false);
		enemyObjects = new List<GameObject>(maxEnemies);

		for (int i = 0; i < maxEnemies; i++) {
			GameObject spawnedObject = enemies.Get ();
			EnemyAI spawnedAI = spawnedObject.GetComponent<EnemyAI> ();
			spawnedAI.target = player;
			spawnedObject.GetComponent<PointsOnDisable> ().playerData = playerData;
			spawnedObject.SetActive(true);
			enemyObjects.Add(spawnedObject);
		}

		for(int i = 0; i < enemyObjects.Count; i++)
		{
			enemyObjects[i].SetActive(false);
		}


		timer = new Timer (NextWave, waveTime, true);
		timer.Start ();
		SetWaveText ();
		SpawnWave ();

		playerData.Points = 0;
	}

	void NextWave () {
		wave++;
		SetWaveText ();
		currentNumEnemies = Mathf.Clamp((int) (currentNumEnemies * enemyWaveMuliplier),0,maxEnemies);
		SpawnWave ();
	}

	void SpawnWave () {
		for (int i = 0; i < currentNumEnemies; i++) {
			GameObject spawnedObject = enemies.Get ();
			spawnedObject.transform.position = (Vector2) player.transform.position +
				new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized * spawnRadius;
			spawnedObject.SetActive (true);
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

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (player.transform.position, spawnRadius);
	}
}