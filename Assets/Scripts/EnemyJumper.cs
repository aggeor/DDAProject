using UnityEngine;
using System.Collections;

public class EnemyJumper : MonoBehaviour {
	public Vector3 startPos;
	public float jumpSpeed;
	public float horSpeed;
	public float groundCheckRadius;
	public float playerRange;

	public Transform groundCheck;

	public LayerMask whatIsGround;
	public LayerMask playerLayer;


	private PlayerController thePlayer;
	Rigidbody2D rb;
	private bool playerInRange;
	private bool grounded;

	private Animator anim;

	void Start () {
		startPos = transform.position;
		thePlayer = FindObjectOfType<PlayerController> ();
		GameManager gm = FindObjectOfType<GameManager> ();
		jumpSpeed = gm.jumperJumpSpeed;
		horSpeed = gm.jumperHorSpeed;
		playerRange = gm.jumperPlayerRange;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);
		if (playerInRange) {
			
			Movement ();
		} else {
			StopMoving ();
		}
	}
	void FixedUpdate(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		anim.SetBool ("Grounded",grounded);

	}
	void Movement(){
		Vector2 move = rb.velocity;
		if (Input.GetKeyDown ("up") && grounded&&thePlayer.grounded) {
			
			move.y = jumpSpeed;
		} 
		if (!grounded) {
			if (thePlayer.transform.position.x > transform.position.x) {
				move.x = horSpeed;
			} else {
				move.x = -horSpeed;
			}
		} else {
			move.x = 0;
		}

		rb.velocity = move;
		if (thePlayer.transform.position.x>transform.position.x) {
			transform.localScale = new Vector2 (1f, 1f);
		} else if (thePlayer.transform.position.x<transform.position.x) {
			transform.localScale = new Vector2 (-1f, 1f);
		}
	}
	void StopMoving(){
		Vector2 move = rb.velocity;
		move.x = 0;
		rb.velocity = move;
	}

}
