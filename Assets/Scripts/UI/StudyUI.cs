using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyUI : MonoBehaviour
{
    public Image myImage;
    public TMPro.TMP_Text myLabel;
    public Button myButton;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        myLabel.text = "<size=60><#00ff00ff>안녕</color></size>하세요!";
        //myButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mySlider.value -= 0.1f;
            if(Mathf.Approximately(mySlider.value,0.0f))
            {
                mySlider.value = 1.0f;
            }
            //myButton.interactable = !myButton.interactable;
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            PopupManager.Inst.CreatePopup("테스트", "테스트 임");
        }
    }

    public void RotateImage()
    {
        myImage.transform.Rotate(Vector3.forward * 90.0f);
    }
}
