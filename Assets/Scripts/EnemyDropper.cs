using UnityEngine;
using System.Collections;

public class EnemyDropper : MonoBehaviour {

	public float dropSpeed;
	public Vector3 startPos;
	public Rigidbody2D rb;
	void Start () {
		startPos = transform.position;
		GameManager gm = FindObjectOfType<GameManager> ();
		dropSpeed = gm.dropSpeed;
		rb = transform.GetComponent<Rigidbody2D> ();
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Player")) {
			rb.gravityScale = dropSpeed;
			rb.isKinematic = false;
		}

	}
}
