using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ana_Menu : MonoBehaviour
{
    public Text en_yuksek_puan_text;    //en iyi puan değerini yazdırmak için oluşturduğumuz değişkenimiz.

    public Text oyun_puani_text;        //yandığımız zaman oyundaki puanımızı ekrana yazdırmak için oluşturduğumuz değişken.
    
    void Start()
    {
        int en_yuksek_puan = PlayerPrefs.GetInt("kayit");   //başlangıçta en yüksek puanımızı değişkene atıyoruz.

        en_yuksek_puan_text.text = "BEST SCORE\n " + en_yuksek_puan;  //en iyi puanı ekrana yazdırıyoruz.

        int oyun_puani = PlayerPrefs.GetInt("puan_kayit");      //yandığımızda topladığımız puanların kaydından puanımızı değişkenimize atıyoruz.

        oyun_puani_text.text = "SCORE\n "+ oyun_puani;       //oyunda topladığımız puanı ekrana yazdırıyoruz.

        int reklam_sayaci = PlayerPrefs.GetInt("reklam_kayit");     //yüklenen reklam sayımızı değişkenimize atadık.

        if (reklam_sayaci == 5)        //yüklenen reklam sayacımız 5 olduğundaki koşulumuz.
        {
            GameObject.FindGameObjectWithTag("reklam_tag").GetComponent<reklam>().reklami_goster();     //reklam script indeki fonksiyonu çağırıp reklam gösteriyoruz.

            PlayerPrefs.SetInt("reklam_kayit", 0);      //beşincide reklamı gösterip tekrardan kayıta sıfırdan başlıyor.
        }

        //PlayerPrefs.DeleteAll();      //deneme aşamasındaki tüm kayıtları silmek için çalıştırılan kod.

    }

    void Update()
    {
       
        
    }

    public void Oyuna_Git()
    {
        SceneManager.LoadScene("level_1");      //yüklenmesini istediğimiz sahne.

    }

    public void Oyundan_Cik()
    {
        Application.Quit();     //uygulamadan çıkış komutu. 

    }
}
