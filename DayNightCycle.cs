using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
	[Tooltip ("Number of minutes per second that pass")]
	public float timeScale;	//minutes per second

	void Start(){
		//Starts the game at Dusk, Allows it to be "sunny" in the inspector
		transform.localEulerAngles = new Vector3(180, 0,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.right * Time.deltaTime / 360 * timeScale);
	}
}
