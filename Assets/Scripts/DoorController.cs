using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public List<Animator> _doorAnimators;
    public List<DoorBehaviour> _DoorBehaviours;
    public DoorInteractionBehaviour _doorInteractionBehaviour;
    void Start()
    {
        _doorInteractionBehaviour.doorInteraction.AddListener(Open);
    }
    

    void Open(GameObject door)
    {
        for (int i = 0; i < _doorAnimators.Count; i++)
        {
            if (door.transform.parent.gameObject.transform.parent.gameObject == _doorAnimators[i].gameObject && _DoorBehaviours[i].canOpen)
            {
                _doorAnimators[i].SetBool("Open", true);
                _DoorBehaviours[i].PlayOpenSound();
            }

            if (door.transform.parent.gameObject.transform.parent.gameObject == _doorAnimators[i].gameObject &&
                !_DoorBehaviours[i].canOpen)
            {
                _DoorBehaviours[i].PlayLockedSound();
            }
        }
    }
}
