using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class YardımBolum : MonoBehaviour {

    public Sprite[] resimler = new Sprite[6];
    int indis = 0;
    public Button sonraki;
    public Button önceki;

	public void ResimDegistir(bool durum)
    {
        if (durum) indis++;
        else indis--;

        if (indis < 0)
            indis = 0;
        else if (indis > 5)
            indis = 5;

        gameObject.GetComponent<Image>().sprite = resimler[indis];
        
    }


}
