using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyClass : MonoBehaviour
{
	public float maxHealth = 10;
	private float health = 0;
	public float speed = 5;
	public float damage = 1;
	private GameObject player;
	private GameObject playerCam;
	public Slider healthBar;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerCam = GameObject.FindGameObjectWithTag("MainCamera");
		health = maxHealth;
	}

	// Update is called once per frame
	void Update()
    {
		if (playerCam != null)
			healthBar.transform.LookAt(playerCam.transform.position);

		healthBar.value = health / maxHealth;

		if (health <= 0)
		{
			Destroy(gameObject);
			Scores.AddKill(1);
		}

		if (player != null)
		{
			Rigidbody thisRB = GetComponent<Rigidbody>();
			Vector3 playerPosition = player.transform.position;
			Vector3 vectorToPlayer = playerPosition - transform.position;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

		if (playerHealth != null)
		{
			transform.LookAt(other.transform.position);
			playerHealth.TakeDamage(damage);
		}
	}

	public void TakeDamage(float amount)
    {
		float currentHP = health;
		health = currentHP - amount;
	}
}
