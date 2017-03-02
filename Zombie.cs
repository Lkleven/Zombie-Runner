using UnityEngine;

public class Zombie : MonoBehaviour {
	private Animator animator;
	private Transform target;
	float distanceToTarget;
	void Start(){
		animator = GetComponent<Animator> ();
		target = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl> ().target;
	}

	void Update(){
		distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

		//Zombie attacks player if close enough
		if (distanceToTarget < 2f) {
			animator.SetTrigger ("ZombieAttack");
		}
	}
}
