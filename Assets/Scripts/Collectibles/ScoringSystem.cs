using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Scores
{
	public static int totalScore = 0;
	public static int totalCoins;

	public static void AddScore(int amount)
	{
		totalScore += amount;
	}
}

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    //public AudioSource collectSound;

    private void Awake()
    {
        Scores.totalCoins = GameObject.FindGameObjectsWithTag("Collectible").Length;
	}

    void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Collectibles Found: " + Scores.totalScore.ToString()
            + " / " + Scores.totalCoins;
	}
}
