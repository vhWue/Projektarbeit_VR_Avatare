 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPostitionOffet;
    public Vector3 trackingRotationOffet;
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPostitionOffet);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffet);
    }
}
public class VRRig : MonoBehaviour
{
    // Start is called before the first frame update

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
