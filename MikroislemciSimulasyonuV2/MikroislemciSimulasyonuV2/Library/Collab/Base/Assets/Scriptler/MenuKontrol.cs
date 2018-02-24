using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public string seciliRegister, seciliBellek;
    public string data;
    public Text txIslemler;
    public Text txKontrol;
    string komut;
    int i = 0, seciliRegisterNumarasi, seciliRegisterNumarasi2, bellek, islem, tglDurum, seciliBellekNo;
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
                dropdownlar[2].ClearOptions();
                List<string> secenek3 = new List<string>();
                secenek3.Add("MOV");
                secenek3.Add("CMP");
                secenek3.Add("ADD");
                dropdownlar[2].AddOptions(secenek3);
                togglelar[2].interactable = true;
                dropdownlar[3].interactable = true;
                togglelar[3].interactable = false;
                dropdownlar[4].interactable = false;
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
                    switch (dropdownlar[2].value)
                    {
                        case 0:
                            Debug.Log("Indirect Adresleme modunda ADD komutu");
                            break;
                        case 1:
                            Debug.Log("Indirect Adresleme modunda MOV komutu");
                            break;
                        case 2:
                            Debug.Log("Indirect Adresleme modunda AND komutu");
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
            komut = islemler2[islem] + " " + seciliRegister + ", [" + seciliBellek + "]";
        }
        else
            komut = islemler2[islem] + " [" + seciliBellek + "]" + ", " + seciliRegister;

        bellekler[i].text = komut;
        bellekler[i].color = Color.red;
        i++;
        anim.SetBool("runDirect", true);
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
                        txKontrol.text = data;
                        txIslemler.text += "Carry Flag resetlendi\n";
                        txIslemler.text += "Parity Flag resetlendi\n";
                        txIslemler.text += "Auxiliary Flag resetlendi\n";
                        if (int.Parse(data) == 0)
                        {
                            txIslemler.text += "Zero Flag SET edildi\n";
                            bayraklar[3].text = "1";
                            bayraklar[3].color = Color.white;
                        }
                        else
                        {
                            txIslemler.text += "Zero Flag resetlendi\n";
                        }
                        if (int.Parse(data) < 0)
                        {
                            txIslemler.text += "Sign Flag SET edildi\n";
                            bayraklar[5].text = "1";
                            bayraklar[5].color = Color.white;
                        }
                        else
                        {
                            txIslemler.text += "Sign Flag resetlendi\n";
                        }
                        txIslemler.text += "Tarp Flag resetlendi\n";
                        txIslemler.text += "Interrupt Flag resetlendi\n";
                        txIslemler.text += "Direction Flag resetlendi\n";
                        txIslemler.text += "Overflow Flag resetlendi\n";
                        break;
                    case 1:
                        txKontrol.text = data;
                        break;
                    case 2:
                        int x = int.Parse(ozelRegisterlar1[seciliRegisterNumarasi].GetComponent<InputField>().text);
                        int y = int.Parse(data);
                        sonuc = x & y;
                        txKontrol.text = sonuc.ToString();
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
                        }
                        if (sonuc < 0)
                        {
                            txIslemler.text += "Sign Flag SET edildi\n";
                            bayraklar[5].text = "1";
                            bayraklar[5].color = Color.white;
                        }
                        else
                        {
                            txIslemler.text += "Sign Flag resetlendi\n";
                        }
                        txIslemler.text += "Tarp Flag resetlendi\n";
                        txIslemler.text += "Interrupt Flag resetlendi\n";
                        txIslemler.text += "Direction Flag resetlendi\n";
                        txIslemler.text += "Overflow Flag resetlendi\n";
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
                anim.SetBool(seciliRegister, false);
                anim.SetBool("runImmediate", false);
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
                        txIslemler.text += "Sign Flag resetlendi\n";
                        txIslemler.text += "Tarp Flag resetlendi\n";
                        txIslemler.text += "Interrupt Flag resetlendi\n";
                        txIslemler.text += "Direction Flag resetlendi\n";
                        txIslemler.text += "Overflow Flag resetlendi\n";
                        txKontrol.text = sonuc.ToString();
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
                if (tglDurum == 1)
                {
                    anim.SetBool(seciliRegister, false);
                }
                else
                    anim.SetBool("bellek", false);

                anim.SetBool("runDirect", false);
                txKontrol.text = registerlar[0].text;
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

}
