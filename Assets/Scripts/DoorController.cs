using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public List<Animator> _doorAnimators;
    public DoorInteractionBehaviour _doorInteractionBehaviour;
    void Start()
    {
        _doorInteractionBehaviour.doorInteraction.AddListener(Open);
    }

   
    void Update()
    {
        
    }

    void Open(GameObject door)
    {
        foreach (var doorAnimator in _doorAnimators)
        {
            if (door.transform.parent.gameObject.transform.parent.gameObject == doorAnimator.gameObject)
            {
                doorAnimator.SetBool("Open", true);
            }
        }
        
    }

    void Close(GameObject door)
    {
        //_doorAnimator.SetBool("Close", false);
    }
}
