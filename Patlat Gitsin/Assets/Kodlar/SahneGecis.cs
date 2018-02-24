using UnityEngine;
using System.Collections;

public class SahneGecis : MonoBehaviour {
    public float sayac = 5.0f;
    public string sahneAdi;
	void Update () 
    {
        sayac -= Time.deltaTime;
        if(sayac<=0)
        {
            Application.LoadLevel(sahneAdi);
        }
	
	}
}
