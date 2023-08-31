using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform ikTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class IKTargetFollowVRRig : MonoBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 0.1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;

    //Change avatar
    public GameObject[] allObjects;
    public List<GameObject> menuObjects;
    public GameObject nearestObject;
    public GameObject panel;
    public GameObject globalAvatar;
    public float distance;
    public float nearestDistance = 100;

    private void Start()
    {
        OnAddButtonPressed();
        SelectAvatar(0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = head.ikTarget.position + headBodyPositionOffset;
        float yaw = head.vrTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z),turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }

    public void OnAddButtonPressed()
    {
        Debug.Log("hey button pressed woo!");

        GameObject current = GetNearestGameObject();

        if (current != null)
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
        Debug.Log("GetNearestGameObject!");
        allObjects = GameObject.FindGameObjectsWithTag("avatar");
        nearestObject = null;
        for (int i = 0; i < allObjects.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, allObjects[i].transform.position);
            if (distance < nearestDistance)
            {
                nearestObject = allObjects[i];
                nearestDistance = distance;
            }
        }
        return nearestObject;
    }

    private async Task WaitOneSecondAsync()
    {
        Debug.Log("wait...");
        await Task.Delay(TimeSpan.FromSeconds(1));
        Debug.Log("Finished waiting.");
    }

    public Transform getTransformInHierarchyByName(GameObject gameObject, string name)
    {
        Transform[] AllChildren = gameObject.GetComponentsInChildren<Transform>();

        foreach (var child in AllChildren)
        {
            // here is where you decide if you want this (replace it with whatever you like)
            if (child.gameObject.name == name)
            {
                return child;
            }
        }
        return null;
    }

    public async void SelectAvatar(int index)
    {
        Debug.Log("SelectAvatar: " + index);
        Transform newHeadTarget = getTransformInHierarchyByName(allObjects[index], "Head Target");
        Debug.Log(newHeadTarget);
        Transform newLeftHandTarget = getTransformInHierarchyByName(allObjects[index], "Left Arm IK_target");
        Debug.Log(newLeftHandTarget);
        Transform newRightHandTarget = getTransformInHierarchyByName(allObjects[index], "Right Arm IK_target");
        Debug.Log(newRightHandTarget);
        head.ikTarget = newHeadTarget;
        leftHand.ikTarget = newLeftHandTarget;
        rightHand.ikTarget = newRightHandTarget;
        await WaitOneSecondAsync();
        for (var i = globalAvatar.transform.childCount - 1; i >= 0; i--)
        {
            Debug.Log("Destroy");
            await WaitOneSecondAsync();
            Destroy(globalAvatar.transform.GetChild(i).gameObject);
        }

        Debug.Log("parent");
        await WaitOneSecondAsync();
        menuObjects[index].transform.parent = globalAvatar.transform;
        Debug.Log("position");
        await WaitOneSecondAsync();
        menuObjects[index].transform.position = globalAvatar.transform.position;
        Debug.Log("rotation");
        await WaitOneSecondAsync();
        menuObjects[index].transform.rotation = globalAvatar.transform.rotation;
        Debug.Log("localScale");
        await WaitOneSecondAsync();
        menuObjects[index].transform.localScale = new Vector3(1, 1, 1);
    }
}
