using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSwitchSecond : MonoBehaviour
{
    public GameObject[] avatars;
    private int avatarIndex = 0;
    public GameObject[] listOfInputObjects;
    private Boolean active = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < avatars.Length; i++)
        {

           // avatars[i].SetActive(false);
        }
       // avatars[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.Tab))
         {
             avatars[avatarIndex].SetActive(false);
             avatarIndex = (avatarIndex + 1) % avatars.Length;
             avatars[avatarIndex].SetActive(true);

         } */
        if (Input.GetKeyDown(KeyCode.R) || (Input.GetKeyDown(KeyCode.Joystick1Button1)))
        {
            
            if (active)
            {
                foreach(GameObject obj in listOfInputObjects)
                {

                    obj.SetActive(false);

                }
                active = false;

            }
            else
            {
                foreach(GameObject obj in listOfInputObjects) {
                    obj.SetActive(true);
                }
                active = true;
            }

        }
    }
}
