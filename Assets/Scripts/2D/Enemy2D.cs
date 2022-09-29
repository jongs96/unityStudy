using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : Character2DProperty,IBattle
{
    public Transform HeadPos { get; }
    public void OnDamage(float dmg)
    {
        myAnim.SetTrigger("Damage");
    }
    public bool IsLive
    {
        get => true;
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
