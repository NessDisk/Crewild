using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Cinematica : MonoBehaviour
{

    private movimiento PlayerMov;
    private Transform playertransf;
    private Animation animacion;
    public string NombreAnimacion;

    /// <summary>
    /// Array de datos los diaogos que se van a usar
    /// </summary>
    public LineasDeDialogo[] Dialogos;

    /// <summary>
    ///  Es lector de texto
    /// </summary>
    dialogueclassNPC LectorDetexto;


    /// <summary>
    /// es el texto que se va llenando
    /// </summary>
    Text texto;

    /// <summary>
    /// es el mensaje que se le manda 
    /// </summary>
    string message;

    /// <summary>
    /// es el tiempo de pausa entre letra y letra
    /// </summary>
    public float TimepoDePausa = 0.2f;



    public float speedMoveText = 1f;

    public float distance = 1f;

    Text TextoDialogo;

    Canvas Canvastext;

    /// <summary>
    /// define el numero del ciclo al cual  el dialogo esta
    /// </summary>
    public int Contador;

    /// <summary>
    /// se desactiva depues de la primera entrada y se  reactiva al reiniciar el contador
    /// </summary>
    bool PrimeraEntrada = true;

    /// <summary>
    ///  activa en un ciclo el canvas y cierra la segunda extrada hasta que se  reinicia
    /// </summary>
    bool ValidadorCanvastext = false;

    /// <summary>
    /// recoge el compnente imagen del obj Puntero el canvas
    /// </summary>
    Image punteroImagen;

    /// <summary>
    /// valida la entrada al metodo puntero
    /// </summary>
    bool ValidadorPuntero;


    Puntero Puntero_;



    /// <summary>
    /// gameobjet del cuadro de seleccion de accion
    /// </summary>
    GameObject BoxSelecction , BoXCrewild;

    /// <summary>
    /// activa dialogo en la cinematica
    /// </summary>
    public bool DisparadorDialogo = false;

    public bool EjecutaAnimacionAlempezar = false;



    /// <summary>
    ///     caracteristicas de las  criaturas que se usan para llamar
    /// </summary>
    public DefinirCreaturasAllamar[] Criaturas;

    public NpcBrauler Brawler;

    //cuando esta act
    public static bool EstoyEnBrauler;
    private bool TriggerBrawler;

    /// <summary>
    /// cuando se ejecuta desde el principio con el activador encendido genera error si se utiliza funcion de esta  variable 
    /// </summary>
    animationScritpBatle ScritBatalla;
    /// <summary>
    /// cuando se ejecuta desde el principio con el activador encendido genera error si se utiliza funcion de esta  variable 
    /// </summary>
    Inventario inventario;

    public bool RepetirCinematica;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "personaje")
        {
            PlayerMov = collision.transform.GetComponent<movimiento>();
            PlayerMov.TargerPath = transform;
            PlayerMov.DisparadorEvento = true;
            PlayerMov.animt.SetBool("walk", false);
            playertransf = collision.transform;
            playertransf.parent = GameObject.Find(this.gameObject.name + "/parent personaje").GetComponent<Transform>();

            animacion = GetComponent<Animation>();



            GetComponent<BoxCollider2D>().enabled = false;


            StartCoroutine(EjecutaAnmacion());




        }
    }
    void Start()
    {
        if (EjecutaAnimacionAlempezar == false)
        {
            ScritBatalla = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>();
            inventario = GameObject.Find("objetos/objetos").GetComponent<Inventario>();
            Brawler = new NpcBrauler();
            Brawler.inicializa();
        }
        animacion = GetComponent<Animation>();

        TextoDialogo = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();
        punteroImagen = GameObject.Find("Canvas/box Texto/mask/Puntero").GetComponent<Image>();
        Canvastext = GameObject.Find("Canvas").GetComponent<Canvas>();
        LectorDetexto = new dialogueclassNPC();
        punteroImagen.enabled = false;
        Puntero_ = new Puntero();
        BoxSelecction = GameObject.Find("Canvas/box Election");
        BoXCrewild = GameObject.Find("Canvas/cuadroCrewild");


        if (EjecutaAnimacionAlempezar == true)
        {
            animacion.Play(NombreAnimacion);
        }

        Invoke("AuxInvoke", 0.5f);
    }

    void  AuxInvoke()
    {
        if (BoxSelecction != null)
        {
            BoxSelecction.SetActive(false);
        }
           
        else
        {
            BoxSelecction = FindObjectOfType<menus_interface>().BoxSelecction;
            BoXCrewild = FindObjectOfType<menus_interface>().BoXCrewild;
            BoxSelecction.SetActive(false);
            BoXCrewild.SetActive(false);
        }


        BoxSelecction.SetActive(false);
       
    }

    void Update()
    {
        if (DisparadorDialogo == true)
        {
            //ativa canvas
            if (LectorDetexto.LeyendoTexto == true && ValidadorCanvastext == false)
            {
                if (BoxSelecction != null)
                    BoxSelecction.SetActive(false);
                else
                {
                    BoxSelecction = FindObjectOfType<menus_interface>().BoxSelecction;
                    BoxSelecction.SetActive(false);
                }

                Canvastext.enabled = true;
                ValidadorCanvastext = true;
            }



            //lectura de mensaje
            if (LectorDetexto.LeyendoTexto == false)
            {
                //puntero de espera
                // puntero();
                StartCoroutine(Puntero_.Parpaderopuntero(LectorDetexto.LeyendoTexto));


                EntraATexto();
            }

        }
        else if (EstoyEnBrauler == true && TriggerBrawler == true)
        {
            if (ScritBatalla.TriggerEntraceCinema == false)
            {
                ContinuaCinematica();
                TriggerBrawler = false;
                EstoyEnBrauler = false;
                Debug.Log("esta a ContinuaCinematica");
            }
        }
    }


    /// <summary>
    ///metodo que recoge  los la accione en la lectura de texto 
    /// </summary>
    void EntraATexto()
    {

        if (PrimeraEntrada == true || Input.GetKeyDown(KeyCode.Space))
        {

            //KISS
            //bool Salir de Texto
            if (Contador != 0)
            {
                //modo  brauler
                if (Dialogos[Contador - 1].cinema.DipararBrawler == true && PrimeraEntrada == false)
                {
                    ModoBrawler();
                    EstadoPausado();
                    return;
                }

                //continua Dialogo
                if (Dialogos[Contador - 1].cinema.continuarDialogo == true && PrimeraEntrada == false)
                {
                    ContinuaCinematica();
                    return;
                }
                //Reinicia recorrido
                if (Contador == Dialogos.Length)
                {
                    reinicioArray();
                    //->  poner un player pfef para marcar la cinematica como usada

                    return;
                }
            }

            // recorrido normal
            if (Dialogos[Contador].Itens.Dariten == false)
            {
                TextoDialogo.text = "";
                StartCoroutine(LectorDetexto.LecturaTexto(Dialogos[Contador].Mensaje, TextoDialogo, TimepoDePausa));
            }
            // Mensaje para entregar iten al jugador 
            else if (Dialogos[Contador].Itens.Dariten == true)
            {

                darIten();
            }

            PrimeraEntrada = false;
            Contador++;



        }
    }


    /// <summary>
    /// reinicia los valores del Array
    /// </summary>
    void reinicioArray()
    {
        Invoke("Reiniciavalores", 0.2f);

    }


    /// <summary>
    /// reinicia los valores de los e los objetos relacionados
    /// </summary>
    void Reiniciavalores()
    {
        Canvastext.enabled = false;
        TextoDialogo.text = "";
        punteroImagen.enabled = false;

        ValidadorCanvastext = false;
        ValidadorPuntero = false;
        PrimeraEntrada = true;


    }

    //activa el modo Brauler
    void ModoBrawler()
    {
        // Brawler.Start();
        Brawler.InvokeEntraCinematica(Criaturas);
        StartCoroutine(Brawler.iniciaBrauler());
        animacion[NombreAnimacion].speed = 0;
        // Invoke("invokeEntraBrauler", 6f);
    }

    //activa el modo Brauler
    void ModoBrawleriniciales()
    {
        EstoyEnBrauler = true;

        // Brawler.Start();
        animacion[NombreAnimacion].speed = 0;
        SacarCamaraBrawler();
        SeleccionInicial();
        Brawler.InvokeEntraCinematica(Criaturas);
        StartCoroutine(Brawler.iniciaBrauler());

        Invoke("invokeEntraBrauler", 6f);
    }



    void invokeEntraBrauler()
    {
        TriggerBrawler = true;
    }

    IEnumerator EjecutaAnmacion()
    {
        yield return new WaitWhile(() => PlayerMov.transform.position != PlayerMov.pos);

        //posiciona al juagdor
        PlayerMov.followpath(new Vector2(PlayerMov.transform.position.x, PlayerMov.transform.position.y)
                             , new Vector2(transform.position.x, transform.position.y)
                             , PlayerMov.DistanciaRecorrida);

        //espera a que el jugador llegue a la posicion de animacion
        yield return new WaitWhile(() => PlayerMov.path.ruteNum.Count != 0);

        PlayerMov.animt.SetBool("walk", false);
        PlayerMov.StopPlayer = true;
        print("ejecuta animacion");
        //ejecutaanimacion
        animacion.Play(NombreAnimacion);


        yield return 0;
    }

    private void SeleccionInicial()
    {
        string nombreInicial = RetornaNombreCriaturaInicial();
        switch (nombreInicial)
        {
            case "Grismon":
                Criaturas[0].NombreCriatura = "Ihluv";
                break;
            case "Kraten":
                Criaturas[0].NombreCriatura = "Grismon";
                break;
            case "Ihluv":
                Criaturas[0].NombreCriatura = "Kraten";
                break;
        }

        Criaturas[0].nivel = 3;
    }


    private string RetornaNombreCriaturaInicial()
    {
        string ValorARetornar = "";
        libreriaDeScrips LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();


        for (int i = 0; i < 6; i++)
        {
            if (LibreriaS.informacionCrewild.CrewillInstancia[i] != null)
            {
                ValorARetornar = LibreriaS.informacionCrewild.CrewillInstancia[i].NombreTaxonomico;
                break;
            }
        }
        return ValorARetornar;
    }

    /// <summary>
    /// pausa el modo cinematico
    /// </summary>
    public void ActivaTextoCinematica()
    {
        DisparadorDialogo = true;
        animacion[NombreAnimacion].speed = 0;
    }

    /// <summary>
    /// salir de cinematica
    /// </summary>
    public void salirCinematica()
    {

        PlayerMov.pos = PlayerMov.transform.position;

        Invoke("InvokePlayercondicion", 0.1f);
        animacion.Stop();
        DisparadorDialogo = false;

        Contador = 0;
        BoxSelecction.SetActive(true);
        Incrementaevento();

        Devolvercamara();
        if (RepetirCinematica == true)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        //->  poner un player pfef para marcar la cinematica como usada
    }

    private void SacarCamaraBrawler()
    {
        GameObject Camara = GameObject.Find("Main Camera");

        Camara.transform.parent = null;
        /* PlayerMov.pos = PlayerMov.transform.position;
         playertransf.parent = null;
         PlayerMov.pos = Ajustadordeposicon(playertransf.position);
         playertransf.position = PlayerMov.pos;
         PlayerMov.animt.SetBool("walk", false);*/
    }
    private void Devolvercamara()
    {
        GameObject Camara = GameObject.Find("Main Camera");

        Camara.transform.SetParent(FindObjectOfType<movimiento>().GetComponent<Transform>());
    }

    void InvokePlayercondicion()
    {

        PlayerMov.StopPlayer = false;
        PlayerMov.DisparadorEvento = false;
        playertransf.parent = null;
        PlayerMov.pos = Ajustadordeposicon(playertransf.position);
        playertransf.position = PlayerMov.pos;
        PlayerMov.animt.SetBool("walk", false);
    }


    /// <summary>
    /// le da movimiento al modo cinema
    /// </summary>
    void ContinuaCinematica()
    {

        PrimeraEntrada = true;
        TextoDialogo.text = "";
        Canvastext.enabled = false;
        DisparadorDialogo = false;
        ValidadorCanvastext = false;
        animacion[NombreAnimacion].speed = 1;
    }

    Vector3 Ajustadordeposicon(Vector2 ValoresX_y)
    {
        Vector3 valoresAdevolver;
        float ValorX, ValorY, decimalX, DecimalY;
        ValorX = ValoresX_y.x;
        ValorY = ValoresX_y.y;
        decimalX = ValorX - (int)ValoresX_y.x;
        DecimalY = ValorY - (int)ValoresX_y.y;


        if (decimalX < 0.50f && decimalX > 0)
        {
            decimalX = 0.25f;
        }
        else if (decimalX > 0.50f && decimalX < 0)
        {
            decimalX = 0.75f;
        }

        if (DecimalY < 0.50f && DecimalY > 0)
        {
            DecimalY = 0.25f;
        }
        else if (DecimalY > 0.50f && DecimalY < 0)
        {
            DecimalY = 0.75f;
        }
        valoresAdevolver = new Vector3((ValoresX_y.x - (ValorX - (int)ValoresX_y.x)) + decimalX, (ValoresX_y.y - (ValorY - (int)ValoresX_y.y)) + DecimalY, 0);
        print("valor x:" + valoresAdevolver.x + " , valor y: " + valoresAdevolver.y);
        return valoresAdevolver;
    }

    /// <summary>
    /// pausa en modo brauler
    /// </summary>
    void EstadoPausado()
    {
        PrimeraEntrada = true;
        TextoDialogo.text = "";
        Canvastext.enabled = false;
        DisparadorDialogo = false;
        ValidadorCanvastext = false;
    }

    /// <summary>
    /// entra un Iten al Jugador
    /// </summary>
    void darIten()
    {
        Dialogos[Contador].Mensaje = MensajesGlovales.EntregarIten(Dialogos[Contador].Itens.ItenInfo);

        TextoDialogo.text = "";
        StartCoroutine(LectorDetexto.LecturaTexto(Dialogos[Contador].Mensaje, TextoDialogo, TimepoDePausa));
        inventario.DefineList(Dialogos[Contador].Itens.ItenInfo);
    }

    public void Incrementaevento()
    {
        EventoScript.SharedInstancia.IncrementoNumActual();

    }

    public void ActivaObjSecuencia(string NombreEvento)
    {
        EventoScript.SharedInstancia.EncentraEventoName(NombreEvento).SetActive(true);
        Incrementaevento();
        //salirCinematica();
        Invoke("auxinvoke", 2f);
    }

    void auxinvoke()
    {
        this.gameObject.SetActive(false);
    }

    public void DesactivaEvento(string NombreEvento)
    {
        EventoScript.SharedInstancia.EncentraEventoName(NombreEvento).SetActive(false);
        Incrementaevento();
    }
    public void CargarScena( string NameScene)
    {
        SceneManager.LoadScene(NameScene);
    }
}
