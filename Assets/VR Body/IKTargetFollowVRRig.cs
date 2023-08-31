using UnityEngine;
using System.Collections.Generic;

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

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform == null || head == null)
        {
            return;
        }
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
        Debug.Log("hey button pressed woo!");
        allObjects = GameObject.FindGameObjectsWithTag("avatar");
        Debug.Log(allObjects.Length + "length");
        nearestObject = null;
        for (int i = 0; i < allObjects.Length; i++)
        {
            Debug.Log(allObjects[i].transform.position);
            distance = Vector3.Distance(this.transform.position, allObjects[i].transform.position);
            if (distance < nearestDistance)
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

        for (var i = globalAvatar.transform.childCount - 1; i >= 0; i--)
        {
            Debug.Log("for loop");

            new WaitForSeconds(2);
            Object.Destroy(globalAvatar.transform.GetChild(i).gameObject);

        }

        menuObjects[index].transform.parent = globalAvatar.transform;
        menuObjects[index].transform.position = globalAvatar.transform.position;
        menuObjects[index].transform.rotation = globalAvatar.transform.rotation;
        menuObjects[index].transform.localScale = new Vector3(1, 1, 1);
    }
}
