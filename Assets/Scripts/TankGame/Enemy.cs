using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject orgCreateEff = null;
    public GameObject orgDestroyEff = null;
    public LayerMask crashMask;
    Vector3 dir = Vector3.zero;
    float Speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(orgCreateEff, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Speed * Time.deltaTime);
    }

    public void Initialize(Vector3 dir)
    {
        this.dir = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if((crashMask & 1 << other.gameObject.layer) != 0)
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Castle>().Damage();
            Instantiate(orgDestroyEff, transform.position, Quaternion.identity);
        }
    }
}
