using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PuanGuncelle : MonoBehaviour 
{
    public string veriTuru="harf";
    public int levelNo;
    string yuksekSkor;
    int yildizPuan;
    public Sprite yildizTex;
	// Use this for initialization
    void Start()
    {
        yuksekSkor=dbErisim.PuanCek(veriTuru,levelNo);
        yildizPuan = int.Parse(dbErisim.YildizPuanCek(veriTuru, levelNo));
        gameObject.GetComponent<Text>().text = yuksekSkor;
        PuanKontrolEt(int.Parse(yuksekSkor));
        if(int.Parse(yuksekSkor)>0)
        {
            gameObject.transform.parent.GetComponent<Button>().interactable = true;
        }
        else
            gameObject.transform.parent.GetComponent<Button>().interactable = false;


    }
    void PuanKontrolEt(int yuksekSkor)
    {
        if (yuksekSkor >= yildizPuan)
        {
            gameObject.transform.parent.GetChild(3).GetComponent<Image>().sprite = yildizTex;
            gameObject.transform.parent.GetChild(4).GetComponent<Image>().sprite = yildizTex;
            gameObject.transform.parent.GetChild(5).GetComponent<Image>().sprite = yildizTex;

        }
        else if (yuksekSkor >= (yildizPuan-20) && yuksekSkor < yildizPuan)
        {
            gameObject.transform.parent.GetChild(3).GetComponent<Image>().sprite = yildizTex;
            gameObject.transform.parent.GetChild(4).GetComponent<Image>().sprite = yildizTex;

        }
        else if (yuksekSkor >= (yildizPuan - 40) && yuksekSkor < (yildizPuan - 20))
        {
            gameObject.transform.parent.GetChild(3).GetComponent<Image>().sprite = yildizTex;
        }



    }
}
