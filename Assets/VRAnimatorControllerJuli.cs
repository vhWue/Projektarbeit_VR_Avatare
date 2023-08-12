using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorControllerJuli : MonoBehaviour
{
    private Animator animator;
    private Vector3 previousPos;
    private VRRig vrRig;

    public float speedTreshhold = 0.1f;
    public float smoothing = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;

        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed); ;
        previousPos = vrRig.head.vrTarget.position;

        float prevDirX = animator.GetFloat("DirectionX");
        float prevDirZ = animator.GetFloat("DirectionZ");

        //animator.SetBool("isMoving", headsetLocalSpeed.magnitude >= speedTreshhold);
        animator.SetFloat("DirectionX", Mathf.Lerp(prevDirX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
        animator.SetFloat("DirectionZ", Mathf.Lerp(prevDirZ, Mathf.Clamp(headsetLocalSpeed.z, -1, 1), smoothing));
    }
}
