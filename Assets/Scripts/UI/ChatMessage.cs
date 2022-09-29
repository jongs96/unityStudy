using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ChatMessage : MonoBehaviour
{
    RectTransform _rect = null;
    RectTransform myRT
    {
        get 
        { 
            if(_rect == null)
            {
                _rect = GetComponent<RectTransform>();
            }
            return _rect;
        }
    }
    public TMPro.TMP_Text myLabel;    
    public void SetText(string str)
    {
        myLabel.text = str;                
        
        StringBuilder temp = new StringBuilder();
        StringBuilder res = new StringBuilder();            
        for (int i = 0; i < str.Length; ++i)
        {
            Vector2 TextSize = myLabel.GetPreferredValues(temp.ToString() + str[i]);
            if (TextSize.x > myRT.sizeDelta.x)
            {
                temp.Append('\n');
                res.Append(temp);
                temp.Clear();                    
            }
            temp.Append(str[i]);
        }
        res.Append(temp);        
        
        myRT.sizeDelta = myLabel.GetPreferredValues(res.ToString());
        myLabel.text = res.ToString();        
    }
}
