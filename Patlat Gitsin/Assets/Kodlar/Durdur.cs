using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Durdur : MonoBehaviour 
{
    public  GameObject durdurCanvas;
   public void OyunuDurdur()
    {
        Time.timeScale = 0;
        durdurCanvas.SetActive(true);

    }
    public void DevamEt()
   {
       durdurCanvas.SetActive(false);
       Time.timeScale = 1;
   }
}
