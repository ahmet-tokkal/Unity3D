using UnityEngine;
using System.Collections;

public class HakkimizdaHareket : MonoBehaviour {


    public float hiz = ((100 * Screen.height) / 888);//hızın telefonun çözünürlüğe orantılı bir şekilde ayarlanması
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * hiz);
    }
}
