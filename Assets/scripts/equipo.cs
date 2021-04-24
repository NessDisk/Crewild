using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class equipo : MonoBehaviour {

    public bool activador;

    private RectTransform SelectorOpciones, SelectorContacto;
    public int NumPanel,  NumOpciones, NumContantos;  

    libreriaDeScrips LibreriaS ;


    private int[] tabla = new int[2];

    private GameObject General, BoxText , Mapa;

    public bool MapaEnrtada;

    private mapa MapaAScript;
       

    // Use this for initialization
    void Start()
    {
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();
        SelectorContacto = GameObject.Find("equipo/equipo/celular/Selector").GetComponent<RectTransform>();
        SelectorOpciones = GameObject.Find("equipo/equipo/celular/Selector opciones").GetComponent<RectTransform>();

        SelectorOpciones.GetComponent<Image>().enabled = false;
        SelectorContacto.GetComponent<Image>().enabled = false;
        tabla[0] = 0;
        tabla[1] = 0;

        General = GameObject.Find("equipo/equipo/celular");
        BoxText = GameObject.Find("equipo/equipo/Box Texto");
        Mapa    = GameObject.Find("equipo/equipo/Mapa");
     //  DesactivarObj();

        MapaAScript = new mapa();
    }

    // Update is called once per frame
    void Update()
    {

        if (activador == false)
        {
            return;
        }

        if (MapaEnrtada == false)
        {
            PosiconPnaleSeccion();
            // mapa de 
            if (NumPanel == 0)
            {
                PosicionContactos();
            }

            else if (NumPanel == 1)
            {
                PanelOpciones();
                PosPanelOpciones();
                Acciones();
            }

        }

        if (MapaEnrtada == true)
           {
            Atras();
            MapaAScript.Acciones();
           }



    }


    void Atras()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            MapaEnrtada = false;
            DesactivarObj();
            General.SetActive(true);
            BoxText.SetActive(true);

        }

    }
    void PosiconPnaleSeccion()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NumPanel++;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            NumPanel--;
        }

        if (NumPanel > 1)
            NumPanel = 0;
        else if (NumPanel < 0)
            NumPanel = 1;


        if (NumPanel == 0)
            {
                SelectorOpciones.GetComponent<Image>().enabled = false;
                SelectorContacto.GetComponent<Image>().enabled = true;
            }

            else if (NumPanel == 1)
            {
                SelectorOpciones.GetComponent<Image>().enabled = true;
                SelectorContacto.GetComponent<Image>().enabled = false;
            }
    }
    void Acciones()
    {
        if (Input.GetKeyDown(KeyCode.Space ))
        {
            
            if (NumOpciones == 0)
            {

            }
            else if (NumOpciones == 1)
            {

            }
            else if (NumOpciones == 2)
            {
                DesactivarObj();
                MapaEnrtada = true;
                Mapa.SetActive(true);
                MapaAScript.PosicionInicial();
            }
        }
       
    }
    void PosicionContactos()
    {
        
    }
    void PanelOpciones()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NumOpciones--;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            NumOpciones++;
        }

        if (NumOpciones > 2)
        {
            NumOpciones = 0;
        }

        if (NumOpciones < 0)
        {
            NumOpciones = 2;
        }
        
    }

    void PosPanelOpciones()
    {
        if (NumOpciones == 0)
        {
            SelectorOpciones.anchoredPosition3D = new Vector3(-292f, -13.6f);
        }
        else if (NumOpciones == 1)
        {
            SelectorOpciones.anchoredPosition3D = new Vector3(-292f, -97f);
        }
        else if (NumOpciones == 2)
        {
            SelectorOpciones.anchoredPosition3D = new Vector3(-292f, -173f);
        }
    }

    void DesactivarObj()
    {
        General.SetActive(false);
        BoxText.SetActive(false);
        Mapa.SetActive(false);
    }

  
   
}


