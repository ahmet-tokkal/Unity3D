using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    public int levelNo;
    public GameObject kelime;
    public string veriTuru="Harf";
    public Canvas cnvs;
    public List<GameObject> klonlar = new List<GameObject>();
    public int kelimeSayisi = 4;
    public int kelimeDalgasi=4;
    public float uretmeSuresi = 1;
    public float baslamaSuresi = 3;
    public float dalgaSuresi = 2;
    public int toplamKelimeSayisi = 500;
    public int baslangıcHizi =-100;
    public int hiz=25;
    public int bonusPuan = 100;
    public int yildizPuan = 150;
    public InputField alan;

    public float zaman = 90;
    public Slider zamanSlider;
    public int puan = 0;
    public int yuksekSkor;
    public Text puanYazi;
    public Sprite yildizTex;

    public Slider bonusSlider;
    public Button patlatici;
    public int bonusSlidePuan;

    public GameObject bSMenu;
    public AudioSource bolumSonuMuzik;
    public GameObject oSMenu;
    public AudioSource oyunSonuMuzik;

    public GameObject arkaplanMuzik;

    public GameObject patlamaEfekt;
    public AudioSource patlatmaAktifSes;
    public AudioSource patlatmaSes;
    public GameObject konfetiEfekt;

    public bool bolumSonuMu;

    //public List<string[]> harfler = new List<string[]>();
    public BagliListe harfler;
    void Start()
    {
        harfler = new BagliListe();
        string[] veriler = new string[kelimeSayisi];
        veriler = dbErisim.VeriCek(veriTuru, kelimeSayisi,0);
        for (int i = 0; i < veriler.Length; i++)
        {
            harfler.Ekle(veriler[i]);
        }
        bolumSonuMu = false;
        //harfler.Add(dbErisim.VeriCek(veriTuru,kelimeSayisi));//Veritabanındaki harfler listeye çekildi.
        yuksekSkor = int.Parse(dbErisim.PuanCek(veriTuru,levelNo));//Veri tabanından en yüksek puan çekildi.
        Time.timeScale = 1;
        YaziHareket.hiz = baslangıcHizi;//Başlangıç hızı ayarlandı.
        zamanSlider.maxValue = zaman;//Zaman göstergesinin ilk değerinin verilmesi
        bonusSlider.maxValue = bonusPuan;
        zamanSlider.value = 0;
        StartCoroutine(KelimeUret());
    }

    void Update()
    {
        bonusSlider.value = bonusSlidePuan;
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
            if (GameObject.FindGameObjectsWithTag(veriTuru) != null)
            {
                KontrolEt();
            }

        }
        if (bonusSlidePuan == bonusPuan)
        {
            patlatmaAktifSes.Play();
            patlatici.gameObject.SetActive(true);
            bonusSlidePuan = 0;
        }

    }
    public IEnumerator KelimeUret()
    {
        
        Vector2 positionDegeri;
        Quaternion rotationDegeri;
        GameObject gameObj;
        yield return new WaitForSeconds(baslamaSuresi);//ilk kelimenin gelme süresi.
        while (bolumSonuMu==false)
        {
            
            for (int i = 0; i < kelimeDalgasi; i++)
            {
                //(Screen.height / 12), Screen.width - (Screen.height / 12)
                if (veriTuru == "Kelime" || veriTuru == "Ingilizce")
                {
                    positionDegeri = new Vector2(Random.Range(-170, 170), 500);

                }
                else
                {
                    positionDegeri = new Vector2(Random.Range(-238, 238), 500);//ekranın rastgele bir yerini seçme(yazı boyutuna göre kenarlardan öteleme yapıldı.)
                }
                rotationDegeri = Quaternion.identity;//3 boyutlu uzayda dönme hareketinin hesaplanması

                gameObj = Instantiate(kelime, positionDegeri, rotationDegeri) as GameObject;//belirtilen position ve rotation da kelimenin kopyasını oluşturma
                gameObj.tag = veriTuru;
                gameObj.transform.SetParent(cnvs.transform);//oluşturalan kopya nesne canvasın childrenı yapıldı.
                //gameObj.GetComponent<Text>().text = harfler[0][Random.Range(0, kelimeSayisi)];//diziden rastgele harfler verildi.
                gameObj.GetComponent<Text>().text = harfler[Random.Range(0, kelimeSayisi)].ToString();

                klonlar.Add(gameObj);

                yield return new WaitForSeconds(uretmeSuresi);//bir sonraki kelime için 
            }
            yield return new WaitForSeconds(dalgaSuresi);//Yeni dalga öncesi 
            YaziHareket.hiz -= hiz;
        }
    }
    public void KontrolEt()
    {
       for (int i = 0; i < klonlar.Count; i++)
       {
          
           if (klonlar[i].transform.position.y <= alan.transform.position.y + 20)//eğer kelime InputFieldın altına düşerse 
           {
               klonlar[i].GetComponent<Text>().text = "";
               OyunSonu();
               Destroy(klonlar[i]);
               klonlar.Remove(klonlar[i]);

           }
           if (alan.text.Length >= 1)
           {
               if (klonlar[i].GetComponent<Text>().text != "" && alan.text == klonlar[i].GetComponent<Text>().text)//eğer kullanıcı kelimeyi doğru girerse
               {
                   bonusSlidePuan += 5;
                   alan.text = "";
                   Patlat(klonlar[i]);
               }
                #region @
           if (alan.text == "starwars")
           {
               arkaplanMuzik.GetComponent<AudioSource>().Pause();
               gameObject.GetComponent<AudioSource>().Play();
               alan.text = "";
               StartCoroutine(StarWars());
           } 
           #endregion
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
        if(arkaplanMuzik.GetComponent<AudioSource>().isPlaying==true)
        {
            arkaplanMuzik.GetComponent<MuzikKontrol>().MuzikDurum();
        }
        if(bolumSonuMuzik.isPlaying==false)
        {
            bolumSonuMuzik.PlayOneShot(bolumSonuMuzik.clip);
        }
        
       
        bSMenu.transform.GetChild(9).GetComponent<Text>().text = puan.ToString();//puanın menüde gözükmesi
        //Time.timeScale = 0;
        bSMenu.SetActive(true);//menünün açılması.
        PuanKontrolEt(bSMenu);
        if(dbErisim.PuanCek(veriTuru,levelNo+1)=="0")
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
    #region @
    public IEnumerator StarWars()
    {
        YaziHareket.hiz = -(YaziHareket.hiz);
        yield return new WaitForSeconds(4);
        YaziHareket.hiz = -(YaziHareket.hiz);
        gameObject.GetComponent<AudioSource>().Pause();
        arkaplanMuzik.GetComponent<AudioSource>().Play();
        
        
    } 
    #endregion
    void PuanKontrolEt(GameObject menu)
    {
        //puana göre kaç yıldız alındığının hesaplanması
        if(puan>=yildizPuan)
        {
            menu.transform.GetChild(3).GetComponent<Image>().sprite = yildizTex;
            menu.transform.GetChild(4).GetComponent<Image>().sprite = yildizTex;
            menu.transform.GetChild(5).GetComponent<Image>().sprite = yildizTex;

        }
        else if (puan>=(yildizPuan-20) && puan<yildizPuan)
        {
            menu.transform.GetChild(3).GetComponent<Image>().sprite = yildizTex;
            menu.transform.GetChild(4).GetComponent<Image>().sprite = yildizTex;
            
        }
        else if (puan >= (yildizPuan - 40) && puan < (yildizPuan - 20))
        {
            menu.transform.GetChild(3).GetComponent<Image>().sprite = yildizTex;
        }
    }
    public void Patlatici()
    {
        patlatici.gameObject.SetActive(false);
        foreach(GameObject g in GameObject.FindGameObjectsWithTag(veriTuru))
        {
            patlatmaSes.Play();
            Patlat(g);
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
        alan.text = "";
        
    }
}
