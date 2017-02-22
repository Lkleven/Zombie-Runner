using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Transform playerSpawnPoints; // The parent of the spawn points

	private bool respawn = false;
	private Transform[] spawnPoints;
	private bool heliCalled = false;

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
		if (Input.GetButtonDown ("CallHeli") && !heliCalled && Time.realtimeSinceStartup > 10f) {
			BroadcastMessage ("CallHeli");		//InnerVoice.cs
			BroadcastMessage ("OnAttemptCall");	//ClearArea.cs
		}
	}

	private void Respawn(){
		int randomSpawnNumber = Random.Range (1, spawnPoints.Length);
		transform.position = spawnPoints [randomSpawnNumber].transform.position;
	}

	void OnNotClearArea(){
		BroadcastMessage ("OnInnerVoiceNotClearArea");	//InnerVoice.cs
	}

	void OnFindClearArea(){
		if(!heliCalled){
			BroadcastMessage ("OnInnerVoiceFindClearArea");	//InnerVoice.cs
			Invoke ("DropFlare", 3f);
			//Spawn zombies
		}
	}

	void OnHeliCalled(){
		heliCalled = true;
	}

	void DropFlare(){
		//Drop a flare
	}

}
