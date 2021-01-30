using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerBehaviour : MonoBehaviour
{
    public Volume _postProcessVolume;
    private DepthOfField _depthOfField;
    public float defaultApertureValue;
    public float visionCoolDownTime;
    public float visionTime;
    private float _coolDownTimer;
    private float _visionTimer;
    private bool _canSee;
    void Start()
    {
        _postProcessVolume.profile.TryGet(out _depthOfField);
        _depthOfField.aperture.value = defaultApertureValue;
        _coolDownTimer = 0;
        _canSee = true;
    }

    void Update()
    {
        if (_canSee && Input.GetKey(KeyCode.F))
        {
            _depthOfField.aperture.value = 100f;
            _visionTimer += Time.deltaTime;
            if (_visionTimer >= visionTime)
            {
                _visionTimer = 0;
                _canSee = false;
            }
        }
        else
        {
            _depthOfField.aperture.value = 0f;
            _coolDownTimer += Time.deltaTime;
            if (_coolDownTimer >= visionCoolDownTime)
            {
                _canSee = true;
            }
        }
    }
}
