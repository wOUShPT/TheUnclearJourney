using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VisionBehaviour : MonoBehaviour
{
    private Camera playerCamera;
    public Volume _postProcessVolume;
    public Slider staminaBarUI;
    public Animator eyesAnimator;
    private DepthOfField _depthOfField;
    private LensDistortion _LensDistortion;
    public float defaultApertureValue;
    public float focusApertureValue;
    public int defaultFOV;
    public int focusFOV;
    public float focusFOVSpeed;
    private float _currentFOV;
    public float visionCoolDownTime;
    public float visionTime;
    public float staminaAmount;
    public float staminaIncreaseTime;
    private float _staminaRegenTimer;
    private float _currentStamina;
    private float _coolDownTimer;
    private bool _canFocus;
    private bool _canRegenStamina;
    void Start()
    {
        playerCamera = Camera.main;
        _postProcessVolume.profile.TryGet(out _depthOfField);
        _postProcessVolume.profile.TryGet(out _LensDistortion);
        _depthOfField.aperture.value = defaultApertureValue;
        _LensDistortion.intensity.value = 0;
        _coolDownTimer = 0;
        _canFocus = true;
        _currentStamina = staminaAmount;
        _currentFOV = defaultFOV;
        staminaBarUI.maxValue = staminaAmount;
    }

    void Update()
    {
        if (_canFocus)
        {
            if (Input.GetKey(KeyCode.F))
            {
                eyesAnimator.SetBool("canFocus", true);
                _currentStamina -= (staminaAmount / visionTime) * Time.deltaTime;
                _currentStamina = Mathf.Clamp(_currentStamina, 0, staminaAmount);
                playerCamera = Camera.main;
                _depthOfField.aperture.value = focusApertureValue;
                _currentFOV = Mathf.RoundToInt(Mathf.SmoothStep(_currentFOV, focusFOV, focusFOVSpeed*Time.deltaTime));
                playerCamera.fieldOfView = _currentFOV;
            }
            else
            {
                eyesAnimator.SetBool("canFocus", false);
                _staminaRegenTimer += Time.deltaTime;
                if (_staminaRegenTimer >= staminaIncreaseTime)
                {
                    _canRegenStamina = true;
                }
                
                if (_canRegenStamina)
                {
                    _currentStamina += (staminaAmount / visionTime) * Time.deltaTime;
                    _currentStamina = Mathf.Clamp(_currentStamina, 0, staminaAmount);
                }
                
                _depthOfField.aperture.value = defaultApertureValue;
                _LensDistortion.intensity.value =
                    Mathf.Lerp(_LensDistortion.intensity.value, 0, 0.5f);
                _currentFOV = Mathf.RoundToInt(Mathf.SmoothStep(_currentFOV, defaultFOV, focusFOVSpeed*Time.deltaTime));
                playerCamera.fieldOfView = _currentFOV;
            }
            
            if (Input.GetKeyUp(KeyCode.F))
            {
                _canRegenStamina = false;
                _staminaRegenTimer = 0;
            }
            
            if (_currentStamina == 0)
            {
                _canFocus = false;
            }
        }
        else
        {
            eyesAnimator.SetBool("canFocus", false);
            _depthOfField.aperture.value = defaultApertureValue;
            _currentFOV = Mathf.RoundToInt(Mathf.SmoothStep(_currentFOV, defaultFOV, focusFOVSpeed*Time.deltaTime));
            playerCamera.fieldOfView = _currentFOV;
            _coolDownTimer += Time.deltaTime;
            if (_coolDownTimer >= visionCoolDownTime)
            {
                _coolDownTimer = 0;
                _canFocus = true;
                _canRegenStamina = true;
            }
        }

        staminaBarUI.value = _currentStamina;
    }

    public void AutoFocus()
    {
        StartCoroutine(Focus());
    }

    IEnumerator Focus()
    {
        while (true)
        {
            eyesAnimator.SetBool("canFocus", true);
            _currentStamina -= (staminaAmount / visionTime) * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0, staminaAmount);
            playerCamera = Camera.main;
            _depthOfField.aperture.value = focusApertureValue;
            _currentFOV = Mathf.RoundToInt(Mathf.SmoothStep(_currentFOV, focusFOV, focusFOVSpeed*Time.deltaTime));
            playerCamera.fieldOfView = _currentFOV;
            yield return null;
        }
    }
   
}
