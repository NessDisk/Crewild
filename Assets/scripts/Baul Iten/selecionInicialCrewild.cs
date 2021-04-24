using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class selecionInicialCrewild : MonoBehaviour
{

    public string NombreCrewild;
    public bool Usado;

    /// <summary>
    /// limpia todos las key del player prefac solo es adsedibles desde el editor.
    /// </summary>
    public bool LimpiarTodasPlayerPref;
    /// <summary>
    ///  Cuando el objeto esta suelto  desactiva;
    /// </summary>
    public bool Desactivador;

    private CrewildBase crewildInicial;

    private GameObject ObjImahenCrewil, imagenCrewild;
    public GameObject eventoADesactivars, eventoAActivar;
    /// <summary>
    /// activa el dispara para Recoger Itens
    /// </summary>
    public bool Disparador;

    /// <summary>
    /// Si esta marcada como True el objeto desaparece luego de se activa
    /// </summary>
    public bool ObjetoDestruible;


    public Sprite[] Imgenes = new Sprite[2];



     private dialogueclassNPC LectorDetexto = new dialogueclassNPC();

    private Text TextoDialogo;
    /// <summary>
    /// define el numero del ciclo al cual  el dialogo esta
    /// </summary>
    public int NumDesicionDesion = 0;

    public float TimepoDePausa;

    Canvas Canvastext;

    GameObject menuSiNo;

    libreriaDeScrips LibreriaS;

    bool ValidadorCanvastext;

    /// <summary>
    /// se desactiva depues de la primera entrada y se  reactiva al reiniciar el contador
    /// </summary>
    bool PrimeraEntrada = true;

    Puntero puntero;

    // Start is called before the first frame update
    void Start()
    {

        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

        Canvastext = GameObject.Find("Canvas").GetComponent<Canvas>();

        TextoDialogo = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();

        puntero = new Puntero();

        menuSiNo = GameObject.Find("Canvas/box Election");

        crewildInicial = EncontrarMetodo.EncontrarCrewild(NombreCrewild,3);

        ObjImahenCrewil = GameObject.Find("Canvas/cuadroCrewild");

        imagenCrewild = GameObject.Find("Canvas/cuadroCrewild/Crewild");

        Invoke("InvokeDesactivarObjetos", 1f);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Desactivador == true)
        {
            return;
        }
        if (Disparador == true)
        {
            //leer Texto
            if (LectorDetexto.LeyendoTexto == false)
            {
                if (PrimeraEntrada == false)
                {
                    ActicaSelector();
                    StartCoroutine(puntero.Parpaderopuntero(LectorDetexto.LeyendoTexto));
                    return;
                }
                DarCrewildInicial();
            }
        }

        
    }


    private void ActicaSelector()
    {
        menuSiNo.SetActive(true);
        NumDesicionDesion = EncontrarMetodo.numDefinirNumSelector(NumDesicionDesion);
        EncontrarMetodo.definirMovimientoSelector_Yes_no(NumDesicionDesion);       

        if ( Input.GetKeyDown(KeyCode.Space))
        {
            Acciones(NumDesicionDesion);
        }
    }
    

   private  void Acciones(int Acciones)
    {
        // yes
        if (Acciones == 0)
        {

            EncontrarMetodo.AnadirABulleWild(crewildInicial);
            Invoke("SalirBaul", 0.2f);
            Invoke("destroidInvoke", 0.3f);
            //añadie criatura
            // salir del objeto y destruirlo
        }
        //no
        else if (Acciones == 1)
        {
            // salir de objeto
            Invoke("SalirBaul", 0.2f );
          
        }

    }

    private  void InvokeDesactivarObjetos()
    {
        ObjImahenCrewil.SetActive(false);

    }

    private void destroidInvoke()
    {
        Destroy(this.gameObject);

    }

    public void ActivaObjetos()
    {
        //cambia imagen Baul
        //GetComponent<SpriteRenderer>().sprite = Imgenes[0];
        ObjImahenCrewil.SetActive(true);
        imagenCrewild.GetComponent<Image>().sprite = crewildInicial.animaCrewildFrentre[0];
        Canvastext.enabled = true;
        menuSiNo.SetActive(false);
        ValidadorCanvastext = true;
    }
    /// <summary>
    ///metodo que recoge  los la accione en la lectura de texto 
    /// </summary>
    void DarCrewildInicial()
    {



        if (PrimeraEntrada == true || Input.GetKeyDown(KeyCode.Space))
        {
            PrimeraEntrada = false;

       
            TextoDialogo.text = "";

            string mensaje = "Dentro hay un " + crewildInicial.NombreTaxonomico + " de tipo " + crewildInicial.TipoDecrewild[0].ToString() + ", deseas obtenerlo.";


            StartCoroutine(LectorDetexto.LecturaTexto(mensaje, TextoDialogo, TimepoDePausa));
            

          

        }
    }

    void SalirBaul()
    {

        Canvastext.enabled = false;
        menuSiNo.SetActive(true);
        ObjImahenCrewil.SetActive(false);
        GameObject.FindObjectOfType<movimiento>().DisparadorEvento = false;
        PrimeraEntrada= true;
        Disparador = false;
        // Desactivador = true;
        eventoADesactivars.SetActive(false);
        eventoAActivar.SetActive(true);


    }
}
