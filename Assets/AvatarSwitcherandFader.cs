using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.XR.Management;
using Unity.Template.VR;

public class AvatarSwitcherandFader : MonoBehaviour
{
    public GameObject[] avatars;
    private int avatarIndex = 0;
    public float transitionDuration = 1.0f; 
    private float transitionTimer = 0.0f;
    private bool isTransitioning = false;
    private GameObject currentAvatar;
    private GameObject nextAvatar;

    void Start()
    {
       
        for (int i = 1; i < avatars.Length; i++)
        {
            avatars[i].SetActive(false);
        }
        currentAvatar = avatars[avatarIndex];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           
            avatarIndex = (avatarIndex + 1) % avatars.Length;
            nextAvatar = avatars[avatarIndex];
            Debug.Log("Button pressed! " + avatarIndex);
            StartCoroutine(TransitionAvatar());
        }
    }

    IEnumerator TransitionAvatar()
    {
        isTransitioning = true;
        transitionTimer = 0.0f;

      
        currentAvatar.GetComponent<IKTargetFollowVRRig>().enabled = false;
        currentAvatar.GetComponent<Animator>().enabled = false;

     
        nextAvatar.SetActive(true);

        while (transitionTimer < transitionDuration)
        {
        
            float alpha = transitionTimer / transitionDuration;

        
            Color currentAvatarColor = currentAvatar.GetComponent<Renderer>().material.color;
            Color nextAvatarColor = nextAvatar.GetComponent<Renderer>().material.color;
            currentAvatarColor.a = 1.0f - alpha;
            nextAvatarColor.a = alpha;

        
            currentAvatar.GetComponent<Renderer>().material.color = currentAvatarColor;
            nextAvatar.GetComponent<Renderer>().material.color = nextAvatarColor;

            yield return null;
            transitionTimer += Time.deltaTime;
        }

      
        currentAvatar.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        nextAvatar.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

       
        currentAvatar.SetActive(false);

      
        currentAvatar = nextAvatar;
        isTransitioning = false;

       
        currentAvatar.GetComponent<IKTargetFollowVRRig>().enabled = true;
        currentAvatar.GetComponent<Animator>().enabled = true;
    }
}
