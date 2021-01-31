using System.Collections;
using UnityEngine;

public class CoffeeMachineTrigger : MonoBehaviour
{
    public GameObject coffeeCup;
    public GameObject coffeeCapsule;
    public GameObject coffee;
    public SpriteRenderer coffeeMachineSpriteRenderer;
    public Sprite CoffeMachineWithCup;
    public Sprite CoffeMachineWithoutCup;
    public float coffeeSpawnTime;
    public Transform coffeeSpawnTransform;
    private AudioSource _coffeSound;
    private bool coffeCupReady;
    private bool coffeeCapsuleReady;
    private bool isReady;
    private ItemInteractionBehaviour _ItemInteractionBehaviour;

    private void Awake()
    {
        _coffeSound = GetComponent<AudioSource>();
        _ItemInteractionBehaviour = FindObjectOfType<ItemInteractionBehaviour>();
        coffeCupReady = false;
        coffeeCapsuleReady = false;
        isReady = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReady == false)
        {
            if (other.gameObject == coffeeCup)
            {
                _ItemInteractionBehaviour.ResetPick();
                coffeCupReady = true;
                coffeeMachineSpriteRenderer.sprite = CoffeMachineWithCup;
                other.gameObject.SetActive(false);
            }
        
            if (other.gameObject == coffeeCapsule)
            {
                _ItemInteractionBehaviour.ResetPick();
                coffeeCapsuleReady = true;
                other.gameObject.SetActive(false);
            }

            if (coffeCupReady && coffeeCapsuleReady)
            {
                isReady = true;
                StartCoroutine(coffeeSpawner());
            }
        }
    }

    IEnumerator coffeeSpawner()
    {
        _coffeSound.Play();
        yield return new WaitForSeconds(coffeeSpawnTime);
        coffeeMachineSpriteRenderer.sprite = CoffeMachineWithoutCup;
        Instantiate(coffee, coffeeSpawnTransform.position, Quaternion.identity);
        enabled = false;
        yield return null;
    }
}
