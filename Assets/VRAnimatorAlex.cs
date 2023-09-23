using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorAlex : MonoBehaviour
{
    public float speedTreshold = 0.01f;
    [Range(0, 1)]
    public float smoothing = 1f;
    private Animator animatior;
    private Vector3 previousPos;
    private IKTargetFollowVRRig vrRig;

    // Start is called before the first frame update
    void Start()
    {
        animatior = GetComponent<Animator>();
        vrRig = GetComponent<IKTargetFollowVRRig>();
        previousPos = vrRig.head.vrTarget.position;
        Debug.Log("Animator initialized");
    }

    // Update is called once per frame
    void Update()
    {
        //Compute the speed
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        //Local Speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        //Set Animator Values

        float previousDirectionX = animatior.GetFloat("DirectionX");
        float previousDirectionY = animatior.GetFloat("DirectionY");

        animatior.SetBool("IsMoving", headsetLocalSpeed.magnitude > speedTreshold);
        animatior.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        Debug.Log(animatior.GetFloat("DirectionX"));
        animatior.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
    }
}
