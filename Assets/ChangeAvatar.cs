using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeAvatar : MonoBehaviour
{
    public GameObject[] allObjects;
    public GameObject nearestObject;
    public float distance;
    public float nearestDistance = 100;

    public void OnButtonPressed()
    {
        Debug.Log("hey button pressed woo!");
    }
    public GameObject GetNearestGameObject()
    {
        Debug.Log("hey button pressed woo!");
        allObjects = GameObject.FindGameObjectsWithTag("avatar");
        Debug.Log(allObjects.Length + "length");
        for(int i = 0; i < allObjects.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, allObjects[i].transform.position);
            if(distance < nearestDistance)
            {
                nearestObject = allObjects[i];
                nearestDistance = distance;
            }
        }
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetNearestGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
