using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	private float rateOfFire;
	private float fireTime;
	private int damage;
	private float radius;
	private RTDmanager rtdmanager;

	public GameObject bullet;
	public Queue<Soldier> soldierQueue;
	public float Radius{get{ return radius; }}

	// Use this for initialization
	void Start () {
		rtdmanager = GameObject.FindGameObjectWithTag ("RTDmanager").GetComponent<RTDmanager> ();
		fireTime = rateOfFire;
		soldierQueue = new Queue<Soldier>();
	}
	
	// Update is called once per frame
	void Update () {
		// fireTime starts at rateOfFire, and decreases by deltaTime each update, so the tower fires when fireTime is 0
		fireTime -= Time.deltaTime;

		// If there is a soldier in the queue and the tower is ready to fire, it will shoot the soldier at the front of the queue
		if (soldierQueue.Count != 0 && fireTime <= 0) {
			fireTime = rateOfFire;
			Shoot(soldierQueue.Peek());
		}
	}

	// Method that shoots a soldier
	void Shoot(Soldier s) {
		// Doesn't shoot a bullet yet, just does damage
		s.TakeDamage(damage);
	}
}
