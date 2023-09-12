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
    public int avatarIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        avatarIndex = -1;
        //get all the Button Text Components
        foreach (Transform child in panel.transform)
        {
            TextMeshProUGUI text = child.GetComponentInChildren<TextMeshProUGUI>();
            if(text.text == "")
            {
                textObjects.Add(text);
            }
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

        if(menuObjects.Count == textObjects.Count)
        {
            return;
        }

        GameObject current = GetNearestGameObject();

        if (current != null)
        {
            textObjects[menuObjects.Count].text = current.name;
            if (avatarIndex != -1)
            {
                menuObjects[avatarIndex].SetActive(false);
            }
            GameObject duplicate = Instantiate(current);
            duplicate.name = current.name;
            menuObjects.Add(current);
            avatarIndex = menuObjects.Count - 1;
            enableAvatarComponents(current);
        }

    }

    private void enableCurrentAvatar()
    {
        menuObjects[avatarIndex].SetActive(true);
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

    [ContextMenu("remove")]
    public void RemoveLastAvatar()
    {
        if(menuObjects.Count >= 0)
        {
            Destroy(menuObjects[menuObjects.Count -1]);
            menuObjects.RemoveAt(menuObjects.Count - 1);
            if (avatarIndex >= menuObjects.Count)
            {
                avatarIndex = menuObjects.Count - 1;
            }
            textObjects[menuObjects.Count].text = "";
            enableCurrentAvatar();
        }
    }

    private async Task WaitOneSecondAsync()
    {
        Debug.Log("wait...");
        await Task.Delay(TimeSpan.FromSeconds(0.25));
        Debug.Log("Finished waiting.");
    }

    public async void SelectAvatar(int index)
    {
        if (menuObjects.Count <= 0 || index == avatarIndex)
        {
            return;
        }
        menuObjects[avatarIndex].SetActive(false);
        menuObjects[index].SetActive(true);
        avatarIndex = index;
    }
}
