using UnityEngine;
using System.Collections;

public class EnemyWalker : MonoBehaviour {
	public Vector3 startPos;
	public float currentDistance=2f;
	float initialDistance;
	float start;
	float target;
	public float movSpeed=0.5f;
	bool direction;
	GameManager gm;
	void Start () {
		startPos = transform.position;
		initialDistance = currentDistance;
		direction = true;
		start = transform.position.x;
		target = transform.position.x + currentDistance;
		gm = FindObjectOfType<GameManager> ();
		movSpeed = gm.walkSpeed;
	}
	void Update () {
		start = transform.position.x;
		if (direction == true) {
			currentDistance = target - start;
			transform.Translate (Vector3.right * Time.deltaTime * movSpeed, Space.World);

		} else {
			currentDistance = start - target;
			transform.Translate (Vector3.left * Time.deltaTime * movSpeed, Space.World);

		}
		if (currentDistance <= 0) {
			direction = !direction;
			if (direction == false) {
				target = target - initialDistance;
				transform.localScale = new Vector2 (-1f, 1f);

			} else {
				target = target + initialDistance;
				transform.localScale = new Vector2 (1f, 1f);

			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.layer==32){
			direction = !direction;
			if (direction == false) {
				target = target - initialDistance;
			} else {
				target = target + initialDistance;
			}
		} 


	}
}
