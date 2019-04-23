using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Transform[] enemy_spawnPoint;
	public GameObject enemy;
	public float reSpawn = 2f;

	private int maxEnemy = 0;
	private bool isGameOver = false;

	private void Awake() {
		if ( init == null ) init = this;
		else Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		enemy_spawnPoint = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
		StartCoroutine(SpawnEnemy());
		maxEnemy = enemy_spawnPoint.Length - 1;
	}

	IEnumerator SpawnEnemy() {
		while ( !isGameOver ) {
			int currEnemy = GameObject.FindGameObjectsWithTag("ENEMY").Length;
			if ( currEnemy < maxEnemy ) {
				yield return new WaitForSeconds(reSpawn);

				int idx = Random.Range(1, enemy_spawnPoint.Length);
				Instantiate(enemy, enemy_spawnPoint[idx].position, enemy_spawnPoint[idx].rotation);
			}
			else
				yield return null;
		}
	}

	public void IsGameOver() {
		isGameOver = !isGameOver;
	}

	public static GameManager init = null;
}