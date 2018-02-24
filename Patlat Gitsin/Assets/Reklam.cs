using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api;

public class Reklam : MonoBehaviour
{
    private static Reklam instance = null;

    public string bannerID;
    public string interstitialID;

    public bool testMod = false;
    public string testDeviceID;

    private BannerView bannerReklam;
    private InterstitialAd interstitialReklam;
    public static bool interstitialDurum = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (this != instance)
            return;

        BannerReklamAl();
        InterstitialReklamAl();
        if (Application.loadedLevelName == "0-Menu")
        {
            BannerGoster();
        }
        else
        {
            BannerGizle();
        }
    }

    void BannerReklamAl()
    {
        bannerReklam = new BannerView(bannerID, AdSize.SmartBanner, AdPosition.Top);

        AdRequest reklamiAl;
        if (testMod)
            reklamiAl = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator)
                                     .AddTestDevice(testDeviceID).Build();
        else
            reklamiAl = new AdRequest.Builder().Build();

        bannerReklam.LoadAd(reklamiAl);
        bannerReklam.Hide();
    }

    void InterstitialReklamAl()
    {
        if (interstitialReklam != null)
            interstitialReklam.Destroy();

        interstitialReklam = new InterstitialAd(interstitialID);
        interstitialReklam.AdClosed -= InterstitialDelegate;
        interstitialReklam.AdClosed += InterstitialDelegate;

        AdRequest reklamiAl;
        if (testMod)
            reklamiAl = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator)
                                     .AddTestDevice(testDeviceID).Build();
        else
            reklamiAl = new AdRequest.Builder().Build();

        interstitialReklam.LoadAd(reklamiAl);
    }

    public void InterstitialDelegate(object sender, EventArgs args)
    {
        InterstitialReklamAl();
    }

    /*void OnGUI()
    {
        if( GUI.Button( new Rect( Screen.width / 2 - 150, 0, 300, 150 ), "Banner Goster" ) )
            ReklamScript.BannerGoster();
             
        if( GUI.Button( new Rect( Screen.width / 2 - 150, 150, 300, 150 ), "Banner Gizle" ) )
            ReklamScript.BannerGizle();
         
        if( GUI.Button( new Rect( Screen.width / 2 - 150, 300, 300, 150 ), "Interstitial Goster" ) )
            ReklamScript.InsterstitialGoster();
    }*/

    public static void BannerGoster()
    {
        if (instance == null)
            return;

        if (instance.bannerReklam == null)
            instance.BannerReklamAl();

        instance.bannerReklam.Show();
    }

    public static void BannerGizle()
    {
        if (instance == null)
            return;

        if (instance.bannerReklam == null)
            return;

        instance.bannerReklam.Hide();
    }

    public static void InsterstitialGoster()
    {
        if (instance == null)
            return;

        if (instance.interstitialReklam == null)
            instance.InterstitialReklamAl();
        interstitialDurum = true;
        instance.StopCoroutine(instance.InsterstitialCoroutine());
        instance.StartCoroutine(instance.InsterstitialCoroutine());
    }

    IEnumerator InsterstitialCoroutine()
    {
        while (!interstitialReklam.IsLoaded())
            yield return null;

        interstitialReklam.Show();
    }
    void Update()
    {
        if (Application.loadedLevelName != "0-Menu")
        {
            BannerGizle();
            if (Application.loadedLevelName == "2-Yardım" && interstitialDurum ==false)
            {
                InterstitialReklamAl();
                InsterstitialGoster();
            }
        }
        else
            BannerGoster();

    }
}