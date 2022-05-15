using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IEnemy
{
	private EnemyAI enemyAI;
	public PatrolPoint[] patrolPoints;
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP;					// How many times the enemy can be hit before it dies.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	public bool dead = false;			// Whether or not the enemy is dead.
	public bool facingRight = false;
	public float jumpForce;
	
	//Enemy AI stuff
	public float playerRadius = 10f;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	public bool grounded = false;			// Whether or not the player is grounded.
	private PlayerControl playerCtrl;
	public float time;
	public int moveLeftOrRight;
	public int howFarToMove;
	public int howLongToWait;
	public bool followPlayer = true;
	private Animator anim;					// Reference to the player's animator component.
	public float enemyVelocty;
	public GameObject gun;
	private Rigidbody2D rigidbody2D;
	private float currentTime;
	private bool rBodyEnabled = true;

	//particle systems
	private ParticleSystem deathRing;

	void Start() {
		enemyAI = ScriptableObject.CreateInstance<EnemyAI> ();
		//Add enemy component
		//enemyAI = ScriptableObject.CreateInstance<EnemyAI> ();
		enemyAI.parentTransform = GetComponent<Transform> ();
		enemyAI.Points = patrolPoints;
		enemyAI.parent = this;
		enemyAI.moveSpeed = moveSpeed;
	}

	void Awake()
	{
		// Setting up the references.
		ren = GetComponent<SpriteRenderer>();
		//ren = transform.Find("body").GetComponent<SpriteRenderer>();
		//frontCheck = transform.Find("frontCheck").transform;
		rigidbody2D = transform.GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("groundCheck");
		playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		deathRing = GameObject.FindGameObjectWithTag("enemyDeathRing").GetComponent<ParticleSystem>();
		//spawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner>();;
		//spawnPosition = GameObject.FindGameObjectWithTag("Spawner").transform.position;
		//targetPosition.x = spawnPosition.x - randomDistance;
		anim = GetComponent<Animator>();

	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "ground" && !dead)
			Jump();
	}

	void FixedUpdate() {

		if (!dead) {
			if (this.GetComponent<Rigidbody2D>().velocity.x > 1 || this.GetComponent<Rigidbody2D>().velocity.x < 0)
				anim.SetBool ("walking", true);
		}

		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground")); 
		time += Time.deltaTime;	

		if (HP <= 0 && !dead) {
			playParticleSystem(deathRing);
			anim.SetBool ("walking", false);
			anim.SetBool("dead", true);
			currentTime = time;
			Death ();
		}

		// Wait for 2 seconds
		float waitTime = 1.3f;
		Vector2 enemyPosition = this.transform.position;
		bool enemyOnGround = groundCheck.position.y <= enemyPosition.y;

		if( time > currentTime + waitTime && enemyOnGround && rBodyEnabled && dead )
		{
		   	Destroy (rigidbody2D);
			DisableColliders();
			rBodyEnabled = false;
		}

		if(!dead)
			enemyAI.PublicFixedUpdate ();
	}

	public void Hurt(){
		HP--;
	}

	public void TakeDamage(int damage){
		HP = HP - damage;
	}

	void playParticleSystem(ParticleSystem particleSystem)
	{
		particleSystem.transform.position = transform.position;
		particleSystem.Play();
	}

	void DisableColliders()
	{
		Collider2D[] cols = this.GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}
	}

	void Death()
	{
		// Set dead to true.
		dead = true;

		// Play a random audioclip from the deathClips array.
		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint (deathClips [i], transform.position);

		//descrease number of enemies
		//spawner.enemyCount--;
		float currentTime = Time.deltaTime;

		float yPlaneDistance = 250;
		float xPlaneDistance = 250;

		if(!facingRight)
			rigidbody2D.AddForce(new Vector2(xPlaneDistance,yPlaneDistance));
		else
			rigidbody2D.AddForce(new Vector2( -1*xPlaneDistance,yPlaneDistance));

		gun.SetActive (false);
		//	Destroy (gameObject);
	}

	public void setDeadToFalse()
	{
		//anim.SetBool("dead", false);
	}
	
	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
	public bool jumping = false;

	public void Jump()
	{
		if (grounded) {
			// Add a vertical force to the player.
			jumping = true;
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
		}
	}

	#region IEnemy implementation
	public void setPatrolPoints(PatrolPoint[] points) {
		this.patrolPoints = points;
	}

	public void faceLeft ()
	{
		if (!facingRight) {
			Flip ();
		}
	}

	public void faceRight ()
	{
		if (facingRight) {
			Flip();
		}
	}

	#endregion
}
