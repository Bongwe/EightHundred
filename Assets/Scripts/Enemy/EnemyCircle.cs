using UnityEngine;
using System.Collections;

public class EnemyCircle : MonoBehaviour, IEnemy
{
	private EnemyAI enemyAI;
	public PatrolPoint[] patrolPoints;

	public float moveSpeed = 2f;        // The speed the enemy moves at.
	public int HP = 10;                 // How many times the enemy can be hit before it dies.
	public Sprite Enemy;            // A sprite of the enemy when it's dead.
	public AudioClip[] deathClips;      // An array of audioclips that can play when the enemy dies.
	private SpriteRenderer ren;         // Reference to the sprite renderer.
	public bool dead = false;           // Whether or not the enemy is dead.
	public bool facingRight = false;
	public float jumpForce = 10f;
	public int damage;

	//Enemy AI stuff
	public float playerRadius = 10f;
	private Transform groundCheck;          // A position marking where to check if the player is grounded.
	private bool grounded = false;          // Whether or not the player is grounded.
	private PlayerControl playerCtrl;
	public float time;
	public float outerLoopTime;
	public int moveLeftOrRight;
	public int howFarToMove;
	public int howLongToWait;
	public bool followPlayer = true;

	private Vector2 spawnPosition;
	int randomDistance;
	Vector2 targetPosition;

	private ParticleSystem explosionFX;     // Reference to the particle system of the explosion effect.
	private int damageUnits = 10;
	public GameObject explosion;            // Prefab of explosion effect.
	public AudioClip boom;                  // Audioclip of explosion.
	public Vector2 pleryX;
	public Vector2 BoxX;
	private Animator anim;                  // Reference to the player's animator component.
	private Rigidbody2D rigidbody2D;
	private bool rBodyEnabled = true;
	private float currentTime;

	private Transform target;
	private int currentTargetIndex = 0;
	public float MinDistance = 1f;

	//particle systems
	private ParticleSystem deathRing;
	private bool shooting = false;
	public float lastShotTime;
	public float delayBeforeShooting = 2f;
	public AudioClip[] gunSound;
	public Rigidbody2D bullet;
	public float bulletSpeed;

	void Start()
	{
		enemyAI = ScriptableObject.CreateInstance<EnemyAI>();
		//Add enemy component
		//enemyAI = ScriptableObject.CreateInstance<EnemyAI> ();
		enemyAI.parentTransform = GetComponent<Transform>();
		enemyAI.Points = patrolPoints;
		enemyAI.parent = this;
		enemyAI.moveSpeed = moveSpeed;
	}

	void Awake()
	{
		// Setting up the references.
		ren = GetComponent<SpriteRenderer>();
		groundCheck = transform.Find("GroundCheck");
		playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		randomDistance = Random.Range(3, 10);
		targetPosition.x = spawnPosition.x - randomDistance;
		//explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
		//deathRing = GameObject.FindGameObjectWithTag("enemyDeathRing").GetComponent<ParticleSystem>();
		anim = GetComponent<Animator>();
		rigidbody2D = transform.GetComponent<Rigidbody2D>();
	}

	public void TakeDamage(int damage)
	{
		HP = HP - damage;

		if (HP <= 0)
		{
			playParticleSystem(deathRing);
			anim.SetBool("walking", false);
			anim.SetBool("dead", true);
			currentTime = time;
			Death();
		}
	}

	void playParticleSystem(ParticleSystem particleSystem)
	{
		particleSystem.transform.position = transform.position;
		particleSystem.Play();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Ground")
		{
			Jump();
		}
		else if (col.tag == "Player" && !dead)
		{
			if (col.gameObject.GetComponent<PlayerControl>() != null)
			{
				col.gameObject.GetComponent<PlayerControl>().playerHealth.TakeDamage(damage);
				Explode();
				Destroy(gameObject);
			}
		}
	}

	void FixedUpdate()
	{

		// add AI for shooting the player when the player is close to the house!

		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		time += Time.deltaTime;

		if (!dead)
		{
			if (this.GetComponent<Rigidbody2D>().velocity.x > 1 || this.GetComponent<Rigidbody2D>().velocity.x < 0)
				anim.SetBool("walking", true);
		}

		if (HP <= 0 && !dead)
		{
			playParticleSystem(deathRing);
			anim.SetBool("walking", false);
			anim.SetBool("dead", true);
			currentTime = time;
			Death();
		}

		if (!dead)
		{
			if (patrolPoints != null && transform != null)
			{

				if (patrolPoints.Length > 0 && target == null)
				{
					target = patrolPoints[currentTargetIndex].GetComponent<Transform>();
				}

				if (patrolPoints.Length > 0)
				{

					float step = moveSpeed * Time.deltaTime;
					transform.position = Vector2.MoveTowards(transform.position, target.position, step);

					if (Vector2.Distance(transform.position, target.position) < MinDistance)
					{ // 0.1f) {
					  //Debug.Log("End reached....");
					}
				}
			}

			if (transform != null && target != null && Vector2.Distance(transform.position, target.position) < MinDistance)
			{
				//if () {
				//The end target has been reached
				currentTargetIndex = (currentTargetIndex + 1) % patrolPoints.Length;
				//Debug.Log ("CurrentTargetIndex: " + currentTargetIndex);
				target = patrolPoints[currentTargetIndex].GetComponent<Transform>();
				//Debug.Log("End reached....");
			}
		}

		if(!shooting)
				Shoot();
		if (shooting == true && Time.realtimeSinceStartup > (lastShotTime + delayBeforeShooting))
		{
			shooting = false;
		}

		// Wait for 2 seconds
		float waitTime = 1.3f;
		Vector2 enemyPosition = this.transform.position;
		bool enemyOnGround = groundCheck.position.y <= enemyPosition.y;

		if (time > currentTime + waitTime && enemyOnGround && rBodyEnabled && dead)
		{
			Destroy(rigidbody2D);
			DisableColliders();
			rBodyEnabled = false;
		}

	}

	public void Explode()
	{
		// Set the explosion effect's position to the bomb's position and play the particle system.
		//explosionFX.transform.position = transform.position;
		//explosionFX.Play();

		// Instantiate the explosion prefab.
		Instantiate(explosion, transform.position, Quaternion.identity);

		// Play the explosion sound effect.
		AudioSource.PlayClipAtPoint(boom, transform.position);

		// Destroy the EnemyBomber.
		Destroy(gameObject);
	}

	void Death()
	{
		// Set dead to true.
		dead = true;

		// Play a random audioclip from the deathClips array.
		int i = Random.Range(0, deathClips.Length);
		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		float currentTime = Time.deltaTime;

		float yPlaneDistance = 250;
		float xPlaneDistance = 250;

		if (!facingRight)
			rigidbody2D.AddForce(new Vector2(xPlaneDistance, yPlaneDistance));
		else
			rigidbody2D.AddForce(new Vector2(-1 * xPlaneDistance, yPlaneDistance));
	}

	void DisableColliders()
	{
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach (Collider2D c in cols)
		{
			c.isTrigger = true;
		}
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

	public void Jump()
	{
		if (grounded)
		{
			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
		}
	}

	#region IEnemy implementation
	public void setPatrolPoints(PatrolPoint[] points)
	{
		Debug.Log("Setting a patrol point");
		this.patrolPoints = points;
	}

	public void faceLeft()
	{
		if (!facingRight)
		{
			Flip();
		}
	}

	public void faceRight()
	{
		if (facingRight)
		{
			Flip();
		}
	}

	public void Shoot()
	{
		shooting = true;
		lastShotTime = Time.realtimeSinceStartup;
		//play the audioclip.	
		//AudioSource.PlayClipAtPoint(gunSound[0], transform.position);

		Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(0, -1 * bulletSpeed);

	}

	#endregion
}
