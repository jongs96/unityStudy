using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Popup : MonoBehaviour
{
    public TMPro.TMP_Text myTitleLabel;
    public TMPro.TMP_Text myContent;
    public void OnClose()
    {
        PopupManager.Inst.ClosePopup();
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //myTitleLabel.text = $"Width:{Screen.width}, Height:{Screen.height}";
    }
}
