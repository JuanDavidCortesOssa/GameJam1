using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLoop : MonoBehaviour
{
    Slider Bar;

    public float max;
    public float act;
    public Text Valuestring;
    public bool loop = false;
    int cont;

    private void Awake()
    {
        Bar = GetComponent<Slider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FunctionPrueba();
        }
    }

    void UpdateValueBar (float valueMax, float valueAct)
    {
        float porcentant;
        porcentant = valueAct / valueMax;
        Bar.value = porcentant;
    }

    public void FunctionPrueba()
    {
        loop = false;
        UpdateValueBar(4, cont);
        cont++;
        Valuestring.text = cont + "Seg";
        if (cont <= 4)
        {
            Invoke("FunctionPrueba", 1f);
        }
        
    }
}
