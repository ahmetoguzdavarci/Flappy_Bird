using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oyun_Kontrol : MonoBehaviour       //oyun kontrol nesnemizin içindeyiz.
{
    public GameObject gokyuzu1;     //panelden gökyüzümüzü bu nesnemize aticaz.
    public GameObject gokyuzu2;     //panelden gökyüzümüzü bu nesnemize aticaz.

    Rigidbody2D fizik1;     //gökyüzü kompenentine ulaşmak için oluşturduğumuz değişken.
    Rigidbody2D fizik2;     //gökyüzü kompenentine ulaşmak için oluşturduğumuz değişken.

    float uzunluk = 0;      //gökyüzünün hareketini yakalamak için oluşturduumuz değişken.

    public float arka_plan_hiz = 1.5f;     //arka gökyüzünün akış hızını belirlemek için oluşturduğumuz değişken.

    public GameObject engel;    //kod içerisinde engel oluşturmak için tanımladığımız nesne. Prefabımızı içine sürükleyip bırakıcaz.

    public int kac_adet_engel=10;   //oluşturcağımız engellerin sayısını belirlemek için oluşturulan değişken.

    GameObject[] engeller;      //birden fazla engel oluşturcağımız için bunları içinde tutacağımız bir dizi oluşturuyoruz.

    float degisim_zaman = 0;    //koşulumuzun kaç saniyede bir çalışacağını ayarlamak için oluşturduğumuz değişken.

    int sayac = 0;  //engeller dizimizdeki engelleri gezmek için oluşturduğumuz değşken.

    bool oyun_bitti = false;    //oyun bittikten sonra nesne üretilmemesini sağlamak için oluşturduğumuz değişken.

    void Start()
    {
        fizik1 = gokyuzu1.GetComponent<Rigidbody2D>();      //gökyüzünün komponentini nesnemize atadık.
        fizik2 = gokyuzu2.GetComponent<Rigidbody2D>();      //gökyüzünün komponentini nesnemize atadık.


        fizik1.velocity = new Vector2(-arka_plan_hiz, 0);   //gökyüzümüze X ekseninde hareket kazandırıyoruz
        fizik2.velocity = new Vector2(-arka_plan_hiz, 0);   //gökyüzümüze X ekseninde hareket kazandırıyoruz

        uzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x;    //gökyüzümüzün hareketini sınırlandırmak için collider uzunluğunu atıyoruz.

        engeller = new GameObject[kac_adet_engel];  //dizimizi tanımlıyoruz.

        for (int i = 0; i < engeller.Length; i++)   //engellerimizi oluşturmak için döngü kullanıyoruz.
        {
            engeller[i] = Instantiate(engel, new Vector2(-20, -20), Quaternion.identity);       //engel oluşturuyoruz.

            Rigidbody2D fizik_engel = engeller[i].AddComponent<Rigidbody2D>();  //oluşan engele rigidbody komponenti ekliyoruz ve bu komponenti bir değişkene atıyoruz.

            fizik_engel.gravityScale = 0;   //engelimizin aşağı düşmemesi için yer çekimini kapatıyoruz.

            fizik_engel.velocity = new Vector2(-arka_plan_hiz, 0);   //engelimize arka planla beraber hareket etmei için aynı hızı veriyoruz.
        }
    }
     
    void Update()
    {
        if (!oyun_bitti)    //eğer oyun bitmemişse koşulumuz.
        {
            Arkaplan_Hareketi();

            Engel_Oluşturma();

        }
        
        
    }

    void Arkaplan_Hareketi()
    {
        if (gokyuzu1.transform.position.x <= -uzunluk)      //eğer gökyüzümüzün X deki konumu eksi collider uzunluğunun altına düşerse koşulu.
        {
            gokyuzu1.transform.position += new Vector3(uzunluk * 2, 0);     //gökyüzümüzün konumuna collider uzunluğunun 2 katını ekliyoruz. Böylece ilk gökyüzü
        }                                                                   //hareketini tamamladığında ikinci gökyüzünün arkasına geçip hareketine devam ediyor.

        if (gokyuzu2.transform.position.x <= -uzunluk)      //eğer gökyüzümüzün X deki konumu eksi collider uzunluğunun altına düşerse koşulu.
        {
            gokyuzu2.transform.position += new Vector3(uzunluk * 2, 0);      //gökyüzümüzün konumuna collider uzunluğunun 2 katını ekliyoruz. Böylece ikinci gökyüzü
        }                                                                   //hareketini tamamladığında ilk gökyüzünün arkasına geçip hareketine devam ediyor.
    }

    void Engel_Oluşturma()
    {
        degisim_zaman += Time.deltaTime;    //nesnemize her frame de geçen zamanı ekliyoruz.

        if (degisim_zaman > 1.5f)      //koşulumuzda zaman 2 saniyeden büyükse koşul sağlanıcak.
        {
            degisim_zaman = 0;      //tekrar koşulumuza sokmak için zamanı sıfırlıyoruz.

            float y_ekseni = Random.Range(-4f, -1.65f);     //konum vermek için öncelikle Y mizi random oluşturuyoruz.

            engeller[sayac].transform.position = new Vector3(8.75f, y_ekseni);      //oluşan engelimizin konumunu Y ekseninde random olarak ayarlıyoruz.

            sayac++;    //engeller dizimizin içerisinde gezen sayacımızı arttırıyoruz.

            if (sayac >= engeller.Length)   //sayacımız dizimizin uzunluğuna eriştiğindeki koşulumuz.
            {
                sayac = 0;      //engeller dizimizin uzunluğunu aşmasın diye sayacımızı sıfırlıyoruz.
            }
        }
    }

    public void Oyun_Bitti()   //oyun bittiğinde olucak durumları yapan fonksiyon.
    {
        for (int i = 0; i < engeller.Length; i++)   //engel nesnelerime ulaşmak için yapılan döngü.
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;    //engel nesnemiz yukarıdaki dizide oluşturulup yok olduğu için burada tekrardan oluşturuyoruz.
                                                                                //oyun bittiğinde ise engellerimizin hızlarını 0 yapıyoruz yani oyun duruyor.

            fizik1.velocity = Vector2.zero;     //hareket eden arka plandaki gökyüzünü durduruyoruz.
            fizik2.velocity = Vector2.zero;     //hareket eden arka plandaki gökyüzünü durduruyoruz.
        }

        oyun_bitti = true;      //oyun bittiyse bu kontrol değişkenimiz true olacak.
    }
}

