using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum soldierType { Light, Medium, Heavy };


public class RTDmanager : MonoBehaviour 
{
	public GameObject flowchartObject;
	private Fungus.Flowchart flowchart;


	public GameObject soldierPrefab;
	public GameObject spritePrefab;
	public GameObject towerPrefab;
	public GameObject basePrefab;
	public GameObject[] nodes;
	public GameObject[] lines;
	public List<GameObject> currPath;
	public soldierType currType;
	public List<GameObject> towers;
	public List<GameObject> soldiers;
	public Text timerText;
	public Text unitCountText;
	public int totalSoldiers = 0;
	public int maxSoldiers = 12;
	public float spawnCoolDown = 1.0f;
	public bool canSpawn = true;
    public bool win = false;
	private float timer;

	// Use this for initialization
	void Start ()
	{
		flowchart = flowchartObject.GetComponent<Fungus.Flowchart> ();
		soldiers = new List<GameObject> ();
		timer = 45;
		int[] pathToBuild = {33, 23, 20, 10};
		int[] towerPositions = { 28, 12 };
		BuildPath(pathToBuild);
		AddTowers (towerPositions);
		createLines();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(flowchart.GetBooleanVariable("isRTDrunning"))
		{
			timer -= Time.deltaTime;
			timerText.text = "Timer: " + (int)timer;

			if (!canSpawn) {
				spawnCoolDown += Time.deltaTime;
				if (spawnCoolDown >= 1.5f) { spawnCoolDown = 0.0f; canSpawn = true;}
			}
			RadiusCheck();
			SoldierPlacement ();
			if (Input.GetKeyDown(KeyCode.E) || totalSoldiers == maxSoldiers || timer <= 0)
			{
				Lose ();
			}
            if(win)
            {
                Win();
            }
		}
	}
		
	/// <summary>
	/// Adds the towers.
	/// </summary>
	/// <param name="towerPositions">Tower positions.</param>
	void AddTowers(int[] towerPositions) 
	{
		for (int i = 0; i < towerPositions.Length; i++) 
		{
			towers.Add(Instantiate(towerPrefab, nodes[towerPositions[i]].transform.position, Quaternion.identity));
		}
	}
		

	// Check if soldiers are in the radius of towers
	/// <summary>
	/// Radiuses the check.
	/// </summary>
	void RadiusCheck() {

		// Loop through the soldiers and see what tower's they are in
		foreach (GameObject sol in soldiers) {
			UnitScript s = sol.GetComponent<UnitScript>();
			foreach (GameObject tow in towers) {
				Tower t = tow.GetComponent<Tower>();

				// Check if this soldier is in this tower's radius
				bool inRadius = (Vector3.Distance(s.transform.position, t.transform.position) <= t.Radius);

				// If this tower has this unit in its list already, we will remove it if it is not in the radius anymore
				if (t.unitList.Contains (s)) {
					if (!inRadius) t.unitList.Remove (s);
				}

				// Otherwise, we add it to the list if it is in the tower's radius now
				else if (inRadius) t.unitList.Add(s);
			}
		}
	}


	/// <summary>
	/// Unimplemented methods from the diagram
	/// Method to place a soldier
	/// </summary>
	void SoldierPlacement() {
		if (Input.GetKeyDown(KeyCode.A) && canSpawn && totalSoldiers < 12)
		{
			currType = soldierType.Light;
			soldiers.Add(Instantiate (soldierPrefab, currPath [0].transform.position, Quaternion.identity));
			totalSoldiers++;
			canSpawn = false;
		}
		if (Input.GetKeyDown(KeyCode.S) && canSpawn && totalSoldiers < 12)
		{
			currType = soldierType.Medium;
			soldiers.Add(Instantiate (soldierPrefab, currPath [0].transform.position, Quaternion.identity));
			totalSoldiers++;
			canSpawn = false;
		}
		if (Input.GetKeyDown(KeyCode.D) && canSpawn && totalSoldiers < 12)
		{
			currType = soldierType.Heavy;
			soldiers.Add(Instantiate (soldierPrefab, currPath [0].transform.position, Quaternion.identity));
			totalSoldiers++;
			canSpawn = false;
		}
		unitCountText.text = totalSoldiers + " / " + maxSoldiers + " Units Spawned";
	}

	//Method for building a path out of the grid of nodes. 
	void BuildPath(int[] nodeIndexes){
		for (int i = 0; i < nodeIndexes.Length; i++) {
			currPath.Add (nodes [nodeIndexes[i]]);
		}
		Instantiate (basePrefab, currPath [nodeIndexes.Length - 1].transform.position, Quaternion.identity);
	}
		
	/// <summary>
	/// Create a list that stores Gameobjects with 1x1 cube sprites equal to the amount of nodes - 1 that are being used in the path
	/// </summary>
	void createLines()
	{
		lines = new GameObject[currPath.Count];

		for (int i = 0; i < currPath.Count - 1; i++) 
		{
			lines [i] = Instantiate (spritePrefab, currPath[i].transform.position, Quaternion.identity);
		}

		for(int i = 0; i < currPath.Count - 1; i++)
		{
			if(currPath[i].transform.position.x == currPath[i + 1].transform.position.x) // if the two nodes are in the same column
			{
				float fScale = Mathf.Abs(currPath[i].transform.position.y - currPath[i + 1].transform.position.y);
				Vector3 newPos = new Vector3(0, 0, -0.3f);
				Vector3 newScale = new Vector3(1, 1, 1);
				newPos.y = (currPath[i].transform.position.y + currPath[i + 1].transform.position.y) / 2f;
				newPos.x = currPath[i].transform.position.x;
				newScale.y *= fScale + 1.0f;
				lines[i].transform.position = newPos;
				lines [i].transform.localScale = newScale;
			}//end if
			else	//if the two nodes are in the same row
			{
				float fScale = Mathf.Abs(currPath[i].transform.position.x - currPath[i + 1].transform.position.x);
				Vector3 newPos = new Vector3(0, 0, -0.3f);
				Vector3 newScale = new Vector3(1, 1, 1);
				newPos.x = (currPath[i].transform.position.x + currPath[i + 1].transform.position.x) / 2f;
				newPos.y = currPath[i].transform.position.y;
				newScale.x *= fScale + 1.0f;
				lines[i].transform.position = newPos;
				lines [i].transform.localScale = newScale;
			}//end else
		}
	}//end createLines

	// Method for losing, called when you run out of time
	void Lose() 
	{
		flowchart.SetBooleanVariable("isRTDrunning", false);
        flowchart.SetBooleanVariable("bWon", false);
		GameObject.Find (flowchart.GetStringVariable ("CurrentFlowchart")).GetComponent<Fungus.Flowchart> ().ExecuteBlock ("EndRTD");
	}

	// Method for winning, called when you get a soldier to the goal
	void Win() 
    {   
        flowchart.SetBooleanVariable("isRTDrunning", false);
        flowchart.SetBooleanVariable("bWon", true);
		GameObject.Find (flowchart.GetStringVariable ("CurrentFlowchart")).GetComponent<Fungus.Flowchart> ().ExecuteBlock ("EndRTD");
	}
}