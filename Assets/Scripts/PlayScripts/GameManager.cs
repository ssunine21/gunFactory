using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Sprite[] gunSprite;
	private Transform[] enemy_spawnPoint;
	//private Transform[] enemy_SpawnPoint;
	public GameObject enemy;
	public GameObject gunBackground;
	public Image mainWeapon_Image;
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

	public void ChangeGunBoxSprite(int idx) {
		Debug.Log(idx);
		gunBackground.GetComponent<Image>().sprite = gunSprite[idx];
		mainWeapon_Image.sprite =  gunBackground.transform.GetChild(idx).GetComponent<Image>().sprite;


		//UI.transform.GetChild(idx).GetComponent<Image>().sprite = gunSprite[idx];
	}

	public static GameManager init = null;
}