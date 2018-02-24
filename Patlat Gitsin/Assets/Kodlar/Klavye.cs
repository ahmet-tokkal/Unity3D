using UnityEngine;
using System.Collections;

public class Klavye : MonoBehaviour
{
    public UnityEngine.UI.InputField alan;
    void Start()
    {
        alan.text = "";
    }
    public void tusAl(string tus)//klavye tuşları
    {
        alan.text += tus;
    }
    public void TusSesi(bool durum)
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void Sil()//silme tuşu
    {
        if (alan.text.Length > 0)
            alan.text = alan.text.Remove(alan.text.Length - 1, 1);
    }
}
