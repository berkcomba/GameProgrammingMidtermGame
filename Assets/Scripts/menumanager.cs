using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menumanager : MonoBehaviour
{
    public void Load(int index)
    {
        PlayerPrefs.SetInt("mode", index);              //Hangi butuna bastigini oyun 
        SceneManager.LoadScene(0);                      //sahnesinden cekmek icin kolay bir yol
    }

}
