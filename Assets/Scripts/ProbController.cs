using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbController : MonoBehaviour
{
	public StateProbability probAggressive;
	public StateProbability probNeutral;
	public StateProbability probPassive;
	public float uncertainty;

	// Start is called before the first frame update
	void Start()
    {
		CreateProbabilities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void CreateProbabilities()
	{
		probAggressive.moodName = EnemyMood.AGGRESSIVE;
		probNeutral.moodName = EnemyMood.NEUTRAL;
		probPassive.moodName = EnemyMood.PASSIVE;

		// aggressive probablibites
		probAggressive.hide.x = 0.0f;
		probAggressive.hide.y = 0.2f;
		probAggressive.move.x = 0.2001f;
		probAggressive.move.y = 0.5f;
		probAggressive.look.x = 0.5001f;
		probAggressive.look.y = 1.0f;

		// neutral probablibites
		probNeutral.hide.x = 0.0f;
		probNeutral.hide.y = 0.4f;
		probNeutral.move.x = 0.4001f;
		probNeutral.move.y = 0.6f;
		probNeutral.look.x = 0.6001f;
		probNeutral.look.x = 1.0f;

		// passive probablibites
		probPassive.hide.x = 0.0f;
		probPassive.hide.y = 0.6f;
		probPassive.move.x = 0.6001f;
		probPassive.move.y = 0.8f;
		probPassive.look.x = 0.8001f;
		probPassive.look.y = 1.0f;
	}

	public EnemyState CalculateNextState(EnemyMood mood)
	{
		EnemyState state = new EnemyState();
		float randProb = Random.Range(0, 1);
		FindRandomProb(state, randProb, mood);
		return state;
	}

	private void FindRandomProb(EnemyState state, float randProb, EnemyMood mood)
	{
		switch (mood)
		{
			case EnemyMood.PASSIVE:
				{
					if (randProb >= probPassive.hide.x && randProb <= probPassive.hide.y)
						state = EnemyState.HIDE;
					if (randProb >= probPassive.move.x && randProb <= probPassive.move.y)
						state = EnemyState.MOVE;
					if (randProb >= probPassive.look.x && randProb <= probPassive.look.y)
						state = EnemyState.LOOK;
					break;
				}
			case EnemyMood.NEUTRAL:
				{
					if (randProb >= probNeutral.hide.x && randProb <= probNeutral.hide.y)
						state = EnemyState.HIDE;
					if (randProb >= probNeutral.move.x && randProb <= probNeutral.move.y)
						state = EnemyState.MOVE;
					if (randProb >= probNeutral.look.x && randProb <= probNeutral.look.y)
						state = EnemyState.LOOK;
					break;
				}
			case EnemyMood.AGGRESSIVE:
				{
					if (randProb >= probAggressive.hide.x && randProb <= probAggressive.hide.y)
						state = EnemyState.HIDE;
					if (randProb >= probAggressive.move.x && randProb <= probAggressive.move.y)
						state = EnemyState.MOVE;
					if (randProb >= probAggressive.look.x && randProb <= probAggressive.look.y)
						state = EnemyState.LOOK;
					break;
				}
		}
	}
}
public class StateProbability
{
	public EnemyMood moodName;
	public Vector2 hide;
	public Vector2 move;
	public Vector2 look;
}
public enum EnemyMood
{
	PASSIVE = 0,
	NEUTRAL = 1,
	AGGRESSIVE = 2
}

public enum EnemyState
{
	HIDE = 0,
	MOVE = 1,
	LOOK = 2,
	SHOOT = 3
}
