using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KilitSistemi : MonoBehaviour 
{
    
	void Update () 
    {
        GameObject btn = gameObject.transform.parent.gameObject;
        if (btn.GetComponent<Button>().interactable)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false); 
        }
	
	}
}
