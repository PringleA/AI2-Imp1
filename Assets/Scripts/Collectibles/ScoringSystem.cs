using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Scores
{
	public static int totalScore = 0;
	public static int totalCoins;
    public static int totalEnemiesKilled;
    public static int totalEnemies;

	public static void AddScore(int amount)
	{
		totalScore += amount;
	}

    public static void AddKill(int amount)
    {
        totalEnemiesKilled += amount;
    }
}

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
	public GameObject enemyText;
	//public AudioSource collectSound;

	private void Awake()
    {
        Scores.totalCoins = GameObject.FindGameObjectsWithTag("Collectible").Length;
		Scores.totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
	}

    void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Collectibles Found: " + Scores.totalScore.ToString()
            + " / " + Scores.totalCoins;
		enemyText.GetComponent<TextMeshProUGUI>().text = "Enemies Killed: " + Scores.totalEnemiesKilled.ToString()
			+ " / " + Scores.totalEnemies;
	}
}
