using UnityEngine;
using System.Collections;

public class LifePack : MonoBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.
	public int healthIncrease = 100;
	public PlayerHealth playerHealth;

	void Awake()
	{	

	}
	
	
	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player")
		{
			// ... play the pickup sound effect.
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			
			playerHealth.health = healthIncrease;
			playerHealth.UpDateHealth();
			
			// Destroy the crate.
			Destroy(gameObject);
		}
		
	}
}
