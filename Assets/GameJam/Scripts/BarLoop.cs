using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLoop : MonoBehaviour
{
    Slider bar;

    public Text valuestring;
    private bool loop = true;
    private int cont;

    private void Awake()
    {
        bar = GetComponent<Slider>();
    }

    void Update()
    {
        // key to start timer loop and check loop
        if (Input.GetKeyDown(KeyCode.L) && loop == true)
        {
            loop = false;
            TimerLoop();
            Invoke("CoolDownLoop", 5.0f);
        }
    }

    //Progress timer
    void UpdateValueBar (float valueMax, float valueAct)
    {
        float porcentant;
        porcentant = valueAct / valueMax;
        bar.value = porcentant;
    }

    //Start timer
    public void TimerLoop()
    {
        loop = false;
        UpdateValueBar(4, cont);
        cont++;
        valuestring.text = cont + "Seg";
        if (cont <= 4)
        {
            Invoke("TimerLoop", 1f);
        } else if (cont >= 5)
        {
            cont = 0;
            bar.value = 0;
            valuestring.text = 0 + "Seg";
        }
        
    }

    //Reload timer
    private void CoolDownLoop()
    {
        loop = true;
    }

}
