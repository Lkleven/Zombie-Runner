﻿using UnityEngine;
using System.Collections;

public class Eyes : MonoBehaviour {
	private Camera eyes;
	private float defaultFOV;

	// Use this for initialization
	void Start () {
		eyes = GetComponent<Camera> ();
		defaultFOV = eyes.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Zoom")) {
			eyes.fieldOfView = defaultFOV * 0.5f;
		} else {
			eyes.fieldOfView = defaultFOV;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.name.Equals("Zombie(Clone)")){
			Debug.Log ("Player Hit");
		}
	}
}
