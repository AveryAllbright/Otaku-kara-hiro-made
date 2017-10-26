using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Light, Medium, Heavy };

public class UnitManagerScript : MonoBehaviour {

    public GameObject[] pathNodes;
    public GameObject unitPrefab;


    public UnitType currType;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currType = UnitType.Light;
            spawnUnit();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currType = UnitType.Medium;
            spawnUnit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currType = UnitType.Heavy;
            spawnUnit();
        }
    }

    public void spawnUnit()
    {
            Instantiate(unitPrefab, pathNodes[0].transform.position, Quaternion.identity);
    }
}
