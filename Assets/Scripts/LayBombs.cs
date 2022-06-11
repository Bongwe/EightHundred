using UnityEngine;
using System.Collections;

public class LayBombs : MonoBehaviour
{
	[HideInInspector]
	public bool bombLaid = false;		// Whether or not a bomb has currently been laid.
	public int bombCount = 0;			// How many bombs the player has.
	public AudioClip bombsAway;			// Sound for when the player lays a bomb.
	public Rigidbody2D bomb;				// Prefab of the bomb.
	public BombText bombText;

	//private GUITexture bombHUD;			// Heads up display of whether the player has a bomb or not.
	public PlayerControl playerCtrl;       // Reference to the PlayerControl script.
	private bool isBombing = false;

	void Awake ()
	{
		// Setting up the reference.
		//bombHUD = GameObject.Find("ui_bombHUD").GetComponent<GUITexture>();
		//playerCtrl = transform.root.GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
		if(isBombing && !bombLaid && bombCount > 0)
		{
			// Decrement the number of bombs.
			bombCount--;
			bombText.bombsLeft = bombCount;
			bombText.UpdateText();

			// Set bombLaid to true.
			//bombLaid = true;

			// Play the bomb laying sound.
			AudioSource.PlayClipAtPoint(bombsAway,transform.position);

			Rigidbody2D bombInstance;
			float sideDisatnce = 10;
			float upDistance = 3;
			float bombDistance = 0.9f;
			Vector2 bombPosition;

			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				// Instantiate the bomb prefab.
				bombPosition = new Vector2(transform.position.x + bombDistance,transform.position.y);
				bombInstance = Instantiate(bomb, bombPosition, transform.rotation) as Rigidbody2D;
				bombInstance.velocity = new Vector2(sideDisatnce,upDistance);
				
			}
			// If the player is facing left...
			else
			{
				// Instantiate the bomb prefab.
				bombPosition = new Vector2(transform.position.x + bombDistance * -1,transform.position.y);
				bombInstance = Instantiate(bomb, bombPosition, transform.rotation) as Rigidbody2D;
				bombInstance.velocity = new Vector2(sideDisatnce * -1,upDistance);
			}
		}

		isBombing = false;
		// The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
		//bombHUD.enabled = bombCount > 0;
	}

	public void UpdateBombText()
	{
		bombText.bombsLeft = bombCount;
		bombText.UpdateText();
	}

	public void throwBomb()
	{
		isBombing = true;
	}
}
