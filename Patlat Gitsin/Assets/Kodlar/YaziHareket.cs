using UnityEngine;
using System.Collections;
public class YaziHareket : MonoBehaviour 
{
    public Camera kam;
    private Vector3 pos;
    static public float hiz = -((100*Screen.height)/888);//hızın telefonun çözünürlüğe orantılı bir şekilde ayarlanması
    
    void Start()
    {
        kam = Camera.main;
    }
	void Update () 
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * hiz ;//kelimelerin aşağı doğru hareket etmesi
	}
    public void HareketEttir()
    {
        
    }
    
    
}
