﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private UnityEvent endGame;
    private Transform mirrorTransform;
    private Transform PlayerTransform;
    
    void Update()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerTransform.LookAt(mirrorTransform);
        endGame.Invoke();
    }
}