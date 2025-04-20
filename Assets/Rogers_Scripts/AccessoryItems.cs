using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "AccessoryItems")]
public class AccessoryItems : ScriptableObject
{
    public Sprite ItemSprite;
    public string ItemName;
    [HideInInspector]
    public bool isTaken;
    public enum specialItem { None,Diary,MagicAxe,MinotaurHorns}
    public specialItem ItemType;
    public AccessoryItems[] AllItems; //Only for the diary item

    
}


