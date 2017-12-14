using UnityEngine;
using System.Collections;

public class EnemyDropper_DeathZone : MonoBehaviour {
	public GameObject deathParticle;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Enemy")) {
			Destroy (transform.parent.gameObject);
			Instantiate (deathParticle, transform.position, transform.rotation);
		}
	}
}
