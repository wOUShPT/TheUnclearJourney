using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ItemInteractionBehaviour : MonoBehaviour
{
    public string interactibleItemTag;
    public Transform itemPickPivotTransform;
    public float interactionMinDistance;
    private PlayerMovement _playerMovement;
    private MouseLook _mouseLook;
    private GameObject currentInteractible;
    private RaycastHit hit;
    private bool isInteractKeyPressed;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _mouseLook = GetComponentInChildren<MouseLook>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteractKeyPressed = !isInteractKeyPressed;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(interactibleItemTag))
        {
            Rigidbody itemRb = hit.transform.GetComponent<Rigidbody>();
            
            currentInteractible = hit.transform.gameObject;

            if (currentInteractible.transform.CompareTag(interactibleItemTag))
            {
                if (isInteractKeyPressed && Vector3.Distance(transform.position , hit.transform.position) < interactionMinDistance)
                {
                    //itemInteraction.Invoke(currentItem, true);
                    currentInteractible = hit.transform.gameObject;
                    itemRb.useGravity = false;
                    currentInteractible.transform.position = itemPickPivotTransform.position;
                    currentInteractible.transform.parent = transform;
                    //_playerMovement.enabled = false;
                    //_mouseLook.enabled = false;
                    Debug.Log(currentInteractible.name);
                }
                else
                {
                    //itemInteraction.Invoke(currentItem, false);
                    currentInteractible.transform.parent = null;
                    itemRb.useGravity = true;
                    Debug.Log("bla");
                    //_playerMovement.enabled = true;
                    //_mouseLook.enabled = true;
                }
            }
        }
    }
}
