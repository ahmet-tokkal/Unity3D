using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeriSayim : MonoBehaviour {
    public float sayac = 3.0f;
    public GameObject arkaplanMuzik;
	void Update () 
    {
        if(sayac<=3)
        {
            if (arkaplanMuzik.GetComponent<AudioSource>().isPlaying == true)
            {
                arkaplanMuzik.GetComponent<MuzikKontrol>().MuzikDurum();
            }
            sayac -= Time.deltaTime;//saniye olayı
            gameObject.GetComponent<Text>().text = sayac.ToString("0");//sayacın ekrana yazdırılması
        }
       
        if(sayac<=1)
        {
            gameObject.GetComponent<Text>().text = "Başla";
            Destroy(gameObject,0.5f);//nesnenin yok edilmesi
            if(arkaplanMuzik.GetComponent<AudioSource>().isPlaying==false)
            {
                arkaplanMuzik.GetComponent<MuzikKontrol>().MuzikDurum();
            }
        }
	}
}
