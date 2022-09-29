using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public enum ITEMGRADE
    {
        Normal, Magic, Unique, Legend
    }
    public ITEMGRADE Grade = ITEMGRADE.Normal;
    public enum ITEMTYPE
    {
        Normal, Weapon, Amor, Potion, Quest
    }
    public ITEMTYPE Type = ITEMTYPE.Normal;
    public string Name;
    public int Price;
    [SerializeField] int _value;
    public GameObject Resource;
    public int Value
    {
        get
        {
            float modify = 1.0f;
            switch(Grade)
            {
                case ITEMGRADE.Magic:
                    modify = 1.5f;
                    break;
                case ITEMGRADE.Unique:
                    modify = 3.0f;
                    break;
                case ITEMGRADE.Legend:
                    modify = 10f;
                    break;
            }
            return (int)(_value * modify);
        }
    }
}
