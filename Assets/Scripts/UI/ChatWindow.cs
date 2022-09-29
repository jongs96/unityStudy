using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ChatWindow : MonoBehaviour
{
    public enum CHANNEL
    {
        Normal, All, Party, Guild, Trade
    }    
    public Transform myContent;
    public TMPro.TMP_InputField myInput;
    public Scrollbar myScroll;
    public TMPro.TMP_Dropdown myChannel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddChat(string str)
    {
        if (str.Length > 0)
        {
            myInput.text = string.Empty;
            GameObject obj = Instantiate(Resources.Load("Prefabs/UI/ChatMessage"), myContent) as GameObject;
            StringBuilder temp = new StringBuilder();
            ChannelMessage(temp,str);
            obj.GetComponent<ChatMessage>().SetText(temp.ToString());
            //if (myScroll.gameObject.activeSelf)
            {
                StartCoroutine(MakingScrollZero());
            }
            myInput.ActivateInputField();
        }        
    }

    void ChannelMessage(StringBuilder msg, string str)
    {
        switch((CHANNEL)myChannel.value)
        {
            case CHANNEL.Normal:
                msg.Append("<#ffffffff>");
                msg.Append("[일반]");                
                break;
            case CHANNEL.All:
                msg.Append("<#ff0000ff>");
                msg.Append("[전체]");
                break;
            case CHANNEL.Party:
                msg.Append("<#0000ffff>");
                msg.Append("[파티]");
                break;
            case CHANNEL.Guild:
                msg.Append("<#00ff00ff>");
                msg.Append("[길드]");
                break;
            case CHANNEL.Trade:
                msg.Append("<#ffff00ff>");
                msg.Append("[거래]");
                break;
        }
        msg.Append(str);
        msg.Append("</color>");        
    }

    IEnumerator MakingScrollZero()
    {
        yield return new WaitForSeconds(0.2f);
        while(myScroll.value > 0.0f)
        {
            myScroll.value -= Time.deltaTime * 3.0f;
            yield return null;
        }
    }
}
