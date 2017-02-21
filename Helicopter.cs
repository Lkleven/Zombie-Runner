using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

	private bool called = false;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CallForHelicopter(){
		called = true;
		audioSource.Play ();
	}

	public bool IsCalled(){
		return called;
	}
}
