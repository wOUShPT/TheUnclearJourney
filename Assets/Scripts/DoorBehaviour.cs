using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool canOpen;
    public AudioSource lockedDoor;
    public AudioSource openDoor1;
    public AudioSource openDoor2;
    void Awake()
    {
        canOpen = false;
    }

    void UnlockDoor()
    {
        canOpen = true;
    }

    public void PlayOpenSound()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            openDoor1.Play();
        }
        else
        {
            openDoor2.Play();
        }
    }

    public void PlayLockedSound()
    {
        lockedDoor.Play();
    }
}
