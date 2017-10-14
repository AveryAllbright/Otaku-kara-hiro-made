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
	}
}
