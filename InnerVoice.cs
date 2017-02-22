using UnityEngine;
using System.Collections;

public class InnerVoice : MonoBehaviour {
	private AudioSource audioSource; //player inner voice
	private bool areaSuitedForLanding = false;
	private bool landingAreaSet = false;
	public AudioClip goodLandingArea, whatHappened, areaNotSuited;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = whatHappened;
		audioSource.Play ();
	}

	void OnInnerVoiceFindClearArea(){
		audioSource.clip = goodLandingArea;
		audioSource.Play ();
		areaSuitedForLanding = true;
		//Invoke ("CallHeli", goodLandingArea.length + 1f);
	}

	void OnInnerVoiceNotClearArea(){
		areaSuitedForLanding = false;
	}

	void CallHeli(){
		if (areaSuitedForLanding) {
			SendMessageUpwards ("OnMakeInitialHeliCall");	//RadioSystem.cs
			SendMessageUpwards ("OnHeliCalled");			//Player.cs;
		} else {
			audioSource.clip = areaNotSuited;
			audioSource.Play ();
		}
	}
}
