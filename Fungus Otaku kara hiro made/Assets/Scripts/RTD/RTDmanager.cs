using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTDmanager : MonoBehaviour 
{
	public GameObject soldierPrefab;
	public List<GameObject> nodes;
	public List<GameObject> towers;
	private List<GameObject> soldiers;
	private float timer;

	// Use this for initialization
	void Start ()
	{
		soldiers = new List<GameObject> ();
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		RadiusCheck();
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

	}

	// Method for losing, called when you run out of time
	void Lose() {

	}

	// Method for winning, called when you get a soldier to the goal
	void Win() {

	}
}