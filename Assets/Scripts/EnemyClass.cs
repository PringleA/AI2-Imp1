using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float health = 30;

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
		if (health <= 0)
		{
			Destroy(gameObject);
			Scores.AddKill(1);
		}
	}
}
