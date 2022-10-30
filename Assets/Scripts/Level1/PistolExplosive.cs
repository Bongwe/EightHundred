using UnityEngine;
using System.Collections;

public class PistolExplosive : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.

	public float explosionRadius = 5f;			// Radius within which enemies are killed.
	public float explosionForce = 50f;			// Force that enemies are thrown from the blast.
	public AudioClip boom;					// Audioclip of explosion.
	private ParticleSystem explosionFX;		// Reference to the particle system of the explosion effect.
	public int damage = 4;

	void Start () 
	{
		// Setting up references.
		//explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();

		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject,0.7f);
	}
	
	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		//Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the Bullet is with the random rotation.
		//Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Enemy")
		{
			/// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
			
			// Call the explosion instantiation.
			OnExplode();
			
			// Destroy the rocket.
			Destroy (gameObject);
		}
		if(col.tag == "EnemyBomber")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<EnemyBomber>().TakeDamage(damage);
			
			// Call the explosion instantiation.
			OnExplode();
			
			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if it hits a Shack...
		else if(col.tag == "Shack")
		{	
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Shack>().pistolDamage(damage);
			
			// Call the explosion instantiation.
			//Do not destroy the bullet if the shack has not been destroyed
			if(!col.gameObject.GetComponent<Shack>().shackIsDestroyed)
			{
				OnExplode();
				// Destroy the bullets	.
				Destroy (gameObject);
			}
		}
		else if(col.tag == "door")
		{	
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Level3Door>().pistolDamage(damage);
			
			// Call the explosion instantiation.
			//Do not destroy the bullet if the shack has not been destroyed
			if(!col.gameObject.GetComponent<Level3Door>().doorIsDestroyed)
			{
				OnExplode();
				// Destroy the bullets	.
				Destroy (gameObject);
			}
			
		}
	}

	public void Explode()
	{	
		// Find all the colliders on the Enemies layer within the explosionRadius.
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, 1 << LayerMask.NameToLayer("Enemies"));
		
		// For each collider...
		foreach(Collider2D en in enemies)
		{
			// Check if it has a rigidbody (since there is only one per enemy, on the parent).
			Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
			if(rb != null && rb.tag == "Enemy")
			{
				// Find the Enemy script and set the enemy's health to zero.
				rb.gameObject.GetComponent<Enemy>().HP = 0;
				
				// Find a vector from the bomb to the enemy.
				Vector3 deltaPos = rb.transform.position - transform.position;
				
				// Apply a force in this direction with a magnitude of bombForce.
				Vector3 force = deltaPos.normalized * explosionForce;
				rb.AddForce(force);
			}
		}
		
		// Set the explosion effect's position to the bomb's position and play the particle system.
		explosionFX.transform.position = transform.position;
		explosionFX.Play();
		
		// Instantiate the explosion prefab.
		Instantiate(explosion,transform.position, Quaternion.identity);
		
		// Play the explosion sound effect.
		AudioSource.PlayClipAtPoint(boom, transform.position);
		
		// Destroy the bomb.
		Destroy (gameObject);
	}
}
