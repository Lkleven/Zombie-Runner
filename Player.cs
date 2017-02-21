using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool respawn = false;
	public AudioSource audioSource1;	// For running/jump sounds
	public AudioSource audioSource2;	// Other sounds such as radio
	public AudioClip helicopterCall;
	public Transform playerSpawnPoints; // The parent of the spawn points

	private Transform[] spawnPoints;
	private Helicopter helicopter;

	// Use this for initialization
	void Start () {
		spawnPoints = playerSpawnPoints.GetComponentsInChildren<Transform> ();
		helicopter = GameObject.FindObjectOfType<Helicopter> ();

	}
	
	// Update is called once per frames
	void Update (){
		if (respawn) {
			respawn = false;
			Respawn ();			
		}
		if (Input.GetButtonDown ("CallHeli") && !helicopter.IsCalled()) {
			audioSource2.clip = helicopterCall;
			audioSource2.Play ();
			helicopter.CallForHelicopter ();
		}

	
	}

	private void Respawn(){
		int randomSpawnNumber = Random.Range (1, spawnPoints.Length);
		transform.position = spawnPoints [randomSpawnNumber].transform.position;
	}

}
