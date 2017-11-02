using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	private float rateOfFire;
	private float fireTime;
	private int damage;
	private float radius;

	private GameObject bullet;					// Bullet that has been shot
	private UnitScript shotSolider;				// Soldier that has been shot at
	private float bulletLifetime;				// Amount of time it takes a bullet to hit a soldier
	private float bulletTravelTime; 			// Amount of time bullet has been travelling
	public GameObject bulletPrefab;
	public GameObject radiusPrefab;
	public List<UnitScript> unitList;			// List of units in the tower's radius
	public float Radius{get{ return radius; }}

	// Use this for initialization
	void Start () {
		radius = 6.5f;
		damage = 1;
		rateOfFire = 1.2f;
		bulletLifetime = rateOfFire / 4f;
		fireTime = rateOfFire;
		unitList = new List<UnitScript>();

		// Create a circle for our radius
		GameObject radiusObject = Instantiate (radiusPrefab, transform.position, Quaternion.identity);
		radiusObject.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		// fireTime starts at rateOfFire, and decreases by deltaTime each update, so the tower fires when fireTime is 0
		fireTime -= Time.deltaTime;

		// If there is a soldier in the queue and the tower is ready to fire, it will shoot the soldier at the front of the list
		if (unitList.Count != 0 && fireTime <= 0) {
			fireTime = rateOfFire;
			Shoot(unitList[0]);
		}

		// If a bullet has been fired, move it to the shot soldier
		if (bullet != null) 
		{
			// LERP the bullet to the shot soldier
			bulletTravelTime += Time.deltaTime;
			float travelPercentage = bulletTravelTime / bulletLifetime;
			bullet.transform.position = Vector3.Lerp (transform.position, shotSolider.transform.position, travelPercentage);

			// If our travelPercentage is 1, the bullet has hit
			if (travelPercentage >= 1f) 
			{
				// Destroy our bullet, have the soldier take damage, and reset our bulletTravelTime
				Destroy (bullet);
				bullet = null;
				shotSolider.TakeDamage (damage);
				shotSolider = null;
				bulletTravelTime = 0;
			}
		}
	}

	// Method that shoots a soldier
	void Shoot(UnitScript s) {
		bullet = Instantiate (bulletPrefab, transform.position, Quaternion.identity);
		shotSolider = s;
	}
}
