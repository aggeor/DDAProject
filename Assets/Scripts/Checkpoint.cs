using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public LevelManager levelManager;
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		levelManager.currentCheckpoint = gameObject;
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.name == "Player") {
			levelManager.currentCheckpoint = gameObject;
		}
	}
}
