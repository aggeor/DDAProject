using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	GameManager gm;
	public LevelManager levelManager;
	int killCount;
	public KillEnemy killEnemy;

	void Start () {
		gm = FindObjectOfType<GameManager> ();
		levelManager = FindObjectOfType<LevelManager> ();
		killCount = 0;
	}

	void Update () {
		if (killCount >= 5) {
			Invoke("DisableSelf", 1f);
		}

	}

	void DisableSelf(){
		this.gameObject.SetActive(false);
	}
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.CompareTag("Player")) {
			killCount += 1;
			gm.deathCount += 1;
			levelManager.RespawnPlayer ();
			levelManager.RespawnEnemies ();

			//Reset player gravity
			if (other.GetComponent<Rigidbody2D> ().gravityScale <= 0) {
				other.transform.Rotate (new Vector3 (0, 0, 180));
				other.GetComponent<Rigidbody2D> ().gravityScale *= -1;
				other.gameObject.GetComponent<SpriteRenderer> ().flipX = !other.gameObject.GetComponent<SpriteRenderer> ().flipX;
			}
		}

	}

}
