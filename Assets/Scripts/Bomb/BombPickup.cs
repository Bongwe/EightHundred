using UnityEngine;
using System.Collections;

public class BombPickup : MonoBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.

	//private Animator anim;				// Reference to the animator component.
	//private bool landed = false;		// Whether or not the crate has landed yet.

	public int numOFBombs = 5;

	void Awake()
	{
		// Setting up the reference.
		//anim = transform.root.GetComponent<Animator>();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player")
		{
			// Increase the number of bombs the player has.
			other.GetComponent<LayBombs>().bombCount += numOFBombs;
			other.GetComponent<LayBombs>().UpdateBombText();

			// ... play the pickup sound effect.
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);

			// Destroy the crate.
			Destroy(gameObject);
		}

	}
}
