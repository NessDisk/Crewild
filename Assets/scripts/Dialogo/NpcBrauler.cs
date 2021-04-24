using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NpcBrauler : MonoBehaviour
{

    public string NombreNpc;
    public bool Usado;

    public bool desactivador, DisparadorEntradaBrauler, ValidadorCanvastext, validadorDeteccion, ActualNpcEnbatalla;

    public Canvas Canvastext, canvasTransicion, CanvasBrawler;

    public LayerMask layer;

    public float Distancie;


    public RaycastHit2D RangoDecteccion = new RaycastHit2D();

    public float tiempoPausa;

    public dialogueclassNPC LectorDetexto;



    public LineasDeDialogo Dialogo;

    /// <summary>
    /// si el Npc quiere entrar en batalla tiene que tener un mensaje para la batalla
    /// </summary>
    [TextArea(3, 10)]
    public string MensajeInicioBatalla;

    public LineasDeDialogo SegundaRespuesta;

    public Text text;

    public Npc_movimiento MovNpc;

    /// <summary>
    /// Entradas para  lector de texto.
    /// </summary>
    bool PrimeraEntrada = true, SegundaEntrada;

    Vector3 Directionface;

     public Puntero Puntero_;



    /// <summary>
    /// SpriteRedender con la panel de detectar un  jugador 
    /// </summary>
    SpriteRenderer ImpresionPanel;



    /// <summary>
    ///     caracteristicas de las  criaturas que se usan para llamar
    /// </summary>
    public DefinirCreaturasAllamar[] Criaturas;

   GameObject  BoxSelecction;

    BaulItenScript itensScrips;


    /// <summary>
    /// imagenes del enemigo usadas en batalla no pueden ser mas de 3 fuera de eso se sale del rango
    /// </summary>
    public Sprite[] ImagenesNpcBatalla = new Sprite[3];

    // Start is called before the first frame update
    void Start()
    {
        MovNpc = GetComponent<Npc_movimiento>();
        Puntero_ = new Puntero();
        LectorDetexto = new dialogueclassNPC();

        Canvastext = GameObject.Find("/Canvas").GetComponent<Canvas>();
        canvasTransicion = GameObject.Find("transiciones").GetComponent<Canvas>();
        CanvasBrawler = GameObject.Find("baltle interfaceC").GetComponent<Canvas>();

        text = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();

        BoxSelecction = GameObject.Find("/Canvas/box Election");

        SpriteRenderer[] ArraySprites = GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer Gobj in ArraySprites)
            {
                if (Gobj.name == "impresion")
                {
                    ImpresionPanel = Gobj.GetComponent<SpriteRenderer>();
                    break;
                }
            }

        itensScrips = new BaulItenScript();

        if (MensajeInicioBatalla == "")
        {
            MensajeInicioBatalla = "Te reto a un duelo(mensaje por defecto)";
        }

        if (PlayerPrefs.HasKey(NombreNpc) == true)
        {
          //Usado = itensScrips.RetornaUso(PlayerPrefs.GetInt(NombreNpc));
          //GetComponent<Npc_movimiento>().validador = false;
        }
    }

    public void inicializa()
    {

        Puntero_ = new Puntero();
        LectorDetexto = new dialogueclassNPC();

        Canvastext = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasTransicion = GameObject.Find("transiciones").GetComponent<Canvas>();
        CanvasBrawler = GameObject.Find("baltle interfaceC").GetComponent<Canvas>();

        text = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();

        BoxSelecction = GameObject.Find("/Canvas/box Election");



    }

    // Update is called once per frame
    void Update()
    {
        if (desactivador == true )
        {
            return;
        }

        if (Usado == false)
        {
            RaycastDetector();

            detectaJugador();
        }



        if (DisparadorEntradaBrauler == true && Usado == false)
        {

            /*  //ativa canvas
                 if (LectorDetexto.LeyendoTexto == true && ValidadorCanvastext == false)
                 {
                     Canvastext.enabled = true;
                     ValidadorCanvastext = true;
                     BoxSelecction.SetActive(false);
                     print("pista");
                 }


                 // lector de texto
                 if (LectorDetexto.LeyendoTexto == false)
                 {

                     EntradaEnBrawler();
                 }
                 //Puntero_.Parpaderopuntero(LectorDetexto.LeyendoTexto);

             */

        }


       else if (DisparadorEntradaBrauler == true && Usado == true && validadorDeteccion == false)
        {

            
                validadorDeteccion = true;
                StartCoroutine(SecuenciaMEnsaje2());
         
         
         /*   //ativa canvas
            if (LectorDetexto.LeyendoTexto == true && ValidadorCanvastext == false)
            {
                Canvastext.enabled = true;
                ValidadorCanvastext = true;
                BoxSelecction.SetActive(false);
            }
            // lector de texto
            if (LectorDetexto.LeyendoTexto == false)
            {
                
               
                SegundaMensaje();
            }

            if (Puntero_.ValidadorPuntero == false && LectorDetexto.LeyendoTexto == false)
            {
                StartCoroutine(Puntero_.Parpaderopuntero(LectorDetexto.LeyendoTexto));
            }

            print(Puntero_.ValidadorPuntero); */
        }
    }



    /// <summary>
    /// define la rotacion del  Raycast en base a la rotacion del nps en base el movimiento_NPC
    /// </summary>
    public void RaycastDetector()
    {
        // raycast
        if (MovNpc.Face == 1)
        {
            Directionface = Vector3.up;
        }
        else if (MovNpc.Face == 2)
        {
            Directionface = Vector3.right;
        }
        else if (MovNpc.Face == 3)
        {
            Directionface = Vector3.down;
        }
        else if (MovNpc.Face == 4)
        {
            Directionface = Vector3.left;
        }
        RangoDecteccion = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Directionface, Distancie, layer);


        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector3(transform.position.x, transform.position.y) + Directionface * Distancie, Color.red);

    }





    /// <summary>
    /// la variable lectura de textoesta activa 
    /// </summary>
    void EntradaEnBrawler()
    {

     /*     if (PrimeraEntrada == true || SegundaEntrada == true)
          {

              PrimeraEntrada = false;
              // activa animacion de transision cinematica
              if (SegundaEntrada == true)
              {
                  DisparadorEntradaBrauler = false;
                  PrimeraEntrada = true; 
                  SegundaEntrada = false;
                  ValidadorCanvastext = false;

                  ActualNpcEnbatalla = true;

                  Invoke("InvokePrimerEntrada",2f);
                  Invoke("invokeVarPPrefec", 6f);
                  Invoke("CerraCanvas", 5f);
                  Invoke("InvokeEntraCinematica", 2f);
                  EnviaSprite();
                  LectorDetexto.LeyendoTexto = false;

                  return;
              } 
              SegundaEntrada = true;

              text.text = "";
              StartCoroutine(LectorDetexto.LecturaTexto(Dialogo.Mensaje, text, tiempoPausa)); 

    } */  

}

  public  IEnumerator SecuenciaMEnsaje2()
    {
        Canvastext.enabled = true;
        ValidadorCanvastext = true;
        BoxSelecction.SetActive(false);
     
        text.text = "";
        StartCoroutine(LectorDetexto.LecturaTexto(SegundaRespuesta.Mensaje, text, tiempoPausa));

        print("Sigue buscando");

        yield return new WaitWhile(() => LectorDetexto.LeyendoTexto == true );

        yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Space) == false);

        yield return new WaitForSeconds(0.2f);

        salirbrauler();

        yield return 0;
    }
    /// <summary>
    /// la variable lectura de textoesta activa 
    /// </summary>
    void SegundaMensaje()
    {
        if (PrimeraEntrada == true || Input.GetKeyDown(KeyCode.Space))
        {
               GetComponent<Npc_movimiento>().StopAllCoroutines();
                PrimeraEntrada = false;
                // activa animacion de transision cinematica
                if (SegundaEntrada == true)
                {
                    Invoke("salirbrauler", 0.2f);
                    return;
                }

                SegundaEntrada = true;


                text.text = "";
                StartCoroutine(LectorDetexto.LecturaTexto(SegundaRespuesta.Mensaje, text, tiempoPausa));

            

        }
        
    }

    void salirbrauler()
    {
      //  GetComponent<Npc_movimiento>().validador = true;
        DisparadorEntradaBrauler = false;
        validadorDeteccion = false;
        PrimeraEntrada = true;
        SegundaEntrada = false;
        ValidadorCanvastext = false;
        CerraCanvas();

        StopCoroutine(Puntero_.Parpaderopuntero(LectorDetexto.LeyendoTexto));
        Puntero_.ValidadorPuntero = false;
        FindObjectOfType<movimiento>().DisparadorEvento = false;
    }

    /// <summary>
    /// detecta si el jugaador esta en Rango de vision del personaje
    /// </summary>
    public void detectaJugador()
    {

        if (RangoDecteccion.collider != null && validadorDeteccion == false)
        {
            if (RangoDecteccion.collider.tag == "Player")
            {
                if (RangoDecteccion.transform.GetComponent<movimiento>().DisparadorEvento == false)
                {
                    validadorDeteccion = true;


                    RangoDecteccion.transform.GetComponent<movimiento>().DisparadorEvento = true;

                    GetComponent<Npc_movimiento>().StopAllCoroutines();

                    StartCoroutine(PanelDeimpresion());
                }
               

            }
        }

    }

    /// <summary>
    /// activa un panel superior de el npc. 
    /// </summary>
    IEnumerator PanelDeimpresion()
    {
        ImpresionPanel.enabled = true;
        yield return new WaitForSeconds(2);
        ImpresionPanel.enabled = false;

        StartCoroutine(llegarHastaJugador());

        yield return 0;
    }

    /// <summary>
    /// mueve al npc hasta un paso antes 
    /// </summary>
    /// <returns></returns>
    IEnumerator llegarHastaJugador()
    {

        bool DetectarJugador = false;

       // LayerMask layerdDetec = 12;
       // print( layerdDetec.value+" Layer name : "+ LayerMask.LayerToName(layerdDetec.value));
        while (DetectarJugador == false)
        {

            
           
             RangoDecteccion = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Directionface, MovNpc.DistanciaRayCast, layer);
            if (RangoDecteccion.collider != null)
            {
            if (RangoDecteccion.collider.tag == "Player")
                DetectarJugador = true;
            }

                if (DetectarJugador == true)
            {
                break;
            }

            yield return new WaitForSeconds(0.1f);

            MovNpc.MovimientoPos(MovNpc.Face);

            yield return new WaitWhile(() => MovNpc.pos != transform.position);
         
        }
                
        CorregirFaceEnemigo();
        CorregirFacePersonaje();
     //   DisparadorEntradaBrauler = true;
        MovNpc.DesactivaRadioBoxCollider();



        // mensaje inicial
        StartCoroutine(LectorDetexto.LecturaTexto(Dialogo.Mensaje, text, tiempoPausa));

        Canvastext.enabled = true;
        ValidadorCanvastext = false;
        BoxSelecction.SetActive(false);

        yield return new WaitWhile(() => LectorDetexto.LeyendoTexto == true);

        // carga animacion
        DisparadorEntradaBrauler = false;


        // 
        ActualNpcEnbatalla = true;

        Invoke("InvokePrimerEntrada", 2f);
        Invoke("invokeVarPPrefec", 6f);
        Invoke("CerraCanvas", 5f);
        Invoke("InvokeEntraCinematica", 2f);
        EnviaSprite();
        LectorDetexto.LeyendoTexto = false;

        Npc_movimiento.AllnpcStop = true;
        yield return 0;
    }


  

   public void CorregirFaceEnemigo()
    {
        MovNpc.animatorNpc.SetInteger("face", MovNpc.Face);
    }

    void CorregirFacePersonaje()
    {
      
        GameObject.Find("/personaje").transform.GetComponent<movimiento>().animt.SetInteger("face", MovNpc.Face);
        DisparadorEntradaBrauler = true;
    }

    public void InvokeEntraCinematica()
    {
        canvasTransicion.enabled = true;
        GameObject.Find("/transiciones/Animation").GetComponent<Animation>().Play("Transicion LineasNegras1");

        CriaturasParalabatalla(Criaturas);
        
           Invoke("iniciaBatalla", 3.8f);
           Invoke("cerrarCanvasTransition", 5.5f);

    }

    public void InvokeEntraCinematica(DefinirCreaturasAllamar[] CriaturasAllamar)
    {
        canvasTransicion.enabled = true;
        GameObject.Find("/transiciones/Animation").GetComponent<Animation>().Play("Transicion LineasNegras1");

        CriaturasParalabatalla(CriaturasAllamar);      

    }

    public void InvokeEntraCinematica(CrewildBase CriaturasAllamar)
    {
        canvasTransicion.enabled = true;
        GameObject.Find("/transiciones/Animation").GetComponent<Animation>().Play("Transicion LineasNegras1");

        CriaturasParalabatalla(CriaturasAllamar);

    }

    void EnviaSprite()
    {
     PlayScritp AuxVal =   GameObject.Find("baltle interfaceC/baltle interface/animation enemy").GetComponent<PlayScritp>();

        //encapsula el sprite base de npc en batalla
        GameObject.Find("baltle interfaceC/baltle interface/animation enemy").GetComponent<Image>().sprite = ImagenesNpcBatalla[0];

        //encapsula los sprite a utilizar en el barulaer para el npc
        for (int i = 0;i < AuxVal.animacion.Length ; i++)
        {
            AuxVal.animacion[i] = ImagenesNpcBatalla[i];
        }
    }



    /// <summary>
    /// activa las variables de player prefec
    /// </summary>
    void invokeVarPPrefec()
    {
        PlayerPrefs.SetInt(NombreNpc, 1);
        Usado = itensScrips.RetornaUso(PlayerPrefs.GetInt(NombreNpc));
    }


    /// <summary>
    /// puede ser llamado como metodo alternatico si
    /// </summary>
    /// <returns></returns>
    public IEnumerator iniciaBrauler()
    {
        yield return new WaitForSeconds(3.8f);
        iniciaBatalla();
        yield return new WaitForSeconds(1.7f);
        cerrarCanvasTransition();
        yield return 0;

    }

    /// <summary>
    /// empieza la cinematica de batalla
    /// </summary>
    public void iniciaBatalla()
    {
      
        CanvasBrawler.enabled = true;
        GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>().empiezaBatalla();

    }
    /// <summary>
    /// cierra el panel del texto
    /// </summary>
    void CerraCanvas()
    {
        text.text = "";
        Canvastext.enabled = false;
    }

    void InvokePrimerEntrada()
    {
        PrimeraEntrada = true;

    }


   public void cerrarCanvasTransition()
    {

        canvasTransicion.enabled = false;
    }


    // metodos para llamar   las criaturas para la batalla 
     
    /// <summary>
    /// Encapsula las criaturas en el array de datos de animationscriptbrauler
    /// </summary>
    /// <param name="DefinirCriaturas"></param>
   public void CriaturasParalabatalla(DefinirCreaturasAllamar[] DefinirCriaturas)
    {
               

        FindObjectOfType<animationScritpBatle>().EnemigosBatalla = new CrewildBase[DefinirCriaturas.Length];

        int contadorAux= 0;
        foreach(DefinirCreaturasAllamar DFC in DefinirCriaturas)
        {
            //definir criatura
            DFC.criatura = EncontrarMetodo.EncontrarCrewild(DFC.NombreCriatura, DFC.nivel);
          
            
            // definir Ataques
            if (DFC.ataques.Length != 0)
            {
                //limpia Array de ataques
                for (int i = 0; i <4; i++ )
                {
                    DFC.criatura.ataques[i] = null;
                }
                //encapsula nuevo ataque
                for (int i = 0;i < DFC.ataques.Length; i++ )
                {
                    DFC.criatura.ataques[i] = EncontrarMetodo.EncontrarAtaques(DFC.ataques[i]);
                }
            }
         GameObject.FindObjectOfType<animationScritpBatle>().EnemigosBatalla[contadorAux] = DFC.criatura;
         contadorAux++;
        }
    }


    /// <summary>
    /// Encapsula una criatura en el array de datos EnemigosBatalla de animationscriptbrauler
    /// </summary>
    /// <param name="DefinirCriaturas"></param>
    public void CriaturasParalabatalla(DefinirCreaturasAllamar DefinirCriaturas)
    {


        GameObject.FindObjectOfType<animationScritpBatle>().EnemigosBatalla = new CrewildBase[1];

       

        //definir criatura
        DefinirCriaturas.criatura = EncontrarMetodo.EncontrarCrewild(DefinirCriaturas.NombreCriatura, DefinirCriaturas.nivel);


            // definir Ataques
            if (DefinirCriaturas.ataques.Length != 0)
            {
                //limpia Array de ataques
                for (int i = 0; i < 4; i++)
                {
                DefinirCriaturas.criatura.ataques[i] = null;
                }
                //encapsula nuevo ataque
                for (int i = 0; i < DefinirCriaturas.ataques.Length; i++)
                {
                DefinirCriaturas.criatura.ataques[i] = EncontrarMetodo.EncontrarAtaques(DefinirCriaturas.ataques[i]);
                }
            }
            GameObject.FindObjectOfType<animationScritpBatle>().EnemigosBatalla[0] = DefinirCriaturas.criatura;
           
        
    }

    /// <summary>
    /// Encapsula una criatura en el array de datos EnemigosBatalla de animationscriptbrauler
    /// </summary>
    /// <param name="DefinirCriaturas"></param>
    public void CriaturasParalabatalla(CrewildBase[] DefinirCriatura)
    {
        FindObjectOfType<animationScritpBatle>().EnemigosBatalla = new CrewildBase[DefinirCriatura.Length];


        for (int i = 0; i < DefinirCriatura.Length;i++ )
        {
            FindObjectOfType<animationScritpBatle>().EnemigosBatalla[i] = DefinirCriatura[i];
        }
            


    }


    /// <summary>
    /// Encapsula una criatura en el array de datos EnemigosBatalla de animationscriptbrauler
    /// </summary>
    /// <param name="DefinirCriatura"></param>
    public void CriaturasParalabatalla(CrewildBase DefinirCriatura)
    {
        FindObjectOfType<animationScritpBatle>().EnemigosBatalla = new CrewildBase[1];
        FindObjectOfType<animationScritpBatle>().EnemigosBatalla[0] = DefinirCriatura;


    }


    /// <summary>
    /// retorna la criatura a utilizar
    /// </summary>
    /// <param name="DefinirCriaturas"></param>
    /// <returns></returns>
    public CrewildBase RetornaCriatura(DefinirCreaturasAllamar DefinirCriaturas)
    {


        CrewildBase CriaturaARetornar = null;



        //definir criatura
        DefinirCriaturas.criatura = EncontrarMetodo.EncontrarCrewild(DefinirCriaturas.NombreCriatura, DefinirCriaturas.nivel);


        // definir Ataques
        if (DefinirCriaturas.ataques.Length != 0)
        {
            //limpia Array de ataques
            for (int i = 0; i < 4; i++)
            {
                DefinirCriaturas.criatura.ataques[i] = null;
            }
            //encapsula nuevo ataque
            for (int i = 0; i < DefinirCriaturas.ataques.Length; i++)
            {
                DefinirCriaturas.criatura.ataques[i] = EncontrarMetodo.EncontrarAtaques(DefinirCriaturas.ataques[i]);
            }
        }
        CriaturaARetornar = DefinirCriaturas.criatura;

        return CriaturaARetornar;
    }

}

/// <summary>
/// struck que tiene variables que definen las caracteristicas de los personajes
/// </summary>
[System.Serializable]
public class DefinirCreaturasAllamar
{
    public string NombreCriatura;

    public int nivel;

    /// <summary>
    /// si array es mayor que 0 y no tiene un nombre genera error
    /// </summary>
    public string[] ataques;

    public CrewildBase criatura;
    

   public DefinirCreaturasAllamar(string nombre,int niveles )    
    {
        NombreCriatura = nombre;
        nivel = niveles;
        criatura = EncontrarMetodo.EncontrarCrewild(NombreCriatura, nivel);
    }

} 