using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool respawn = false;
	public Transform playerSpawnPoints; // The parent of the spawn points

	private Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
		spawnPoints = playerSpawnPoints.GetComponentsInChildren<Transform> ();

	}
	
	// Update is called once per frames
	void Update (){
		if (respawn) {
			respawn = false;
			Respawn ();			
		}
	
	}

	private void Respawn(){
		int randomSpawnNumber = Random.Range (1, spawnPoints.Length);
		transform.position = spawnPoints [randomSpawnNumber].transform.position;
	}

}
