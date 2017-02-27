using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

	public GameObject zombie; // the prefab to be instantiated
	public GameObject zombieParent; // the prefab to be instantiated
	public GameObject target; // the target of the zombie i.e. the player
	public bool spawning = false;

	private Transform[] spawnPoints;
	private int zombieStartCount = 4, zombieCounter = 0, maxZombies = 100;
	private float minimumDistance = 300f; //The minimum distance between a player and a spawn to actually spawn a zombie (prevents zombies appearing in front of the player)
	private float maximumDistance = 800f;

	//Variables for controlling spawnrate
	private float createRate = 1.5f, createRateTimer;
	private float rateIncrease = 0.1f, initialCreateDelay = 3.0f;
	private int spawnCounter = 0, spawnsBeforeRateIncrease = 10;

	// Use this for initialization
	void Start () {
		spawnPoints = new Transform[transform.childCount];

		//Iterate through all the children and put them in array
		int i = 0;
		foreach (Transform spawn in transform) {
			spawnPoints [i] = spawn;
			i++;
		}

		for (int y = 0; y < zombieStartCount; y++) {
			SpawnZombie();
		}

		createRateTimer = createRate + initialCreateDelay;
	
	}
	
	// Update is called once per frame
	void Update () {
		if(spawning){
			createRateTimer -= Time.deltaTime;
			if (createRateTimer <= 0) {
				SpawnZombie ();
			}
		}
	}


	//Spawns a zombie at a random spawn point
	//If spawn point is to close to the player, the method will call itself until a suitable spawn is found
	void SpawnZombie(){
		int randomSpawnNumber = Random.Range (1, spawnPoints.Length);
		float distance = Vector3.Distance (spawnPoints [randomSpawnNumber].transform.position, target.transform.position);

		if (distance > minimumDistance && distance < maximumDistance && zombieCounter < maxZombies) {
			spawnCounter++;
			zombieCounter++;
			GameObject zomb = Instantiate (zombie, spawnPoints [randomSpawnNumber].transform.position, Quaternion.identity) as GameObject;
			zomb.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ().target = target.transform;
			zomb.transform.parent = zombieParent.transform;
		} else {
			//SpawnZombie ();
		}

		if (spawnCounter >= spawnsBeforeRateIncrease) {
			if (createRate - rateIncrease > 2f) {
				createRate -= rateIncrease;
				spawnCounter = 0;
			} else {
				createRate = 2f;
			}
		}
		createRateTimer = createRate;
	}
}
