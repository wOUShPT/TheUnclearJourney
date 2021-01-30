using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ItemController : MonoBehaviour
{
    public string itemTag;
    public GameObject interactionPanel;
    public Image currentItemImage;
    private ItemInteractionBehaviour m_ItemInteractionBehaviour;
    private GameObject[] itemSpriteList;
    private List<SpriteRenderer> _itemSpriteRendererList;

    void Awake()
    {
        m_ItemInteractionBehaviour = FindObjectOfType<ItemInteractionBehaviour>();
        itemSpriteList = GameObject.FindGameObjectsWithTag(itemTag);
        foreach (var item in itemSpriteList)
        {
            item.AddComponent<LookToPlayer>();
            
        }
    }

    void Interact(GameObject currentItem, bool state)
    {
        if (state)
        {
            currentItemImage.sprite = currentItem.GetComponent<SpriteRenderer>().sprite;
            currentItemImage.preserveAspect = true;
            interactionPanel.gameObject.SetActive(true);
        }
        else
        {
            interactionPanel.gameObject.SetActive(false);
        }

    }
}
