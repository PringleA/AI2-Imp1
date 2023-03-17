using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour
{
	public float maxHealth = 10;
	private float health = 0;
	public float speed = 0.1f;
	public float damage = 1;
	private GameObject player;
	private GameObject playerCam;
	private GameObject[] cover;
	private BehaviourHandler behaviour;
	public Slider healthBar;
	private bool alerted = false;
	private bool hidden = false;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerCam = GameObject.FindGameObjectWithTag("MainCamera");
		cover = GameObject.FindGameObjectsWithTag("HidePosition");
		behaviour = gameObject.GetComponent<BehaviourHandler>();
		health = maxHealth;
	}

	// Update is called once per frame
	void Update()
    {
		// make health bar point at player cam at all times
		if (playerCam != null)
			healthBar.transform.LookAt(playerCam.transform.position);

		// set health bar to current hp
		healthBar.value = health / maxHealth;

		if (health <= 0)
		{
			Destroy(gameObject);
			Scores.AddKill(1);
		}

		if (player != null)
		{
			// if alerted and not hiding
			if (alerted && behaviour.state != BehaviourHandler.EnemyState.HIDE)
			{
				Rigidbody thisRB = GetComponent<Rigidbody>();
				Vector3 playerPosition = player.transform.position;
				Vector3 vectorToPlayer = playerPosition - transform.position;
				transform.LookAt(playerPosition);
				//check next behaviour
				if (behaviour.state == BehaviourHandler.EnemyState.SHOOT)
				{
					//shoot at player
				}
			}
			//
			if (behaviour.state == BehaviourHandler.EnemyState.LOOK)
				IdleLook();

			if (behaviour.state == BehaviourHandler.EnemyState.MOVE)
			{
				
			}

			if (behaviour.state == BehaviourHandler.EnemyState.HIDE)
			{
				//check next behaviour
				if (!hidden)
				{
					GameObject nearestCover = FindNearestCover();
					MoveTowards(nearestCover.transform.position);
				}
			}
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

		// if object in trigger is player
		if (playerHealth != null)
		{
			alerted = true;
			//playerHealth.TakeDamage(damage);
		}
	}

	public void TakeDamage(float amount)
    {
		float currentHP = health;
		health = currentHP - amount;
	}

	private void IdleLook()
	{

	}

	private GameObject FindNearestCover()
	{
		GameObject nearestObj = cover[0];
		Vector2 nearestPos;
		Vector2 testPos;
		float testDistance;
		float nearestDistance;

		if (cover != null)
		{
			nearestPos.x = gameObject.transform.position.x - cover[0].transform.position.x;
			nearestPos.y = gameObject.transform.position.x - cover[0].transform.position.z;
			nearestDistance = Mathf.Sqrt((nearestPos.x * nearestPos.x) + (nearestPos.y * nearestPos.y));

			for (int i = 1; i < cover.Length; i++)
			{
				testPos.x = gameObject.transform.position.x - cover[i].transform.position.x;
				testPos.y = gameObject.transform.position.x - cover[i].transform.position.z;
				testDistance = Mathf.Sqrt((testPos.x * testPos.x) + (testPos.y * testPos.y));

				if (testDistance < nearestDistance)
				{
					nearestDistance = testDistance;
					nearestObj = cover[i];
				}
			}
		}
		return nearestObj;
	}

	private void MoveTowards(Vector3 coverPos)
	{
		var step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, coverPos, step);

		if (Vector3.Distance(transform.position, coverPos) < 0.001f)
		{
			hidden = true;
		}
	}
}
