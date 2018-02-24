using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuKontrol : MonoBehaviour
{
    public MenuKontrol() { }
    /*
     *  0->Programlar 
     *  1->AdreslemeModları
     *  2->Komutlar
     *  3->Register
     *  4->Bellek
     *  5->Bellek2
     *  6->Register2
    */
    public Dropdown[] dropdownlar;
    /*
     *  0->ProgramModu 
     *  1->Komutmodu
     *  2->Register
     *  3->Bellek
     *  4->Bellek2
     *  5->register2
     *  6->data
    */
    public Toggle[] togglelar;
    /*
    *  0->Data 
    *  1->008
    *  2->009
    *  3->00A
    *  4->00B
    *  5->00C
    *  6->00D
    *  7->00E
    *  8->00F
   */
    public InputField[] inputfieldlar;
    /*
     * 0->PC
     * 1->MAR
     * 2->MBR
     * 3->IR
     * 4->
     */
    public Text[] registerlar;
    /*
     * Carry
     * Parity
     * Aux
     * Zero
     * Sign
     * Trap
     * Interrupt
     * Direction
     * Overflow
     */
    public Text[] bayraklar;
    /*
     * AX
     * BX
     * CX
     * DX
     * */
    public InputField[] ozelRegisterlar1;
    /*
     * SP
     * BP
     * SI
     * DI
     * */
    public Text[] ozelRegisterlar2;

    public string[] islemler = { "ADD", "MOV", "AND" };
    public string[] islemler2 = { "MOV", "SUB", "OR" };
    public string[] islemler3 = { "MOV", "CMP", "ADD" };
    public Text[] bellekler;

    public GameObject panel;
    public GameObject btnPanelAc;

    public Animator anim;
    public string seciliRegister,seciliRegister2, seciliBellek;
    public string data;
    public Text txIslemler;
    public Text txKontrol;
    GameObject temp;
    string komut;
    int i = 0, seciliRegisterNumarasi, seciliRegisterNumarasi2, bellek, islem, tglDurum, seciliBellekNo, yontem, fark;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void DropDownDegisti()
    {
        switch (dropdownlar[1].value)
        {
            case 0:
                Debug.Log("Durum 0");
                yontem = 0;
                dropdownlar[2].ClearOptions();
                List<string> secenek = new List<string>();
                secenek.Add("ADD");
                secenek.Add("MOV");
                secenek.Add("AND");
                dropdownlar[2].AddOptions(secenek);
                togglelar[2].interactable = true;
                dropdownlar[3].interactable = true;
                togglelar[3].interactable = false;
                dropdownlar[4].interactable = false;
                togglelar[4].interactable = false;
                dropdownlar[5].interactable = false;
                togglelar[5].interactable = false;
                dropdownlar[6].interactable = false;
                togglelar[6].interactable = true;
                inputfieldlar[0].interactable = true;
                break;
            case 1:
                Debug.Log("Durum 1");
                yontem = 1;
                dropdownlar[2].ClearOptions();
                List<string> secenek2 = new List<string>();
                secenek2.Add("MOV");
                secenek2.Add("SUB");
                secenek2.Add("OR");
                dropdownlar[2].AddOptions(secenek2);
                togglelar[2].interactable = true;
                dropdownlar[3].interactable = true;
                togglelar[3].interactable = true;
                dropdownlar[4].interactable = true;
                togglelar[4].interactable = true;
                dropdownlar[5].interactable = true;
                togglelar[5].interactable = false;
                dropdownlar[6].interactable = false;
                togglelar[6].interactable = false;
                inputfieldlar[0].interactable = false;
                break;
            case 2:
                Debug.Log("Durum 2");
                yontem = 2;
                dropdownlar[2].ClearOptions();
                List<string> secenek3 = new List<string>();
                secenek3.Add("MOV");
                secenek3.Add("CMP");
                secenek3.Add("ADD");
                dropdownlar[2].AddOptions(secenek3);
                togglelar[2].interactable = true;
                dropdownlar[3].interactable = true;
                togglelar[3].interactable = true;
                dropdownlar[4].interactable = true;
                togglelar[4].interactable = true;
                dropdownlar[5].interactable = true;
                togglelar[5].interactable = true;
                dropdownlar[6].interactable = true;
                togglelar[6].interactable = false;
                inputfieldlar[0].interactable = false;
                break;

        }
    }


    public void SimulasyonuBaslat()
    {
        panel.SetActive(false);
        btnPanelAc.SetActive(true);
        if (togglelar[0].isOn)
        {
            switch (dropdownlar[0].value)
            {
                case 0:
                    islem = 0;
                    anim.SetBool("program1", true);
                    break;
                case 1:
                    islem = 1;
                    anim.SetBool("program1", true);
                    break;
                case 2:
                    anim.SetBool("program1", true);
                    islem = 2;
                    break;
                case 3:
                    anim.SetBool("program1", true);
                    islem = 3;
                    break;
                case 4:
                    anim.SetBool("program2", true);
                    islem = 4;
                    break;
                case 5:
                    anim.SetBool("program3", true);
                    islem = 5;
                    break;
            }
        }
        else
        {
            switch (dropdownlar[1].value)
            {
                case 0:
                    seciliRegister = dropdownlar[3].captionText.text;
                    seciliRegisterNumarasi = dropdownlar[3].value;
                    data = inputfieldlar[0].text;
                    switch (dropdownlar[2].value)
                    {
                        case 0:
                            Debug.Log("Immediate Adresleme modunda ADD komutu");
                            data = inputfieldlar[0].text;
                            islem = 0;
                            ImmediateIslemYap();
                            break;
                        case 1:
                            Debug.Log("Immediate Adresleme modunda MOV komutu");
                            data = inputfieldlar[0].text;
                            islem = 1;
                            ImmediateIslemYap();
                            break;
                        case 2:
                            Debug.Log("Immediate Adresleme modunda AND komutu");
                            data = inputfieldlar[0].text;
                            islem = 2;
                            ImmediateIslemYap();
                            break;
                    }
                    break;
                case 1:
                    if (togglelar[2].isOn)
                    {
                        seciliRegister = dropdownlar[3].captionText.text;
                        seciliRegisterNumarasi = dropdownlar[3].value;
                        seciliBellek = dropdownlar[5].captionText.text;
                        seciliBellekNo = dropdownlar[5].value;
                        tglDurum = 1;
                    }
                    else
                    {
                        seciliBellek = dropdownlar[4].captionText.text;
                        seciliRegister = dropdownlar[6].captionText.text;
                        seciliRegisterNumarasi = dropdownlar[6].value;
                        seciliBellekNo = dropdownlar[4].value;
                        tglDurum = 2;
                    }
                    switch (dropdownlar[2].value)
                    {
                        case 0:
                            Debug.Log("Direct Adresleme modunda MOV komutu");
                            islem = 0;
                            DirectIslemYap();
                            break;
                        case 1:
                            Debug.Log("Direct Adresleme modunda SUB komutu");
                            islem = 1;
                            DirectIslemYap();
                            break;
                        case 2:
                            Debug.Log("Direct Adresleme modunda OR komutu");
                            islem = 2;
                            DirectIslemYap();
                            break;
                    }
                    break;
                case 2:
                    if (togglelar[2].isOn)
                    {
                        seciliRegister = dropdownlar[3].captionText.text;
                        seciliRegisterNumarasi = dropdownlar[3].value;
                        if (togglelar[5].isOn)
                        {
                            seciliRegister2 = dropdownlar[6].captionText.text;
                            seciliRegisterNumarasi2 = dropdownlar[6].value;
                            tglDurum = 1;
                        }
                        else
                        {
                            seciliBellek = dropdownlar[5].captionText.text;
                            seciliBellekNo = dropdownlar[5].value;
                            tglDurum = 2;
                        }
                    }
                    else
                    {
                        seciliBellek = dropdownlar[4].captionText.text;
                        seciliBellekNo = dropdownlar[4].value;
                        seciliRegister = dropdownlar[6].captionText.text;
                        seciliRegisterNumarasi = dropdownlar[6].value;
                        tglDurum = 3;
                    }
                    switch (dropdownlar[2].value)
                    {
                        case 0:
                            Debug.Log("Indirect Adresleme modunda MOV komutu");
                            islem = 0;
                            IndirectIslemYap();
                            break;
                        case 1:
                            Debug.Log("Indirect Adresleme modunda CMP komutu");
                            islem = 1;
                            IndirectIslemYap();
                            break;
                        case 2:
                            Debug.Log("Indirect Adresleme modunda ADD komutu");
                            islem = 2;
                            IndirectIslemYap();
                            break;
                    }
                    break;
            }
        }
    }

    public void ImmediateIslemYap()
    {
        if (i > 7)
        {
            i = 0;
        }
        komut = islemler[islem] + " " + seciliRegister + ", #" + data;  // eğer baştaki add'i dropdown'dan alırsak diğer fonksiyonlara gerek kalmaz
        bellekler[i].text = komut;
        bellekler[i].color = Color.red;
        i++;
        anim.SetBool("runImmediate", true);
    }
    public void DirectIslemYap()
    {
        if (i > 7)
        {
            i = 0;
        }
        if (tglDurum == 1)
        {
            komut = islemler2[islem] + " " + seciliRegister + ", " + seciliBellek;
        }
        else
            komut = islemler2[islem] + " " + seciliBellek + ", " + seciliRegister;

        bellekler[i].text = komut;
        bellekler[i].color = Color.red;
        i++;
        anim.SetBool("runDirect", true);
    }
    public void IndirectIslemYap()
    {
        if(tglDurum==1)
        {
            temp = GameObject.FindGameObjectWithTag(ozelRegisterlar1[seciliRegisterNumarasi2].GetComponent<InputField>().text);
        }
        else if (tglDurum == 2)
        {
            temp = GameObject.FindGameObjectWithTag(inputfieldlar[1 + seciliBellekNo].GetComponent<InputField>().text);
        }
        else
            temp = GameObject.FindGameObjectWithTag(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);

        if (i > 7)
        {
            i = 0;
        }
        if (tglDurum == 1)
        {
            komut = islemler3[islem] + " " + seciliRegister + ", [" + seciliRegister2 + "]";
        }
        else if(tglDurum == 2)
        {
            komut = islemler3[islem] + " " + seciliRegister + ", [" + seciliBellek + "]";
        }
        else
            komut = islemler3[islem] + " " + seciliBellek + ", [" + seciliRegister + "]";

        bellekler[i].text = komut;
        bellekler[i].color = Color.red;
        i++;
        anim.SetBool("runIndirect", true);
    }

    public void ImmediateAdresleme(int step)
    {
        int sonuc = 0;
        IlkAdimlar(step);
        switch (step)
        {
            case 6:
                txIslemler.text += "Kontrol birimi komutu ALU'ya yönlendirdi\n";
                anim.SetBool(seciliRegister, true);
                break;
            case 7:
                switch (islem)
                {
                    case 0:
                        BayraklarıDuzenle(int.Parse(data));
                        break;
                    case 1:
                        txKontrol.text = data;
                        break;
                    case 2:
                        int x = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                        int y = int.Parse(data);
                        sonuc = x & y;
                        BayraklarıDuzenle(sonuc);
                        break;
                }
                break;
            case 8:
                string sifirlar;
                int sayi1;
                switch (islem)
                {
                    case 0:
                        txIslemler.text += seciliRegister + "'a " + data + " değeri atandı\n";
                        sifirlar = "";   // ozelregisterların başına koyacağımız sıfırlar için
                        if (ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text == "")
                        {
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = "0";
                        }
                        sayi1 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text) + int.Parse(data);
                        for (int i = 0; i < 8 - sayi1.ToString().Length; i++)
                        {
                            sifirlar += "0";
                        }
                        ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = sifirlar + sayi1.ToString();
                        ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                        txKontrol.text = sifirlar + sayi1.ToString();

                        break;
                    case 1:
                        txIslemler.text += seciliRegister + "'a " + data + " değeri taşındı\n";
                        sifirlar = "";   // ozelregisterların başına koyacağımız sıfırlar için
                        if (ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text == "")
                        {
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = "0";
                        }
                        for (int i = 0; i < 8 - data.ToString().Length; i++)
                        {
                            sifirlar += "0";
                        }
                        ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = sifirlar + data;
                        ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                        txKontrol.text = sifirlar + data;
                        break;
                    case 2:
                        int x = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                        int y = int.Parse(data);
                        sonuc = x & y;
                        txIslemler.text += seciliRegister + "'i " + data + " değeri ile And işlemi yapıldı\n";
                        sifirlar = "";   // ozelregisterların başına koyacağımız sıfırlar için
                        if (ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text == "")
                        {
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = "0";
                        }
                        for (int i = 0; i < 8 - sonuc.ToString().Length; i++)
                        {
                            sifirlar += "0";
                        }
                        ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = sifirlar + sonuc.ToString();
                        ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                        txKontrol.text = sifirlar + sonuc.ToString();
                        break;
                }
                anim.SetBool("runImmediate", false);
                anim.SetBool("exit", true);

                //anim.SetBool("runImmediate", false);
                break;
        }
    }

    public void DirectAdresleme(int step)
    {
        int sonuc = 0;
        IlkAdimlar(step);
        switch (step)
        {
            case 6:
                txIslemler.text += "Kontrol birimi komutu ALU'ya yönlendirdi\n";
                if (tglDurum == 1)
                {
                    anim.SetBool(seciliRegister, true);
                }
                else
                    anim.SetBool("bellek", true);
                break;
            case 7:
                int o1, o2;
                switch (islem)
                {
                    case 0:
                        if (tglDurum == 1)
                        {
                            txKontrol.text = inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text;
                        }
                        else
                        {
                            txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                        }

                        break;
                    case 1:
                        if (tglDurum == 1)
                        {
                            o1 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                            o2 = int.Parse(inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text);
                        }
                        else
                        {
                            o1 = int.Parse(inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text);
                            o2 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                        }
                        sonuc = o1 - o2;
                        BayraklarıDuzenle(sonuc);
                        break;
                    case 2:
                        if (tglDurum == 1)
                        {
                            o1 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                            o2 = int.Parse(inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text);
                        }
                        else
                        {
                            o1 = int.Parse(inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text);
                            o2 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                        }
                        sonuc = o1 | o2;
                        BayraklarıDuzenle(sonuc);
                        break;
                }
                break;
            case 8:
                switch (islem)
                {
                    case 0:
                        if (tglDurum == 1)
                        {
                            txIslemler.text += seciliRegister + "'a " + inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text + " değeri taşındı\n";
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text;
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                            txKontrol.text = inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text;
                        }
                        else
                        {
                            txIslemler.text += seciliBellek + " adresine " + ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text + " değeri taşındı\n";
                            inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                            txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                            inputfieldlar[seciliBellekNo + 1].GetComponentsInChildren<Text>()[1].color = Color.red;
                        }
                        break;
                    case 1:
                        if (tglDurum == 1)
                        {
                            txIslemler.text += seciliRegister + "'den " + inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text + " değeri çıkarıldı\n";
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = txKontrol.text;
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                            txKontrol.text = inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text;
                        }
                        else
                        {
                            txIslemler.text += seciliBellek + " adresinden " + ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text + " değeri çıkarıldı\n";
                            inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text = txKontrol.text;
                            txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                            inputfieldlar[seciliBellekNo + 1].GetComponentsInChildren<Text>()[1].color = Color.red;
                        }
                        break;
                    case 2:
                        if (tglDurum == 1)
                        {
                            txIslemler.text += seciliRegister + "'ile " + inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text + " değeri OR işlemi yapıldı\n";
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = txKontrol.text;
                            ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                            txKontrol.text = inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text;
                        }
                        else
                        {
                            txIslemler.text += seciliBellek + " adresi ile " + ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text + " değeri OR işlemi yapıldı\n";
                            inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text = txKontrol.text;
                            txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                            inputfieldlar[seciliBellekNo + 1].GetComponentsInChildren<Text>()[1].color = Color.red;
                        }
                        break;
                }
                anim.SetBool("runDirect", false);
                anim.SetBool("exit", true);
                txKontrol.text = registerlar[0].text;
                break;
        }
    }
    int drm = 0;
    public void IndirectAdresleme(int step)
    {
        //int sonuc = 0;
        IlkAdimlar(step);
        switch (step)
        {
            case 6:
                txIslemler.text += "Kontrol birimi komutu ALU'ya yönlendirdi\n";
                if (tglDurum == 1)
                {
                    anim.SetBool(seciliRegister2, true);
                }
                else if (tglDurum == 2)
                {
                    anim.SetBool("bellek", true);
                }
                else
                {
                    anim.SetBool(seciliRegister, true);
                    anim.SetBool(seciliRegister + "geri", true);
                    anim.SetBool("bellek", true);
                }
                break;
            case 7:
                if (tglDurum == 1)
                {
                    txKontrol.text = seciliRegister2;
                    anim.SetBool(seciliRegister2 + "geri", true);
                }
                else if (tglDurum == 2)
                {
                    txKontrol.text = seciliBellek;
                    anim.SetBool("bellekGeri", true);
                    anim.SetBool("bellek", true);
                }
                else
                {
                    txKontrol.text = seciliRegister;
                    anim.SetBool(seciliRegister, false);
                }
                break;
            case 8:
                Debug.Log("8");
                if (drm==0)
                {
                    drm++;
                    if (tglDurum == 1)
                    {
                        Debug.Log("8-0");
                        txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi2].GetComponent<InputField>().text;
                        anim.SetBool("bellek", true);
                        anim.SetBool("bellekGeri", true);
                        anim.SetBool(seciliRegister2, false);
                        IndirectAdresleme(9);
                    }
                    else if (tglDurum == 2)
                    {
                        txKontrol.text = inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text;
                        anim.SetBool("bellekGeri", false);
                    }
                    else
                    {
                        txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                    }
                }
                else if(drm==1)
                {
                    Debug.Log("8-1");
                    txKontrol.text = temp.GetComponent<InputField>().text;
                    drm++;
                    if (tglDurum == 2 )
                    {
                        anim.SetBool(seciliRegister, false);
                        anim.SetBool("bellekGeri", true);
                        IndirectAdresleme(9);
                    }
                    else if(tglDurum == 3)
                    {
                        anim.SetBool("bellekGeri", true);
                        IndirectAdresleme(9);
                    }
                }
                else
                {
                    int o1, o2, sonuc;

                    if (tglDurum == 1)
                    {
                        switch (islem)
                        {
                            case 0:
                                txIslemler.text += seciliRegister + "'a " + seciliRegister2 + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri taşındı\n";
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = temp.GetComponent<InputField>().text;
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                                txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                                break;
                            case 1:
                                o1 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                                o2 = int.Parse(temp.GetComponent<InputField>().text);
                                txIslemler.text += seciliRegister + "'a " + seciliRegister2 + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri karşılaştırıldı\n";
                                sonuc = o1 - o2;
                                BayraklarıDuzenle(sonuc);
                                break;
                            case 2:
                                o1 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                                o2 = int.Parse(temp.GetComponent<InputField>().text);
                                txIslemler.text += seciliRegister + "'a " + seciliRegister2 + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri eklendi\n";
                                sonuc = o1 + o2;
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = sonuc.ToString();
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                                BayraklarıDuzenle(sonuc);
                                break; 
                        }
                        anim.SetBool(seciliRegister, true);
                        txKontrol.text = registerlar[0].text;
                        anim.SetBool("exit", true);
                        
                    }
                    else if(tglDurum==2)
                    {
                        switch (islem)
                        {
                            case 0:
                                txIslemler.text += seciliRegister + "'a " + seciliBellek + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri taşındı\n";
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = temp.GetComponent<InputField>().text;
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                                txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text;
                                break;
                            case 1:
                                o1 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                                o2 = int.Parse(temp.GetComponent<InputField>().text);
                                txIslemler.text += seciliRegister + "'a " + seciliBellek + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri karşılaştırıldı\n";
                                sonuc = o1 - o2;
                                BayraklarıDuzenle(sonuc);
                                break;
                            case 2:
                                o1 = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                                o2 = int.Parse(temp.GetComponent<InputField>().text);
                                txIslemler.text += seciliRegister + "'a " + seciliBellek + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri eklendi\n";
                                sonuc = o1 + o2;
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text = sonuc.ToString();
                                ozelRegisterlar1[seciliRegisterNumarasi].GetComponentsInChildren<Text>()[1].color = Color.white;
                                BayraklarıDuzenle(sonuc);
                                break;
                        }
                        anim.SetBool("runIndirect", false);
                        anim.SetBool(seciliRegister, true);
                        anim.SetBool("exit", true);
                        txKontrol.text = registerlar[0].text;
                    }
                   else
                    {
                        switch (islem)
                        {
                            case 0:
                                txIslemler.text += seciliBellek + "'a " + seciliRegister + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri taşındı\n";
                                inputfieldlar[1 + seciliBellekNo].GetComponent<InputField>().text = temp.GetComponent<InputField>().text;
                                inputfieldlar[1 + seciliBellekNo].GetComponentsInChildren<Text>()[1].color = Color.red;
                                txKontrol.text = inputfieldlar[1 + seciliBellekNo].GetComponent<InputField>().text;
                                break;
                            case 1:
                                o1 = int.Parse(inputfieldlar[seciliBellekNo + 1].GetComponent<InputField>().text);
                                o2 = int.Parse(temp.GetComponent<InputField>().text);
                                txIslemler.text += seciliBellek + "'a " + seciliRegister + "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri karşılaştırıldı\n";
                                sonuc = o1 - o2;
                                Debug.Log("->"+sonuc);
                                BayraklarıDuzenle(sonuc);
                                break;
                            case 2:
                                o1 = int.Parse(inputfieldlar[seciliBellekNo+1].GetComponent<InputField>().text);
                                o2 = int.Parse(temp.GetComponent<InputField>().text);
                                txIslemler.text += seciliBellek + "'a " + seciliRegister+ "'de tutulan bellek adresinin " + temp.GetComponent<InputField>().text + " değeri eklendi\n";
                                sonuc = o1 + o2;
                                inputfieldlar[1+seciliBellekNo].GetComponent<InputField>().text= sonuc.ToString();
                                inputfieldlar[1 + seciliBellekNo].GetComponentsInChildren<Text>()[1].color = Color.red;
                                BayraklarıDuzenle(sonuc);
                                break;
                        }
                        txKontrol.text = registerlar[0].text;
                    }
                }
                break;
            case 9:
                if (tglDurum == 1)
                {
                    Debug.Log("9");
                    txKontrol.text = ozelRegisterlar1[seciliRegisterNumarasi2].GetComponent<InputField>().text;
                    anim.SetBool("runIndirect", false);
                    anim.SetBool(seciliRegister, true);
                }
                else if (tglDurum == 2)
                {
                    txKontrol.text = temp.GetComponent<InputField>().text;
                    anim.SetBool(seciliRegister, true);
                }
                else
                {
                    txKontrol.text = temp.GetComponent<InputField>().text;
                }
                break;
        }
    }
    public void AnimOynat()
    {
        if (dropdownlar[1].value == 0)
        {
            ImmediateAdresleme(8);
        }
        else if (dropdownlar[1].value == 1)
        {
            DirectAdresleme(8);
        }
        else
        {
            IndirectAdresleme(8);
        }
    }
    public void IndirectAnimOynat()
    {
        IndirectAdresleme(9);
    }
    public void IlkAdimlar(int step)
    {
        switch(step)
        {
            case 0:
                txKontrol.text = registerlar[0].text;
                txIslemler.text = komut + " belleğe yazıldı\n";
                break;
            case 1:
                registerlar[1].text = txKontrol.text;
                registerlar[1].color = Color.white;
                txIslemler.text += "Komut adresi adres bus'a yerleştirildi\n";
                txIslemler.text += "PC içeriği MAR'a kopyalandı\n";
                break;
            case 2:
                int temp = int.Parse(registerlar[0].text);
                registerlar[0].text = "0000000" + (++temp).ToString();
                registerlar[0].color = Color.blue;
                txKontrol.text = komut;
                txIslemler.text += "PC arttırıldı\n";
                break;
            case 3:
                txIslemler.text += "Komut Data Bus'ta\n";
                registerlar[2].text = txKontrol.text;
                registerlar[2].color = Color.white;
                txIslemler.text += "Komut MBR'ye yüklendi\n";
                break;
            case 4:
                registerlar[3].text = txKontrol.text;
                registerlar[3].color = Color.blue;
                txIslemler.text += "Komut IR'ye yüklendi\n";
                break;
            case 5:
                txIslemler.text += "Komut Instruction Decoder'da çözülüyor\n";
                break;
        }
    }

    public void ToggleDurumlar(int tgl)
    {
        switch (yontem)
        {
            case 0:

            case 1:
                switch (tgl)
                {
                    case 0:
                        togglelar[5].interactable = false;
                        togglelar[4].interactable = true;
                        togglelar[5].isOn = false;
                        togglelar[4].isOn = true;
                        break;
                    case 1:
                        togglelar[5].interactable = true;
                        togglelar[4].interactable = false;
                        togglelar[5].isOn = true;
                        togglelar[4].isOn = false;
                        break;
                }
                break;
            case 2:
                switch (tgl)
                {
                    case 0:
                        togglelar[5].interactable = true;
                        togglelar[4].interactable = true;
                        togglelar[5].isOn = true;
                        togglelar[4].isOn = false;
                        dropdownlar[4].interactable = false;
                        break;
                    case 1:
                        togglelar[5].interactable = true;
                        togglelar[4].interactable = false;
                        togglelar[5].isOn = true;
                        togglelar[4].isOn = false;
                        dropdownlar[3].interactable = false;
                        dropdownlar[5].interactable = false;
                        break;
                }
                break;
            default:
                break;
        }
    }
    public void Resetle()
    {
        SceneManager.LoadScene(0);
       
    }

    public void Kapat()
    {
        Application.Quit();
    }

    public void BellekGeriFalse()
    {
        if(tglDurum==3)
        {
            anim.SetBool("bellekGeri", false);
            anim.SetBool("runIndirect", false);
            anim.SetBool("exit",true);
            Debug.Log(anim.GetBool("runIndirect"));
        }
    }

    public void BayraklarıDuzenle(int sonuc)
    {
        txIslemler.text += "Carry Flag resetlendi\n";
        if (sonuc % 2 == 0)
        {
            txIslemler.text += "Parity Flag SET edildi\n";
            bayraklar[1].text = "1";
            bayraklar[1].color = Color.white;
        }
        else
        {
            txIslemler.text += "Parity Flag resetlendi\n";
            bayraklar[1].text = "0";
        }
        txIslemler.text += "Auxiliary Flag resetlendi\n";
        if (sonuc == 0)
        {
            txIslemler.text += "Zero Flag SET edildi\n";
            bayraklar[3].text = "1";
            bayraklar[3].color = Color.white;
        }
        else
        {
            txIslemler.text += "Zero Flag resetlendi\n";
            bayraklar[3].text = "0";
        }
        if (sonuc < 0)
        {
            txIslemler.text += "Sign Flag SET edildi\n";
            bayraklar[4].text = "1";
            sonuc *= -1;
            bayraklar[4].color = Color.white;
        }
        else
        {
            txIslemler.text += "Sign Flag resetlendi\n";
            bayraklar[4].text = "0";
        }
        txIslemler.text += "Tarp Flag resetlendi\n";
        txIslemler.text += "Interrupt Flag resetlendi\n";
        txIslemler.text += "Direction Flag resetlendi\n";
        txIslemler.text += "Overflow Flag resetlendi\n";
        txKontrol.text = sonuc.ToString();
    }

    public void ProgramModu(int step)
    {
        switch (islem)
        {
            case 0:
                ProgramModundakiAdimlar(step);
                switch (step)
                {
                    case 0:
                        txKontrol.text = registerlar[0].text;
                        bellekler[0].text = "MOV AX, #F8";
                        bellekler[1].text = "MOV BX, #0F";
                        bellekler[2].text = "ADD AX, BX";
                        txIslemler.text = "Komutlar belleğe yazıldı\n";
                        break;
                    case 7:
                        txIslemler.text += "AX'e F8 değeri atandı\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = "F8";
                        break;
                    case 8:
                        txIslemler.text += "BX'e 0F değeri atandı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "0F";
                        break;
                    case 9:
                        txIslemler.text += "AX'e BX eklendi\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "07";
                        anim.SetBool("program1", false);
                        break;
                   
                }
                break;
            case 1:
                ProgramModundakiAdimlar(step);
                switch (step)
                {
                    case 0:
                        txKontrol.text = registerlar[0].text;
                        bellekler[0].text = "MOV AX, #10";
                        bellekler[1].text = "MOV BX, #20";
                        bellekler[2].text = "SUB AX, BX";
                        txIslemler.text = "Komutlar belleğe yazıldı\n";
                        break;
                    case 7:
                        txIslemler.text += "AX'e 10 değeri atandı\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = "10";
                        break;
                    case 8:
                        txIslemler.text += "BX'e 20 değeri atandı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "20";
                        break;
                    case 9:
                        txIslemler.text += "AX'e BX çıkarıldı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "10";
                        BayraklarıDuzenle(-10);
                        anim.SetBool("program1", false);
                        break;

                }
                break;
            case 2:
                ProgramModundakiAdimlar(step);
                switch (step)
                {
                    case 0:
                        txKontrol.text = registerlar[0].text;
                        bellekler[0].text = "MOV AX, #EA";
                        bellekler[1].text = "MOV BX, #15";
                        bellekler[2].text = "XOR AX, BX";
                        txIslemler.text = "Komutlar belleğe yazıldı\n";
                        break;
                    case 7:
                        txIslemler.text += "AX'e EA değeri atandı\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = "EA";
                        break;
                    case 8:
                        txIslemler.text += "BX'e 15 değeri atandı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "15";
                        break;
                    case 9:
                        txIslemler.text += "AX ile BX arasında XOR işlemi yapıldı\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = "FF";
                        anim.SetBool("program1", false);
                        break;

                }
                break;
            case 3:
                ProgramModundakiAdimlar(step);
                switch (step)
                {
                    case 0:
                        txKontrol.text = registerlar[0].text;
                        bellekler[0].text = "MOV BX, #AF";
                        bellekler[1].text = "MOV CX, #40";
                        bellekler[2].text = "AND BX, CX";
                        txIslemler.text = "Komutlar belleğe yazıldı\n";
                        break;
                    case 7:
                        txIslemler.text += "BX'e AF değeri atandı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "AF";
                        break;
                    case 8:
                        txIslemler.text += "CX'e 40 değeri atandı\n";
                        ozelRegisterlar1[2].GetComponent<InputField>().text = "40";
                        break;
                    case 9:
                        txIslemler.text += "BX ile CX arasında AND işlemi yapıldı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "0";
                        BayraklarıDuzenle(0);
                        anim.SetBool("program1", false);
                        break;

                }
                break;
            case 4:
                ProgramModundakiAdimlar(step);
                switch (step)
                {
                    case 0:
                        txKontrol.text = registerlar[0].text;
                        bellekler[0].text = "MOV AX, #008";
                        bellekler[1].text = "MOV BX, #009";
                        bellekler[2].text = "CMP AX, BX";
                        bellekler[3].text = "JGE 006";
                        bellekler[4].text = "SUB BX, AX";
                        bellekler[5].text = "JMP 007";
                        bellekler[6].text = "SUB AX, BX";
                        bellekler[7].text = "HALT";
                        txIslemler.text = "Komutlar belleğe yazıldı\n";
                        break;
                    case 7:
                        txIslemler.text += "AX'e 008 değeri atandı\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = inputfieldlar[1].GetComponent<InputField>().text;
                        txKontrol.text = "0000000" + i;
                        break;
                    case 8:
                        txIslemler.text += "BX'e 009 değeri atandı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = inputfieldlar[2].GetComponent<InputField>().text;
                        txKontrol.text = "0000000" + i;
                        break;
                    case 9:
                        txIslemler.text += "AX ile BX arasında CMP işlemi yapıldı\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = (int.Parse(inputfieldlar[1].GetComponent<InputField>().text) - int.Parse(inputfieldlar[2].GetComponent<InputField>().text)).ToString();
                        BayraklarıDuzenle(int.Parse(ozelRegisterlar1[1].GetComponent<InputField>().text));
                        txKontrol.text = "0000000" + i;
                        break;
                    case 10:
                        if (int.Parse(ozelRegisterlar1[1].GetComponent<InputField>().text) >= 0)
                        {
                            txIslemler.text += "006 adresine dallandı\n";
                            i = 6;
                            txKontrol.text = "0000000" + i;
                        }
                        break;
                    case 11:
                        txIslemler.text += "AX'e BX çıkarıldı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = ozelRegisterlar1[1].GetComponent<InputField>().text;
                        BayraklarıDuzenle(int.Parse(ozelRegisterlar1[1].GetComponent<InputField>().text));
                        anim.SetBool("program2", false);
                        txKontrol.text = "0000000" + i;
                        break;
                }
                break;
            case 5:
                ProgramModundakiAdimlar(step);
                switch (step)
                {
                    case 0:
                        txKontrol.text = registerlar[0].text;
                        bellekler[0].text = "MOV AX, #00";
                        bellekler[1].text = "MOV BX, #03";
                        bellekler[2].text = "ADD AX, [BX]";
                        bellekler[3].text = "INC BX";
                        bellekler[4].text = "CMP BX, #06";
                        bellekler[5].text = "JL 002";
                        bellekler[6].text = "MOV 008, AX";
                        txIslemler.text = "Komutlar belleğe yazıldı\n";
                        break;
                    case 7:
                        txIslemler.text += "AX'e 00 değeri atandı\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = "0";
                        txKontrol.text = "0000000" + i;
                        break;
                    case 8:
                        txIslemler.text += "BX'e 03 değeri atandı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = "3";
                        txKontrol.text = "0000000" + i;
                        break;
                    case 9:
                        txIslemler.text += "AX e BX değerine eklendi\n";
                        ozelRegisterlar1[0].GetComponent<InputField>().text = (int.Parse(ozelRegisterlar1[0].GetComponent<InputField>().text)+int.Parse(ozelRegisterlar1[1].GetComponent<InputField>().text)).ToString();
                        BayraklarıDuzenle(3);
                        txKontrol.text = "0000000" + i;
                        break;
                    case 10:
                        txIslemler.text += "BX bir arttırıldı\n";
                        ozelRegisterlar1[1].GetComponent<InputField>().text = (int.Parse(ozelRegisterlar1[1].GetComponent<InputField>().text)+1).ToString();
                        txKontrol.text = "0000000" + i;
                        break;
                    case 11:
                        txIslemler.text += "BX ile 006 arasında CMP işlemi yapıldı\n";
                        fark = (int.Parse(ozelRegisterlar1[1].GetComponent<InputField>().text) - 6);
                        BayraklarıDuzenle(fark);
                        txKontrol.text = "0000000" + i;
                        break;
                    case 12:
                        if (fark < 0)
                        {
                            Debug.Log("case12->1");
                            txIslemler.text += "002 adresine dallanıyor\n";
                            i = 2;
                            txKontrol.text = "0000000" + i;
                            registerlar[0].text = "0000000" + i;
                            anim.SetBool("program3Dongu", true);
                        }
                        else
                        {
                            Debug.Log("case12->2");
                            txKontrol.text = "0000000" + i;
                            anim.SetBool("program3Dongu", false);
                        }
                        break;
                    case 13:
                        txIslemler.text += "AX değeri 008 e taşındı\n";
                        inputfieldlar[1].GetComponent<InputField>().text = "6";
                        txKontrol.text = "0000000" + i;
                        anim.SetBool("program3", false);
                        anim.SetBool("program3Son", true);
                        break;
                }
                break;
        }
    }
    public void ProgramModundakiAdimlar(int step)
    {
        switch (step)
        {
            case 1:
                registerlar[1].text = txKontrol.text;
                registerlar[1].color = Color.white;
                txIslemler.text += "Komut adresi adres bus'a yerleştirildi\n";
                txIslemler.text += "PC içeriği MAR'a kopyalandı\n";
                break;
            case 2:
                int temp = int.Parse(registerlar[0].text);
                registerlar[0].text = "0000000" + (++temp).ToString();
                registerlar[0].color = Color.blue;
                txKontrol.text = bellekler[i++].text;
                txIslemler.text += "PC arttırıldı\n";
                break;
            case 3:
                txIslemler.text += "Komut Data Bus'ta\n";
                registerlar[2].text = txKontrol.text;
                registerlar[2].color = Color.white;
                txIslemler.text += "Komut MBR'ye yüklendi\n";
                break;
            case 4:
                registerlar[3].text = txKontrol.text;
                registerlar[3].color = Color.blue;
                txIslemler.text += "Komut IR'ye yüklendi\n";
                break;
            case 5:
                txIslemler.text += "Komut Instruction Decoder'da çözülüyor\n";
                break;
            case 6:
                txIslemler.text += "Kontrol birimi komutu ALU'ya yönlendirdi\n";
                break;
        }
    }
}