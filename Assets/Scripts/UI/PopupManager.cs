using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Inst = null;
    public GameObject myNoTouch = null;
    List<Popup> list = new List<Popup>();
    private void Awake()
    {
        Inst = this;
    }
    
    public void CreatePopup(string title, string content)
    {
        myNoTouch.transform.SetAsLastSibling();
        GameObject obj = Instantiate(Resources.Load("Prefabs/UI/Popup"), transform) as GameObject;
        list.Add(obj.GetComponent<Popup>());
        list[list.Count - 1].myTitleLabel.text = title;
        list[list.Count - 1].myContent.text = content;
        myNoTouch.SetActive(true);
    }

    public void ClosePopup()
    {
        list.RemoveAt(list.Count - 1);
        if (list.Count == 0)
        {
            myNoTouch.SetActive(false);
        }
        else
        {            
            list[list.Count - 1].transform.SetAsLastSibling();
        }
    }
}
