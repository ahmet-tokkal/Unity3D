using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.UI;
using System.Drawing;



public class dbErisim: MonoBehaviour
{
    public static string baglanti;
    public static IDbConnection dbBaglanti;
    public static IDbCommand dbkomut;
    public static IDataReader oku;
    public static StringBuilder builder;
    public static string[] dizi = new string[32];


    // Use this for initialization
    void Start()
    {
        Debug.Log("SQLiteLoad yüklemeye baslandı");
        DbAc("DBpatlat.db");
        //VeriCek
    }

    public void DbAc(string p)
    {
        Debug.Log("Call to OpenDB:" + p);
        // check if file exists in Application.persistentDataPath
        string dosyaYolu = Application.persistentDataPath + "/" + p;
        #if UNITY_ANDROID
        {
            if (!File.Exists(dosyaYolu))
            {
                Debug.LogWarning("File \"" + dosyaYolu + "\" does not exist. Attempting to create from \"" +
                                 Application.dataPath + "!/assets/" + p);
                // if it doesn't ->
                // open StreamingAssets directory and load the db -> 
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
                while (!loadDB.isDone) { }
                // then save to Application.persistentDataPath
                File.WriteAllBytes(dosyaYolu, loadDB.bytes);
            }
        }
        #endif
        #if UNITY_EDITOR
        {
            Debug.Log("editör");
            baglanti = "URI=file:" + Application.dataPath + "/StreamingAssets/DBpatlat.db";
              
        }
        #endif

        
        
        //open db connection
        baglanti = "URI=file:" + dosyaYolu;
        Debug.Log("Stablishing connection to: " + baglanti);
        dbBaglanti = new SqliteConnection(baglanti);
        dbBaglanti.Open();
    }

    public static void DbKapat()
    {
        oku.Close(); // clean everything up
        oku = null;
        dbkomut.Dispose();
        dbkomut = null;
        dbBaglanti.Close();
        dbBaglanti = null;
    }



    public static string[] VeriCek(string veriTuru, int giris, int kolonNo)
    { 
        string query;
        query = "select * from "+veriTuru +" ORDER BY ID ASC LIMIT"+" "+giris;                                  
        dbkomut = dbBaglanti.CreateCommand();
        dbkomut.CommandText = query;
        oku = dbkomut.ExecuteReader();
        string[] dizi = new string[giris];
        int sayac=0;
        while (oku.Read())
        {
            Debug.Log(oku.GetString(kolonNo));
            dizi[sayac++] = oku.GetString(kolonNo);
        }
        return dizi;
    }
    public static string[] SoruCek(string veriTuru,int soruSayisi, int ilkAralik,int sonAralik, int kolonNo)
    {
        string query;
        query = "select * from " + veriTuru + " ORDER BY ID ASC LIMIT " + ilkAralik+"," + (sonAralik);
        dbkomut = dbBaglanti.CreateCommand();
        dbkomut.CommandText = query;
        oku = dbkomut.ExecuteReader();
        string[] dizi = new string[(sonAralik-ilkAralik)];
        int sayac = 0;
        while (oku.Read()&& sayac<soruSayisi)
        {
            Debug.Log(oku.GetString(kolonNo));
            dizi[sayac++] = oku.GetString(kolonNo);
        }
        return dizi;
    }
    public static void PuanGuncelle(string veriTuru,int levelNo, int puan)
    {
        string deger;
        deger = "Update "+veriTuru+"Puan set "+veriTuru+"level"+levelNo+" = '"+puan+"'";
        dbkomut = dbBaglanti.CreateCommand();
        dbkomut.CommandText = deger;
        oku = dbkomut.ExecuteReader();
    }
    public static string PuanCek(string veriTuru,int levelNo)
    {
        string deger;
        deger = "Select * from "+veriTuru+"Puan";
        dbkomut = dbBaglanti.CreateCommand();
        dbkomut.CommandText = deger;
        oku = dbkomut.ExecuteReader();
        string puan="Hata";
        while(oku.Read())
        {
            puan= oku[veriTuru+"level"+levelNo].ToString();
        }
        return puan;

    }
    public static string YildizPuanCek(string veriTuru, int levelNo)
    {
        string deger;
        deger = "Select * from " + veriTuru + "YildizPuan";
        dbkomut = dbBaglanti.CreateCommand();
        dbkomut.CommandText = deger;
        oku = dbkomut.ExecuteReader();
        string puan = "Hata";
        while (oku.Read())
        {
            puan = oku[veriTuru + "level" + levelNo].ToString();
        }
        return puan;

    } 
    public static string MuzikDurumDB(string veriTuru,string tip)
    {
        string deger;
        deger = "Select * from "+veriTuru;
        dbkomut = dbBaglanti.CreateCommand();
        dbkomut.CommandText = deger;
        oku = dbkomut.ExecuteReader();
        string puan="Hata";
        while(oku.Read())
        {
            puan= oku[tip].ToString();
        }
        return puan;

    }
    public static void MuzikGuncelle(string veriTuru, string tip,string durum)
    {
        string deger;
        deger = "Update " + veriTuru + " set " + tip + " = '" + durum + "'";
        dbkomut = dbBaglanti.CreateCommand();
        dbkomut.CommandText = deger;
        oku = dbkomut.ExecuteReader();
    }
}