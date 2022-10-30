using UnityEngine;
using System.Collections;

public class PistolBullet : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public int damage = 2;
	private Animator anim;					// Reference to the shack's animator component.

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject,0.7f);
	}
	
	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		//Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Enemy" && !col.gameObject.GetComponent<Enemy>().dead)
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
			
			// Call the explosion instantiation.
			OnExplode();

			// Destroy the rocket.
			Destroy (gameObject);
		}
		if(col.tag == "EnemyBomber" && !col.gameObject.GetComponent<EnemyBomber>().dead)
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
}
