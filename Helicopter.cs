using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {
	public GameObject mainRotor;

	private Rigidbody rigidBody;
	private Vector3 startPos, destination;
	private float startTime;
	private bool dispatched = false, landing = false;
	private Animator animator;
	private GameObject helicopter;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		rigidBody = GetComponent<Rigidbody> ();
		helicopter = transform.GetChild (0).gameObject;
		animator = helicopter.GetComponent<Animator> ();
			//GetComponent<Animator> ();
	
	}

	void Update(){
		mainRotor.transform.Rotate (Vector3.forward * Time.deltaTime * 3600);	//degrees per second

		if (dispatched && !landing) {
			transform.position = Vector3.Lerp (startPos, destination, (Time.time - startTime) / 15);
		}

		if (transform.position == destination && !landing) {
			landing = true;
			Debug.Log ("LAND");
			//animator.SetTrigger ("HelicopterFlareLanding");
			animator.Play("Helicopter Flare Land");
		}

		if (landing && !animator.GetCurrentAnimatorStateInfo (0).IsName ("Helicopter Flare Land")) {
			Vector3 stayHere = helicopter.transform.position;
			Debug.Log ("STAY");
			helicopter.transform.position = stayHere;
			helicopter.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ;
		}
	}

	void OnDispatchHelicopter(){
		dispatched = true;
		startTime = Time.time;

		destination = GameObject.FindGameObjectWithTag ("LandingArea").transform.position;
		float temp = destination.z;
		temp-= 40;
		destination.z = temp;
		Debug.Log (destination);
		//rigidBody.velocity = new Vector3 (0, 0, 50f);
	}

	void OnCollisionEnter(Collision col){
		Debug.Log (col);
	}

	private IEnumerator WaitForAnimation (Animation animation){
		do{ yield return null;} while (animation.isPlaying);}
}
