using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reklam : MonoBehaviour
{
    InterstitialAd interstitial;    //reklam sınıfımıza ulaşmak için oluşturduğumuz nesnemiz.

    static reklam reklam_kontrol;   //reklam oluştururken kullandığımız değişkenimiz.

    void Start()
    {

        if (reklam_kontrol == null)     //reklam objemiz boşsa koşulu.
        {
            DontDestroyOnLoad(gameObject);      //oyun çalışmaya başlayınca reklamların sahneden silinmesini engellemek için yazılan kod.

            reklam_kontrol = this;      //reklamımızın değerini tekrardan atıyoruz.

            //1. aşama----------------------------------------------------

        #if UNITY_ANDROID
                    string appId = "ca-app-pub-6744395249425312~4854780824";
        #elif UNITY_IPHONE
                        string appId="ca-app-pub-3940256099942544 1458002511"
        #else
                        string appId="unexpected_platform";
        #endif

            MobileAds.Initialize(appId);

            //2. aşama--------------------------------------------------------

        #if UNITY_ANDROID
                    string adUnitId = "ca-app-pub-3940256099942544/1033173712";     //test ads deki kodla değiştirildi denemek için.
        #elif UNITY_IPHONE
                        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
                        string adUnitId = "unexpected_platform";
        #endif

            interstitial = new InterstitialAd(adUnitId);

            //3. aşama-------------------------------------------------------

            //AdRequest request = new AdRequest.Builder().Build();      //gerçek aşamada kullanılacak kodlar.
            //test yapacağımız için aşağıdaki test kodlarını kullanacağız.
            //interstitial.LoadAd(request);

            AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

            interstitial.LoadAd(request);

            //4. aşama-------------------------------------------------------
        }

        else
        {
            Destroy(gameObject);        //birden fazla oluştuysa eğer reklamı siliyoruz.
        }
    }
    

    public void reklami_goster()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }

        reklam_kontrol = null;      //oyunu tekrar başlattığımızda bir daha reklam oluşturmak için yukarıdaki if koşulumuzu sağlasın diye boşaltıyoruz.
        Destroy(gameObject);        //reklam nesnemizi baştan yükleyebilmek için siliyoruz.
    }
}
