using UnityEngine;
using System.Collections;

public class KillEnemy : MonoBehaviour {

	GameObject enemy;
	public GameObject deathParticleFlyer;
	public GameObject deathParticleJumper;
	public GameObject deathParticleDropper;

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag("Enemy")) {
			
			enemy = other.gameObject;
			Kill (enemy);
		}

	}
	public void Kill(GameObject enemy){
		
		if (enemy.layer == 28) {
			Instantiate (deathParticleDropper, enemy.transform.localPosition, enemy.transform.localRotation);
		} else if (enemy.layer == 29) {
			Instantiate (deathParticleFlyer, enemy.transform.localPosition, enemy.transform.localRotation);
		} else if (enemy.layer == 30) {
			Instantiate (deathParticleJumper, enemy.transform.localPosition, enemy.transform.localRotation);
		} else if (enemy.layer == 31) {
			Instantiate (deathParticleDropper, enemy.transform.localPosition, enemy.transform.localRotation);
		}

		enemy.SetActive(false);

	}
}
