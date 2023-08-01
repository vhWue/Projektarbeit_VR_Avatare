using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMapping
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPostionOffset;
    public Vector3 trackingRotationOffset;
    
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPostionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public float turnSmoothness;
    public VRMapping head;
    public VRMapping rightHand;
    public VRMapping leftHand;

    public Transform headConstraints;
    public Vector3 headBodyOffset;
    public Transform constraints;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - constraints.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = headConstraints.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(constraints.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        rightHand.Map();
        leftHand.Map();
    }
}
