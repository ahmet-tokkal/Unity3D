using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public  class MuzikKontrol : MonoBehaviour 
{
    public Slider muzikSlider;
    public static float muzikSes;
    public static bool muzikDurum;
    public Toggle muzikToggle;
    void Start()
    {
        int muzikDurumDb = int.Parse(dbErisim.MuzikDurumDB("Muzik", "ses"));

        if (muzikDurumDb==1)
        {
            muzikDurum = true;
            muzikToggle.isOn= true;
        }
        if (muzikDurumDb==0)
        {
            muzikDurum = false;
            muzikToggle.isOn = false;
        }
    }
    void Update()
    {
        if (muzikSlider != null)
        {
            gameObject.GetComponent<AudioSource>().volume = muzikSlider.value;
            muzikSes = muzikSlider.value;
        }
        if(muzikDurum)
        {
            if(gameObject.GetComponent<AudioSource>().isPlaying==false)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
            gameObject.GetComponent<AudioSource>().Stop();
    }
    public void MuzikDurum()
    {
        if (gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            muzikDurum = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Play();
            muzikDurum = true;
        }   
    }
    public void MuzikDbGuncelle()
    {
        if(muzikDurum)
        {
            dbErisim.MuzikGuncelle("Muzik", "ses","1");  
        }
        else
        {
            dbErisim.MuzikGuncelle("Muzik", "ses", "0");
        }
    }

    
}
