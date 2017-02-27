using UnityEngine;
using System.Collections;

public class ClearArea : MonoBehaviour {

	private float timeSinceLastTrigger = 0f;
	private bool areaClear = false, helicopterCalled = false;
	public GameObject areaHighlight, landingSite;
	private float maxHeightDifference = 3f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!helicopterCalled) {
			timeSinceLastTrigger += Time.deltaTime;
			if (timeSinceLastTrigger > 1f && !areaClear && Time.realtimeSinceStartup > 10f) {
				if (CheckAreaFlatEnoughForLanding ()) {
					areaClear = true;
					SuitableArea ();
				}
			}
		}
	}

	//Throws raycast at set points within the landing area prefab in inspector
	//Returns true if the height difference is good enough
	//Returns false if terrain height difference is too large
	bool CheckAreaFlatEnoughForLanding(){
		float max = 0f, min = 0f;
		RaycastHit hitInfo;

		foreach (Transform point in transform) {
			Debug.DrawRay (point.position, new Vector3 (0, -50, 0), Color.magenta);
			if (Physics.Raycast (point.position, Vector3.down, out hitInfo, Mathf.Infinity, LayerMask.GetMask ("Solid Ground"))) {
				if (hitInfo.point.y > max || max == 0) {
					max = hitInfo.point.y;
				} else if (hitInfo.point.y < min || min == 0) {
					min = hitInfo.point.y;
				}
			}
		}
		return (max - min) < maxHeightDifference;
	}
		

	void OnTriggerStay (Collider collider){
		timeSinceLastTrigger = 0f;
		areaClear = false;
		if (!helicopterCalled) {
			NotSuitableArea ();
		}
	}

	void SuitableArea(){
		SendMessageUpwards ("OnInnerVoiceFindClearArea");	//InnerVoice.cs
	}

	void NotSuitableArea(){
		SendMessageUpwards ("OnInnerVoiceNotClearArea");	//InnerVoice.cs
	}

	void OnAttemptCall(){
		if (!areaClear) {
			StartCoroutine (areaBlink (Color.red));
		} else {
			StartCoroutine (areaBlink (Color.green));
			Instantiate (landingSite, transform.position, Quaternion.identity);
			helicopterCalled = true;
			SendMessageUpwards ("SpawnZombies");
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
