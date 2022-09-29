using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IDropHandler
{
    public Transform myIconSlot;

    public void OnDrop(PointerEventData eventData)
    {
        SkillIcon child = GetComponentInChildren<SkillIcon>();
        SkillIcon icon = eventData.pointerDrag.GetComponent<SkillIcon>();
        child?.SetParent(icon.myParent);
        icon?.SetParent(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
