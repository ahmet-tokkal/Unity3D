using UnityEngine;
using System.Collections;

public class MenuAyarlar : MonoBehaviour {

	public void TusSesleriniKapat()
    {
        AudioListener.volume = 0;
    }
}
