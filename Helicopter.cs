using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {
	public GameObject mainRotor;

	private Vector3 startPos, destination;
	private float startTime;
	private bool dispatched = false, landing = false;
	private Animator animator;
	private GameObject helicopter;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		helicopter = transform.GetChild (0).gameObject;
		animator = helicopter.GetComponent<Animator> ();
		animator.enabled = false;							//An active animator prevents script from rotating the rotor in Update() below


	}

	void Update(){
		mainRotor.transform.Rotate (Vector3.forward * Time.deltaTime * 3600);	//degrees per second

		if (dispatched && !landing) {
			transform.position = Vector3.Lerp (startPos, destination, (Time.time - startTime) / 15);
		}

		if (transform.position == destination && !landing) {
			animator.enabled = true;
			landing = true;
			animator.SetTrigger ("HelicopterFlareLanding");
		}
	}

	void OnDispatchHelicopter(){
		dispatched = true;
		startTime = Time.time;

		destination = GameObject.FindGameObjectWithTag ("LandingArea").transform.position;
		float tempZ = destination.z;
		float tempY = destination.y;
		tempZ -= 40f;
		tempY += 0.7f;
		destination.z = tempZ;
		destination.y = tempY;
	}
		

	//Freezes the helicopters position in place
	//Temporary solution for wonky animations and issues with heli going through the ground after completed animation
	//Called from landing animation "Helicopter Land"
	void FreezePosition(){
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.constraints = RigidbodyConstraints.FreezePosition;
		Animator animator = GetComponent<Animator> ();
		animator.enabled = false;
	}
}
