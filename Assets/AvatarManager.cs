using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TMPro;
using UnityEngine.Animations.Rigging;

public class AvatarManager : MonoBehaviour
{

    //Change avatar
    public GameObject[] allObjects;
    public List<GameObject> menuObjects;
    public List<TextMeshProUGUI> textObjects;
    public GameObject nearestObject;
    public GameObject panel;
    public GameObject globalAvatar;
    public float distance;
    public float nearestDistance = 100;
    private int avatarIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        avatarIndex = -1;
        //get all the Button Text Components
        foreach (Transform child in panel.transform)
        {
            textObjects.Add(child.GetComponentInChildren<TextMeshProUGUI>());
        }
        AddNearestObjectOnButtonPress();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Test Function")]
    public void AddNearestObjectOnButtonPress()
    {
        Debug.Log("AddNearestObjectOnButtonPress!");

        GameObject current = GetNearestGameObject();

        if (current != null)
        {
            if (avatarIndex != -1)
            {
                menuObjects[avatarIndex].SetActive(false);
            }
            menuObjects.Add(current);
            avatarIndex = menuObjects.Count - 1;
            enableAvatarComponents(current);
        }

    }

    public void enableAvatarComponents(GameObject avatar)
    {
        Debug.Log("enableAvatarComponents!");
        Debug.Log(avatar.name);
        avatar.GetComponent<RigBuilder>().enabled = true;
        avatar.GetComponent<IKTargetFollowVRRig>().enabled = true;
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
            if (distance < currentNearestDistance && !menuObjects.Contains(allObjects[i]))
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
