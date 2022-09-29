using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform dragTarget;
    Vector2 DragOffset = Vector2.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragOffset = (Vector2)dragTarget.position - eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {        
        Vector2 pos = eventData.position + DragOffset;
        pos.x = Mathf.Clamp(pos.x, dragTarget.sizeDelta.x / 2, Screen.width - dragTarget.sizeDelta.x / 2);
        pos.y = Mathf.Clamp(pos.y, dragTarget.sizeDelta.y / 2, Screen.height - dragTarget.sizeDelta.y / 2);
        dragTarget.position = pos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {

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
