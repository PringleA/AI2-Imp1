using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float health = 10;

    // Update is called once per frame
    void Update()
    {
		if (health <= 0)
		{
			Destroy(gameObject);
			Scores.AddKill(1);
		}
	}

    public void TakeDamage(float amount)
    {
		float currentHP = health;
		health = currentHP - amount;
	}
}
