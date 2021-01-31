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
    public AudioClip WhyBathLocked;
    public AudioClip AhMuchBetter;
    public AudioClip INeedToFindMySlippers;
    public AudioClip FuckOFf;
    public AudioClip INeedCoffee;
    public AudioClip FuckYeahImGonnaFindMyGlasses;
    private ItemInteractionBehaviour _ItemInteractionBehaviour;
    private ItemSound _ItemSound;
    public UnityEvent firstDoorEvent;
    public UnityEvent secondDoorEvent;
    public UnityEvent thirdDoorEvent;
    public UnityEvent slippersEvent;
    public UnityEvent drinkCoffeeEvent;
    private bool isPlayingMonologue;
    private GameObject player;
    public VisionBehaviour _VisionBehaviour;
    public PlayerMovement _PlayerMovement;
    public MouseLook _MouseLook;
    private Animator sceneTransitionAnimator;
    private EndGame _EndGame;

    private void Awake()
    {
        sceneTransitionAnimator = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        _ItemSound = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemSound>();
        _ItemSound.slurped.AddListener(AfterSlurpLine);
        isBoxClosed = true;
        isKeyTaken = false;
        isPlayingMonologue = false;
        _EndGame = gameObject.GetComponent<EndGame>();
        _EndGame.endGame.AddListener(EndGame);
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
                    StartCoroutine(FuckYeahImFinGlasses());
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
        _VisionBehaviour.AutoFocus();
        yield return new WaitForSeconds(2f);
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


    public IEnumerator NeedCoffee()
    {
        yield return new WaitForSeconds(1);
        monologuesAudioSource.clip = INeedCoffee;
        monologuesAudioSource.Play();
        float waitTime = INeedCoffee.length; 
        yield return  new WaitForSeconds(waitTime+0.1f);
        yield return null;
    }

    public IEnumerator KeyMonologue()
    {
        monologuesAudioSource.clip = WhyBathLocked;
        monologuesAudioSource.Play();
        float waitTime = WhyBathLocked.length; 
        yield return  new WaitForSeconds(waitTime+0.2f);
        monologuesAudioSource.clip = WheresTheKey;
        monologuesAudioSource.Play();
        waitTime = WheresTheKey.length;
        yield return  new WaitForSeconds(waitTime+0.1f);
        yield return null;
    }

    public IEnumerator FuckYeahImFinGlasses()
    {
        yield return new WaitForSeconds(1);
        monologuesAudioSource.clip = FuckYeahImGonnaFindMyGlasses;
        monologuesAudioSource.Play();
        float waitTime = FuckYeahImGonnaFindMyGlasses.length; 
        yield return  new WaitForSeconds(waitTime+0.1f);
        yield return null;
    }
}
