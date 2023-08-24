using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeAvatar : MonoBehaviour
{
    public GameObject[] allObjects;
    public ArrayList menuObjects;
    public GameObject nearestObject;
    public GameObject panel;
    public float distance;
    public float nearestDistance = 100;

    public void OnAddButtonPressed()
    {
        Debug.Log("hey button pressed woo!");

        GameObject current = GetNearestGameObject();

        if(current != null)
        {
            current.transform.position = transform.position;
            menuObjects.Add(current);
            current.transform.parent = panel.transform;
            current.transform.localScale = new Vector3(100, 100, 100);
        }

        //Vector3(339.559998,342.700012,277.470032)

    }
    public GameObject GetNearestGameObject()
    {
        Debug.Log("hey button pressed woo!");
        allObjects = GameObject.FindGameObjectsWithTag("avatar");
        Debug.Log(allObjects.Length + "length");
        nearestObject = null;
        for (int i = 0; i < allObjects.Length; i++)
        {
            Debug.Log(allObjects[i].transform.position);
            distance = Vector3.Distance(this.transform.position, allObjects[i].transform.position);
            if(distance < nearestDistance)
            {
                nearestObject = allObjects[i];
                nearestDistance = distance;
            }
        }
        return nearestObject;
    }

    public void SelectAvatar(int index)
    {
        Debug.Log("SelectAvatar: " + index);
    }
    // Start is called before the first frame update
    void Start()
    {
        menuObjects = new ArrayList();
        OnAddButtonPressed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
