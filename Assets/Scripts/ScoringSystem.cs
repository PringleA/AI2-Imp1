using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public int totalScore = 0;
    public int totalCoins;
    //public AudioSource collectSound;

    private void Awake()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Collectible").Length;
	}

    void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Collectibles Found: " + totalScore.ToString()
            + " / " + totalCoins;
	}

    public void AddScore(int amount)
    {
        totalScore += amount;
    }
}
