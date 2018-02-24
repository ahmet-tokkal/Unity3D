using UnityEngine;
using System.Collections;

public class Kuyruk : MonoBehaviour 
{
    string[] dizi;
    int bas;
    int son;
    public Kuyruk(int boyut)
    {
        dizi = new string[boyut];
        bas = 0;
        son = 0;
    }
    public void Ekle(string s1)
    {
        if(son==dizi.Length)
        {
            Debug.Log("Kuyruk Dolu");
            return;
        }
        dizi[son++] = s1;
    }
    public string Cek()
    {
        if(son==bas)
        {
            Debug.Log("Kuyruk bos");
            return "0";
        }
        return dizi[bas++]; 
    }

	
}
