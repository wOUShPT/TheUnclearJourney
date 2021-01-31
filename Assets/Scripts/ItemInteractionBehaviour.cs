using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ItemInteractionBehaviour : MonoBehaviour
{
    private GameController m_GameController;
    public Transform propsParent;
    public string interactibleItemTag;
    public Transform itemPickPivotTransform;
    public float interactionMinDistance;
    private PlayerMovement _playerMovement;
    private GameObject currentInteractibleItem;
    private Rigidbody _itemRb;
    private RaycastHit hit;
    private bool isInteractKeyPressed;
    private bool itemPicked;
    public ItemPickEvent _ItemPickEvent;

    private void Awake()
    {
        m_GameController = FindObjectOfType<GameController>();
        _ItemPickEvent = new ItemPickEvent();
        _ItemPickEvent.AddListener(m_GameController.UnlockDoor);
        _playerMovement = GetComponent<PlayerMovement>();
        itemPicked = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteractKeyPressed = !isInteractKeyPressed;
        }
        
        if (itemPicked && isInteractKeyPressed == false)
        {
            if (_itemRb != null)
            {
                _itemRb.useGravity = true;
                currentInteractibleItem.transform.parent = propsParent.transform;
                currentInteractibleItem = null;
                itemPicked = false;
                return;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && itemPicked == false && isInteractKeyPressed == false)
        {

            currentInteractibleItem = hit.transform.gameObject;
            
            if(currentInteractibleItem.transform.CompareTag("Coffee"))
            {
                if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position , hit.transform.position) < interactionMinDistance)
                {
                    _ItemPickEvent.Invoke(currentInteractibleItem);
                    isInteractKeyPressed = false;
                }
            }
            
            if(currentInteractibleItem.transform.CompareTag("Box"))
            {
                if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position , hit.transform.position) < interactionMinDistance)
                {
                    _ItemPickEvent.Invoke(currentInteractibleItem);
                    isInteractKeyPressed = false;
                }
            }
            
            if(currentInteractibleItem.transform.CompareTag("Slippers"))
            {
                if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position , hit.transform.position) < interactionMinDistance)
                {
                    _ItemPickEvent.Invoke(currentInteractibleItem);
                    isInteractKeyPressed = false;
                }
            }

            if (currentInteractibleItem.transform.CompareTag(interactibleItemTag))
            {
                if (Input.GetKeyDown(KeyCode.E) && itemPicked == false && Vector3.Distance(transform.position , hit.transform.position) < interactionMinDistance)
                {
                    isInteractKeyPressed = true;
                    itemPicked = true;
                    _itemRb = hit.transform.GetComponent<Rigidbody>();
                    Debug.Log("bla");
                    _ItemPickEvent.Invoke(currentInteractibleItem);
                    currentInteractibleItem = hit.transform.gameObject;
                    _itemRb.useGravity = false;
                    currentInteractibleItem.transform.position = itemPickPivotTransform.position;
                    currentInteractibleItem.transform.parent = transform;
                }
            }
            else
            {
                itemPicked = false;
                isInteractKeyPressed = false;
            }
        }
    }

    public void ResetPick()
    {
        _itemRb = null;
        currentInteractibleItem.transform.parent = propsParent.transform;
        currentInteractibleItem = null;
        itemPicked = false;
        isInteractKeyPressed = false;
    }
    
    public class ItemPickEvent : UnityEvent<GameObject>
    {
        
    }
}
