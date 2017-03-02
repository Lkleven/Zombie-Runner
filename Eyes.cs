using UnityEngine;
using System.Collections;

public class Eyes : MonoBehaviour {
	private Camera eyes;
	private float defaultFOV, maxZoom;

	// Use this for initialization
	void Start () {
		eyes = GetComponent<Camera> ();
		defaultFOV = eyes.fieldOfView;
		maxZoom = defaultFOV * 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Zoom")) {
			if (eyes.fieldOfView <= maxZoom) {
				eyes.fieldOfView = maxZoom;
			} else {
				eyes.fieldOfView--;
			}
		}else if (! Input.GetButton("Zoom") && eyes.fieldOfView < defaultFOV) {
				eyes.fieldOfView++;
		} else {
			//eyes.fieldOfView = defaultFOV;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.name.Equals("mesh_L_Hand") || col.name.Equals("mesh_R_Hand") ){
			Debug.Log ("Player Hit");
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, 10f);
	}
}
