using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcAbrir : MonoBehaviour
{
    public bool Activador = true;
    private Canvas PcC;
    private GameObject objOpciones, ObjCaja;

    private int Numopciones;

    private RectTransform selectorPos;

    private Pc Pcscript;

    private bool GuardarCriatura;

    // Start is called before the first frame update
    void Start()
    {
        PcC = GameObject.Find("PcC").GetComponent<Canvas>();
        objOpciones = GameObject.Find("PcC/OpcionesIniciales");
        ObjCaja = GameObject.Find("PcC/pc");

        selectorPos = GameObject.Find("PcC/OpcionesIniciales/Selector").GetComponent<RectTransform>();
        Pcscript = FindObjectOfType<Pc>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Activador == false)
        {
            return;
        }

        if (Pcscript.Activador == false && GuardarCriatura == false)
        {
            numPosOpciones();
            PosSelectorOpciones();

            if (Input.GetKeyDown(KeyCode.C))
            {
                salir();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AccionOpciones();
            }
        }

        if (Pcscript.Activador == true
            && Pcscript.navOpciones == false
            && Pcscript.moverValidador == false
            && Pcscript.infoCriatura == false
            && Pcscript.sacarvalidador == false            
            && Input.GetKeyDown(KeyCode.C)
            )
        {
            Pcscript.Activador = false;
            Pcscript.condionesInicialesSalida();
            abrirpanelopciones();
        }

        else if (GuardarCriatura == true)
        {
            Pcscript.GuardarCriatura();

            if (Input.GetKeyDown(KeyCode.C) && Pcscript.SeleccionarGuardar == false)
            {
                Pcscript.condionesInicialesSalida();
                abrirpanelopciones();
                GuardarCriatura = false;
            }
        }
  
    }

    void AccionOpciones()
    {
        if (Numopciones == 0)
        {
            Activarcaja();
            objOpciones.SetActive(false);
            ObjCaja.SetActive(true);
        }
        else if (Numopciones == 1)
        {
            Invoke("AuxMetodoGuardar", 0.1f);
            objOpciones.SetActive(false);
            ObjCaja.SetActive(true);
            Pcscript.condicionesInicialesGuardar();

        }
        else if (Numopciones == 2)
        {
            Invoke("salir", 0.1f);
        }
    }

    void AuxMetodoGuardar()
    {
        GuardarCriatura = true;
    }

    void Activarcaja()
    {

        Pcscript.Activador = true;
    }

    void salir()
    {
       
        PcC.enabled = false;
        objOpciones.SetActive(false);
        ObjCaja.SetActive(false);
        FindObjectOfType<movimiento>().DisparadorEvento = false;
        Activador = false;
        Numopciones = 0;
        PosSelectorOpciones();
        Pcscript.Activador = false;
    } 

    void numPosOpciones()
    {
        if (Input.GetKeyDown(KeyCode.W))
         Numopciones--;
        
        else if (Input.GetKeyDown(KeyCode.S))
         Numopciones++;

        if (Numopciones < 0)
            Numopciones = 2;

        else if (Numopciones > 2)
            Numopciones = 0;
    }

    void PosSelectorOpciones()
    {
        if (Numopciones == 0)
            selectorPos.anchoredPosition3D = new Vector2(-2f,38.2f);
        else if (Numopciones == 1)
            selectorPos.anchoredPosition3D = new Vector2(-2f, 5f);
        else if (Numopciones == 2)
            selectorPos.anchoredPosition3D = new Vector2(-2f, -28f);
    }

    public void  abrirpanelopciones()
    {
        PcC.enabled = true;
        objOpciones.SetActive(true);
        ObjCaja.SetActive(false);
    }
}
