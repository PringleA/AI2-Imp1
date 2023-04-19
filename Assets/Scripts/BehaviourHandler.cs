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
    public float currentDelay = 0;
	public bool findNewState = false;
	private float randStartDelay = 0;
    public ProbController prob;
	private NavMeshAgent agent;
    //private float min

	// Start is called before the first frame update
	void Start()
    {
        // varying initial decision for enemies
		float minStartDelay = 0.0f;
	    float maxStartDelay = 1.0f;
		randStartDelay = Random.Range(minStartDelay, maxStartDelay);
		currentDelay = randStartDelay;

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
            else if (currentDelay >= stateSwitchDelay)
            {
                findNewState = true;
                currentDelay = 0;
            }

            if (findNewState)
            {
                state = prob.CalculateNextState(mood);
                findNewState = false;
            }
        } 
	}
}
