using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class SkillIcon : UIProperty,IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler,IPointerUpHandler, IPointerClickHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent myAction;
    public Image myIcon;
    public TMPro.TMP_Text myLabel;
    bool IsCooling = false;   

    public SkillSlot myParent = null;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //���콺 ����Ʈ ���Խ�
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //���콺 ����Ʈ ����������
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //���콺 ��ư�� ��������        
        //Debug.Log("������.");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //���콺 ��ư�� ������
        //Debug.Log("����.");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //���콺 Ŭ�� ������
        if(!eventData.dragging) OnAction();
        //Debug.Log("Ŭ��.");
    }

    Vector2 dragOffset = Vector2.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //�巡�� ����
        //Debug.Log("�巡�� ����");
        dragOffset = (Vector2)transform.position - eventData.position;
        transform.SetParent(transform.parent.parent.parent);
        myIcon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�巡�� ��
        //Debug.Log("�巡�� ��: " + eventData.position);
        transform.position = eventData.position + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //�巡�� ��
        //Debug.Log("�巡�� ��");        
        transform.SetParent(myParent.myIconSlot);
        myRT.anchoredPosition = Vector2.zero;
        myIcon.raycastTarget = true;
    }

    public void OnAction()
    {
        if (!IsCooling) StartCoroutine(Cooling(10.0f));
    }

    IEnumerator Cooling(float t)
    {
        myAction?.Invoke();
        IsCooling = true;
        myIcon.fillAmount = 0.0f;        
        float speed = 1.0f / (t == 0.0f? 1.0f : t);
        myLabel.gameObject.SetActive(true);        
        while (myIcon.fillAmount < 1.0f)
        {
            t -= Time.deltaTime;
            myLabel.text = t.ToString("0.0");
            myIcon.fillAmount += Time.deltaTime * speed;            
            yield return null;
        }
        myIcon.fillAmount = 1.0f;
        IsCooling = false;
        myLabel.gameObject.SetActive(false);
    }

    public void SetParent(SkillSlot slot)
    {
        myParent = slot;
        transform.SetParent(myParent.myIconSlot);
        myRT.anchoredPosition = Vector2.zero;
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
