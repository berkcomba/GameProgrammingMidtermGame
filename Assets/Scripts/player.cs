using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    int i = 0;
    public int mode;
    public int score { get; set; }
    public int hscore { get; set; }
    public float ms = 10;
    public float distance;
    string axisName = "Horizontal";                                                         //A ve D tuslarina erisim icin                                          
    string axisName2 = "Jump";                                                              //Space tusuna erisim icin
    string path = "C:/Users/berk_/Desktop/gamepro/save.karabuk";                            //.karabuk uzantili save dosyasinin pathì
    public Text scoreTct, hscoreTxt;                            
    public GameObject rocket;                                                               //public objelerin atamalari unity editorunde yapildi
    public GameData gd, savedgd;                                                            
    Vector3 initialPosition = new Vector3(0, -3.95f, 0);                                    //Player objesinin ilk konumu
    public target target;


    // Start is called before the first frame update
    void Start()
    {
        mode = PlayerPrefs.GetInt("mode");                                                  //yeni oyun mu saved oyun mu? Playerprefsten aldim
        Debug.Log(mode.ToString());                                                         

        if (mode == 0)
        {
            score = 0;                                                                      //mode eger 0 ise score 0 --> yeni oyun
        }
        else
        {
           savedgd = SaveLoadManager.Load(path);                                            //Degil ise son kaydi savedgd ye at 
           score = savedgd.score+1;                                                         //score ve y degerini player objesine ata
           float y = savedgd.y;
           transform.position = new Vector3(transform.position.x, y, transform.position.z);
           scoreTct.text = score.ToString();
        }
        hscore = PlayerPrefs.GetInt("hiscore");                                             //Playerpref ten hiscoreu al ve kerana yaz
        hscoreTxt.text = hscore.ToString();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float moveAxis = Input.GetAxis(axisName) * ms;                                      //a ya da d tuslarindan hangisine basiliyor. bunu move speed ile carp
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveAxis, 0);                    //player nesnesine basilan tusa gore hiz ver
        calculatedistance();                                                                //Player hedefe cok yaklasti mi diye bak

        gd = new GameData(this);                                                            //player degerlerini daha ileride kullanabilecegi icin gdye kaydet
        if (Input.GetAxis(axisName2) == 1)                                                  //Space e basildi mi kontrol
        {
            i++;                                                                            //basili oldugu her framede fuze atmamasi icin geciktirici bir loop
            if (i >= 20)                                                                    //20 framede bir fuze
            {
                fire();
                i = 0;
            }

        }


    }


    public void fire()
    {
        GameObject clone = Instantiate(rocket.gameObject);                                  //clone yarat
        clone.transform.position = this.transform.position;                                 //cloneun konumunu playerin konumuna esitle

        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * ms;                //fuzeyi firlat
        Destroy(clone, 2f);                                                                 //2 saniye sonra fuzeyi yok et 

    }

    public void makescore()
    {
        score++;                                                                                //scoru 1 arttir                    
        scoreTct.text = score.ToString();                                                       //ekrana yaz
        try
        {
            savedgd = SaveLoadManager.Load(path);                                               //Save Dosyasi var mi diye bak
        }
        catch
        {
            Debug.Log("save dosyasi bulanamadi olusturuluyor.");                                //eger yok ise mevcut 1 scorunu kaydet ve savedgd degiskenine yukle               
            SaveLoadManager.Save(path, gd);                                                     
            savedgd = SaveLoadManager.Load(path);
        }
        if (score > savedgd.score)                                                              //mevcut skor kaydedilmis skordan buyukse mevcut durumu kaydet
        {
            SaveLoadManager.Save(path, gd);
        }


        if (score >= hscore)                                                                    //score highscoredan buyukse ekrana yaz ve playerprefslere kaydet
        {
            hscore = score;
            PlayerPrefs.SetInt("hiscore", hscore);
            hscoreTxt.text = hscore.ToString();
        }
        if (score % 5 == 0)
        {
            this.transform.position += Vector3.up;                                              //score 5 in kati ise 1 birin yukari

        }
    }

    public void calculatedistance()
    {
        distance = target.transform.position.y - transform.position.y;                          //Uzaklik hedef ile player`in y eksenlerinin farki
        if (distance < 2)
        {
            transform.position = initialPosition;                                               //eger uzaklik 2 den kucuk ise ilk konuma isinlan
        }
    }




}