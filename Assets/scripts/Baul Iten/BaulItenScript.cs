using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaulItenScript : MonoBehaviour
{
    public string NombreIten;
    public bool Usado;

    /// <summary>
    /// limpia todos las key del player prefac solo es adsedibles desde el editor.
    /// </summary>
    public bool LimpiarTodasPlayerPref;
    /// <summary>
    ///  Cuando el objeto esta suelto  desactiva;
    /// </summary>
    public  bool Desactivador;
    
    /// <summary>
    /// activa el dispara para Recoger Itens
    /// </summary>
    public bool DisparadorBaul;

    /// <summary>
    /// Si esta marcada como True el objeto desaparece luego de se activa
    /// </summary>
    public bool ObjetoDestruible;


    public Sprite[] Imgenes = new Sprite[2];

   

    public CajaInventario[] Itens;

    private dialogueclassNPC LectorDetexto =  new dialogueclassNPC();

    private Text TextoDialogo;
    /// <summary>
    /// define el numero del ciclo al cual  el dialogo esta
    /// </summary>
    public int Contador;

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

        Canvastext = GameObject.Find("/Canvas").GetComponent<Canvas>();

        TextoDialogo = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();

        puntero = new Puntero();

        menuSiNo = GameObject.Find("Canvas/box Election");

        if (PlayerPrefs.HasKey(NombreIten) == true)
        {
        //    Usado = RetornaUso(PlayerPrefs.GetInt(NombreIten));
        }

        if (Usado == true)
        {
            SalirBaul();

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LimpiarTodasPlayerPref == true)
        {
            LimpiarTodasPlayerPref = false;
            PlayerPrefs.DeleteAll();
          
        }
        if (Desactivador == true)
        {
            return;
        }
        if (DisparadorBaul == true)
        {
            //cambia imagen Baul
            GetComponent<SpriteRenderer>().sprite = Imgenes[1];

            if (ObjetoDestruible == true)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }


            //activa canvas 
            if (LectorDetexto.LeyendoTexto == true && ValidadorCanvastext == false)
            {
                Canvastext.enabled = true;
                menuSiNo.SetActive(false);
                ValidadorCanvastext = true;
            }

             //leer Texto
            if (LectorDetexto.LeyendoTexto == false)
            {
               
                DarITen();
            }

            StartCoroutine( puntero.Parpaderopuntero(LectorDetexto.LeyendoTexto));

        }
    }

    /// <summary>
    ///metodo que recoge  los la accione en la lectura de texto 
    /// </summary>
    void DarITen()
    {



        if (PrimeraEntrada == true || Input.GetKeyDown(KeyCode.Space))
        {
            PrimeraEntrada = false;

            if (Contador == Itens.Length)
            {
                SalirBaul();
                PlayerPrefs.SetInt(NombreIten,1);
                Usado = RetornaUso(PlayerPrefs.GetInt(NombreIten));
                return;
            }           
                TextoDialogo.text = "";
                StartCoroutine(LectorDetexto.LecturaTexto(MensajesGlovales.EntregarIten(Itens[Contador]), TextoDialogo, TimepoDePausa));
                LibreriaS.inventario.DefineList(Itens[Contador]);

            Contador++;
                       
        }
    }

    void SalirBaul()
    {

        Canvastext.enabled = false;
        GameObject.FindObjectOfType<movimiento>().StopPlayer = false;
      //  GetComponent<BaulItenScript>().enabled = false;
        Desactivador = true;
        // Destruye el Objeto
        if (ObjetoDestruible == true)
        {           
            Destroy(this.gameObject);
        }
        //desactiva el baul
        else 
        {
            GetComponent<SpriteRenderer>().sprite = Imgenes[1];
            Desactivador = true;
        }
    }

    /// <summary>
    /// retorna un  valor true o false si el objeto es 0 o 1 en el int
    /// </summary>
  public  bool RetornaUso(int value)
    {
        bool RetornoBool = true;
        //false
        if (value == 0)
        {
            RetornoBool = false;
        }
        //true
        else if (value == 1)
        {
            RetornoBool = true;
        }

        return RetornoBool;
    }


    

  
}

