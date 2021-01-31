using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class DoorInteractionBehaviour : MonoBehaviour
{
    public float interactionMinDistance;
    public string doorTag;
    public DoorInteractionEvent doorInteraction;
    private bool _doorTriggered;
    private GameObject currentInteractible;
    private RaycastHit hit;
    private bool isInteractKeyPressed;
    
    void Awake()
    {
        doorInteraction = new DoorInteractionEvent();
    }
    
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(doorTag))
        {
            currentInteractible = hit.transform.gameObject;
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorInteraction.Invoke(currentInteractible);
            }
        }
    }

    public class DoorInteractionEvent : UnityEvent<GameObject>
    {
        
    }
}
