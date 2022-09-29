using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform Castle;
    //3초마다 랜덤한 위치에 에너미가 생성되게 한다.    
    // Start is called before the first frame update
    //IEnumerator Create = null;
    void Start()
    {
        //Create = Creating(3.0f);
        StartCoroutine(Creating(3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        //Create.MoveNext();        
    }

    IEnumerator Creating(float t)
    {
        while (Castle != null)
        {
            yield return new WaitForSeconds(t);
            Vector3 dir = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 1.0f), 0.0f);
            GameObject obj = Instantiate(Resources.Load("Prefabs\\Enemy"), transform.position + dir, Quaternion.identity) as GameObject;
            obj.GetComponent<Enemy>().Initialize((Castle.position - obj.transform.position).normalized);            
        }
    }
}
