using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.
	public int enemyCount = 0;
	public int enemiesToGenerate = 3;

	public PatrolPoint[] patrolPoints;

	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		if (enemyCount < enemiesToGenerate) {
			// Instantiate a random enemy.
			int enemyIndex = Random.Range (0, enemies.Length);
			GameObject newEnemey = (GameObject)Instantiate (enemies [enemyIndex], transform.position, transform.rotation);

			IEnemy enemyInstance = newEnemey.GetComponent<IEnemy>();

			//Give this new enemy points of which they should patrol
			enemyInstance.setPatrolPoints(patrolPoints);

			enemyCount++;
		}

		/*// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}*/

	}
}
