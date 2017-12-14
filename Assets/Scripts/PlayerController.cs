using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float horSpeed;
	public float verSpeed;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public GameObject jumpParticle;
	public Vector3 angle;
	public Rigidbody2D rb;
	public Animator anim;
	public bool grounded;

	bool doubleJumped;
	bool running;
	void Start () {
		
		angle = transform.localRotation.eulerAngles;
	}

	void Update () {
		Move ();
	}
	void FixedUpdate(){
		Animate ();
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	void Move(){
		if(transform.localRotation.eulerAngles != angle){
			transform.localRotation=Quaternion.Euler(angle);
		}
		if (grounded) {
			doubleJumped = false;
		}
		Vector2 move = rb.velocity;
		float hor = Input.GetAxis ("Horizontal");
		move.x = hor * horSpeed;
		//Jump check
		if (Input.GetKeyDown ("up") && grounded) {
			move.y = verSpeed;
		} 
		//Doublejump check
		if (Input.GetKeyDown ("up") && !doubleJumped && !grounded) {
			move.y = verSpeed;
			doubleJumped = true;
			Instantiate (jumpParticle, transform.localPosition, transform.localRotation);
		} 
		//Grav flip check
		if (Input.GetKeyDown(KeyCode.S)&&grounded) {
			//Rotate player
			this.transform.Rotate (new Vector3 (0, 0, 180));
			//Flip player gravity
			rb.gravityScale*=-1;
			//Flip sprites
			angle=angle+new Vector3(0,0,180);
			this.gameObject.GetComponent<SpriteRenderer>().flipX=!this.gameObject.GetComponent<SpriteRenderer>().flipX;
		}
		//Run check
		if(Mathf.Abs(rb.velocity.x)>=5&&Input.GetKey(KeyCode.A)){
			running=true;
			move.x=move.x*2;
		}else{
			running=false;
		}

		rb.velocity = move;
		if (rb.velocity.x > 0) {
			transform.localScale = new Vector2 (1f, 1f);
		} else if (rb.velocity.x < 0) {
			transform.localScale = new Vector2 (-1f, 1f);
		}

		//Limit drop speed to 50
		if(rb.velocity.magnitude > 50f)
         {
            //rb.velocity = rb.velocity.normalized * maxSpeed;
         	rb.velocity = rb.velocity.normalized * 50f ;
         }

	}

	public void  Animate(){
		anim.SetFloat ("VerSpeed", rb.velocity.y);
		anim.SetFloat ("HorSpeed", Mathf.Abs(rb.velocity.x));
		anim.SetBool ("Grounded", grounded);
		anim.SetBool ("DoubleJump", doubleJumped);
		anim.SetBool ("Running", running);
		if (rb.velocity.x == 0) {
			anim.SetBool ("Idle", true);
		} else {
			anim.SetBool ("Idle", false);
		}
		

	}
}
