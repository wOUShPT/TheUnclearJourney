using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    private Vector3 _position;
    private Transform playerTransform;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _position = transform.position;
    }
    
    void Update()
    {
        _position = transform.position;
        transform.LookAt(playerTransform);
        transform.position = new Vector3(_position.x, _position.y, transform.position.z);
    }
}
