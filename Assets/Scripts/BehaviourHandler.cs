using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourHandler : MonoBehaviour
{
    public EnemyMood mood;
    public EnemyState state;
    private EnemyClass enemyClass;
    public float stateSwitchDelay = 2.0f;
    public float minDelay = 1.0f;
    public float maxDelay = 5.0f;
    public float currentDelay = 0;
	public bool findNewState = false;
    public ProbController prob;
	private NavMeshAgent agent;
    //private float min

	// Start is called before the first frame update
	void Start()
    {
		// varying initial decision for enemies
		stateSwitchDelay = Random.Range(minDelay, maxDelay);
        currentDelay = stateSwitchDelay;

		prob = gameObject.transform.parent.GetComponent<ProbController>();
		agent = gameObject.GetComponent<NavMeshAgent>();
        enemyClass = gameObject.GetComponent<EnemyClass>();

		if (prob == null)
            prob = new ProbController();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyClass.playerVisible)
        {
            // iterate delay for state switching
            if (currentDelay < stateSwitchDelay)
                currentDelay += Time.fixedDeltaTime;

            // allow state change if max delay is reached
            if (currentDelay >= stateSwitchDelay)
            {
                findNewState = true;
				stateSwitchDelay = Random.Range(minDelay, maxDelay);
				currentDelay = 0;
			}

            if (findNewState)
            {
                state = prob.CalculateNextState(mood);
                enemyClass.ResetBools();
				findNewState = false;
            }
        } 
	}
}
