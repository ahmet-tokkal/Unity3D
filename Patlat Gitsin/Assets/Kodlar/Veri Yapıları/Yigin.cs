using UnityEngine;
using System.Collections;

public class Yigin : MonoBehaviour 
{
    string[] dizi;
    int sayac;
    public Yigin(int boyut)
    {
        dizi = new string[boyut];
        sayac = 0;
    }
    public void Ekle(string s)
    {
        if(sayac==dizi.Length)
        {
            Debug.Log("Yığın Dolu");
            return;
        }
        dizi[sayac++] = s;
    }
    public string Cek()
    {
        if(sayac==0)
        {
            Debug.Log("Yığın Bos");
            return "0";
        }
        sayac--;
        return dizi[sayac];

    }

	
}
