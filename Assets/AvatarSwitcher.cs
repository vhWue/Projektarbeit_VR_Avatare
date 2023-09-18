using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AvatarSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    public GameObject[] avatars;
    private int currentCameraIndex = 0;
    public GameObject[] rightHands;
    public GameObject[] leftHands;

    private void Start()
    {

        for (int i = 0; i < cameras.Length -1; i++)
        {
            cameras[i].gameObject.SetActive(false);
            avatars[i].gameObject.SetActive(false);
            rightHands[i].gameObject.SetActive(false);
            leftHands[i].gameObject.SetActive(false);
        }

        cameras[0].gameObject.SetActive(true);
       // avatars[0].gameObject.SetActive(true);
        rightHands[0].gameObject.SetActive(true);
        leftHands[0].gameObject.SetActive(true);
    }

  private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log($"Kamera before:\nIndex: {currentCameraIndex},\nCamera:{cameras[currentCameraIndex].name}");
            cameras[currentCameraIndex].gameObject.SetActive(false);
            avatars[currentCameraIndex].gameObject.SetActive(false);
            rightHands[currentCameraIndex].gameObject.SetActive(false);
            leftHands[currentCameraIndex].gameObject.SetActive(false);

            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            Debug.Log($"Kamera after:\nIndex: {currentCameraIndex},\nCamera:{cameras[currentCameraIndex].name}");

            cameras[currentCameraIndex].gameObject.SetActive(true);
            avatars[currentCameraIndex].gameObject.SetActive(true);
            rightHands[currentCameraIndex].gameObject.SetActive(true);
            leftHands[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
public class GameObjects
{

    public GameObject avatar;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject camera;

    public GameObjects(GameObject avatar, GameObject leftHand, GameObject rightHand, GameObject camera)
    {
        this.avatar = avatar;
        this.leftHand = leftHand;
        this.rightHand = rightHand;
        this.camera = camera;
    }
}
