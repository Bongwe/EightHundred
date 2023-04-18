using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PistolPickup : MonoBehaviour
{
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.
	
	//private Animator anim;				// Reference to the animator component.
	//private bool landed = false;		// Whether or not the crate has landed yet.
	public Gun gun;
	public ShotGun shotGun;
	public Rigidbody2D pistolBullet;
	public Sprite PistolSprite;
	public ShotGunHolder shotGunHolder;
	public AudioClip pistolThemeSong;
	public ThemeSong themeSong;
	public PlayerControl playerControl;
	public Bullets BulletText;
	public BombText bombText;
	public string shotGunType;
	public Rigidbody2D shotGunShell;
	public Vector2 shotGunShellForceDirection;


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

			if (gun != null)
            {
				gun.hasGun = true;
				gun.weapon = pistolBullet;
				gun.ammunition = 30;
				gun.gameController.amoLeft = gun.ammunition;
				gun.GetComponent<SpriteRenderer>().sprite = PistolSprite;
			}
			if (shotGun != null)
            {
				shotGun.hasGun = true;
				shotGun.weapon = pistolBullet;
				shotGun.shotGunType = shotGunType;
				shotGun.ammunition = 30;
				shotGun.bulletText.amoLeft = shotGun.ammunition;
				shotGun.GetComponent<SpriteRenderer>().sprite = PistolSprite;
                //ShotGunHolder shotGunHolder1 = Instantiate(shotGunHolder);
				//shotGunHolder1.transform.position = shotGun.transform.position;
				//shotGunHolder1.transform.parent = shotGun.transform;
			}

			if (shotGunShell != null)
            {
				shotGun.shotGunShell = shotGunShell;
				shotGun.shotGunShellForceDirection = shotGunShellForceDirection;
			}
			
			themeSong.playThemeSong(pistolThemeSong);
			BulletText.amoLeft  = 30;
			BulletText.UpdateText();
			
			Destroy(gameObject);
		}
		
	}
}
