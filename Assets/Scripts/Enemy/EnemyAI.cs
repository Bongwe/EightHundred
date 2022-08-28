using UnityEngine;
using System.Collections;

public class EnemyAI : ScriptableObject {

	public float MinDistance = 1f;
	public float moveSpeed = 0;
	public PatrolPoint[] points;
	//private GameObject parent;
	public float playerRadius = 10f;
	private bool playerInRange = false;

	private int currentTargetIndex = 0;
	private Transform target;
	public IEnemy parent;
	public Transform parentTransform;

	public Transform gamePlayer;

	private Transform playerTransform;
	private bool playerSeen = false;

	/*public GameObject Parent {
		get {
			return parent;
		}

		set {
			parentTransform = value.GetComponent<Transform>();
			parent = value;
		}
	}*/

	public PatrolPoint[] Points {
		set {
			points = value;
		}

		get {
			return points;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void PublicFixedUpdate () {

		checkIfPlayerIsInRangerAndShoot ();

		if (points != null && parentTransform != null) {

			if (points.Length > 0 && target == null) {
				target = points[currentTargetIndex].GetComponent<Transform>();
			}

			if (points.Length > 0) {

                float step = moveSpeed * Time.deltaTime;
				parentTransform.position = Vector2.MoveTowards(parentTransform.position, target.position, step);

				if (Vector2.Distance (parentTransform.position, target.position) < MinDistance) { // 0.1f) {
					//Debug.Log("End reached....");
				}
			}
		}

		if (playerSeen == false && parentTransform != null && target != null && Vector2.Distance (parentTransform.position, target.position) < MinDistance) {
		//if () {
			//The end target has been reached
			currentTargetIndex = (currentTargetIndex + 1) % points.Length;
			//Debug.Log ("CurrentTargetIndex: " + currentTargetIndex);
			target = points[currentTargetIndex].GetComponent<Transform>();
			//Debug.Log("End reached....");
		}

		if (playerSeen == true) {
			target = playerTransform;

		}
	}

	private void checkIfPlayerIsInRangerAndShoot() {
		Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll (parentTransform.position, playerRadius, 1 << LayerMask.NameToLayer ("Player"));

		//Debug.Log ("In range: " + objectsInRadius.Length);
		playerInRange = false;
		foreach (Collider2D en in objectsInRadius)
		{
			// Check if it has a rigidbody (since there is only one per enemy, on the parent).
			Rigidbody2D rb = en.GetComponent<Rigidbody2D> ();
			if (rb != null && rb.tag == "Player") {
				/*if(!shooting)
					Shoot();*/
				//Debug.Log ("Player in range");
				playerTransform = en.GetComponent<Transform>();
				playerSeen = true;
				playerInRange = true;
				face(playerTransform);
			}
		}
	}

	public bool PlayerIsInRange() {
		return playerInRange;
	}

	private void face(Transform player) {

		gamePlayer = player;
		if (player.position.x > parentTransform.position.x) {
			parent.faceRight();
		} else {
			parent.faceLeft();
		}

	}
}
