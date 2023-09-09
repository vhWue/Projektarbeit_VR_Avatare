using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TMPro;

public class AvatarManager : MonoBehaviour
{

    //Change avatar
    public GameObject[] allObjects;
    public List<GameObject> menuObjects;
    public List<TextMeshPro> textObjects;
    public GameObject nearestObject;
    public GameObject panel;
    public GameObject globalAvatar;
    public float distance;
    public float nearestDistance = 100;
    // Start is called before the first frame update
    void Start()
    {
        AddNearestObjectOnButtonPress();

        foreach (Transform child in panel.transform)
        {
            //child is your child transform
            Debug.Log(child.GetChild(0).name);
            Debug.Log(child.GetComponentInChildren<TextMeshPro>());
            Debug.Log(child.GetComponentInChildren<TextMeshPro>().text);
            textObjects.Add(child.GetComponentInChildren<TextMeshPro>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNearestObjectOnButtonPress()
    {
        Debug.Log("AddNearestObjectOnButtonPress!");

        GameObject current = GetNearestGameObject();

        if (current != null)
        {
            menuObjects.Add(current);
        }

    }
    public GameObject GetNearestGameObject()
    {
        Debug.Log("GetNearestGameObject!");
        allObjects = GameObject.FindGameObjectsWithTag("avatar");
        nearestObject = null;
        float currentNearestDistance = nearestDistance;
        for (int i = 0; i < allObjects.Length; i++)
        {
            Debug.Log(allObjects[i].name);
            distance = Vector3.Distance(this.transform.position, allObjects[i].transform.position);
            if (distance < currentNearestDistance)
            {
                nearestObject = allObjects[i];
                currentNearestDistance = distance;
            }
        }
        return nearestObject;
    }

    private async Task WaitOneSecondAsync()
    {
        Debug.Log("wait...");
        await Task.Delay(TimeSpan.FromSeconds(0.25));
        Debug.Log("Finished waiting.");
    }

    public async void SelectAvatar(int index)
    {

    }
}
