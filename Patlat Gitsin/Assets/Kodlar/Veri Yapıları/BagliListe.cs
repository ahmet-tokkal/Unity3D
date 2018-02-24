using UnityEngine;
using System.Collections;

public class BagliListe  
{
    Dugum Birinci = new Dugum("");
    Dugum Sonuncu = new Dugum("");

    public int sayac = 0;

    public BagliListe()
    {
        this.Birinci.Sonraki = this.Sonuncu;
        this.Sonuncu.Onceki = this.Birinci;

        this.Birinci.Onceki = null;
        this.Sonuncu.Sonraki = null;
    }

    public void Ekle(object deger)
    {
        Dugum yeniDugum = new Dugum(deger);
        yeniDugum.Sonraki = Sonuncu.Sonraki;

        Sonuncu.Onceki.Sonraki = yeniDugum;
        Sonuncu.Onceki = yeniDugum;
        yeniDugum.Sonraki = Sonuncu;

        sayac++;
    }

    public void Sil()
    {
        Birinci.Sonraki.Sonraki.Onceki = Birinci;
        Birinci.Sonraki = Birinci.Sonraki.Sonraki;

        sayac--;
    }

    public object this[int index]
    {
        get
        {
            if (index >= sayac)
            {
                return "-";
            }

            return DegerAl(index).deger;
        }
    }

    private Dugum DegerAl(int Index)
    {
        Dugum sonuc = Birinci.Sonraki; // 0.
        for (int i = 0; i < Index; i++) // 1 >= 
        {
            sonuc = sonuc.Sonraki;
        }

        return sonuc;
    }
        
}
public class Dugum 
{
    public object deger { get; set; }

    public Dugum Sonraki { get; set; }

    public Dugum Onceki { get; set; }

    public Dugum(object deger)
    {
        this.deger = deger;
        this.Sonraki = null;
        this.Onceki = null;
    }
}
