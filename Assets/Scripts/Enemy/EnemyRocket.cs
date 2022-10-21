using UnityEngine;
using System.Collections;

public class EnemyRocket : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public int damage;

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
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
		/*if(col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Hurt();
			
			// Call the explosion instantiation.
			OnExplode();
			
			// Destroy the rocket.
			Destroy (gameObject);
		}*/
		// Otherwise if it hits a Shack...
		/*else if(col.tag == "Shack")
		{	
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Shack>().Hurt();
			
			// ... find the Shack script and call the Explode function.
			col.gameObject.GetComponent<Shack>().Explode();
			
			// Destroy the Shack.
			Destroy (col.transform.root.gameObject);
				// Call the explosion instantiation.
				OnExplode();
			
			
			// Destroy the rocket.
			Destroy (gameObject);
		}*/
		if (col.tag == "Player")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<PlayerControl>().playerHealth.TakeDamage(damage);

			float yPlaneDistance = 250;
			float xPlaneDistance = 250;
			bool facingRight = col.gameObject.GetComponent<PlayerControl>().facingRight;

			if (facingRight != null && !facingRight)
				col.gameObject.GetComponent <Rigidbody2D>().AddForce(new Vector2(xPlaneDistance, yPlaneDistance));
			else
				col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xPlaneDistance, yPlaneDistance));

			// Instantiate the explosion and destroy the rocket.
			//OnExplode();
			Destroy (gameObject);
		}
		// Otherwise if it hits a ground...
		else if(col.tag == "Ground")
		{	
			// Call the explosion instantiation.
			OnExplode();
			
			
			// Destroy the rocket.
			Destroy (gameObject);
		}
	}
}
