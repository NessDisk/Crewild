using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveDatos : MonoBehaviour
{
    public bool activador, pause;

    private RectTransform Selector;
    public int CountMov;

    public string TextoGuardado;

    private Text texto, selectorText;

    public Canvas canvasMenu;

    public libreriaDeScrips LibreriaS ;


    // Start is called before the first frame update
    void Start()
    {
        Selector = GameObject.Find("guardado/panel/box guardado/Selector").GetComponent<RectTransform>();
        selectorText = GameObject.Find("guardado/panel/box guardado/Selector").GetComponent<Text>();

        canvasMenu = GameObject.Find("guardado").GetComponent<Canvas>();
        texto = GameObject.Find("guardado/panel/box guardado/Texto").GetComponent<Text>();
       
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activador == false || pause == true)
        {
            return;
        }


        SumaSelector();

        Movimiento();

        Acciones();

    }

    void Acciones()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // esta En Si.
            if (CountMov == 0)
            {
               
                StartCoroutine(TiempoDeGiardado());
                Debug.Log("accion si");
            }
            //Esta En  No.
            else if (CountMov == 1)
            {
                Debug.Log("accion no");

                Invoke("salirPanelGuardado",0.1f);
                
            }
        }

    }

    /// <summary>
    /// salir del menu de seleccion
    /// </summary>
    void salirPanelGuardado()
    {
        activador = false;
        LibreriaS.menuInterface.RetocederMenuDeSeleccion();
        pause = false;
    }
    /// <summary>
    /// define la posicion del Selector
    /// </summary>
    void Movimiento()
    {
      
            // esta En Si.
            if (CountMov == 0)
            {
                Selector.localPosition = new Vector3(-120.6f, -42f);
            }
            //Esta En  No.
            else if (CountMov == 1)
            {
                Selector.localPosition = new Vector3(-8f, -42f);
            }
        }
        
        
    

    /// <summary>
    /// incrementa o decrementa la valor Int CountMov.
    /// </summary>
    void SumaSelector()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("A");
            CountMov--;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            print("D");
            CountMov++;
        }


        if (CountMov > 1)
        {
            CountMov = 0;
        }
        else if (CountMov < 0)
        {
            CountMov = 1;
        }

    }


    IEnumerator TiempoDeGiardado()
    {
        pause = true;
        string textoReponer ;
        textoReponer = texto.text;
        selectorText.enabled = false;

        //corregir panel de guardado
          LibreriaS.informacionCrewild.GuardaData();
        //texto para guardar Info
        for (int i = 0; i <3 ; i++ )
        {
            texto.text = "guardando";
            yield return new WaitForSeconds(0.5f);
            texto.text = "guardando.";
            yield return new WaitForSeconds(0.5f);
            texto.text = "guardando..";
            yield return new WaitForSeconds(0.5f);
            texto.text = "guardando...";
            yield return new WaitForSeconds(0.5f);
        }

        selectorText.enabled = true;
        salirPanelGuardado();
        texto.text = textoReponer;
        yield return 0;
    }
}
