using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {
	private int hp;
	private float speed;
	private int targetNodeIndex;
	private RTDmanager rtdmanager;

	public int HP{get{return hp; }}

	// Use this for initialization
	void Start () {
		targetNodeIndex = 0;
		rtdmanager = GameObject.FindGameObjectWithTag ("RTDmanager").GetComponent<RTDmanager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Move ()
	{
		//move towards node
		//if in range of target, change target to the next node
	}

	void TakeDamage(int damage)
	{
		hp -= damage;

		if(hp < 0)
		{
			Die ();
		}
	}

	private void Die()
	{
		
	}
}
