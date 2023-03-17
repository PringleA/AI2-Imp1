using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoverHandler : MonoBehaviour
{
	GameObject[] cover;
	Vector3 randPos;
    float[] xPositions;
	float[] zPositions;
	float minPos = -20.0f;
	float gridSpacing = 10.0f;
	int totalSquares = 5;

	// Start is called before the first frame update
	void Start()
    {
		cover = GameObject.FindGameObjectsWithTag("Cover");
		GetXZPositions();
		PlaceCover();
		BakeNavMesh();
    }

	void GetXZPositions()
	{
		float xPos = 0;
		float zPos = 0;
		// creating 5 different positions for a total of 25 unique potential positions
		xPositions = new float[totalSquares];
		zPositions = new float[totalSquares];

		// data driven method of retrieving every potential position for cover
		for (int i = 0; i < totalSquares; i++)
		{
			xPos = minPos + (i * gridSpacing);
			for (int k = 0; k < totalSquares; k++)
			{
				zPos = minPos + (k * gridSpacing);
				xPositions[i] = xPos;
				zPositions[k] = zPos;
			}
		}
	}

	void PlaceCover()
	{
		int randXPos;
		int randZPos;
		float yPos = 0.19f;
		// iterating through cover and giving spots
		int totalCover = cover.Length;
		for (int i = 0; i < totalCover; i++)
		{
			randXPos = Random.Range(1, totalSquares-1);
			randZPos = Random.Range(1, totalSquares-1);

			cover[i].transform.position = new Vector3(xPositions[randXPos], yPos, zPositions[randZPos]);
		}
	}

	void BakeNavMesh()
	{
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
