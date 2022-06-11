using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PistolPickup : MonoBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.
	
	//private Animator anim;				// Reference to the animator component.
	//private bool landed = false;		// Whether or not the crate has landed yet.
	public Gun gun;
	public Rigidbody2D pistolBullet;
	public Sprite PistolSprite;
	public AudioClip pistolThemeSong;
	public ThemeSong themeSong;
	public PlayerControl playerControl;
	public Bullets BulletText;
	public BombText bombText;
	
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
			// ... play the pickup sound effect.
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			
			// Increase the number of bombs the player has.
			other.GetComponent<LayBombs>().bombCount += 5;
			other.GetComponent<LayBombs>().UpdateBombText();

			gun.hasGun = true;
			gun.weapon = pistolBullet;
			gun.ammunition = 30;
			gun.gameController.amoLeft = gun.ammunition;
			gun.GetComponent<SpriteRenderer>().sprite = PistolSprite;
			themeSong.playThemeSong(pistolThemeSong);
			BulletText.amoLeft  = 30;
			BulletText.UpdateText();
			
			Destroy(gameObject);
		}
		
	}
}
