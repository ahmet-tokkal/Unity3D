using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SoruOyunKontrol : MonoBehaviour
{

    public int levelNo;
    public GameObject soru;
    public string veriTuru = "soru";
    public Canvas cnvs;
    public List<GameObject> klonlar = new List<GameObject>();
    public int soruSayisi = 15;
    public int ilkAralik = 0;
    public int sonAralik = 15;
    public int soruDalgasi = 4;
    public float uretmeSuresi = 1;
    public float baslamaSuresi = 3;
    public float dalgaSuresi = 2;
    public int toplamSoruSayisi = 500;
    public int baslangıcHizi = -100;
    public int hiz = 25;


    public float zaman = 90;
    public Slider zamanSlider;
    public int puan = 0;
    public int yuksekSkor;
    public Text puanYazi;
    public Sprite yildizTex;

    public GameObject bSMenu;
    public AudioSource bolumSonuMuzik;
    public GameObject oSMenu;
    public AudioSource oyunSonuMuzik;

    public GameObject arkaplanMuzik;

    public GameObject patlamaEfekt;
    public GameObject konfetiEfekt;

    public bool bolumSonuMu;

    public Kuyruk sorular;
    public Kuyruk cevaplar;

    // Use this for initialization
    void Start()
    {
        sorular = new Kuyruk(soruSayisi);
        cevaplar = new Kuyruk(soruSayisi);
        string[] veriler = new string[soruSayisi];
        string[] veriler2 = new string[soruSayisi];

        veriler = dbErisim.SoruCek(veriTuru, soruSayisi, ilkAralik, sonAralik, 0);
        veriler2 = dbErisim.VeriCek(veriTuru, soruSayisi, 3);
        for (int i = 0; i < veriler.Length; i++)
        {
            sorular.Ekle(veriler[i]);
            cevaplar.Ekle(veriler2[i]);
        }
        bolumSonuMu = false;
        yuksekSkor = int.Parse(dbErisim.PuanCek(veriTuru, levelNo));//Veri tabanından en yüksek puan çekildi.
        Time.timeScale = 1;
        YaziHareket.hiz = baslangıcHizi;//Başlangıç hızı ayarlandı.
        zamanSlider.maxValue = zaman;//Zaman göstergesinin ilk değerinin verilmesi
        zamanSlider.value = 0;
        StartCoroutine(SoruUret());

    }

    // Update is called once per frame
    void Update()
    {
        zaman -= Time.deltaTime;//Zamanın azalması
        zamanSlider.value = zaman + 3;//Zaman göstergesinin sürekli güncellenmesi
        if (zaman + 3 <= 0)//süre dolduğunda yapılacak işlemler
        {
            Destroy(GameObject.FindWithTag(veriTuru));
            bolumSonuMu = true;
            konfetiEfekt.SetActive(true);
            BolumSonu();
        }
        puanYazi.text = puan.ToString();//puanın ekranda gösterilmesi.
        if (bolumSonuMu == false)
        {
            if (GameObject.FindGameObjectsWithTag(veriTuru + "D") != null || GameObject.FindGameObjectsWithTag(veriTuru + "Y") != null)
            {
                KontrolEt();
            }

        }
    }
    public IEnumerator SoruUret()
    {

        Vector2 positionDegeri;
        Quaternion rotationDegeri;
        GameObject gameObj;
        yield return new WaitForSeconds(baslamaSuresi);//ilk sorunin gelme süresi.
        while (bolumSonuMu == false)
        {

            for (int i = 0; i < soruDalgasi; i++)
            {
                //(Screen.height / 12), Screen.width - (Screen.height / 12)
                positionDegeri = new Vector2(0, 500);//ekranın rastgele bir yerini seçme(yazı boyutuna göre kenarlardan öteleme yapıldı.)
                rotationDegeri = Quaternion.identity;//3 boyutlu uzayda dönme hareketinin hesaplanması

                gameObj = Instantiate(soru, positionDegeri, rotationDegeri) as GameObject;//belirtilen position ve rotation da sorunin kopyasını oluşturma
                gameObj.tag = veriTuru + cevaplar.Cek();
                gameObj.transform.SetParent(cnvs.transform);//oluşturalan kopya nesne canvasın childrenı yapıldı.
                //gameObj.GetComponent<Text>().text = harfler[0][Random.Range(0, soruSayisi)];//diziden rastgele harfler verildi.
                gameObj.transform.GetChild(0).GetComponent<Text>().text = sorular.Cek();
                klonlar.Add(gameObj);

                yield return new WaitForSeconds(uretmeSuresi);//bir sonraki soru için 
            }
            yield return new WaitForSeconds(dalgaSuresi);//Yeni dalga öncesi 
            YaziHareket.hiz -= hiz;
        }

    }
    public void KontrolEt()
    {
        for (int i = 0; i < klonlar.Count; i++)
        {
            if (klonlar[i].transform.position.y <= -500)//eğer soru InputFieldın altına düşerse 
            {
                OyunSonu();
                Destroy(klonlar[i]);
                klonlar.Remove(klonlar[i]);
            }

            if (klonlar[i].transform.position.x < -250 || klonlar[i].transform.position.x > 250)
            {
                try
                {
                    OyunSonu();
                    Destroy(klonlar[i]);
                    klonlar.Remove(klonlar[i]);
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            }
            try
            {
                if (klonlar[i].tag == veriTuru + "D")
                {
                    if (klonlar[i].transform.position.x < -125)
                    {
                        Patlat(klonlar[i]);
                    }
                }
                else if (klonlar[i].tag == veriTuru + "Y")
                {
                    if (klonlar[i].transform.position.x > 125)
                    {
                        Patlat(klonlar[i]);
                    }
                }
            }
            catch
            {

               
            }


        }

    }
    void BolumSonu()
    {
        if (puan > yuksekSkor)
        {
            yuksekSkor = puan;
            dbErisim.PuanGuncelle(veriTuru, levelNo, yuksekSkor);//puanı veri tabanına yaz.
        }
        if (arkaplanMuzik.GetComponent<AudioSource>().isPlaying == true)
        {
            arkaplanMuzik.GetComponent<MuzikKontrol>().MuzikDurum();
        }
        if (bolumSonuMuzik.isPlaying == false)
        {
            bolumSonuMuzik.PlayOneShot(bolumSonuMuzik.clip);
        }


        bSMenu.transform.GetChild(9).GetComponent<Text>().text = puan.ToString();//puanın menüde gözükmesi
        //Time.timeScale = 0;
        bSMenu.SetActive(true);//menünün açılması.
        PuanKontrolEt(bSMenu);
        if (dbErisim.PuanCek(veriTuru, levelNo + 1) == "0")
        {
            dbErisim.PuanGuncelle(veriTuru, levelNo + 1, 1);
        }
    }
    void OyunSonu()
    {
        if (puan > yuksekSkor)
        {
            yuksekSkor = puan;
            dbErisim.PuanGuncelle(veriTuru, levelNo, yuksekSkor);//puanı veri tabanına yaz.
        }
        if (arkaplanMuzik.GetComponent<AudioSource>().isPlaying == true)
        {
            arkaplanMuzik.GetComponent<MuzikKontrol>().MuzikDurum();
        }
        oyunSonuMuzik.PlayOneShot(oyunSonuMuzik.clip);
        PuanKontrolEt(oSMenu);
        oSMenu.transform.GetChild(8).GetComponent<Text>().text = puan.ToString();//puanın menüde gözükmesi
        Time.timeScale = 0;
        oSMenu.SetActive(true);//menünün açılması.
    }
    void PuanKontrolEt(GameObject menu)
    {
        //puana göre kaç yıldız alındığının hesaplanması
        if (puan >= 60)
        {
            menu.transform.GetChild(3).GetComponent<Image>().sprite = yildizTex;
            menu.transform.GetChild(4).GetComponent<Image>().sprite = yildizTex;
            menu.transform.GetChild(5).GetComponent<Image>().sprite = yildizTex;

        }
        else if (puan >= 45 && puan < 60)
        {
            menu.transform.GetChild(3).GetComponent<Image>().sprite = yildizTex;
            menu.transform.GetChild(4).GetComponent<Image>().sprite = yildizTex;

        }
        else if (puan >= 30 && puan < 45)
        {
            menu.transform.GetChild(3).GetComponent<Image>().sprite = yildizTex;
        }
    }
    public void Patlat(GameObject g)
    {
        puan += 5;
        Vector3 klonPozisyon;
        Quaternion klonRotasyon;
        GameObject klonPatlama;
        klonPozisyon = new Vector3(g.transform.position.x, g.transform.position.y, 1);
        klonRotasyon = Quaternion.identity;
        klonPatlama = Instantiate(patlamaEfekt, klonPozisyon, klonRotasyon) as GameObject;
        Destroy(g);
        Destroy(klonPatlama, 1);
        klonlar.Remove(g);
    }
}
