using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour
{
	// public
	public float maxHealth = 100;
	public float damage = 1;
	public float lookSpeed = 1.0f;
	public float maxLookTime = 30.0f;
	public bool isMoving = false;
	public bool isHiding = false;
	public bool isRotating = false;
	public Slider healthBar;
	// private
	private float health = 0;
	private float rotLength = 0;
	private float currentYrot = 0;
	private bool rotReversed = false;
	private bool alerted = false;
	private bool hidden = false;
	private GameObject player;
	private GameObject playerCam;
	private GameObject[] cover;
	private NavMeshAgent agent;
	private BehaviourHandler behaviour;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerCam = GameObject.FindGameObjectWithTag("MainCamera");
		cover = GameObject.FindGameObjectsWithTag("HidePosition");
		behaviour = gameObject.GetComponent<BehaviourHandler>();
		health = maxHealth;
		agent = gameObject.GetComponent<NavMeshAgent>();
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

		StateTransform();
	}

	private void StateTransform()
	{
		if (player != null)
		{
			switch (behaviour.state)
			{
				case EnemyState.HIDE:
					{
						HideState();
						break;
					}
				case EnemyState.MOVE:
					{
						MoveState();
						break;
					}
				case EnemyState.LOOK:
					{
						LookState();
						break;
					}
				case EnemyState.SHOOT:
					{
						ShootState();
						break;
					}
				default:
					break;
			}
		}
	}

	private void HideState()
	{
		isMoving = false;
		isRotating = false;
		//check next behaviour
		if (!hidden)
		{
			GameObject nearestCover = FindNearestCover();
			Vector3 newPos = new Vector3(nearestCover.transform.position.x,
			gameObject.transform.position.y,
			nearestCover.transform.position.z);
			if (!isHiding)
				MoveTowardsCover(newPos);

			if (Vector3.Distance(transform.position, newPos) < 0.001f)
				hidden = true;
		}
	}

	private void MoveState()
	{
		isHiding = false;
		isRotating = false;
		if (!isMoving)
			MoveToRandomSpot();
	}

	private void LookState()
	{
		isMoving = false;
		isHiding = false;
		IdleLook();
	}
	private void ShootState()
	{
		isMoving = false;
		isHiding = false;
		isRotating = false;
		// if alerted and not hiding
		if (alerted)
		{
			Rigidbody thisRB = GetComponent<Rigidbody>();
			Vector3 playerPosition = player.transform.position;
			Vector3 vectorToPlayer = playerPosition - transform.position;
			transform.LookAt(playerPosition);
			//shoot at player
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
		// initial setup if just moved into idle movement
		if (!isRotating)
		{
			agent.ResetPath();
			isRotating = true;
		}

		float rotAdd = lookSpeed * Time.fixedDeltaTime;
		rotLength += Time.fixedDeltaTime;

		// rotate left and right at set look speed
		if (isRotating)
		{
			if (!rotReversed)
			{
				currentYrot = rotAdd;
			}
			else if (rotReversed)
			{
				currentYrot = -rotAdd;
			}

			// flip rotation if max time reached
			if (rotLength >= maxLookTime)
			{
				rotReversed = !rotReversed;
				// reset timer
				rotLength = 0;
			}

			// set new rotation each frame
			gameObject.transform.Rotate(0, currentYrot, 0);

		}
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

	private void MoveToRandomSpot()
	{
		// extents of level
		float minPos = -15.0f;
		float maxPos = 15.0f;

		// get random spot within extents to move towards
		float moveX = Random.Range(minPos, maxPos);
		float moveY = gameObject.transform.position.y;
		float moveZ = Random.Range(minPos, maxPos);

		// create vector3 for moving towards
		Vector3 movePos = new Vector3(moveX, moveY, moveZ);

		// move enemy this direction
		agent.SetDestination(movePos);
		//gameObject.transform.LookAt(movePos);

		isMoving = true;
	}

	private void MoveTowardsCover(Vector3 coverPos)
	{
		//var step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position, coverPos, step);

		agent.SetDestination(coverPos);
		//gameObject.transform.LookAt(coverPos);
		isHiding = true;
	}
}
