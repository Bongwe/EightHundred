using UnityEngine;
using System.Collections;

public class EnemyJump : MonoBehaviour {

	private Enemy enemyScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits grond...
		if(col.tag == "ground")
		{
			enemyScript = transform.root.GetComponent<Enemy>();
			enemyScript.Jump();
		}
	}
}
