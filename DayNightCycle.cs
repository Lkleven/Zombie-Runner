using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
	[Tooltip ("Number of minutes per second that pass")]
	public float timeScale;	//minutes per second
	
	// Update is called once per frame
	void Update () {
		float angleThisFrame = Time.deltaTime / 360 * timeScale;
		transform.RotateAround (transform.position, Vector3.forward, angleThisFrame);
	}
}
