using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum soldierType { Light, Medium, Heavy };


public class RTDmanager : MonoBehaviour 
{
	public GameObject soldierPrefab;
	public GameObject[] nodes;
	public List<GameObject> currPath;
	public soldierType currType;
	public List<GameObject> towers;
	private List<GameObject> soldiers;
	private float timer;

	// Use this for initialization
	void Start ()
	{
		soldiers = new List<GameObject> ();
		timer = 0;
		int[] pathToBuild = {33, 23, 20, 10};
		BuildPath(pathToBuild);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		RadiusCheck();
		SoldierPlacement ();
		for(int i = 0; i < currPath.Count - 1; i++) {
			Debug.DrawLine (currPath [i].transform.position, currPath [i + 1].transform.position); }
			
	}

	// Check if soldiers are in the radius of towers
	void RadiusCheck() {
		foreach (GameObject sol in soldiers) {
			Soldier s = sol.GetComponent<Soldier>();
			foreach (GameObject tow in towers) {
				Tower t = tow.GetComponent<Tower>();
				bool inRadius = (Vector3.Distance(s.transform.position, t.transform.position) <= t.Radius);
				if (t.soldierQueue.Contains(s)) {
					if (!inRadius) t.soldierQueue.Dequeue();
				} else if (inRadius) {
					t.soldierQueue.Enqueue(s);
				}
			}
		}
	}

	// Unimplemented methods from the diagram
	// Method to place a soldier
	void SoldierPlacement() {
		if (Input.GetKeyDown(KeyCode.A))
		{
			currType = soldierType.Light;
			Instantiate (soldierPrefab, currPath [0].transform.position, Quaternion.identity);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			currType = soldierType.Medium;
			Instantiate (soldierPrefab, currPath [0].transform.position, Quaternion.identity);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			currType = soldierType.Heavy;
			Instantiate (soldierPrefab, currPath [0].transform.position, Quaternion.identity);
		}
	}

	//Method for building a path out of the grid of nodes. 

	void BuildPath(int[] nodeIndexes){
		for (int i = 0; i < nodeIndexes.Length; i++) {
			currPath.Add (nodes [nodeIndexes[i]]);
		}
	}
	// Method for losing, called when you run out of time
	void Lose() {

	}

	// Method for winning, called when you get a soldier to the goal
	void Win() {

	}
}