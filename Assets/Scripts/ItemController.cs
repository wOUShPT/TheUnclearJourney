using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public string itemTag;
    private GameObject[] itemSpriteList;
    
    void Awake()
    {
        itemSpriteList = GameObject.FindGameObjectsWithTag(itemTag);
        foreach (var item in itemSpriteList)
        {
            item.AddComponent<LookToPlayer>();
        }
    }
    
}
