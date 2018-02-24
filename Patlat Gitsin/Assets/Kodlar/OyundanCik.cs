using UnityEngine;
using System.Collections;

public class OyundanCik : MonoBehaviour 
{
    public void CikisSorusu(bool durum)
    {
        if (durum)
        {
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }
	
}
