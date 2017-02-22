using UnityEngine;
using System.Collections;

public class RadioSystem : MonoBehaviour {
	private AudioSource audioSource; //radio comm
	public AudioClip radioForHelicopter, helicopterETA5min;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMakeInitialHeliCall(){
		audioSource.clip = radioForHelicopter;
		audioSource.Play();
		Invoke ("HelicopterIncoming", radioForHelicopter.length + 1f);
	}

	void HelicopterIncoming(){
		audioSource.clip = helicopterETA5min;
		audioSource.Play ();
		BroadcastMessage ("OnDispatchHelicopter"); //Helicopter.cs
	}
}
