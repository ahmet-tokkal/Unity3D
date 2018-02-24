using UnityEngine;
using System.Collections;

public class SahneKontrol : MonoBehaviour 
    {
        public void SahneyiYukle(string sahnead)
        {
            Application.LoadLevel(sahnead);
        }
        public void SahneTekrar(bool durum)
        {
            if (durum)
            {
                Application.LoadLevel(Application.loadedLevel);
                Time.timeScale = 1;
            }

        }
        public void OyundanCık(bool durum)
        {
            if(durum)
            {
                Application.Quit();
            }
        }
	}

