using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

    public int speed;
    public int health;

	public List<GameObject> path;
    public GameObject unitManager;
    public int currTarget;
    soldierType type;
	// Use this for initialization
	void Start () {
        unitManager = GameObject.Find("RTDManager");
        path = unitManager.GetComponent<RTDmanager>().currPath;
        type = unitManager.GetComponent<RTDmanager>().currType;
        currTarget = 1;
		if (type == soldierType.Light)
        {
            speed = 3;
            health = 2;
        }
		if (type == soldierType.Medium)
        {
            speed = 2;
            health = 3;
        }
		if (type == soldierType.Heavy)
        {
            speed = 1;
            health = 4;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = gameObject.transform.position + ((path[currTarget].transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed);
        this.transform.position = newPos;
        checkIfNextNode();
        checkIfDead(); 
    }

    void checkIfNextNode()
    {
        if(Vector3.SqrMagnitude(gameObject.transform.position - path[currTarget].transform.position) < .01)
        {
			if (currTarget == path.Count - 1)
            {
				foreach (GameObject obj in unitManager.GetComponent<RTDmanager>().towers) 
				{
					Tower tower = obj.GetComponent<Tower>();
					if (tower.unitList.Contains (this)) 
					{
						tower.unitList.Remove(this);
					}
				}
				unitManager.GetComponent<RTDmanager>().soldiers.Remove(gameObject);
                Destroy(gameObject);
            }
            else
            {
                currTarget++;
            }
        }
    }

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

    void checkIfDead()
    {
        if(health <= 0)
        {
			foreach (GameObject obj in unitManager.GetComponent<RTDmanager>().towers) 
			{
				Tower tower = obj.GetComponent<Tower>();
				if (tower.unitList.Contains (this)) 
				{
					tower.unitList.Remove(this);
				}
			}
			unitManager.GetComponent<RTDmanager>().soldiers.Remove(gameObject);
			Destroy(gameObject);
        }
    }
}
