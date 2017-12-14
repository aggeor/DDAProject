using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public float currentDistance=0.25f;
	float initialDistance;
	float start;
	float target;
	public float movSpeed=0.5f;
	bool direction;
	GameManager gm;
	void Start () {
		initialDistance = currentDistance;
		direction = true;
		start = transform.position.y;
		target = transform.position.y + currentDistance;
		gm = FindObjectOfType<GameManager> ();
	}
	void Update () {
		start = transform.position.y;
		if (direction == true) {
			currentDistance = target - start;
			transform.Translate (Vector3.up * Time.deltaTime * movSpeed, Space.World);
		} else {
			currentDistance = start - target;
			transform.Translate (Vector3.down * Time.deltaTime * movSpeed, Space.World);
		}
		if (currentDistance <= 0) {
			direction = !direction;
			if (direction == false) {
				target = target - initialDistance;
			} else {
				target = target + initialDistance;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Player")) {
			gm.coinCount += 1;
			if (gm.mapLayout == "VeryEasy") {
				gm.timeLeft += 1;
			}else if (gm.mapLayout == "Easy") {
				gm.timeLeft += 2;
			}else if (gm.mapLayout == "Medium") {
				gm.timeLeft += 3;
			}else if (gm.mapLayout == "Hard") {
				gm.timeLeft += 4;
			}else if (gm.mapLayout == "VeryHard") {
				gm.timeLeft += 5;
			}
			this.gameObject.SetActive(false);
			Destroy (this);
		}

	}
}
