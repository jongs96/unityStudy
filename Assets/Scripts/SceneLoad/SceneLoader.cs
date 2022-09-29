using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Inst = null;
    public Slider mySlider = null;
    private void Awake()
    {
        Inst = this;
        DontDestroyOnLoad(this);
    }

    public void SceneChange(int i)
    {
        StartCoroutine(Loading(i));
    }

    IEnumerator Loading(int i)
    {
        yield return SceneManager.LoadSceneAsync("Loading");
        mySlider.gameObject.SetActive(true);
        mySlider.value = 0.0f;        
        StartCoroutine(LoadingTarget(i));
    }

    IEnumerator LoadingTarget(int i)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(i);
        //씬로딩이 끝나기 전까진 씬을 활성화 하지 않는다.
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            mySlider.value = ao.progress / 0.9f;
            if(ao.progress >= 0.9f)
            {
                //씬로딩이 끝났으므로 씬을 활성화 시킨다.
                mySlider.gameObject.SetActive(false);
                ao.allowSceneActivation = true; 
            }
            yield return null;
        }        
    }
}
