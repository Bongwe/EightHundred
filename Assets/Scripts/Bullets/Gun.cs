using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D weapon;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
	public float cannonBallSpeed = 10f;
	public int ammunition  = 0;
	public AudioClip[] gunSound;				// Array of clips for when the player shoots.
	public AudioClip themeSong;

	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	//private Animator anim;					// Reference to the Animator component.

	public Bullets gameController;
	public bool hasGun = false;
	private int spwanDistance  = 1;

	void Awake()
	{
		// Setting up the references.
		//anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
		gameController.amoLeft = ammunition;
	}

	void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetButtonDown("Fire1") && hasGun && ammunition > 0)
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			//anim.SetTrigger("Shoot");
			//GetComponent<AudioSource>().Play();
			AudioSource.PlayClipAtPoint(gunSound[0], transform.position);
			ammunition--;
			gameController.amoLeft = ammunition;

			Vector2 bulletSpawnPosition = transform.position;


			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				Rigidbody2D bulletInstance;
				
				if(weapon.tag == "Bomb")
				{
					bulletSpawnPosition.x = bulletSpawnPosition.x + spwanDistance;
					
					// Otherwise instantiate the rocket facing left and set it's velocity to the left.
					bulletInstance = Instantiate(weapon, bulletSpawnPosition, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(cannonBallSpeed, 0);
				}
				else
				{
					// Otherwise instantiate the rocket facing left and set it's velocity to the left.
					bulletInstance = Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(speed, 0);
				}

			}
			// If the player is facing left...
			else
			{
				Rigidbody2D bulletInstance;

				if(weapon.tag == "Bomb")
				{
					bulletSpawnPosition.x = bulletSpawnPosition.x - spwanDistance;
					
					// Otherwise instantiate the rocket facing left and set it's velocity to the left.
					bulletInstance = Instantiate(weapon, bulletSpawnPosition, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(cannonBallSpeed * -1, 0);

				}
				else
				{
					// Otherwise instantiate the rocket facing left and set it's velocity to the left.
					bulletInstance = Instantiate(weapon, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
					bulletInstance.velocity = new Vector2(-speed, 0);
				}
			}
		}
		else if (Input.GetButtonDown("Fire1") && hasGun && ammunition == 0)
			AudioSource.PlayClipAtPoint(gunSound[1], transform.position);

	}


}
