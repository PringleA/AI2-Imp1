using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourHandler : MonoBehaviour
{
    public EnemyMood mood;
    public EnemyState state;
    public float stateSwitchDelay = 5.0f;
    private float currentDelay = 0;
    private bool findNewState = false;
    private ProbController prob;

	// Start is called before the first frame update
	void Start()
    {
	    prob = gameObject.transform.parent.GetComponent<ProbController>();

        if (prob == null)
            prob = new ProbController();
    }

    // Update is called once per frame
    void Update()
    {
        // iterate delay for state switching
		if (currentDelay < stateSwitchDelay)
			currentDelay += Time.deltaTime;

        // allow state change if max delay is reached
		else if (currentDelay >= stateSwitchDelay)
			findNewState = true;

        if (findNewState)
        {
            // use probability to calc next state and reset delay
            state = prob.CalculateNextState(mood);
            currentDelay = 0;
        }
	}
}
