using UnityEngine;
using System.Collections;

public class BulletPickup : MonoBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.
	
	//private Animator anim;				// Reference to the animator component.
	//private bool landed = false;		// Whether or not the crate has landed yet.
	
	public int numOfBullets = 30;
	public Bullets bullets;
	public Gun gun;
	
	void Awake()
	{	
		// Setting up the reference.
		//anim = transform.root.GetComponent<Animator>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		/*if(other.tag == "Player")
		{
			// ... play the pickup sound effect.
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			
			// Increase the number of bullets the player has.
			gun.ammunition += numOfBullets; 
			bullets.amoLeft += numOfBullets;
			
			// Destroy the crate.
			Destroy(gameObject);
		}*/
		
	}
}
