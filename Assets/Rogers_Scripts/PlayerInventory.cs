using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    List<AccessoryItems> possesions = new List<AccessoryItems>();
    public Image[] inven_sprites;

    private void Start()
    {
        for (int i = 0; i < inven_sprites.Length; i++)
        {
            inven_sprites[i].enabled = false;
        }
    }
    public void AddItem(AccessoryItems addedItem)
    {
        possesions.Add(addedItem);
        updateUI();
    }
    void updateUI()
    {
        for (int i = 0; i < possesions.Count; i++)
        {
            inven_sprites[i].enabled = true;
            inven_sprites[i].sprite = possesions[i].ItemSprite;
        }
    }
}
