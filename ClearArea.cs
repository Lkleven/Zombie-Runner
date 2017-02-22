using UnityEngine;
using System.Collections;

public class ClearArea : MonoBehaviour {

	private float timeSinceLastTrigger = 0f;
	private bool areaClear = false;
	public GameObject areaHighlight;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastTrigger += Time.deltaTime;
		if (timeSinceLastTrigger > 1f && !areaClear && Time.realtimeSinceStartup > 10f) {
			areaClear = true;
			SuitableArea ();

		}
	}

	void OnTriggerStay (Collider collider){
		timeSinceLastTrigger = 0f;
		areaClear = false;
		NotSuitableArea ();
	}

	void SuitableArea(){
		SendMessageUpwards ("OnFindClearArea");	//Player.cs
		//areaHighlight.SetActive (true);
		//areaHighlight.GetComponent<Projector> ().material.color = Color.green;
	}

	void NotSuitableArea(){
		SendMessageUpwards ("OnNotClearArea");	//Player.cs
	}

	void OnAttemptCall(){
		if (!areaClear) {
			StartCoroutine (areaBlink (Color.red));
		} else {
			StartCoroutine (areaBlink (Color.green));
		}
	}


	//Creates an area of light that blinks over an area.
	IEnumerator areaBlink(Color color){
		Vector3 temp = transform.position;
		temp.y = temp.y + 33;
		GameObject areaLight = Instantiate (areaHighlight, temp, Quaternion.Euler(90,0,0)) as GameObject;

		areaLight.GetComponent<Projector> ().material.color = color;
		for(int i = 0; i < 3; i++){
			areaLight.SetActive (true);
			yield return new WaitForSeconds (0.5f);
			areaLight.SetActive (false);
			yield return new WaitForSeconds (0.5f);
		}
		Destroy (areaLight);
	}
}
