using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public float radius;

	//Makes it easier to place spawnpoints in Unity by drawing a visual sphere
	public void OnDrawGizmos(){
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
