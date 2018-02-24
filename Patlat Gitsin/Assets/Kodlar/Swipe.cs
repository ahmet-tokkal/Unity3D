using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    Vector3 ilkPoz, sonPoz;
    public float minSwipeMesafesi = 10;
    public bool durum;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        Hareket();
    }

   /* public float speed = 100F;
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
        }
    }*/


    public void Durum(bool durum)
    {
        durum = true;

    }
    public void Hareket()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch dokunma = Input.GetTouch(i);
                TouchPhase aralık = dokunma.phase;

                switch (aralık)
                {
                    case TouchPhase.Began:
                        ilkPoz = dokunma.position;
                        break;
                    case TouchPhase.Moved:
                        sonPoz = dokunma.position;
                        if (Vector3.Distance(ilkPoz, sonPoz) > minSwipeMesafesi)
                        {
                            if ((Mathf.Abs(sonPoz.x - ilkPoz.x) > (Mathf.Abs(sonPoz.y - ilkPoz.y))))
                            {
                                if (ilkPoz.x > sonPoz.x)
                                {
                                    gameObject.transform.Translate(Vector3.left * 10);
                                }
                                else
                                {
                                    gameObject.transform.Translate(Vector3.right * 10);
                                }
                            }
                        }
                        break;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                ilkPoz = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                sonPoz = Input.mousePosition;
                if (Vector3.Distance(ilkPoz, sonPoz) > minSwipeMesafesi)
                {
                    if ((Mathf.Abs(sonPoz.x - ilkPoz.x) > (Mathf.Abs(sonPoz.y - ilkPoz.y))))
                    {
                        if (ilkPoz.x > sonPoz.x)
                        {
                            gameObject.transform.Translate(Vector3.left * 10);
                        }
                        else
                        {
                            gameObject.transform.Translate(Vector3.right * 10);
                        }
                    }
                }
            }
        }
    }
}

