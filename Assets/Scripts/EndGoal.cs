using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
	public ScoringSystem _scoringSystem;

	private void OnTriggerEnter(Collider other)
    {
        if (_scoringSystem.totalScore == _scoringSystem.totalCoins)
        {
            Debug.Log("End game");
        }
    }
}
