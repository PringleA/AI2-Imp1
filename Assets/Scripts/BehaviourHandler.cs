using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourHandler : MonoBehaviour
{
    public EnemyMood mood;
    public EnemyState state;

    public enum EnemyMood
    {
        PASSIVE = 0,
        NEUTRAL = 1,
        AGGRESSIVE = 2
    }

	public enum EnemyState
	{
		HIDE = 0,
		LOOK = 1,
		SHOOT = 2,
        MOVE = 3
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
