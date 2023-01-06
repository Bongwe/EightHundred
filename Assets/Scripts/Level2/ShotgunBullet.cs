using UnityEngine;
using System.Collections;

public class ShotgunBullet : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public int damage = 1;
	private Animator anim;					// Reference to the shack's animator component.
	
	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject,1);
	}
	
	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		Instantiate(explosion, transform.position, randomRotation);
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
		else if(col.tag == "metroTrain")
		{	
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<metroTrain>().pistolDamage(damage);

			if(!col.gameObject.GetComponent<metroTrain>().trainDestroyed)
			{
				// Call the explosion instantiation.
				OnExplode();
				
				// Destroy the rocket.
				Destroy (gameObject);
			}
			
		}
	}
}
