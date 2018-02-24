using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ayarlar : MonoBehaviour {

    public GameObject ayarlarCnvs;
    public GameObject inputfield;
    public Slider tusSesi;
    public Toggle tusSesiToggle;
    public Image oyunArkaplan;
    public Sprite[] renkler = new Sprite[5];
	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        inputfield.GetComponent<AudioSource>().volume = tusSesi.value;
	
	}
    public void AyarlarGoster()
    {
        ayarlarCnvs.SetActive(true);
    }
    public void AyarlarGızle()
    {
        ayarlarCnvs.SetActive(false);
    }
    public void TusSesiKontrol()
    {
        if (inputfield.GetComponent<AudioSource>().mute == false)
        {
            inputfield.GetComponent<AudioSource>().mute = true;
        }
        else
            inputfield.GetComponent<AudioSource>().mute = false; 
    }
    public void ArkaplanDegistir(string renk)
    {
        if(renk=="gri")
        {
            oyunArkaplan.GetComponent<Image>().sprite = renkler[0];
        }
        else if (renk == "yesil")
        {
            oyunArkaplan.GetComponent<Image>().sprite = renkler[1];
        }
        else if (renk == "yesil2")
        {
            oyunArkaplan.GetComponent<Image>().sprite = renkler[2];
        }
        else if (renk == "mavi")
        {
            oyunArkaplan.GetComponent<Image>().sprite = renkler[3];
        }
        else if (renk == "kırmızı")
        {
            oyunArkaplan.GetComponent<Image>().sprite = renkler[4];
        }
    }
}
