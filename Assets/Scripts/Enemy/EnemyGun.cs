using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
	
	//public int ammunition  = 20;
	public AudioClip[] gunSound;				// Array of clips for when the player shoots.
	
	private Enemy enemyCtrl;		// Reference to the PlayerControl script.
	//private Animator anim;		// Reference to the Animator component.

	public float time ;

	private PlayerControl playerCtrl;
	public float playerRadius = 10f;
	private bool shooting = false;
	public float lastShotTime;
	public bool facingRight = true;			// For determining which way the player is currently facing.
	public float delayBeforeShooting;

	void Start() {
		lastShotTime = Time.realtimeSinceStartup;
	}

	void Awake()
	{
		// Setting up the references.
		//anim = transform.root.gameObject.GetComponent<Animator>();
		enemyCtrl = transform.parent.GetComponent<Enemy>();
		playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}																

	void FixedUpdate () 
	{
		// Find all the colliders on the Player layer within the enemy Radius.
		Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, playerRadius, 1 << LayerMask.NameToLayer("Player"));

		foreach (Collider2D en in objectsInRadius)
		{
			// Check if it has a rigidbody (since there is only one per enemy, on the parent).
			
			if (en.GetComponent<Rigidbody2D>() != null && en.GetComponent<Rigidbody2D>().tag == "Player") {
				//Debug.Log ("Player in range");
				if(!shooting)
					Shoot();
				if (shooting == true && Time.realtimeSinceStartup > (lastShotTime + delayBeforeShooting)) {
					shooting = false;
				}
			}
		}
	}
	
	public void Shoot()
	{
		float shoot = Random.Range(0, 5);
		shooting = true;
		lastShotTime = Time.realtimeSinceStartup;
		//play the audioclip.	
		AudioSource.PlayClipAtPoint(gunSound[0], transform.position);

		if(!enemyCtrl.facingRight){
			// ... instantiate the rocket facing right and set it's velocity to the right. 
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(speed, 0);
		}
		else{
			// Otherwise instantiate the rocket facing left and set it's velocity to the left.
			Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(speed * -1, 0);
		}

	}
}
