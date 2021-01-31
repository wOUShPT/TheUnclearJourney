using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSound : MonoBehaviour
{
    public AudioSource pickup1;
    public AudioSource pickup2;
    public AudioSource pickup3;
    public AudioSource slurp;
    public AudioSource key;
    public  UnityEvent slurped;
    public void PlayPickup()
    {
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            pickup1.Play();
            return;
        }

        if (random == 1)
        {
            pickup2.Play();
            return;
        }

        if (random == 2)
        {
            pickup3.Play();
        }
    }

    public void PlaySlurp()
    {
        StartCoroutine(waitForAudio());
    }

    public void PlayPickupKey()
    {
        key.Play();
    }

    IEnumerator waitForAudio()
    {
        slurp.Play();
        yield return new WaitForSeconds(slurp.clip.length);
        slurped.Invoke();
        yield return null;
    }
}
