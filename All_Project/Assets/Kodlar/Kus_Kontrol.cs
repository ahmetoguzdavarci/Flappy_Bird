using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kus_Kontrol : MonoBehaviour        //kuşun içindeyiz.
{
    public Sprite[] kus_sprite;     //uçma hareketini yapmak için 3 tane sprite kullanıcağımız için dizi oluşturduk ve elemanları sürükleyip bırakıcaz.

    SpriteRenderer sprite_renderer;  //komponente ulaşmak için onun türünde oluşturulan nesne.

    bool ileri_geri_kontrol = true;    //kuşun hareketini kontrol için oluşturduğumuz değişken.

    int kus_sayac = 0;      //dizi üzerinde gezinmek için oluşturduğumuz değişken.

    float kus_animasyon_zamani = 0;     //if else yapısının hızını ayarlamak için kullanacağımız değişken.

    Rigidbody2D fizik;      //kuşumuzun fiziksel özelliklerine ulaşmak için oluşturduğumuz değişken.

    int puan = 0;       //oyunda geçtiğimiz engel kadar puan toplamamıza yarayacak değişkenimiz.

    public Text puan_text;  //puanımız ekranda yazdırmak için oluşturduğumuz nesne.

    bool oyun_bitti = false;    //oyunun durumunu kontrol için oluşturduğumuz değişken.

    Oyun_Kontrol oyun_kontrol;  //diğer scripte ulaşmak için onun türünden oluşturduğumuz değişken.

    AudioSource[] sesler;   //3 tane audio source komponenti miz olduğu için dizi oluşturuyoruz.

    int en_yuksek_puan = 0;     //en yüksek puanımızı tutmak için oluşturduğumuz değişken.

    int reklam_sayaci = 0;

    

    void Start()
    {
        //PlayerPrefs.DeleteAll();      //tüm kayıtları resetlemek için yazdığımız kod.

        sprite_renderer = GetComponent<SpriteRenderer>();        //nesnemize komponenti atadık.

        fizik = GetComponent<Rigidbody2D>();    //Rigidbody2D komponentini nesnemize atadık.

        oyun_kontrol = GameObject.FindGameObjectWithTag("oyun_kontrol_tag").GetComponent<Oyun_Kontrol>();   //tag iyle scripte ulaşıp, onun komponentini nesnemize atıyoruz.

        sesler = GetComponents<AudioSource>();      //dizimize komponentlerimizi atıyoruz.

        en_yuksek_puan = PlayerPrefs.GetInt("kayit");       //oyun her başladığında kayıttan en yüksek puanımızı çekiyoruz.

        //reklam olayları

        reklam_sayaci = PlayerPrefs.GetInt("rekla_kayit");      //her defasında sıfırdan başlamasın diye son kayıttan reklam sayısını çekiyoruz.
        reklam_sayaci++;        //oyun ilk çalıştığında reklam sayacımız 1 artıyor.
        PlayerPrefs.SetInt("reklam_kayit", reklam_sayaci);  //reklam sayımızı kaydediyoruz.
    }


    void Update()
    {
        Ziplama();

        Animasyon();

        Dondurme();
        

    }

    void Animasyon()    //kuşumuza kanat çırpıyo gibi efekt vericek olan fonksiyonumuz.
    {
        kus_animasyon_zamani += Time.deltaTime;     //nesnemize her frame de çalışma hızını ekliyoruz.

        if (kus_animasyon_zamani > 0.15f)   //bu zaman 0.15 den büyük olduğu zaman if içerisine giricek. Yani 0.15 saniyede bir çalışacak.
        {
            kus_animasyon_zamani = 0;   //burada tekrardan sıfırlıyoruz.

            if (ileri_geri_kontrol)     //şart sağlandığında buraya giricek.
            {
                sprite_renderer.sprite = kus_sprite[kus_sayac];     //sprite a dizimizdeki kuş resmini atıyoruz.

                kus_sayac++;        //sayacımızı 1 arttırıyoruz.

                if (kus_sayac == kus_sprite.Length)     //dizi uzunluğuna ulaştığında sayaç buraya giriyor.
                {
                    ileri_geri_kontrol = false;     //kontrol değişkenimiz false oluyor ve else durumuna geçiyor.

                    kus_sayac = kus_sayac - 1;      //son resim iki kere yüklenmesin diye 1 azaltıp else sokuyoruz.
                }
            }

            else        //şart ters olduğunda buraya giricek.
            {
                kus_sayac--;    //sayacım en son 3 te kaldığı için azaltıp diziye sokmamız gerek.

                sprite_renderer.sprite = kus_sprite[kus_sayac];     //dizimizdeki kuş resimlerini tersten atıyoruz.

                if (kus_sayac == 0)     //dizimiz bittiğinde buraya giricek.
                {
                    ileri_geri_kontrol = true;      //kontrol değişkenizmiz true olcak ve tekrardan en başa dönücez. Böylelikle kuşumuz kanat çırpıyomuş gibi olucak.

                    kus_sayac = kus_sayac + 1;      //son resim iki kere yüklenmesin diye 1 arttırıp if e sokuyoruz.
                }
            }
        }
    }

    void Ziplama()  //fare tıklandığında çalışacak fonksiyonumuz.
    {
        if (Input.GetMouseButtonDown(0) && oyun_bitti==false)        //fareye tıklandığında ve oyun bitmemişse çalışacak kod.
        {
            fizik.velocity = new Vector2(0, 0);     //yer çekimini her çalıştığında sıfırlıyoruz (daha doğrusu serbest düşüş hızını)
                                                    //yoksa sürekli arttığı için bizim uyguladığımız addforce u geçip nesneyi hareket ettiremememize neden olucak.

            fizik.AddForce(new Vector2(0, 170));    //Y ekseninde nesnemize kuvvet verdik.

            sesler[0].Play();   //birinci komponentteki ses çalıcak.

        }
    }

    void Dondurme()     //kuş yükselirken ve alçalırken yönünü çevirmesi için yazdığımız fonksiyon.
    {
        if (fizik.velocity.y > 0)   //kusumuzun hızı eğer 0 dan büyükse koşulumuz.
        {
            transform.eulerAngles = new Vector3(0, 0, 40);      //koşul sağlandığında kuş kafasını 40 erece yukarı kaldırıyor.
        }

        else if (fizik.velocity.y < 0 && fizik.velocity.y > -2.5f)  //kusumuzun hızı 0 ve -2.5 arasındaysa.
        {
            transform.eulerAngles = new Vector3(0, 0, 0);   //koşulumuz sağlandığında kuşumuz bu aralıkta yatayda düz durucak. Görsel olarak daha iyi oluyor.
        }

        else    //hızımız 0 dan küçükse koşulumuz.
        {
            transform.eulerAngles = new Vector3(0, 0, -40);     //kuşumuz aşağı düşerken hızı sıfırdan küçük olur ve kafaını 40 drece aşağı eğer.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     //kuşumuzun çarpışmasını kontrol ediyoruz.
    {
        if (collision.tag == "puan_tag")    //puan tag lı nesneye çarptığındaki koşul.
        {
            puan ++;    //puanımız 1 artıcak.

            puan_text.text = "Score " + puan;       //ekranda score ve puanımız yazıcak.

            sesler[1].Play();       //ikinci komponentteki ses çalıcak.
        }

        if (collision.tag == "engel_tag")   //engel tag lı nesneye çaptığı zamanki koşulumuz.
        {
            sesler[2].Play();       //üçüncü komponentteki ses çalıcak.

            oyun_bitti = true;      //oyun bitince durum true ya döndü.

            oyun_kontrol.Oyun_Bitti();      //oyun bitince diğer scriptten çağırılan fonksiyon.

            GetComponent<CircleCollider2D>().enabled = false;      //her çarpışmada çalıştığı için kod ses birden fazla çalıyor. Bu yüzden ilk çarpışma
                                                                   //sonrası collider ı kapatıyoruz.

            if (puan > en_yuksek_puan)      //puanımız, en yük puanımızdan büyükse koşulumuz.
            {
                //---------------------------------------------
                /*if (puan <= 5)
                {
                    SceneManager.LoadScene("bir");
                }

                else if(5 < puan && puan <= 10)
                {
                    SceneManager.LoadScene("iki");
                }

                else if(10 < puan && puan <= 100)
                {
                    SceneManager.LoadScene("uc");
                }

                else
                {
                    SceneManager.LoadScene("dort");
                }*/

                //----------------------------------------------

                en_yuksek_puan = puan;      //puanımızı, en yüksek puanımıza atıyoruz.

                PlayerPrefs.SetInt("kayit", en_yuksek_puan);        //kayit adı altında en yüksek puanımızı saklıyoruz.
            }

            Invoke("Ana_Menuye_Don", 1.5f);     //çağırılacak fonksiyonun adını ve kaç saniye sonra çalışmasını isediğimizi yazıyoruz.
        }
    }

    void Ana_Menuye_Don()       //ana menüye dönmek için yazdığımız fonksiyon.
    {
        PlayerPrefs.SetInt("puan_kayit", puan);     //yandığımız zaman oyunda aldığımız puanı kaydediyoruz ekranda göstermek için.     
        
        SceneManager.LoadScene("ana_menu");     //ana menümüze geçiş yapıyoruz.
    }
}
