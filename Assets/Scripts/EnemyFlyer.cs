using UnityEngine;
using System.Collections;

public class EnemyFlyer : MonoBehaviour {
	

	public float moveSpeed;
	public float playerRange;

	public LayerMask playerLayer;

	private PlayerController thePlayer;
	private bool playerInRange;
	public Vector3 startPos;

	Rigidbody2D rb;
	void Start () {
		rb=GetComponent<Rigidbody2D>();
		GameManager gm = FindObjectOfType<GameManager> ();
		moveSpeed = gm.flyerMoveSpeed;
		playerRange = gm.flyerPlayerRange;
		thePlayer = FindObjectOfType<PlayerController> ();
		startPos = transform.position;
	}
	void Update () {
		if (rb.velocity.y == 20) {
			rb.isKinematic=false;
		}
		if (rb.velocity.y < 0) {
			rb.isKinematic = true;
		}

		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);
		if (playerInRange) {
			transform.position = Vector2.MoveTowards (transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);

			if (thePlayer.transform.position.x > transform.position.x) {
				transform.localScale = new Vector2 (1f, 1f);
			} else if (thePlayer.transform.position.x < transform.position.x) {
				transform.localScale = new Vector2 (-1f, 1f);
			}
		} else {
			transform.position = Vector2.MoveTowards (transform.position, startPos, moveSpeed * Time.deltaTime);
			if (transform.position.x > startPos.x) {
				transform.localScale = new Vector2 (-1f, 1f);
			}else if (transform.position.x < startPos.x) {
				transform.localScale = new Vector2 (1f, 1f);
			}
		}
	}
}
