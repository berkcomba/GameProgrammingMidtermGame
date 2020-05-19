using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class target : MonoBehaviour
{
    public float ms = 20;
    public Rigidbody2D rb2;
    
    void Start()
    {
        Invoke("initVelocity", 2);             //2 sn lik delay olusturmak icin
    }

    public void initVelocity()                  //ilk hiz atamasi
    {
        rb2.velocity = new Vector2(1, 0) * ms;
    }

}
