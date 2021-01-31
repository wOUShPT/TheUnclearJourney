using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Playables;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public List<DoorBehaviour> doorBehaviours;
    public GameObject slippers;
    public GameObject key;
    public GameObject box;
    public bool isBoxClosed;
    private bool isKeyTaken;
    public Sprite openBoxSprite;
    public Sprite openBoxSpriteKeyTaken;
    public AudioSource monologuesAudioSource;
    public AudioClip fuckICantFindMyGlasses;
    public AudioClip SuchAMess;
    public AudioClip FuckMyFeetAreFreezing;
    public AudioClip AhShitIcanCantSee;
    public AudioClip WheresTheKey;
    public AudioClip AhMuchBetter;
    public AudioClip INeedToFindMySlippers;
    public AudioClip FuckOFf;
    private ItemInteractionBehaviour _ItemInteractionBehaviour;
    private ItemSound _ItemSound;
    public UnityEvent firstDoorEvent;
    public UnityEvent secondDoorEvent;
    public UnityEvent thirdDoorEvent;
    public UnityEvent slippersEvent;
    public UnityEvent drinkCoffeeEvent;
    private bool isPlayingMonologue;
    private GameObject player;
    private VisionBehaviour _VisionBehaviour;
    private PlayerMovement _PlayerMovement;
    private MouseLook _MouseLook;
    private Animator sceneTransitionAnimator;

    private void Awake()
    {
        sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        _VisionBehaviour = player.GetComponent<VisionBehaviour>();
        _PlayerMovement = player.GetComponent<PlayerMovement>();
        _MouseLook = player.GetComponent<MouseLook>();
        _ItemSound = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemSound>();
        _ItemSound.slurped.AddListener(AfterSlurpLine);
        isBoxClosed = true;
        isKeyTaken = false;
        isPlayingMonologue = false;
    }

    private void Start()
    {
        StartCoroutine(Intro());
    }

    public void UnlockDoor(GameObject item)
    {
        if (item.CompareTag("Slippers"))
        {
            doorBehaviours[0].canOpen = true;
            _ItemSound.PlayPickup();
            item.SetActive(false);
        }

        if (item.CompareTag("Coffee"))
        {
            doorBehaviours[1].canOpen = true;
            _ItemSound.PlaySlurp();
            item.SetActive(false);
        }

        if (item.CompareTag("Box"))
        {
            if (isBoxClosed)
            {
                StartCoroutine(OpenBox());
            }
            else
            {
                box.GetComponent<SpriteRenderer>().sprite = openBoxSpriteKeyTaken;
                if (isKeyTaken == false)
                {
                    _ItemSound.PlayPickupKey();
                    isKeyTaken = true;
                }
                doorBehaviours[2].canOpen = true;
            }
        }
    }

    IEnumerator OpenBox()
    {
        box.GetComponent<SpriteRenderer>().sprite = openBoxSprite;
        box.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        isBoxClosed = false;
        yield return null;
    }

    IEnumerator WaitToDeactivate(GameObject item, float time)
    {
        yield return new WaitForSeconds(time);
        item.SetActive(false);
        yield return null;
    }

    void PlayMonologue(AudioClip clip)
    {
        StartCoroutine(MonologueAudio(clip));
    }

    IEnumerator MonologueAudio(AudioClip clip)
    {
        if (!isPlayingMonologue)
        {
            monologuesAudioSource.clip = clip;
            yield return new WaitForSeconds(clip.length);
            yield return null;
        }
    }

    IEnumerator WaitAudio(AudioClip clip)
    {
        float waitTime = clip.length; 
        yield return  new WaitForSeconds(waitTime);
        monologuesAudioSource.clip = clip;
        yield return null;
    }


    public void AfterSlurpLine()
    {
        PlayMonologue(AhMuchBetter);
    }

    IEnumerator Intro()
    {
        monologuesAudioSource.clip = AhShitIcanCantSee;
        monologuesAudioSource.Play();
        float waitTime = AhShitIcanCantSee.length; 
        yield return  new WaitForSeconds(waitTime+0.1f);
        monologuesAudioSource.clip = FuckMyFeetAreFreezing;
        monologuesAudioSource.Play();
        waitTime = FuckMyFeetAreFreezing.length;
        yield return  new WaitForSeconds(waitTime+0.1f);
        monologuesAudioSource.clip = INeedToFindMySlippers;
        monologuesAudioSource.Play();
        waitTime = INeedToFindMySlippers.length; 
        yield return  new WaitForSeconds(waitTime);
        yield return null;
    }


    void EndGame()
    {
        StartCoroutine(Outro());
    }

    IEnumerator Outro()
    {
        _PlayerMovement.enabled = false;
        _MouseLook.enabled = false;
        monologuesAudioSource.clip = FuckOFf;
        monologuesAudioSource.Play();
        float waitTime = FuckOFf.length; 
        yield return  new WaitForSeconds(waitTime);
        sceneTransitionAnimator.speed = 2;
        sceneTransitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(0.6f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        yield return null;
    }
}
