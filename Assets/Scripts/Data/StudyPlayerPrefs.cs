using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyPlayerPrefs : MonoBehaviour
{
    public TMPro.TMP_Text myLabel;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("GameSound");
        mySlider.value = PlayerPrefs.GetFloat("GameSound");        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(float v)
    {
        myLabel.text = ((int)(v * 100.0f)).ToString();
        //int, float, string
        PlayerPrefs.SetFloat("GameSound", v);
    }
}
