using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 0;
	public Slider healthBar;

	// Start is called before the first frame update
	void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health / maxHealth;
    }

    public void TakeDamage(int hp)
    {
        health -= hp;
    }
}
