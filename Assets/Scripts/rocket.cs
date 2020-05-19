using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rocket : MonoBehaviour
{

    public player player;
    public float ms = 10;
    public int timeout = 2;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)                                 //bir nesneye degdiginde
    {
        if (collision.tag.Equals("corona"))                                             //carpilan nesne coronavirus mu?
        {
            player.makescore();                                                         //skor yap
            GetComponent<AudioSource>().Play();                                         //ses efektini cal
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;       //clonu gorunmez hale getir.
        }
    }
}








