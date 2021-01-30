using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerBheaviour : MonoBehaviour
{
    public Volume _postProcessVolume;
    private DepthOfField _depthOfField;
    void Start()
    {
        _postProcessVolume.profile.TryGet(out _depthOfField);
    }
    
    void Update()
    {
        
    }
}
