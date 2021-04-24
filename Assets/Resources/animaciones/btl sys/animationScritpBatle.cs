using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEditor;



public class animationScritpBatle : MonoBehaviour {

    public static animationScritpBatle SharetIntansie ;

    public Animation animationCinema;

    public bool TriggerEntraceCinema, twovstwo, randomEncounter;

    // ayuda que el modo brauler pause hasta que el jugador pueda 
    public bool PlayerPausaKOSelector = false;

    public bool dialogue, CajadeEleccion, accion;

    /// <summary>
    /// cuando finaliza cuando esta activa pausa el md batalla en ciertos puntos iten y animacion brawler
    /// </summary>
    public bool pausaIenumerator;

    protected Animator[] anim;

    /// <summary>
    /// enemigos de prueba : 0 = enemigos1, 1: enemigos2
    /// </summary>
    public CrewildBase[] EnemigosBatalla;

    protected AnimatorOverrideController[] animatorOverrideController;

    public Image imagenePlayer, ImagenPlayer2, ImagenEnemigo, ImagenEnemigo2;


    private RectTransform selector, selectorCrewild;

    private int acctionint, accionLevesint, attack, states, posSelect, posSelectCrewild2v2;

    public Canvas estadisticasCanvas, criaturasCanvas, menuInicialCanvas, objetosCanvas, equipoCanvas, JugadorCanvas, brawlerCanvas;

    //text box

    public TypeTextClass textclass;

    public NPC_Dialogo DialogoClase;

    private string message;

    public string[] TextoDeBatalla;


    public Text text;

    private RectTransform ActualPosText;

    public float pauseletter, distanceLetter, SpeedMoveText;
    ///
    public int ActualSelNumPlayer, numCrewildActive = 1;

    /// <summary>
    /// captura  el numero de la cruatura por la ques e quiere cambiar
    /// </summary>
    public int AuxCrewildPosInt = 0;

    /// <summary>
    /// Variable sobre la que se activa el metodo de las variables.
    /// </summary>
    public int ActualSeNumEnemy;

    public bool ChoiseCrewildAtack;

    /// <summary>
    /// Si este bool se activa en modo  brawler o normal se declara fin de la partida tanto para el jugador como para el enemigo
    /// </summary>
    public bool GameOverBool = false;


    /// <summary>
    /// panel0 =  principal:
    /// panel1 =  attack:
    /// panel2 =  estates:
    /// panel3 =  crewildSelect:
    /// panel4 =  objects;
    /// panel5 =  rum.
    /// </summary>
    private GameObject ObjSeleccionAccion, ObjattackPanel, ObjEstatesPanel, ObjBulleWildSelectPanel, objPointSelector;


    // batle manager

    private int numAuxArrayText;


    //test batlemanager
    /// <summary>
    /// 0 = player, 
    /// 1 = enemy test 1 ,
    /// 2 = player test 2 ,
    /// 3 = enemy test 3 ,
    /// </summary>
    public StaffStatisticCrewild[] staffStatisCrewild = new StaffStatisticCrewild[4];

    /// <summary>
    /// define un stado para una criatura.
    /// </summary>
    private StaffStatisticCrewild auxStatis;

    private informacionCrewild CrewildInfoPlayer;

    /// <summary>
    /// acceso rapido a todos los scrips
    /// </summary>
    public libreriaDeScrips LibreriaS;

    public enum enumTypeEncounter
    {
        None,
        RandomEncounter,
        pve,
        p2ve2,
        pvp,
        p2vp2



    }




    public enum enumActionsExecute
    {
        None,
        begin,
        select,
        RandomEncounter,
        selectAccion,
        ContinueFight,
        ChangeCrewild



    }

    // Inside your class
    enumActionsExecute AccionBrawler = new enumActionsExecute();





    // Inside your class
    /// <summary>
    /// obj atacante
    /// </summary>
    DefiniteObject[] DefineAtacante = new DefiniteObject[2];


    /// <summary>
    /// objeto atacado
    /// </summary>
    DefiniteObject[] DefineAtacado = new DefiniteObject[2];


    public DefiniteObject ObjAttacked = new DefiniteObject();

    DefiniteObject ObjAttack = new DefiniteObject();

    public bool WaitCourtine, NoPauseTexto, recuperaTionHP, RespuestaADevolver;


    /// <summary>
    /// 0 = enemy , 1 = player , 2 = enemyr2 , 3 = player2
    /// </summary>
    public StaffStatisticCrewild.AttackType[] listAccion = new StaffStatisticCrewild.AttackType[2];

    /// <summary>
    /// Define el orden en que el los enmigos atacan en modo brawler,  0 = enemy , 1 = player , 2 = enemyr2 , 3 = player2
    /// </summary>
    public string[] ListaAcciones = new string[2], listaDeOrder = new string[2], NombreItenAUsar = new string[2], OrdenItenAusar = new string[2];


    /// <summary>
    /// Define sobre quien recae la accion en el uso del iten.
    /// </summary>
    public int SobrequienRecaeEfectoItem = 7;


    /// <summary>
    /// 0 = enemy , 1 = player , 2 = enemyr2 , 3 = player2
    /// </summary>
    public int[] listIntChoiseAttack = new int[4];

    private StaffStatisticCrewild.AttackType[] orderAccion = new StaffStatisticCrewild.AttackType[2];

    /// <summary>
    /// 0 = player , 1 = enemy, Define el tipo iten que se esta usando del inventario.
    /// </summary>
    public ItemTest.ItenType[] ItenUsed = new ItemTest.ItenType[2];

    /// <summary>
    /// 0 = null , 1 - 4, election simpli attack
    /// </summary>
    public int IAElection, IAElection2;

    //si es false el enemigo entra a eleger una accion
    bool EntreAccionEnemy;

    /// <summary>
    /// 0= Enemy, 1 = Player , 2 = Enemy 2 , Player 2 = 3
    /// </summary> 
    public Scrollbar[] HpScrollbar, FatigaBarra, Exp;

    /// <summary>
    /// 0= Enemy, 1 = Player , 2 = Enemy 2 , Player 2 = 3
    /// </summary> 
    public Text[] TextoHP, TextoExp;


    /// <summary>
    /// 0= player, 1 = Enemy, 2 = Player 2 , Enemy 2 = 3
    /// </summary> 
    public float[] HpValue = new float[4], HpWidth = new float[4];
    public float AuxDanTest, numDanger, NumExpIncrementar;

    private bool AuxSubeNiveles = false;

    private CrewildChoiseSelect ChoiseCrewilfScrtip;



    private Inventario inventario;

    //ExitChoiseActionPlayer2v2 = define cuando el  el jugador  ya definio la seleccion dela accion
    private bool ExitChoiseActionPlayer2v2;

    ///define el movimiento del jugador
    private movimiento playerMovScrip;


    /// <summary>
    /// barras de texto que definen los ataques
    /// </summary>
    private Text[] nombresAtaques = new Text[4], contador = new Text[4], GastoEncansancio = new Text[4];


    /// <summary>
    /// 0= player, 1 = Enemy, 2 = Player 2 , Enemy 2 = 3
    /// </summary> 
    private Vector3[] EscalaOriginal = new Vector3[2];

    /// <summary>
    /// desactiva todos los objtos del escenario para mejorar el rendimiento durante las animaciones de lucha
    /// </summary>
    GameObject NcpsOjs, gridObjs, npcEnemigos;

    //audio
    AudioSource Audio;
    public AudioSource AudioVfx;
    /// <summary>
    /// define la velocidad  a la que un audio entra incrementa o decremento su audio entre mas alta mas dura
    /// </summary>
    float VelocidadFade;

    /// <summary>
    /// [0] = player , [1] = enemy
    /// </summary>
    public GameObject[] EstadosBatleCuadro = new GameObject[2];


    void Start() {

        SharetIntansie = this;

        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

        int nivel = 3;
        EnemigosBatalla = new CrewildBase[1];

        EnemigosBatalla[0] = new crear_Crewild_Grismon_Insecto_Energia(nivel);
        /*  EnemigosBatalla[1] = new crear_Crewild_Grismon_Insecto_Energia(nivel);
          EnemigosBatalla[2] = new crear_Crewild_Eghi_salvaje_insecto(nivel);
          EnemigosBatalla[3] = new crear_Crewild_Grismon_Insecto_Energia(nivel);
          EnemigosBatalla[4] = new crear_Crewild_Eghi_salvaje_insecto(nivel);
          EnemigosBatalla[5] = new crear_Crewild_Grismon_Insecto_Energia(nivel); */


      //  EnemigosBatalla[0].Hp = EnemigosBatalla[0].hpTotal / 2;

        NcpsOjs = GameObject.Find("Npcs");
        gridObjs = GameObject.Find("Grid");
        npcEnemigos = GameObject.Find("Npcs Enemigos");

        animationCinema = GetComponent<Animation>();

        DialogoClase = new NPC_Dialogo();
        Invoke("invokeAxuStar", 0.5f);

        estadisticasCanvas = GameObject.Find("informacion").GetComponent<Canvas>();
        criaturasCanvas = GameObject.Find("/Crewild").GetComponent<Canvas>();
        menuInicialCanvas = GameObject.Find("menu inicial").GetComponent<Canvas>();
        objetosCanvas = GameObject.Find("/objetos").GetComponent<Canvas>();
        equipoCanvas = GameObject.Find("equipo").GetComponent<Canvas>();

        brawlerCanvas = GameObject.Find("baltle interfaceC/").GetComponent<Canvas>();

        AuxDanTest = numDanger;


        playerMovScrip = GameObject.Find("personaje").GetComponent<movimiento>();
        //


        selector = GameObject.Find("baltle interfaceC/baltle interface/actions/selector").GetComponent<RectTransform>();

        selectorCrewild = GameObject.Find("baltle interfaceC/baltle interface/SelectorCrewild").GetComponent<RectTransform>();


        // Scrollbars
        HpScrollbar = new Scrollbar[4];
        HpScrollbar[0] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy/hp").GetComponent<Scrollbar>();
        HpScrollbar[1] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ player/hp").GetComponent<Scrollbar>();
        HpScrollbar[2] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy 2/hp").GetComponent<Scrollbar>();
        HpScrollbar[3] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ player 2/hp").GetComponent<Scrollbar>();

        TextoHP = new Text[4];
        TextoHP[0] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy/Hp text").GetComponent<Text>();
        TextoHP[1] = GameObject.Find("baltle interface/HP, Estamine fatigue_ player/Hp text").GetComponent<Text>();
        TextoHP[2] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy 2/Hp text").GetComponent<Text>();
        TextoHP[3] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ player 2/Hp text").GetComponent<Text>();

        FatigaBarra = new Scrollbar[4];

        FatigaBarra[0] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy/Fatige").GetComponent<Scrollbar>();
        FatigaBarra[1] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ player/Fatige").GetComponent<Scrollbar>();
        FatigaBarra[2] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy 2/Fatige").GetComponent<Scrollbar>();
        FatigaBarra[3] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ player 2/Fatige").GetComponent<Scrollbar>();

        Exp = new Scrollbar[4];
        Exp[0] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy/Exp").GetComponent<Scrollbar>();
        Exp[1] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ player/Exp").GetComponent<Scrollbar>();
        Exp[2] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ Enemy 2/Exp").GetComponent<Scrollbar>();
        Exp[3] = GameObject.Find("baltle interfaceC/baltle interface/HP, Estamine fatigue_ player 2/Exp").GetComponent<Scrollbar>();
        //taxt bar
        textclass = new TypeTextClass();

        text = GameObject.Find("baltle interfaceC/barras de texto btl/text").GetComponent<Text>();
        ActualPosText = GameObject.Find("baltle interfaceC/barras de texto btl/text").GetComponent<RectTransform>();


        textclass.initialTexTVar(ActualPosText);

        //panels Selections

        ObjSeleccionAccion = GameObject.Find("baltle interfaceC/baltle interface/actions/acciones");
        ObjattackPanel = GameObject.Find("baltle interfaceC/baltle interface/actions/attack");
        ObjEstatesPanel = GameObject.Find("baltle interfaceC/baltle interface/actions/states");
        objPointSelector = GameObject.Find("baltle interfaceC/baltle interface/actions/selector");


        Audio = GetComponent<AudioSource>();

        AudioVfx = GameObject.Find("baltle interfaceC/Sfxsong").GetComponent<AudioSource>();

        ObjSeleccionAccion.SetActive(false);
        ObjattackPanel.SetActive(false);
        ObjEstatesPanel.SetActive(false);

        EstadosBatleCuadro[0] = GameObject.Find("HP, Estamine fatigue_ player/Estado");
        EstadosBatleCuadro[1] = GameObject.Find("HP, Estamine fatigue_ Enemy/Estado");
        EstadosBatleCuadro[0].SetActive(false);
        EstadosBatleCuadro[1].SetActive(false);



        acctionint = 1;
        accionLevesint = 0;
        attack = 1;
        states = 1;
        posSelect = 1;
        //  Debug.Log(GameObject.FindObjectsOfType<Animator>().Length);

        //iventario itens y demas
        inventario = GameObject.Find("objetos/objetos").GetComponent<Inventario>();


        CrewildInfoPlayer = GameObject.Find("informacion/estadisticas").GetComponent<informacionCrewild>();



        ChoiseCrewilfScrtip = GameObject.Find("Crewild/Crewild").GetComponent<CrewildChoiseSelect>();


        //  animatorOverrideController = new AnimatorOverrideController[5];

        

        imagenePlayer = GameObject.Find("baltle interface/zone fight/Mask Player/crewild player").GetComponent<Image>();
        ImagenPlayer2 = GameObject.Find("baltle interface/zone fight/crewild player 2").GetComponent<Image>();
        ImagenEnemigo = GameObject.Find("baltle interface/zone fight 2/Mask Enemy/crewild enemy").GetComponent<Image>();
        ImagenEnemigo2 = GameObject.Find("baltle interface/zone fight 2/crewild enemy 2").GetComponent<Image>();



        EscalaOriginal[0] = GameObject.Find("baltle interfaceC/baltle interface/zone fight 2/Mask Enemy/crewild enemy").GetComponent<RectTransform>().localScale;
        EscalaOriginal[1] = GameObject.Find("baltle interfaceC/baltle interface/zone fight/Mask Player/crewild player").GetComponent<RectTransform>().localScale;

        //batle manager  entrace the battle.
        if (TriggerEntraceCinema == true)
        {
            batleManager(enumActionsExecute.begin);
        }


    }


    // Update is called once per frame
    void Update() {

        if (criaturasCanvas == null)
        {

        }
        if (TriggerEntraceCinema == true)
        {
            textclass.MoveTowardsText(SpeedMoveText);
        }


        if (dialogue == true)
        {
            DialogueBarText();
        }
        // caja de pregunta
        if (CajadeEleccion == true)
        {

            ActivarSeleccionAccion();

            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                DefinirDeccionEleccionPanel();
            }

        }


        if (accion == true)
        {
            PicinaDeAcciones();
            accions();
            if (EntreAccionEnemy == false)
            {
                EntreAccionEnemy = true;
                IaEnemy();
            }
        }

    }


   
    /// <summary>
    /// define el movimiento en  el cuadro de eleccion de acciones
    /// </summary>
    void ActivarSeleccionAccion()
    {
        DialogoClase.Canvastext.enabled = true;
        DialogoClase.ObJCuadroTexto.SetActive(false);
        DialogoClase.BoxSelecction.SetActive(true);
        DialogoClase.BoxSelecction.GetComponent<RectTransform>().localPosition = new Vector2(292, -278);


        DialogoClase.SeleccionaAccion();

    }

    void DefinirDeccionEleccionPanel()
    {
        DialogoClase.BoxSelecction.SetActive(false);
        DialogoClase.ObJCuadroTexto.SetActive(true);
        DialogoClase.BoxSelecction.GetComponent<RectTransform>().localPosition = new Vector2(395, -116);
        DialogoClase.PosicionNumCorchete = 1;
        DialogoClase.Canvastext.enabled = false;


        CajadeEleccion = false;
        RespuestaADevolver = DialogoClase.RespuestaBool();


    }

    /// <summary>
    /// por aqui voy empeza a  la definicion del orden de entrada en modo brawler 1 v 1 y 2 v 2
    /// </summary>
    void executeAccionMetode()
    {
        if (twovstwo == false)
        {

            //define order

            if (CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].speed > EnemigosBatalla[ActualSeNumEnemy].speed)
            {

                //    orderAccion[0] = listAccion[1];
                //    orderAccion[1] = listAccion[0];

                listaDeOrder[0] = ListaAcciones[1];
                listaDeOrder[1] = ListaAcciones[0];


                DefineAtacante[0] = DefiniteObject.Player;
                DefineAtacante[1] = DefiniteObject.Enemy;


                DefineAtacado[0] = DefiniteObject.Enemy;
                DefineAtacado[1] = DefiniteObject.Player;

                if (ListaAcciones[1] == "Iten" || ListaAcciones[0] == "Iten")
                {
                    OrdenItenAusar[0] = NombreItenAUsar[1];
                    OrdenItenAusar[1] = NombreItenAUsar[0];
                }
            }
            else
            {
                //    orderAccion[0] = listAccion[1];
                //    orderAccion[1] = listAccion[0];


                listaDeOrder[0] = ListaAcciones[0];
                listaDeOrder[1] = ListaAcciones[1];



                DefineAtacante[0] = DefiniteObject.Enemy;
                DefineAtacante[1] = DefiniteObject.Player;

                DefineAtacado[0] = DefiniteObject.Player;
                DefineAtacado[1] = DefiniteObject.Enemy;

                if (ListaAcciones[1] == "Iten" || ListaAcciones[0] == "Iten")
                {
                    OrdenItenAusar[0] = NombreItenAUsar[0];
                    OrdenItenAusar[1] = NombreItenAUsar[1];
                }
            }
            StopAllCoroutines();
            StartCoroutine(CourtineExecuteAccion());
        }

        else if (twovstwo == true)
        {

            definiteaActions2vs2();
            //sin funcionalidad metoso 2 vs 2


        }


    }


    /// <summary>
    /// define el modo que se llevaran los ataques en modo 2vs2
    /// </summary>
    void definiteaActions2vs2()
    {
        //esta variable define las velocidades quien ataca primero
        int[] floatArray = new int[4];

        DefiniteObject[] objDefineAux = new DefiniteObject[4];

        StaffStatisticCrewild.AttackType[] AuxAction = new StaffStatisticCrewild.AttackType[4];

        DefiniteObject[] objAttcketAux = new DefiniteObject[4]
        { DefiniteObject.none, DefiniteObject.none, DefiniteObject.none, DefiniteObject.none };

        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                //enemy 1
                case 0:

                    floatArray[i] = staffStatisCrewild[1].speed;

                    objDefineAux[i] = DefiniteObject.Enemy;

                    AuxAction[i] = listAccion[0];

                    objAttcketAux[i] = DefineobjAttacker(listIntChoiseAttack[0]);

                    break;

                //player 1
                case 1:

                    floatArray[i] = CrewildInfoPlayer.EstadisticasCrewild[ActualSelNumPlayer].speed;

                    objDefineAux[i] = DefiniteObject.Player;

                    AuxAction[i] = listAccion[1];

                    objAttcketAux[i] = DefineobjAttacker(listIntChoiseAttack[1]);
                    break;

                //enemy 2
                case 2:

                    floatArray[i] = staffStatisCrewild[2].speed;

                    objDefineAux[i] = DefiniteObject.Enemy2;

                    AuxAction[i] = listAccion[2];

                    objAttcketAux[i] = DefineobjAttacker(listIntChoiseAttack[2]);
                    break;

                //player 2
                case 3:

                    floatArray[i] = CrewildInfoPlayer.EstadisticasCrewild[ActualSelNumPlayer + 1].speed;

                    objDefineAux[i] = DefiniteObject.Player2;

                    AuxAction[i] = listAccion[3];

                    objAttcketAux[i] = DefineobjAttacker(listIntChoiseAttack[3]);

                    break;

            }


        }

        //ordena quien ataca primero al ultimo
        int AuxOrdenadorArray1;
        int AuxOrdenadorArray2;

        //ordena  quien es el atacante
        DefiniteObject auxDefine1;
        DefiniteObject auxDefine2;

        //ordena cual accion tomo el atacante
        StaffStatisticCrewild.AttackType auxAction1;
        StaffStatisticCrewild.AttackType auxAction2;

        //Ordena  a recive  el ataque
        DefiniteObject AuxAttackerObj1;
        DefiniteObject AuxAttackerObj2;
        // Algoritmo de ordenamiento
        for (int i = 0; i < 4; i++)
        {
            for (int e = 0; e < 4; e++)
            {
                if (floatArray[i] > floatArray[e])
                {
                    AuxOrdenadorArray1 = floatArray[i];
                    AuxOrdenadorArray2 = floatArray[e];

                    auxDefine1 = objDefineAux[i];
                    auxDefine2 = objDefineAux[e];

                    auxAction1 = AuxAction[i];
                    auxAction2 = AuxAction[e];

                    AuxAttackerObj1 = objAttcketAux[i];
                    AuxAttackerObj2 = objAttcketAux[e];




                    floatArray[i] = AuxOrdenadorArray2;
                    floatArray[e] = AuxOrdenadorArray1;

                    objDefineAux[i] = auxDefine2;
                    objDefineAux[e] = auxDefine1;

                    AuxAction[i] = auxAction2;
                    AuxAction[e] = auxAction1;


                    objAttcketAux[i] = AuxAttackerObj2;
                    objAttcketAux[e] = AuxAttackerObj1;

                }

            }

        }

        for (int i = 0; i < 4; i++)
        {

            orderAccion[i] = AuxAction[i];

            DefineAtacante[i] = objDefineAux[i];

            DefineAtacado[i] = objAttcketAux[i];

            print("crewild name " + DefineAtacante[i] + " usa ataque:  " + orderAccion[i] + " En Crewild: " + DefineAtacado[i]);

        }

    }


    /// <summary>
    /// Este metodo llama el  numero de obj a la va  atacar y lo convierte a en una variable legible para para el enum DefiniteObject
    /// se utiliza en el modo 2vs2
    /// </summary>
    /// <param name="numAttacker"></param>
    /// <returns></returns>

    DefiniteObject DefineobjAttacker(int numAttacker)
    {


        DefiniteObject auxDefine = DefiniteObject.none;
        switch (numAttacker)
        {
            //enemy 1
            case 0:

                checked
                {
                    auxDefine = DefiniteObject.Enemy;
                }

                break;

            //player
            case 1:

                checked
                {
                    auxDefine = DefiniteObject.Player;
                }

                break;

            //enemy 2
            case 2:
                checked
                {
                    auxDefine = DefiniteObject.Enemy2;
                }


                break;

            //player 2
            case 3:

                checked
                {
                    auxDefine = DefiniteObject.Player2;
                }

                break;

        }


        checked
        {
            return auxDefine;
        }



    }


    /// <summary>
    /// cortina que ejecuta el modo las acciones en modo batalla
    /// </summary>
    /// <returns></returns>
    IEnumerator CourtineExecuteAccion()
    {

        for (int i = 0; i < orderAccion.Length; i++)
        {
            //Si en turno actual el no hay orden  de accion se sale de modo de batalla
            if (listaDeOrder[i] == null)
            {
                i++;
                break;
            }

            ObjSeleccionAccion.SetActive(false);

            ObjAttack = DefineAtacante[i];
            ObjAttacked = DefineAtacado[i];
            NoPauseTexto = true;


            dialogue = true;


            TextoDeBatalla = new string[1];
            TextoDeBatalla[0] = "";




            if (listaDeOrder[i] == "null")
            {

                //salta toda accion porque esta ya se ejecuto

            }


            //Si la utiliza el opcion para el cambio o change de criatura
            else if (listaDeOrder[i] == "Change")
            {

                TextoDeBatalla[0] = ObjAttack + "  Invoca a " + ValorHpNom(ObjAttacked).Nomber;

                yield return new WaitWhile(() => dialogue == false);

                // animacion guardar
                Animationbrawler(ObjAttack.ToString(), "Guardar");

                yield return new WaitWhile(() => dialogue == true);
                yield return new WaitForSeconds(0.5f);


                ActualSelNumPlayer = AuxCrewildPosInt;

                NombresAtaques();
                UpdateBarraHp(ObjAttack);
                // animacion Aparece criatura 

                Animationbrawler(ObjAttack.ToString(), "Change");
                revisaEstados();
                yield return new WaitWhile(() => dialogue == false);
                yield return new WaitForSeconds(0.5f);
            }

            //se para  indicar el iten  y su uso en batalla si lo tiene hay definir cuales son posibles de usar
            else if (listaDeOrder[i] == "Iten")
            {
                //ejecutacion la "animacion" o manda  llamar la secuencia de particulas 

                TextoDeBatalla[0] = ObjAttack + "  Uso " + OrdenItenAusar[i];



                yield return new WaitWhile(() => dialogue == true);
                yield return new WaitForSeconds(0.2f);


                // AnimacionIten(ObjAttack, OrdenItenAusar[i]);
                pausaIenumerator = true;
                //accion  efecto de item 

                text.text = "";
                Efectoiten(ObjAttack, ObjAttacked, OrdenItenAusar[i]);



                // finaliza efecto
                yield return new WaitWhile(() => pausaIenumerator == true);
                yield return new WaitForSeconds(0.2f);
                SobrequienRecaeEfectoItem = 7;
            }

            /// define si hay un estado alterado como paralisis que se ejecuta al principio del combate pero antes de la secuencia de Batalla

            else if (
                     devuelveCrewildBase(ObjAttack.ToString()).EstadosCrewild == EstadosEnum.Paralize &&
                     probabilidadAciertoEstadoAlterado() == true
                     )
            {

                pausaIenumerator = true;
                DefineEstadoAltertado(ObjAttack.ToString(), devuelveCrewildBase(ObjAttack.ToString()));
                yield return new WaitWhile(() => pausaIenumerator == true);
            }

            /// Esto solo aplica para el jugador, si el cansancio es  igual 0  no se ejecuta ataque 
            else if (RetornaCansancio(ObjAttack.ToString()) <= 0 && ObjAttack.ToString() == "Player")
            {
                print("estoy entre");
                dialogue = true;

                TextoDeBatalla[0] = DefineAtacante[i] + " no pudo atacar tiene poca energia.";

                yield return new WaitWhile(() => dialogue == true);
                yield return new WaitForSeconds(0.5f);
            }

            //Secuencia de batalla
            else
            {
                DevuelveDañoyEvacion DañoyEvacion = new DevuelveDañoyEvacion();



                numDanger = DañoyEvacion.numDaño;


                numDanger = ValordelAtaque(ObjAttack.ToString(), listaDeOrder[i]);

                DañoyEvacion = calculosdañoyPresision(numDanger, ObjAttack, ObjAttacked);


                bool AciertoOfallo = CalculosAcierto.CalculoAcierto(ObjAttack.ToString(), listaDeOrder[i]);



                text.text = "";
                //  si el calculo de la Acierto es malyor o igual que 1 el ataque es efectovo
                if (AciertoOfallo == false)
                {
                    text.text = "";
                    dialogue = true;
                    numDanger = 0;
                    TextoDeBatalla[0] = DañoyEvacion.nombreDelAtacante + " fallo FalloAlUsar" + listaDeOrder[i];

                    yield return new WaitWhile(() => dialogue == true);
                    yield return new WaitForSeconds(0.5f);
                }
                //ejecutacion   de ataque
                else if (AciertoOfallo == true)
                {

                    TextoDeBatalla[0] = DefineAtacante[i] + " Uso " + listaDeOrder[i];

                    yield return new WaitWhile(() => dialogue == true);
                    yield return new WaitForSeconds(0.5f);




                    secuenciaEnbatalla(ObjAttack.ToString(), listaDeOrder[i]);

                    pausaIenumerator = true;
                   
                    yield return new WaitWhile(() => pausaIenumerator == true);

                    yield return new WaitWhile(() => dialogue == false);


                

                   TextoDeBatalla[0] = listaDeOrder[i] + " fue efectivo.";

                    yield return new WaitWhile(() => dialogue == false);
                    yield return new WaitForSeconds(0.5f);
                }

                pausaIenumerator = true;
                StartCoroutine(KoSalirCiclo(i));
                yield return new WaitWhile(() => pausaIenumerator == true);




                yield return new WaitWhile(() => dialogue == true);

            }
            yield return new WaitForSeconds(0.5f);





            if (GameOverBool == true && randomEncounter == true)
            {
                for (int e = 0; e < 2; e++)
                {
                    listaDeOrder[e] = null;
                    ListaAcciones[e] = null;

                }

                //ganacia en  dinero
                print("Gameover");
                GameOver(ObjAttacked);


            }

            NoPauseTexto = false;
            listaDeOrder[i] = null;
            ListaAcciones[i] = null;



        }
        //revisa estados para verificar a accion tormar
        for (int i = 0; i < 2; i++)
        {
            
            if (i == 0 && CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].EstadosCrewild != EstadosEnum.None
               && CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].EstadosCrewild != EstadosEnum.Paralize
                )
            {
                DefineEstadoAltertado("Player", CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer]);
                pausaIenumerator = true;
                ObjAttacked = DefiniteObject.Player;
                ObjAttack = DefiniteObject.Enemy;
                //  CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer];
            }

            else if (i == 1 && EnemigosBatalla[ActualSeNumEnemy].EstadosCrewild != EstadosEnum.None
                && EnemigosBatalla[ActualSeNumEnemy].EstadosCrewild != EstadosEnum.Paralize
                )
            {
                DefineEstadoAltertado("Enemy", EnemigosBatalla[ActualSeNumEnemy]);
                pausaIenumerator = true;
                ObjAttacked = DefiniteObject.Enemy;
                ObjAttack = DefiniteObject.Player;
                //   EnemigosBatalla[ActualSeNumEnemy];
            }

            if(pausaIenumerator == true)
                {
                yield return new WaitWhile(() => pausaIenumerator == true);
                NoPauseTexto = true;
                pausaIenumerator = true;
                StartCoroutine(KoSalirCiclo(10));
                yield return new WaitWhile(() => pausaIenumerator == true);
                NoPauseTexto = false;
            }
           

        }


        // reinicia  los valores  a un estado inicial
        if (GameOverBool == false)
        {
            batleManager(enumActionsExecute.selectAccion);
            ObjSeleccionAccion.SetActive(true);
            accionLevesint = 0;
            posSelect = 1;
            IAElection = 0;
            EntreAccionEnemy = false;
            objPointSelector.SetActive(true);

        }

        else if (GameOverBool == true)
        {
            GameOverBool = false;
        }

       

        yield return 0;
        yield return new WaitForSeconds(0);

    }


    /// <summary>
    /// define el estado Ko de las criaturas
    /// </summary>
    /// <returns></returns>
    public IEnumerator KoSalirCiclo(int i)
    {

        //define estado OK sale del ciclo
        if (ValorHpNom(ObjAttacked).hp <= 0)
        {
            

            // animacion KO
            Animationbrawler(ObjAttacked.ToString(), "KO");

            yield return new WaitForSeconds(1f);

            dialogue = true;
            TextoDeBatalla[0] = ValorHpNom(ObjAttacked).Nomber + " esta en KO";

            // activa estado KO
            EstadoKO(ObjAttacked);

            yield return new WaitWhile(() => dialogue == false);
            yield return new WaitForSeconds(0.5f);

            //Añadir experiencia para la criatura del jugador que esta atacando
            if (ObjAttack == DefiniteObject.Player)
            {
                dialogue = true;
                NumExpIncrementar = ExperienciaADar();

                TextoDeBatalla[0] = ValorHpNom(ObjAttack).Nomber + "gana " + NumExpIncrementar + " Puntos de Exp.";

                yield return new WaitWhile(() => dialogue == true);
                yield return new WaitForSeconds(0.5f);

                while (AuxDanTest != NumExpIncrementar)
                {

                    if (AuxSubeNiveles == true)
                    {
                        AuxSubeNiveles = false;
                        dialogue = true;

                        TextoDeBatalla[0] = ValorHpNom(ObjAttack).Nomber + "Sube de Nvl a  " + ValorHpNom(ObjAttack).ACtualNvl;

                        yield return new WaitWhile(() => dialogue == true && TextoDeBatalla[0] != "");
                        yield return new WaitForSeconds(0.5f);

                    }
                    UpdateExp(ObjAttack);
                    yield return new WaitForSeconds(0.0000001f);
                }


               

                yield return new WaitWhile(() => dialogue == true);
                yield return new WaitForSeconds(0.5f);
            }


            //game over
            if (GameOverBool == true && randomEncounter == false)
            {
              
                for (int e = 0; e < 2; e++)
                {
                    listaDeOrder[e] = null;
                    ListaAcciones[e] = null;

                }
                dialogue = true;

                int dineroGanado = 0;
                dineroGanado = RetornaDinero();


                inventario.Dinero += dineroGanado;

                TextoDeBatalla[0] = ObjAttack.ToString() + " Gana " + dineroGanado + " Doblondolares";

                yield return new WaitWhile(() => dialogue == true);
                yield return new WaitForSeconds(1f);
                //ganacia en  dinero

                print("Gameover");
                GameOver(ObjAttacked);

            }

            else if (GameOverBool == true && randomEncounter == true)
            {
                for (int e = 0; e < 2; e++)
                {
                    listaDeOrder[e] = null;
                    ListaAcciones[e] = null;

                }

                //ganacia en  dinero
                print("Gameover");
                GameOver(ObjAttacked);


            }
            else
            {
                ///pausa el modo de batalla cuando la criatura caida es la del jugador y tiene que elegir una nueva
                if (ObjAttacked == DefiniteObject.Player)
                {
                    PlayerPausaKOSelector = false;

                    ChoiseCrewilfScrtip.TriggerChoiseBrawler = true;
                    ChoiseCrewilfScrtip.SeleccionRapida = true;
                    ChoiseCrewilfScrtip.ActivateCrewildChoise = true;
                    criaturasCanvas.enabled = true;
                    ChoiseCrewilfScrtip.actualizaDatos();

                    yield return new WaitWhile(() => PlayerPausaKOSelector == false);

                }

                UpdateBarraHp(ObjAttacked);
                CalculoDaño.ActualizahpNormal(ObjAttacked.ToString());
                Animationbrawler(ObjAttacked.ToString(), "Change");
                revisaEstados();
                // elimina la orden y la accion de la criatura en siguiente turnos
                if (i == 0)
                {
                    listaDeOrder[i + 1] = null;
                    ListaAcciones[i + 1] = null;

                }

                yield return new WaitWhile(() => dialogue == false);
                dialogue = true;

                TextoDeBatalla[0] = ObjAttacked + "  Invoca a " + ValorHpNom(ObjAttacked).Nomber;
                yield return new WaitWhile(() => dialogue == false);
            }



        }

        pausaIenumerator = false;
    }



    /// <summary>
    /// metodo para salir del modo de batalla
    /// </summary>
    void GameOver(DefiniteObject ObjGameOver)
    {
        if (ObjGameOver == DefiniteObject.Player)
        {
            LibreriaS.ControlManag.TransicionHaciaNegro();
            LibreriaS.ControlManag.TransicionHaciaNegroTexto();
            Invoke("cargarIntro", 7);
          
        }
        else if (ObjGameOver == DefiniteObject.Enemy)
        {
            TextoDeBatalla = new string[1];

            TextoDeBatalla[0] = " ";

            StartCoroutine(TransicionSalirBraler());
            Debug.Log("problema GameOverBool:  " + GameOverBool);
          
            print("finish brawler");
        }

    }
    IEnumerator TransicionSalirBraler()
    {
        LibreriaS.ControlManag.TransicionHaciaNegro();
        yield return new WaitForSeconds(2);
        LibreriaS.ControlManag.TransicionHaciaAlpha();
        exitbrawlwe();
        yield return 0;
    }

    void cargarIntro()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("intro");
    }

    /// <summary>
    /// estos iniciales del modo Brawler
    /// </summary>
    void exitbrawlwe()
    {
        GameOverBool = false;
        TriggerEntraceCinema = false;
        randomEncounter = false;
        brawlerCanvas.enabled = false;
        if (Cinematica.EstoyEnBrauler == false)
        {
            playerMovScrip.StopPlayer = false;
            playerMovScrip.DisparadorEvento = false;
        }

        Npc_movimiento.AllnpcStop = false;

        ChoiseCrewilfScrtip.TriggerChoiseBrawler = false;


        //  define si una  se  empieza a revisar el estado de las crewild fuera de  batalla
        EfectoFueraDeBatalla.verificador();


        StartCoroutine(AudioController.DecrementoGradual(Audio, VelocidadFade / 2));

        GameObject.Find("baltle interfaceC/baltle interface/zone fight 2/Mask Enemy/crewild enemy").GetComponent<RectTransform>().localScale = EscalaOriginal[0];
        GameObject.Find("baltle interfaceC/baltle interface/zone fight/Mask Player/crewild player").GetComponent<RectTransform>().localScale = EscalaOriginal[1];

        ///obejtos del scenario activados despues de que el modo batalla termina
        NcpsOjs.SetActive(true);
        gridObjs.SetActive(true);
        npcEnemigos.SetActive(true);

        //reinicia valores evacion y presicion
        for (int i = 0; i < 6; i++)
        {
            if (CrewildInfoPlayer.CrewillInstancia[i] != null)
            {
                CrewildInfoPlayer.CrewillInstancia[i].Evacion = 3;
                CrewildInfoPlayer.CrewillInstancia[i].precision = 3;
            }
        }



        Npc_movimiento[] movReinicio = FindObjectsOfType<Npc_movimiento>();
        foreach (Npc_movimiento npcmov in movReinicio)
        {

            if (npcmov.transform.GetComponent<NpcBrauler>().Usado == false
                )
            {
                npcmov.validador = true;
            }
            else if (npcmov.transform.GetComponent<NpcBrauler>().desactivador == false
                     && npcmov.transform.GetComponent<NpcBrauler>().Usado == true
                     && npcmov.transform.GetComponent<NpcBrauler>().ActualNpcEnbatalla == true
                     )
            {
                npcmov.transform.GetComponent<NpcBrauler>().CorregirFaceEnemigo();
              //  Invoke("npcmov.transform.GetComponent<NpcBrauler>().CorregirFaceEnemigo();", 1f);
                npcmov.transform.GetComponent<NpcBrauler>().ActualNpcEnbatalla = false;
                npcmov.transform.GetComponent<NpcBrauler>().validadorDeteccion = false;
            }


        }


        GameObject.Find("baltle interfaceC/baltle interface/animation enemy").GetComponent<Image>().enabled = true;
        accionLevesint = 0;
        posSelect = 1;
        IAElection = 0;
        EntreAccionEnemy = false;

        text.text = "";

        accion = false;
        NoPauseTexto = false;
    }

    public void DefineEstadoAltertado(string Afectado ,CrewildBase CRW)
    {

        switch (CRW.EstadosCrewild)
        {
            case EstadosEnum.None:
                Debug.Log("No hay cambio en el estado");
                break;
            case EstadosEnum.poison:
                Debug.Log("esto entrado a la Ejecuta efecto envenenamiento");
                // StartCoroutine( CalculoDaño.EjecutarDañoInvertido(Afectado, ));
               StartCoroutine( PInchosVenenoso.ReduceHp(twovstwo,Afectado, this, CRW));
                break;
            case EstadosEnum.Paralize:
                Debug.Log("esto entrado a Ejecuta efecto envenenamiento");
                // StartCoroutine( CalculoDaño.EjecutarDañoInvertido(Afectado, ));
                StartCoroutine(AtaqueElectrico.EjecutaParalicis(twovstwo, Afectado, this, CRW));
                break;
        }

    }


    public void invokeAxuStar()
    {
        DialogoClase.Inicializacion();
    }
    /// <summary>
    /// actualiza la cantidad de ataques que se pueden utilizar por turno
    /// </summary>
    void NombresAtaques()
    {
        for (int i = 0; i < 4; i++)
        {
            if (CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i] != null)
            {
                nombresAtaques[i].text = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].nombreAtaque;

                contador[i].text = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].cantidadDeusos + "/" + CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].cantidadDeusosTotales;

                GastoEncansancio[i].text = "" + CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].ValorCansancio;

            }
            else
            {
                nombresAtaques[i].text = "";

                contador[i].text = "";

                GastoEncansancio[i].text = "";

            }

        }
    }


    /// <summary>
    /// Define el estado de una criatura y retorna el valor si esta en activo
    /// </summary>
    /// <param name="CriaturaAtacante"> criatura sobre la que se busca medir ese turno si  </param>
    /// <returns></returns>
    DatosClass DefineEstados(DefiniteObject DefineAtacante)
    {
        DatosClass Datos = new DatosClass();

        if (DefineAtacante == DefiniteObject.Player)
        {
            Datos.Estados = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].EstadosCrewild;
        }
        else if (DefineAtacante == DefiniteObject.Enemy)
        {

            Datos.Estados = EnemigosBatalla[ActualSeNumEnemy].EstadosCrewild;

        }

        return Datos;
    }

    /// <summary>
    /// en el modo de batalla cuando se hace un cambio por
    /// </summary>
    public void UpdateBarraHp(DefiniteObject CriaturaKO)
    {

        if (CriaturaKO == DefiniteObject.Player)
        {
            HpScrollbar[1].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].hpTotal;
            imagenePlayer.sprite = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].animaCrewildEspalda[0];

            Exp[1].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experienciaTotal;

            FatigaBarra[1].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansancio / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansanciototal;
        }
        else if (CriaturaKO == DefiniteObject.Enemy)
        {
            HpScrollbar[0].size = EnemigosBatalla[ActualSeNumEnemy].Hp / EnemigosBatalla[ActualSeNumEnemy].hpTotal;
        }

    }

    /// <summary>
    /// cuando una criatura llega a 0hp en como el modo de batlla se pone en estado KO 
    /// tambien declara Game over el jugador como para el enemigo
    /// </summary>
    /// <param name="CriaturaKO"></param>
    void EstadoKO(DefiniteObject CriaturaKO)
    {

        if (CriaturaKO == DefiniteObject.Player)
        {
            CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].EstadosCrewild = EstadosEnum.Ko;



            //Si el contador de Ko es igual al numero total criaturas del jugador se declara Game Over 
            int contadorKO = 0;


            int contadorCantidadActuañ = 0;

            for (int i = 0; i < CrewildInfoPlayer.CrewillInstancia.Length; i++)
            {
                if (CrewildInfoPlayer.CrewillInstancia[i] != null)
                {
                    contadorCantidadActuañ++;
                    if (CrewildInfoPlayer.CrewillInstancia[i].EstadosCrewild == EstadosEnum.Ko)
                    {
                        contadorKO += 1;
                    }
                }

            }

            if (contadorKO == contadorCantidadActuañ)
            {
                //game over para el jugador
                GameOverBool = true;
            }

        }

        //Cambio para el enemigo sobre sobre su criaturas 
        else if (CriaturaKO == DefiniteObject.Enemy)
        {

            EnemigosBatalla[ActualSeNumEnemy].EstadosCrewild = EstadosEnum.Ko;

            while (ActualSeNumEnemy < EnemigosBatalla.Length)
            {

                if (EnemigosBatalla[ActualSeNumEnemy].EstadosCrewild == EstadosEnum.Ko)
                {
                    ActualSeNumEnemy += 1;
                }
                else
                {
                    ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[0];
                    break;
                }


            }

            if (ActualSeNumEnemy >= EnemigosBatalla.Length)
            {
                ActualSeNumEnemy -= 1;
                GameOverBool = true;

            }



        }

    }



    /// <summary>
    /// metodo donde se cumplen los efectos del item
    /// </summary>
    public void Efectoiten(DefiniteObject definiteAtacante, DefiniteObject definiAtacado, string NomAtaque)
    {
        if (definiteAtacante == DefiniteObject.Player)
        {

            for (int i = 0; i < LibreriaS.inventario.listTables[LibreriaS.inventario.NumtablaEnqueSeEsta].Item.Count; i++)
            {
                if (LibreriaS.inventario.listTables[LibreriaS.inventario.NumtablaEnqueSeEsta].Item[i].Nombre == NomAtaque)
                {

                    StartCoroutine(LibreriaS.inventario.listTables[LibreriaS.inventario.NumtablaEnqueSeEsta].Item[i].Funcionbatalla(definiteAtacante, definiAtacado, animationCinema));
                    break;
                }

            }
        }
        else if (definiteAtacante == DefiniteObject.Enemy)
        {


        }

    }
    /// <summary>
    /// Define el si la vida de la criatura atacada si es 0 y devuelve  el el valor del hp y el nombre taxonomico
    /// </summary>
    /// <param name="DefineAtacado">criatura atacada</param>
    /// <returns></returns>
    DatosClass ValorHpNom(DefiniteObject DefineAtacado)
    {
        DatosClass AuxNombreYHp = new DatosClass();

        if (DefineAtacado == DefiniteObject.Player)
        {
            AuxNombreYHp.hp = (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp;
            AuxNombreYHp.Nomber = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].NombreTaxonomico;
            AuxNombreYHp.ACtualNvl = (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Nivel;
        }
        else if (DefineAtacado == DefiniteObject.Enemy)
        {

            AuxNombreYHp.hp = (int)EnemigosBatalla[ActualSeNumEnemy].Hp;
            AuxNombreYHp.Nomber = EnemigosBatalla[ActualSeNumEnemy].NombreTaxonomico;
            AuxNombreYHp.ACtualNvl = (int)EnemigosBatalla[ActualSeNumEnemy].Nivel;
        }

        return AuxNombreYHp;
    }


    /// <summary>
    /// clase llamada pora recopilar datos de los diferentes metodos.
    /// </summary>
    public class DatosClass
    {
        public int hp;
        public string Nomber;
        public EstadosEnum Estados;
        public int ACtualNvl;

    }


    /// <summary>
    /// calcula el daño relativo para cada ataque(valorAtaqueCriatura/10)*ValorDelAtaque*ValorTipoDeAtaqueAltipoCriatura)-ValordefensaVariable)
    /// </summary>
    /// <param name="ValorDeDaaño"> Toma el valor del daño en ataque </param>
    /// <param name="DefineAtacante"> define quien es el atacante </param>
    /// <param name="DefineDefensor">define quien recive el ataque</param>
    /// <returns></returns>
    DevuelveDañoyEvacion calculosdañoyPresision(float ValorDeDaaño, DefiniteObject DefineAtacante, DefiniteObject DefineDefensor)
    {
        int AuxValorRandonAtaque = 0;
        int AuxValorRandonDefensa = 0;

        int AuxValorRandonEvacion = 0;
        int AuxValorRandonPresicion = 0;

        int RangoDeVariacion = 3;

        DevuelveDañoyEvacion RecogerValoresDañoyEvacion = new DevuelveDañoyEvacion();
        //valor del Atacante
        if (DefineAtacante == DefiniteObject.Enemy)
        {
            AuxValorRandonPresicion = (int)Random.Range(EnemigosBatalla[ActualSeNumEnemy].precision - RangoDeVariacion, EnemigosBatalla[ActualSeNumEnemy].precision + RangoDeVariacion);
            AuxValorRandonAtaque = Random.Range(EnemigosBatalla[ActualSeNumEnemy].attack - RangoDeVariacion, EnemigosBatalla[ActualSeNumEnemy].attack + RangoDeVariacion);
            RecogerValoresDañoyEvacion.nombreDelAtacante = EnemigosBatalla[ActualSeNumEnemy].NombreTaxonomico;

        }

        else if (DefineAtacante == DefiniteObject.Player)
        {
            AuxValorRandonPresicion = (int)Random.Range(CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].precision - RangoDeVariacion, CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].precision + RangoDeVariacion);
            AuxValorRandonAtaque = Random.Range(CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].attack - RangoDeVariacion, CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].attack + RangoDeVariacion);
            RecogerValoresDañoyEvacion.nombreDelAtacante = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].NombreTaxonomico;
        }

        //valor variable de defensa

        if (DefineDefensor == DefiniteObject.Enemy)
        {
            AuxValorRandonEvacion = (int)Random.Range(EnemigosBatalla[ActualSeNumEnemy].Evacion - RangoDeVariacion, EnemigosBatalla[ActualSeNumEnemy].Evacion + RangoDeVariacion);
            AuxValorRandonDefensa = Random.Range(EnemigosBatalla[ActualSeNumEnemy].defence - RangoDeVariacion, EnemigosBatalla[ActualSeNumEnemy].defence + RangoDeVariacion);
        }

        else if (DefineDefensor == DefiniteObject.Player)
        {
            AuxValorRandonEvacion = (int)Random.Range(CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Evacion - RangoDeVariacion, CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Evacion - RangoDeVariacion);
            AuxValorRandonDefensa = Random.Range(CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].defence - RangoDeVariacion, CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].defence - RangoDeVariacion);

        }

        int PrescionDeataque = AuxValorRandonPresicion - AuxValorRandonEvacion;
        int ValorAtaqueRetornar = 0;
        if (PrescionDeataque >= 0)
        {
            ValorAtaqueRetornar = (int)(((AuxValorRandonAtaque / 10) * ValorDeDaaño * 1) - AuxValorRandonDefensa);
            //  Debug.Log("AuxValorRandonAtaque ="+ AuxValorRandonAtaque + ", ValorDeDaaño ="+ ValorDeDaaño + " , AuxValorRandonDefensa ="+ AuxValorRandonDefensa);
            if (ValorAtaqueRetornar <= 0)
            {

                //  ValorAtaqueRetornar = 1;

            }

        }



        RecogerValoresDañoyEvacion.numDaño = ValorAtaqueRetornar;
        RecogerValoresDañoyEvacion.numPresicion = PrescionDeataque;

        return RecogerValoresDañoyEvacion;



    }




    /// <summary>
    /// metodo usado para los textos tiene 3 diferentes modos uso
    /// </summary>
    void DialogueBarText()
    {
        if (TextoDeBatalla.Length == numAuxArrayText && NoPauseTexto == false)
        {

            // play();


            text.text = "";
            numAuxArrayText = 0;
            dialogue = false;
            return;
        }

        else if (TextoDeBatalla.Length == numAuxArrayText && NoPauseTexto == true)
        {

            play();

            numAuxArrayText = 0;
            dialogue = false;
            return;
        }


        if (
           textclass.EstoyTypeText == false
           && textclass.EndReadText == false
           && TextoDeBatalla[numAuxArrayText] != ""
           )
        {

            text.text = "";
            textclass.initialposText();



            message = TextoDeBatalla[numAuxArrayText];

            StartCoroutine(textclass.textMetode(message, text, pauseletter, distanceLetter));
        }

        else if
           (
           Input.GetKeyDown(KeyCode.Space)
           && text.text.Length < textclass.rangertext
           && textclass.triggercompleteTExt == false
           && textclass.EstoyTypeText == true
           && textclass.postext == textclass.ActualPosText.localPosition
           && textclass.EndReadText == false
           )
        {
            textclass.triggercompleteTExt = true;

        }


        else if (

                 textclass.EndReadText == true && Input.GetKeyDown(KeyCode.Space) && NoPauseTexto == false
              || NoPauseTexto == true && textclass.EndReadText == true

                )
        {

            numAuxArrayText++;
            textclass.EndReadText = false;
        }
    }




    /// <summary>
    /// posicion del cursos en modo seleccion de accion
    /// </summary>
    /// <returns></returns>
    public int SelectPos()
    {
        if (posSelect == 1)
        {

            selector.localPosition = new Vector2(245.6f, -176.6224f);

        }

        else if (posSelect == 2)
        {
            selector.localPosition = new Vector2(359.2f, -176.6224f);

        }



        else if (posSelect == 3)
        {
            selector.localPosition = new Vector2(245.6f, -244.8f);

        }

        else if (posSelect == 4)
        {
            selector.localPosition = new Vector2(359.2f, -244.8f);

        }





        if (posSelect <= 0)
        {
            posSelect = 4;

        }
        else if (posSelect >= 5)
        {

            posSelect = 1;
        }




        if (Input.GetKeyDown(KeyCode.D))
        {
            posSelect += 1;
            LibreriaS.audioMenus.Audio.Play();
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            posSelect -= 1;
            LibreriaS.audioMenus.Audio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            posSelect += 2;
            LibreriaS.audioMenus.Audio.Play();
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            posSelect -= 2;
            LibreriaS.audioMenus.Audio.Play();
        }

        return posSelect;

    }

    /// <summary>
    /// define el tipo de creawild al que cual se le va aplicar el efecto en modo choise
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public int SelectCrewildInBrawler(int pos)
    {
        if (pos == 1)
        {

            selectorCrewild.localPosition = new Vector2(-250f, -28f);

        }

        else if (pos == 2)
        {
            selectorCrewild.localPosition = new Vector2(-125f, -28f);

        }



        else if (pos == 3)
        {
            selectorCrewild.localPosition = new Vector2(150f, 192f);

        }

        else if (pos == 4)
        {
            selectorCrewild.localPosition = new Vector2(277f, 192);

        }





        if (pos <= -1)
        {
            pos = 3;

        }
        else if (pos == 0)
        {
            pos = 4;
        }
        else if (pos == 5)
        {

            pos = 1;
        }
        else if (pos >= 6)
        {

            pos = 2;
        }



        if (Input.GetKeyDown(KeyCode.D))
        {
            pos += 1;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            pos -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            pos += 2;
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            pos -= 2;
        }

        return pos;

    }




    /// <summary>
    /// metodo que permite navegar por los paneles de seleccion de accion,  posturas, cambios  y inventario 
    /// </summary>
    void accions()
    {

      

        if (accionLevesint == 0)
        {
           
            if (criaturasCanvas.enabled == false)
            {
               
                acctionint = SelectPos();

            }



            //seleccion accion firts panels.

            //attack
            if (acctionint == 1)
            {


                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ObjSeleccionAccion.SetActive(false);
                    ObjattackPanel.SetActive(true);
                    accionLevesint = 1;
                    posSelect = 1;
                    LibreriaS.audioMenus.Audio.Play();
                }




            }
            //stand posture fight
            else if (acctionint == 2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ObjSeleccionAccion.SetActive(false);
                    ObjEstatesPanel.SetActive(true);
                    accionLevesint = 2;
                    posSelect = 1;
                    LibreriaS.audioMenus.Audio.Play();
                }




            }
            //choise action
            else if (acctionint == 3)
            {


                if (Input.GetKeyDown(KeyCode.Space) && criaturasCanvas.enabled == false)
                {
                    //  PanelLevel0.SetActive(false);
                    Invoke("InvokeCanvasEleccionCrewild", 0.15f);

                    ChoiseCrewilfScrtip.PauseStart();
                    ChoiseCrewilfScrtip.ActivateCrewildChoise = true;
                    ChoiseCrewilfScrtip.TriggerChoiseBrawler = true;


                    ChoiseCrewilfScrtip.updateVars(ActualSelNumPlayer + 1);
                    LibreriaS.SeleccionDeCriaturas.actualizaDatos();
                    LibreriaS.audioMenus.Audio.Play();
                }



            }
            //Inventario
            else if (acctionint == 4)
            {


                if (Input.GetKeyDown(KeyCode.Space))
                {

                    Invoke("InvokeCanvasInventario", 0.15f);
                    accionLevesint = 4;
                    inventario.TriggerInventary = true;
                    inventario.brawlerMode = true;
                    LibreriaS.audioMenus.Audio.Play();
                }


            }

        }

        //attack
        else if (accionLevesint == 1)
        {

            LevelAtaque();


        }

        // posiciones
        else if (accionLevesint == 2)
        {
            levelPosturas();
        }

        // choise crywild
        else if (accionLevesint == 3)
        {

        }

        // objects menu
        else if (accionLevesint == 4)
        {
        }
    }

    // define el numero del enemigo al que se ataca, el numero del ataque se esta usando 
    // Da el disparo para la salida del metodo seleccion he incrementa el numero de  NumCrewildAttack
    void selectEnemyAttact(int NumCrewildPlayer, int NumAttackSelect)
    {
        listIntChoiseAttack[NumCrewildPlayer] = NumAttackSelect;

        ChoiseCrewildAtack = false;


        RegresaPanel1();

        if (numCrewildActive == 1)
        {
            numCrewildActive = 3;

        }

    }

    /// <summary>
    /// clase  que recoge los valores de  evación y el nombredel atacante;
    /// </summary>
    public class DevuelveDañoyEvacion
    {
        public int numDaño;
        public int numPresicion;
        public string nombreDelAtacante;


    }

    void InvokeCanvasInventario()
    {
        objetosCanvas.enabled = true;
    }

    void RegresaPanel1()
    {
        ObjSeleccionAccion.SetActive(true);
        ObjattackPanel.SetActive(false);
        ObjEstatesPanel.SetActive(false);
        accionLevesint = 0;
        posSelect = 1;

    }

    void regresaPanelAttack()
    {
        ObjSeleccionAccion.SetActive(false);
        ObjattackPanel.SetActive(true);
        ObjEstatesPanel.SetActive(false);
        accionLevesint = 1;

    }

    void InvokeCanvasEleccionCrewild()
    {
        criaturasCanvas.enabled = true;

    }

    /// <summary>
    /// este metodo se llama cuando  se se activa las  para seleccionar el ataque del crewild
    /// </summary>
    void LevelAtaque()
    {
        if (accionLevesint == 1)
        {
            if (ChoiseCrewildAtack == false)
            {
                attack = SelectPos();

                if (Input.GetKeyDown(KeyCode.C))
                {
                    ObjSeleccionAccion.SetActive(true);
                    ObjattackPanel.SetActive(false);
                    accionLevesint = 0;
                    posSelect = 1;
                    LibreriaS.audioMenus.Audio.Play();
                }


                if (attack == 1)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {


                        SelectAttackAction(ActualSelNumPlayer, 0);

                        LibreriaS.audioMenus.Audio.Play();
                    }
                }

                else if (attack == 2)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //listAccion[0] = infoCrewildScrit.EstadisticasCrewild[ActualCrewildSelectNum].attackType[1].attack;
                        SelectAttackAction(ActualSelNumPlayer, 1);

                        LibreriaS.audioMenus.Audio.Play();
                    }

                }
                else if (attack == 3)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        // listAccion[0] = infoCrewildScrit.EstadisticasCrewild[ActualCrewildSelectNum].attackType[2].attack;
                        SelectAttackAction(ActualSelNumPlayer, 2);

                        LibreriaS.audioMenus.Audio.Play();
                    }
                }
                else if (attack == 4)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //  listAccion[0] = infoCrewildScrit.EstadisticasCrewild[ActualCrewildSelectNum].attackType[3].attack;
                        SelectAttackAction(ActualSelNumPlayer, 3);
                        LibreriaS.audioMenus.Audio.Play();
                    }

                }



            }
            //sin funcionalidad borrar
            // añadir la opcion de exits de aqui.
            else if (ChoiseCrewildAtack == true)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    ChoiseCrewildAtack = false;
                    regresaPanelAttack();
                }

                // choise obj attack
                posSelectCrewild2v2 = SelectCrewildInBrawler(posSelectCrewild2v2);

                //crewild # 1 player
                if (posSelectCrewild2v2 == 1)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        selectEnemyAttact(numCrewildActive, 1);
                    }
                }
                //crewild # 2 player          
                else if (posSelectCrewild2v2 == 2)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        selectEnemyAttact(numCrewildActive, 3);
                    }

                }
                //crewild # 1 Enemy 
                else if (posSelectCrewild2v2 == 3)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        selectEnemyAttact(numCrewildActive, 0);
                    }
                }
                //crewild # 2 enemy
                else if (posSelectCrewild2v2 == 4)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        selectEnemyAttact(numCrewildActive, 2);
                    }

                }


                if (!ExitChoiseActionPlayer2v2)
                {


                }

            }

        }

    }

    /// <summary>
    /// define cuales son las posturas en la batalla depende postura aumenta o disminuye el nivel de cansancio
    /// </summary>
    void levelPosturas()
    {
        states = SelectPos();




        if (Input.GetKeyDown(KeyCode.C))
        {
            ObjSeleccionAccion.SetActive(true);
            ObjEstatesPanel.SetActive(false);
            accionLevesint = 0;
            posSelect = 2;

            LibreriaS.audioMenus.Audio.Play();
        }


        if (states == 1)
        {


        }
        else if (states == 2)
        {



        }
        else if (states == 3)
        {



        }
        else if (states == 4)
        {


        }

    }


    /// <summary>
    ///  Define quien cual es  ataque  autilizar y la criatura que lo llama
    /// </summary>
    /// <param name="intActualCrewildSelect">define cual criatura en la lista es la seleccionada</param>
    /// <param name="numAttack">Int que  defene cual el numero </param>
    void SelectAttackAction(int intActualCrewildSelect, int numAttack)
    {
        if (twovstwo == false)
        {
            if (CrewildInfoPlayer.CrewillInstancia[intActualCrewildSelect].ataques[numAttack] != null)
            {
                if (CrewildInfoPlayer.CrewillInstancia[intActualCrewildSelect].ataques[numAttack].cantidadDeusos > 0)
                {
                    ListaAcciones[1] = CrewildInfoPlayer.CrewillInstancia[intActualCrewildSelect].ataques[numAttack].nombreAtaque;

                    CrewildInfoPlayer.CrewillInstancia[intActualCrewildSelect].ataques[numAttack].cantidadDeusos -= 1;
                    NombresAtaques();

                }
            }


        }


        else if (twovstwo == true)
        {

            //listAccion define cual ataque se va realizar no a quien sele va a realizar, ChoiseCrewildAtack activa la seleccion de a quien se le 
            // ataca
            if (numCrewildActive == 1)
            {

                listAccion[1] = CrewildInfoPlayer.EstadisticasCrewild[intActualCrewildSelect].attackType[numAttack].attack;
                ChoiseCrewildAtack = true;
                DisablePanels();
            }

            else if (numCrewildActive == 3)
            {

                listAccion[3] = CrewildInfoPlayer.EstadisticasCrewild[intActualCrewildSelect + 1].attackType[numAttack].attack;
                ChoiseCrewildAtack = true;
                DisablePanels();
            }

        }
    }

    /// <summary>
    /// activador de panel
    /// </summary>
    void DisablePanels()
    {
        ObjSeleccionAccion.SetActive(false);
        ObjattackPanel.SetActive(false);


    }

    /// <summary>
    /// activa paneles
    /// </summary>
    void EnablePanels()
    {
        ObjSeleccionAccion.SetActive(true);
        ObjattackPanel.SetActive(true);
    }



    /// <summary>
    /// gestos del modo de batalla en el juego controla algunas acciones como la eleccion del comienzo de la acciones
    /// </summary>
    /// <param name="BatleManagerAccion"></param>
    public void batleManager(enumActionsExecute BatleManagerAccion)
    {

        if (BatleManagerAccion == enumActionsExecute.begin)
        {


            if (twovstwo == false)
            {
                ActualSeNumEnemy = 0;

                ActualSelNumPlayer =  PrimerCrwildEnlista();

                if (randomEncounter == true)
                {
                    animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/brawler/1vs1/entrace R"), "entrace");
                    StartCoroutine(SecuenciaEncuentroRandom());
                }
                else
                {
                    animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/brawler/1vs1/entrace"), "entrace");
                    StartCoroutine(SecuenciaEntroNpc());
                }

                Invoke("DesactivadorAuxobj",0.1f);

                animationCinema.Play("entrace");

                VelocidadFade = 5f;

                StartCoroutine(AudioController.IncremntoGradual(Audio, VelocidadFade));

                // listAccion = new StaffStatisticCrewild.AttackType[2];
                //orderAccion = new StaffStatisticCrewild.AttackType[2];

                ListaAcciones = new string[2];
                listaDeOrder = new string[2];
                NombreItenAUsar = new string[2];
                OrdenItenAusar = new string[2];


                SobrequienRecaeEfectoItem = 7;

                //Octener barras de texto para saber los ataques disponible en el modo de batalla
                for (int i = 0; i < 4; i++)
                {

                    nombresAtaques[i] = GameObject.Find("baltle interfaceC/baltle interface/actions/attack/Ataque" + (i + 1) + "/text").GetComponent<Text>();
                    contador[i] = GameObject.Find("baltle interfaceC/baltle interface/actions/attack/Ataque" + (i + 1) + "/contador").GetComponent<Text>();
                    GastoEncansancio[i] = GameObject.Find("baltle interfaceC/baltle interface/actions/attack/Ataque" + (i + 1) + "/Cansancio").GetComponent<Text>();

                }

                TextoHP[0].text = "HP: " + (int)EnemigosBatalla[ActualSeNumEnemy].Hp + "/" + (int)EnemigosBatalla[ActualSeNumEnemy].hpTotal;
                TextoHP[1].text = "HP: " + (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp + "/" + (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].hpTotal;


                NombresAtaques();


                DefineAtacante = new DefiniteObject[2];

                DefineAtacado = new DefiniteObject[2];


                imagenePlayer.sprite = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].animaCrewildEspalda[0];
                ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[0];


                HpScrollbar[1].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].hpTotal;
                HpScrollbar[0].size = EnemigosBatalla[ActualSeNumEnemy].Hp / EnemigosBatalla[ActualSeNumEnemy].hpTotal;

                Exp[1].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experienciaTotal;

                FatigaBarra[1].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansancio / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansanciototal;
                // LibreriaS.SeleccionDeCriaturas.actualizaDatos();
            }
            //esta obsoleto
            // arreglar cuando se haga el metodo  2 Vs 2
            else if (twovstwo == true)
            {

                print("este metodo esta obsoleto es el 2 vs 2 volver a hacer");
                Debug.Break();
            }






        }
        else if (BatleManagerAccion == enumActionsExecute.select)
        {

            accion = true;
        }


        else if (BatleManagerAccion == enumActionsExecute.RandomEncounter)
        {
            TextoDeBatalla = new string[1];

            TextoDeBatalla[0] = "encuentro  random";

            dialogue = true;
        }

        else if (BatleManagerAccion == enumActionsExecute.selectAccion)
        {
            // Debug.Log("take your money");
            text.text = "selecciona Accion";
            accion = true;
        }

        else if (BatleManagerAccion == enumActionsExecute.ContinueFight)
        {

            TextoDeBatalla = new string[1];
            TextoDeBatalla[0] = "";
            dialogue = true;

        }





    }


  void revisaEstados()
    {
        //player
        if (CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].EstadosCrewild != EstadosEnum.None)
            SecuenciasAux.CambiaEstadoCrewild(DefiniteObject.Player, CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer]);
        else
            SecuenciasAux.EstadoNormal(DefiniteObject.Player, CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer]);

        //enemy
         if (EnemigosBatalla[ActualSeNumEnemy].EstadosCrewild != EstadosEnum.None)
            SecuenciasAux.CambiaEstadoCrewild(DefiniteObject.Enemy, EnemigosBatalla[ActualSeNumEnemy]);
        else
            SecuenciasAux.EstadoNormal(DefiniteObject.Enemy, EnemigosBatalla[ActualSeNumEnemy]);
    }
/// <summary>
/// desactiva los objetos con un margen de tiempor atraves de Invoke
/// </summary>
    void DesactivadorAuxobj()
    {
        if (Cinematica.EstoyEnBrauler == false)
         NcpsOjs.SetActive(false);

        gridObjs.SetActive(false);
        npcEnemigos.SetActive(false);
    }

    int PrimerCrwildEnlista()
    {
       int NumARetornar = 0;
        for (int i = 0; i< 5; i++)
        {
            if (CrewildInfoPlayer.CrewillInstancia[i] !=null && CrewildInfoPlayer.CrewillInstancia[i].EstadosCrewild != EstadosEnum.Ko )
            {
                NumARetornar = i;
                break;
            } 
        }

        return NumARetornar;
    }

    IEnumerator SecuenciaEncuentroRandom()
    {

        libreriaAnimaciones LAnimaciones = GetComponent<libreriaAnimaciones>();

        yield return new WaitWhile(() => LAnimaciones.Disparador == false);

        LAnimaciones.Disparador = false;

        animationCinema[NameActualAnimationPlaying(animationCinema)].speed = 0;

        TextoDeBatalla = new string[1];

        TextoDeBatalla[0] = "";

        TextoDeBatalla[0] = "Acabas de Encontrar un " + EnemigosBatalla[ActualSeNumEnemy].NombreTaxonomico + " salvaje";

        dialogue = true;

        yield return new WaitWhile(() => dialogue == true);

        TextoDeBatalla[0] = " Acabalo " + EnemigosBatalla[ActualSeNumEnemy].NombreTaxonomico;
        animationCinema[NameActualAnimationPlaying(animationCinema)].speed = 1;

        dialogue = true;

        yield return new WaitWhile(() => LAnimaciones.Disparador == false);

        LAnimaciones.Disparador = false;

        

        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Criatura");
        AudioVfx.PlayOneShot(clip1);

        yield return 0;

    }

    IEnumerator SecuenciaEntroNpc()
    {
        libreriaAnimaciones LAnimaciones = GetComponent<libreriaAnimaciones>();

        yield return new WaitWhile(() => LAnimaciones.Disparador == false);

        LAnimaciones.Disparador = false;

        animationCinema[NameActualAnimationPlaying(animationCinema)].speed = 0;


        

        TextoDeBatalla = new string[1];

        TextoDeBatalla[0] = "";

        Npc_movimiento[] NpcmovScript = FindObjectsOfType<Npc_movimiento>();

        string AuxReceptorNombre = "";
        foreach (Npc_movimiento npcmov in NpcmovScript)
        {

            if (
                  npcmov.transform.GetComponent<NpcBrauler>().desactivador == false
               && npcmov.transform.GetComponent<NpcBrauler>().Usado == true
               && npcmov.transform.GetComponent<NpcBrauler>().ActualNpcEnbatalla == true
               )
            {
                AuxReceptorNombre = npcmov.transform.GetComponent<NpcBrauler>().MensajeInicioBatalla;

                   TextoDeBatalla[0] = "Te enfrentas a "+ AuxReceptorNombre;
            }
        }
        if (TextoDeBatalla[0] == "")
        {
                TextoDeBatalla[0] = "Te enfrentas a EnemigoX(mensaje por defecto)";
        }

        dialogue = true;

     

        yield return new WaitWhile(() => dialogue == true);

        dialogue = true;
        animationCinema[NameActualAnimationPlaying(animationCinema)].speed = 1;
        TextoDeBatalla[0] = ""+ AuxReceptorNombre + " dispara  un "+ EnemigosBatalla[ActualSeNumEnemy].NombreTaxonomico;


        yield return new WaitWhile(() => LAnimaciones.Disparador == false);

        LAnimaciones.Disparador = false;


        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Criatura");
        AudioVfx.PlayOneShot(clip1);

        yield return new WaitWhile(() => dialogue == true && LAnimaciones.Disparador == false);
       

        LAnimaciones.Disparador = false;

        animationCinema[NameActualAnimationPlaying(animationCinema)].speed = 0;

       
        //mensaje del rival

        yield return new WaitWhile(() => dialogue == true);
        dialogue = true;
        TextoDeBatalla[0] = " Acabalo " + LibreriaS.informacionCrewild.CrewillInstancia[ActualSelNumPlayer].NombreTaxonomico;
        animationCinema[NameActualAnimationPlaying(animationCinema)].speed = 1;


        yield return new WaitWhile(() => LAnimaciones.Disparador == false);

        revisaEstados();

        LAnimaciones.Disparador = false;
       
        AudioVfx.PlayOneShot(clip1);


        yield return new WaitWhile(() => dialogue == true);


        yield return new WaitWhile(() => LAnimaciones.Disparador == false);

        LAnimaciones.Disparador = false;

       

        yield return new  WaitForSeconds(1f);
        yield return 0;
    }

    /// <summary>
    /// define las accion seleccionadas durante la batalla y de inicio la secuancia de ejecucion de acciones 
    /// hasta que no se completa no inicio
    /// </summary>
    void PicinaDeAcciones()
    {
        //trigger pool accion y execute secuence Accions.
        if (

               /*listAccion[0] != StaffStatisticCrewild.AttackType.None
               && listAccion[1] != StaffStatisticCrewild.AttackType.None*/
                ListaAcciones[0] != null
            && ListaAcciones[1] != null

            && accion == true
            && twovstwo == false

           )
        {
            accion = false;
            ObjattackPanel.SetActive(false);
            objPointSelector.SetActive(false);
            Debug.Log("ataque Player " + ListaAcciones[1]);
            Debug.Log("ataque enemy " + ListaAcciones[0]);


            executeAccionMetode();
        }
        else if (
                    accion == true
                 && twovstwo == true
                )
        {
            if (
                 listAccion[0] != StaffStatisticCrewild.AttackType.None
              && listAccion[1] != StaffStatisticCrewild.AttackType.None
              && listAccion[2] != StaffStatisticCrewild.AttackType.None
              && listAccion[3] != StaffStatisticCrewild.AttackType.None
              && ChoiseCrewildAtack == false
               )
            {

                accion = false;
                ObjattackPanel.SetActive(false);
                objPointSelector.SetActive(false);
                Debug.Log("ataque Player " + listAccion[0]);
                Debug.Log("ataque enemy " + listAccion[1]);
                Debug.Log("ataque Player 2 " + listAccion[2]);
                Debug.Log("ataque enemy  2" + listAccion[3]);
                executeAccionMetode();
            }
        }

    }


    /// <summary>
    /// En ejecucion de modo de batalla  actualiza la barra de saluc de las criatuas
    /// </summary>
    /// <param name="NameObje">nombre de la player o enemigo</param>
    void UpdateHp(DefiniteObject NameObje)
    {
        //player 1
        if (NameObje == DefiniteObject.Player)
        {
          //  print("pase por player daño");
            HpScrollbar[(int)NameObje].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].hpTotal;



            if (AuxDanTest < numDanger)
            {
                if (recuperaTionHP == false)
                {
                     CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp -= 0.2f + Time.deltaTime;
                     AuxDanTest += 0.2f + Time.deltaTime;
                    imagenePlayer.sprite = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].animaCrewildEspalda[1];


                }
                else if (recuperaTionHP == true)
                {
                    CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp += 0.2f + Time.deltaTime;
                    AuxDanTest += 0.2f + Time.deltaTime;
                   
                }

            }

            else if (CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp < 0)
            {
                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp = 0;
              
                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].EstadosCrewild = EstadosEnum.Dead;

                imagenePlayer.sprite = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].animaCrewildEspalda[1];


                AuxDanTest = 0;
                numDanger = 0;

            }
            //regulador hp.
            else if (AuxDanTest > numDanger)
            {
                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp + 1;
                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp = (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Hp;
                AuxDanTest = 0;
                numDanger = 0;
                recuperaTionHP = false;
                imagenePlayer.sprite = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].animaCrewildEspalda[0];

            }

        }


        //enemy
        else if (NameObje == DefiniteObject.Enemy)
        {
            HpScrollbar[(int)NameObje].size = EnemigosBatalla[ActualSeNumEnemy].Hp / EnemigosBatalla[ActualSeNumEnemy].hpTotal;

        if (AuxDanTest < numDanger)
            {
                EnemigosBatalla[ActualSeNumEnemy].Hp -= 0.2f + Time.deltaTime;
                AuxDanTest += 0.2f + Time.deltaTime;
                ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[1];
            }

            else if (EnemigosBatalla[ActualSeNumEnemy].Hp < 0)
            {
             
                EnemigosBatalla[ActualSeNumEnemy].Hp = 0;
                AuxDanTest = 0;
                numDanger = 0;
             
                
                ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[1];
            }
            //regulador hp.
            else if (AuxDanTest > numDanger)
            {
                EnemigosBatalla[ActualSeNumEnemy].Hp = EnemigosBatalla[ActualSeNumEnemy].Hp + 1;
                EnemigosBatalla[ActualSeNumEnemy].Hp = (int)EnemigosBatalla[ActualSeNumEnemy].Hp;
                ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[0];

                AuxDanTest = 0;
                numDanger = 0;
            }

        }

        //player 2
        else if (NameObje == DefiniteObject.Player2)
        {
            HpScrollbar[2].size = staffStatisCrewild[2].Hp / staffStatisCrewild[2].MaxlHp;

            if (AuxDanTest == numDanger)
            {

            }

            else if (AuxDanTest < numDanger)
            {
                staffStatisCrewild[2].Hp -= 0.2f + Time.deltaTime;
                AuxDanTest += 0.2f + Time.deltaTime;
            }

            else if (staffStatisCrewild[2].Hp < 0)
            {
                print("que paso llegue 0");
                staffStatisCrewild[2].Hp = 0;
                AuxDanTest = 0;
                numDanger = 0;
                staffStatisCrewild[2].StatesCreiwild = StaffStatisticCrewild.StatesBrawler.Dead;
            }
            //regulador hp.
            else if (AuxDanTest > numDanger)
            {
                staffStatisCrewild[2].Hp = staffStatisCrewild[2].Hp + 1;
                staffStatisCrewild[2].Hp = (int)staffStatisCrewild[2].Hp;


                AuxDanTest = 0;
                numDanger = 0;
            }

        }
    

        //enemy2
        else if (NameObje == DefiniteObject.Enemy2)
        {
            HpScrollbar[3].size = staffStatisCrewild[2].Hp / staffStatisCrewild[2].MaxlHp;

            if (AuxDanTest == numDanger)
            {

            }

            else if (AuxDanTest < numDanger)
            {
                staffStatisCrewild[2].Hp -= 0.2f + Time.deltaTime;
                AuxDanTest += 0.2f + Time.deltaTime;
            }

            else if (staffStatisCrewild[2].Hp < 0)
            {
                print("que paso llegue 0");
                staffStatisCrewild[2].Hp = 0;
                AuxDanTest = 0;
                numDanger = 0;
                staffStatisCrewild[2].StatesCreiwild = StaffStatisticCrewild.StatesBrawler.Dead;
            }
            //regulador hp.
            else if (AuxDanTest > numDanger)
            {
                staffStatisCrewild[2].Hp = staffStatisCrewild[2].Hp + 1;
                staffStatisCrewild[2].Hp = (int)staffStatisCrewild[2].Hp;


                AuxDanTest = 0;
                numDanger = 0;
            }

        }



    }

    /// <summary>
    /// retorna la cantidad de experiencia que gana el jugador dependiendo del nivel de la criatura 
    /// </summary>
    int ExperienciaADar()
    {
        int expAuxNum = 0; 
        for (int i = 0;i < EnemigosBatalla[ActualSeNumEnemy].Nivel;i++)
        {
            expAuxNum = (50 * (i + 1))/4;
        }
        return expAuxNum;
    }

    /// <summary>
    /// metodo utilizado para saber cuanto exactamente gana el jugador por cada lucha
    /// </summary>
    /// <returns></returns>
    int RetornaDinero()
    {
        int CantidadAretornar = 0, sumatoria = 0;

        //sumatoria de enemigos
        for (int  i = 0 ;i < EnemigosBatalla.Length; i++ )
        {
            sumatoria += (int)EnemigosBatalla[i].Nivel;
        }


            CantidadAretornar = (sumatoria * 100) / 2;

        return CantidadAretornar;
    }

    /// <summary>
    ///  incrementa la experiencia acumulado por cada enemigo que llega KO por el ataque del jugador
    /// </summary>
    /// <param name="GanadorDeExp">define quien es el que ovtiene la exp </param>
    void UpdateExp(DefiniteObject GanadorDeExp)
    {
        //player 1
        if (GanadorDeExp == DefiniteObject.Player)
        {
          
            Exp[(int)GanadorDeExp].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia /
                                          CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experienciaTotal;

            if (CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia >= CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experienciaTotal)
            {

                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia = 0f;
                print("Marca este texto");

                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].IncrementodeNivel();

              //  CrewildInfoPlayer.CrewillInstancia[ActualCrewildSelectNum].experienciaTotal += CrewildInfoPlayer.CrewillInstancia[ActualCrewildSelectNum].experienciaTotal * (1 / 2);
                AuxSubeNiveles = true;


            }

             else if (AuxDanTest < NumExpIncrementar)
            {
                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia += ValorHpNom(ObjAttack).ACtualNvl + Time.deltaTime;
                
                AuxDanTest += ValorHpNom(ObjAttack).ACtualNvl + Time.deltaTime;
            }
            //regulador
            else if (AuxDanTest >= NumExpIncrementar)
            {
               
                CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia = (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].experiencia;
                AuxDanTest = 0;
                NumExpIncrementar = 0;
                



            }

        


    }


        //enemy
        else if (GanadorDeExp == DefiniteObject.Enemy)
        {
            HpScrollbar[(int)GanadorDeExp].size = EnemigosBatalla[ActualSeNumEnemy].Hp / EnemigosBatalla[ActualSeNumEnemy].hpTotal;

            /*    if (AuxDanTest == numDanger)
                {

                }

                else */
            if (AuxDanTest < numDanger)
            {
                EnemigosBatalla[ActualSeNumEnemy].Hp -= 0.2f + Time.deltaTime;
                AuxDanTest += 0.2f + Time.deltaTime;
                ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[1];
            }

            else if (EnemigosBatalla[ActualSeNumEnemy].Hp < 0)
            {
                print("que paso llegue 0");
                EnemigosBatalla[ActualSeNumEnemy].Hp = 0;
                AuxDanTest = 0;
                numDanger = 0;


                ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[1];
            }
            //regulador hp.
            else if (AuxDanTest > numDanger)
            {
                EnemigosBatalla[ActualSeNumEnemy].Hp = EnemigosBatalla[ActualSeNumEnemy].Hp + 1;
                EnemigosBatalla[ActualSeNumEnemy].Hp = (int)EnemigosBatalla[ActualSeNumEnemy].Hp;
                ImagenEnemigo.sprite = EnemigosBatalla[ActualSeNumEnemy].animaCrewildFrentre[0];

                AuxDanTest = 0;
                numDanger = 0;
            }

        }
    }

    /// <summary>
    /// pausa la animacion y pide un nombre para el trigger cinematica
    /// </summary>
    /// <param name="dialogueTrigger"></param>
    public void pausa(enumActionsExecute accionExecute)
    {

        //GameObject.Find(dialogueTrigger).GetComponent<Trigger_Cinematic>().dialogueCinema();
        batleManager(accionExecute);

        animationCinema["entrace"].speed = 0;

    }


    /// <summary>
    /// metodo usualmente usado para detener una secuencia en animacion.
    /// </summary>
    /// <param name="accionExecute"></param>
  public  void stop(enumActionsExecute accionExecute)
    {
        // accion = true;
        //  GameObject.Find(dialogueTrigger).GetComponent<Trigger_Cinematic>().dialogueCinema();
        batleManager(accionExecute);
        animationCinema.Stop();
    }


    public void play()
    {
        if (NameActualAnimationPlaying(animationCinema) != "")
        {
            //  GameObject.Find(dialogueTrigger).GetComponent<Trigger_Cinematic>().dialogueCinema();
            animationCinema[NameActualAnimationPlaying(animationCinema)].speed = 1;
        }
    }


    /// <summary>
    /// returna el nombre de la animacion que actualemente esta en ejecucion
    /// </summary>
    /// <param name="Animation"></param>
    /// <returns></returns>
    public string NameActualAnimationPlaying(Animation Animation)
    {



        foreach (AnimationState anim in Animation)
        {
            if (animationCinema.IsPlaying(anim.name))
            {
                return anim.name;

            }
            {

                return string.Empty;
            }
        }

        return string.Empty;
    }

    CrewildBase devuelveCrewildBase(string Atacante)
    {
        CrewildBase devuelveValor = null;

        if (Atacante == "Player")
        {

            devuelveValor = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer];
        }
        //ataca de frente
        else if (Atacante == "Enemy")
        {
            devuelveValor = EnemigosBatalla[ActualSeNumEnemy];
        }

        return devuelveValor;
    }

    bool probabilidadAciertoEstadoAlterado()
    {
        bool ValorAdevolver = false;
        float probabilidad = Random.Range(0f, 1f);

        if (probabilidad > 0.4f)
        {
            ValorAdevolver = true;
        }

        return ValorAdevolver;
    }


    /// <summary>
    /// back to panel choise panel initial brawler from Crewild Choise
    /// </summary>
    public void backChoisePanel()
    {
       
        criaturasCanvas.enabled = false;
        accionLevesint = 0;
        posSelect = 3;
        ChoiseCrewilfScrtip.ActivateCrewildChoise = false;
        ChoiseCrewilfScrtip.TriggerChoiseBrawler = false;

    }

    /// <summary>
    /// retorna el valor el ataque de el Crewild esta usando
    /// </summary>
    /// <param name="QuienAtaca"> crewild atcante en modo brawler</param>
    /// <param name="nombreDelataque">nombre del ataque que se esta usando</param>
    /// <returns></returns>
    public int ValordelAtaque(string QuienAtaca, string nombreDelataque)
    {
        int AuxRetorno = 0;
            
        switch (QuienAtaca)
        {
            case "Player":
                //define cual es elataque
                for (int i = 0; i < 4; i++)
                {
                    if (CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i] !=  null)
                    {
                        if (nombreDelataque == CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].nombreAtaque)
                        {
                            AuxRetorno = (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].PoderAtaque;
                            break;
                        }
                    }
                   
                }


                break;
            case "Enemy":
                for (int i = 0; i < 4; i++)
                {

                    if (EnemigosBatalla[ActualSeNumEnemy].ataques[i] != null)
                    {
                        if (nombreDelataque == EnemigosBatalla[ActualSeNumEnemy].ataques[i].nombreAtaque)
                        {

                            AuxRetorno = (int)EnemigosBatalla[ActualSeNumEnemy].ataques[i].PoderAtaque;
                            break;
                        }
                    }
                   
                }

                break;
            case "Player2":


                break;
            case "Enemy2":


                break;
        }
     


        return AuxRetorno;
    }




    /// <summary>
    /// reduce el valor del cansancion de las criatura
    /// </summary>
    /// <param name="QuienAtaca"> crewild atcante en modo brawler</param>
    /// <param name="nombreDelataque">nombre del ataque que se esta usando</param>
    /// <returns></returns>
    public void ReduceEstamina(string QuienAtaca , string nombreDelataque)
    {
        

        switch (QuienAtaca)
        {
            case "Player":
                //define cual es elataque
                for (int i = 0; i < 4; i++)
                {
                    if (nombreDelataque == CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].nombreAtaque)
                    {
                        CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansancio -=  CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].ValorCansancio;

                        if (CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansancio <= 0)
                        {

                            CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansancio = 0;
                        }

                        FatigaBarra[1].size = CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansancio / CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansanciototal;
                        break;
                    }
                }


                break;
            case "Enemy":
                for (int i = 0; i < 4; i++)
                {
                    if (nombreDelataque == CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].nombreAtaque)
                    {

                   //     (int)EnemigosDePrueba[ActualCrewildSelectNumEnemy].ataques[i].Valordaño;
                        break;
                    }
                }

                break;
            case "Player2":


                break;
            case "Enemy2":


                break;
        }      
        

    }


    /// <summary>
    /// retorna el valor de nivel de cansancio del jugador 
    /// </summary>
    /// <param name="QuienAtaca"> quien  es el que ataca </param>
    /// <param name="nombreDelataque"></param>
    /// <returns></returns>
    public int RetornaCansancio(string QuienAtaca)
    {
        int AuxRetorno = 0;

        switch (QuienAtaca)
        {
            case "Player":
                //define cual es elataque
               
                        AuxRetorno = (int)CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].Cansancio;

                break;    


               
            case "Enemy":
                                //    AuxRetorno = (int)EnemigosDePrueba[ActualCrewildSelectNumEnemy].ataques[i].Valordaño;
                 

                break;
            case "Player2":


                break;
            case "Enemy2":


                break;
        }



        return AuxRetorno;
    }
    /// <summary>
    /// definde ejecuta la ccion ofensiva que player mando a ejecutar
    /// </summary>
    void secuenciaEnbatalla(string QuienAtaca, string nombreDelataque)
    {

        if (QuienAtaca == "Player")
        {

            for (int i = 0; i < 4; i++)
            {
                if (nombreDelataque == CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].nombreAtaque)
                {

                    //  animationCinema.AddClip( CrewildInfoPlayer.CrewillInstancia[ActualCrewildSelectNum].ataques[i].AnimationBrawler(twovstwo, QuienAtaca, animationCinema), "Attack");
                    StartCoroutine(CrewildInfoPlayer.CrewillInstancia[ActualSelNumPlayer].ataques[i].AnimationBrawler(twovstwo, QuienAtaca, animationCinema, nombreDelataque ,this));
                    break;
                }
            }

        }

        else if (QuienAtaca == "Enemy")
        {            
                for (int i = 0; i < 4; i++)
                {
                    if (nombreDelataque == EnemigosBatalla[ActualSeNumEnemy].ataques[i].nombreAtaque)
                    {

                        StartCoroutine(EnemigosBatalla[ActualSeNumEnemy].ataques[i].AnimationBrawler(twovstwo, QuienAtaca, animationCinema, nombreDelataque,this));
                        break;
                    }
                }          

        }

    }
    /// <summary>
    /// ejecuta las animaciones en modo de batalla por defecto o basicas
    /// </summary>
    /// <param name="QuienAtaca">si es el Jugador o enemigo</param>
    /// <param name="nombreDelataque">nombre del ataque se va hacer</param>
    public void Animationbrawler(string QuienAtaca, string nombreDelataque)
    {

        switch (QuienAtaca)
        {
            case "Player":
                // CrewildInfoPlayer.CrewillInstancia[ActualCrewildSelectNum].ataques[]
                //define cual es elataque

                if (nombreDelataque == "KO")
                {
                 
                  animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/DeadAndCrewild/Player/Attack"), "Attack");
                    break;
                }

                else if (nombreDelataque == "Change")
                {                 
                    animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/changecrwild/Player/Attack"), "Attack");
                }

                else if (nombreDelataque == "Guardar")
                {
                    animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/GuardaCrewild/Player/Attack"), "Attack");

                }
                //selector de ataques
                

                break;
            case "Enemy":

                if (nombreDelataque == "KO")
                {

                    animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/DeadAndCrewild/Enemy/Attack"), "Attack");
                    break;
                }

                else if (nombreDelataque == "Change")
                {
                    animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/changecrwild/Enemy/Attack"), "Attack");
                }

                else if (nombreDelataque == "Guardar")
                {
                    animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/GuardaCrewild/Enemy/Attack"), "Attack");

                }

              
              

                break;
            case "Player2":


                break;
            case "Enemy2":


                break;
        }
        animationCinema.Play("Attack");
    }


    /// <summary>
    /// secuencia que activa el menu de eleccion de criatura
    /// </summary>
    void ActivadorChoiseCrewild()
    {
        criaturasCanvas.enabled = true;
        ChoiseCrewilfScrtip.ActivateCrewildChoise = true;
        ChoiseCrewilfScrtip.TriggerChoiseBrawler = true;

    }



 

    /// <summary>
    /// hace una eleccion aleatoria de la accion del enemigo
    /// </summary>
    public void IaEnemy()
    {


        ListaAcciones[0] = selectactionIA();

        listIntChoiseAttack[0] = electionAttack(listIntChoiseAttack[0]);

        if (twovstwo == true)
        {
           // IAElection2 = selectactionIA(IAElection2, 2, 1);
            listIntChoiseAttack[2] = electionAttack(listIntChoiseAttack[2]);
        }

    }



    /// <summary>
    /// selecciona la accion de ataque de la Ia.
    /// </summary>
    string selectactionIA()
    {
        string NombreAtaque = "";
        int NunAccion = 0;
        bool salirCiclo = false;

        while (salirCiclo == false)
        {
            NunAccion = Random.Range(0, 4);
                    
            if (EnemigosBatalla[ActualSeNumEnemy].ataques[NunAccion] != null)
            {
                NombreAtaque = EnemigosBatalla[ActualSeNumEnemy].ataques[NunAccion].nombreAtaque;
                salirCiclo = true;
                break;
            }
        }

        return NombreAtaque;


    }

    /// <summary>
    /// devuelve el alcance de  ataques permitidos del enemigo.
    /// </summary>
    /// <param name="numCrewildEnemiy"></param>
    /// <returns></returns>
    int AtaquesCantidad(int NumCrewildEnemy)
    {
        int Numreturn = 0;

        foreach(AttacksBase A in EnemigosBatalla[NumCrewildEnemy].ataques)
        {
            if (A != null)
            {
                Numreturn++;
            }
        }

        return Numreturn ;
    }

    int electionAttack(int numListTypeCrewildAttack)
    {
          if (numListTypeCrewildAttack != 0)
          {
              return numListTypeCrewildAttack;
          }

          else  if (numListTypeCrewildAttack == 0)
          {
              numListTypeCrewildAttack = Random.Range(1, 5);

          }



           if (numListTypeCrewildAttack == 1 || numListTypeCrewildAttack == 3)
          {
              numListTypeCrewildAttack = 0;
          }

          if (numListTypeCrewildAttack != 0)
          {
              numListTypeCrewildAttack = numListTypeCrewildAttack - 1;
          }
          checked
          {
              return numListTypeCrewildAttack;
          }

      
    
    }


    /// <summary>
    /// sale del panel de inventario
    /// </summary>
    public void exitInventaryPanel()
    {

        objetosCanvas.enabled = false;
        accionLevesint = 0;
        //inventario.exitpaneles();
        inventario.TriggerInventary = false;
        inventario.brawlerMode = false;
      
    }

    /// <summary>
    /// Metodo para eleccion 50/50 pero no se en que se esta usando
    /// </summary>
    /// <returns></returns>
    bool boolElection()
    {
        float numValue = Random.value;
        bool boolreturn = false;
        if (numValue <= 0.5f)
        {
            boolreturn = false;
        }
        else if (numValue > 0.5f)
        {
            boolreturn = true;

        }
        checked
        {
            return boolreturn;
        }
    }

    /// <summary>
    /// comienza la cinematica del modo  batalla
    /// </summary>
    public void empiezaBatalla()
    {
        TriggerEntraceCinema = true;
        batleManager(enumActionsExecute.begin);
    }
}




/// <summary>
/// metodo usado para los textos de juego
/// </summary>
public class TypeTextClass : MonoBehaviour
{



    public   bool EstoyTypeText, saltotexto, triggercompleteTExt , EndReadText;

    //initalposition is optional var is only one referencia for metode MoveTowards
    public   Vector3 postext, initalpostion;

    public   RectTransform ActualPosText;

    public int rangertext ;

    public void initialTexTVar( RectTransform actualposition)
    {
        ActualPosText = actualposition; 
        postext = ActualPosText.localPosition;
        initalpostion = ActualPosText.localPosition;
    }


    public void initialposText ()
        {
                     postext = initalpostion;
 ActualPosText.localPosition = initalpostion;

        }



    public void MoveTowardsText(float speedMoveText )
    {
        ActualPosText.localPosition = Vector3.MoveTowards(ActualPosText.localPosition, postext, speedMoveText * Time.deltaTime);
    }


    public IEnumerator textMetode( string message , Text text,float letterPause , float distance)
    {
        //Debug.Log("Accion hablar class.");

        EstoyTypeText = true;

        saltotexto = true;

        rangertext = 86;

        //metodo para la lectura del texto.
        foreach (char letter in message.ToCharArray())
        {


            text.text += letter;

            if (triggercompleteTExt == true && text.text.Length == rangertext- 1)
            {
                triggercompleteTExt = false;
                saltotexto = true;
                yield return new WaitForSeconds(letterPause);
            }


            else if (triggercompleteTExt == false)
            {
                float auxTIme;
                auxTIme = Time.deltaTime + 5f;

                yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Space) == false && text.text.Length == rangertext );

                //de alguna extraña razon  que desconosco este pedazo funciona.

                if (text.text.Length == rangertext && saltotexto == true)
                {

                    //activa el moviemiento 
                    postext = ActualPosText.localPosition;

                    postext += Vector3.up * distance;// Add -1 to pos.x

                    rangertext += rangertext;
                    saltotexto = false;

                    Debug.Log("tamaño del texto =" + text.text.Length);

                }


                yield return 0;
                yield return new WaitForSeconds(letterPause);
            }

        }

        EndReadText = true;
        triggercompleteTExt = false;
        EstoyTypeText = false;
    }
    
}

public abstract class Persona {
    public int Identificacion { set; get; }
    public string NombreCompleto { get; set; }
}
/// <summary>
/// ejemplo del uso de  herencia
/// </summary>
public class Nestor : Persona
{
    public Nestor(int identificacion, string nombreCompleto) {
          Identificacion = identificacion;
          NombreCompleto = nombreCompleto; 
    }

}

interface IBuscar {
    void BuscarPdf();
}

public class Prueba
{
    Prueba()
    {
        var hermano = new Nestor(1067892548,"Nestor Javier Solera Ayala");


    }

}


[System.Serializable]
public class StaffStatisticCrewild
{



    public string Name;
    [HideInInspector]
    public bool bUsed;

    public string NameTaxonomico;

    public Image imagenCreatura , imagencriaturaMenu;

    public Animation AnimationCrituraMenuDeseleccion;

    public Animation[] AnimatiBattleEspalda;

    public Animation[] AnimatiBattleFrente;

    public float Hp, MaxlHp;


    public  int attack, defence, speed, precision, evation, EspecialAttack, DefenceSpecial, Resistence , Lvl  ;



    public enum AttackType
    {
        None,
        barrida,
        adrenalina,
        recuperacion,
        Bone,
        Defence,
        ChangeCrewild, // no is attack var, use for define changeCrewild methode un inaccessible
        DeadAndChange,
        UseObject
    }


    public Attack[] attackType = new Attack[4];
    // Inside your class
    //  public AttackType[] attackType  = new AttackType[4];

    public StandBrawler[] Stands = new StandBrawler[4];

    public enum StatesBrawler
    {
        None,
        poisoning,
        Paralize,
        Fire,
        Ice, 
        Bleeding,
        Confuse,
        Ko,
        Dead

    }

    // Inside your class
    public StatesBrawler StatesCreiwild = new StatesBrawler();

    public enum CrywildType
    {
        None,
        Fire,
        Wather,
        Plant,
        Beast,
        fighting,
        Magme,
        Durece,
        Cristal,
        Insect,
        Mental,
        Aquatic,
        Savage,
        flying,
        Energi,
        Inorganic,
        Toxic,
        Fear,
        Shadow,
        Tree,
        Earth,
        Gas,
        Undefinided,
        Fantasy

    }

    /// <summary>
    /// ever crewwill your max type class is max, example: beast, rock, fly es you max for this creawild.
    /// </summary>
    public CrywildType[] crywildType = new CrywildType[3];


    public enum Style
    {
        Defensivo,
        ofensivo,
        intermedio,
        Evacion,
        mental,
        desgaste,
        Recuperacion,
        Distancia,
        ataquesRapidos,
        onfundir,
        Berserker      
    }

    public Style style = new Style(); 

   

   

   public void Start()
    {
      
            for(int i = 0; i < 4 ; i++)
        {
            attackType[i].Name = attackType[i].attack.ToString() ;
        }
    }

    public virtual void Datos(
             string Name, string nameTaxonomico, Image criatura ,Image hp, CrywildType tipe1 , CrywildType tipe2
        ,Animation[] AnimatiBattleEspalda, Animation[] AnimatiBattleFrente, Animation AnimationCrituraMenuDeseleccion)
    {
      this.Name = Name;
      this.NameTaxonomico = nameTaxonomico;
      this.crywildType[0] = tipe1;
      this.crywildType[1] = tipe2;
      this.AnimatiBattleEspalda = AnimatiBattleEspalda;
      this.AnimatiBattleFrente =  AnimatiBattleFrente;
      this.AnimationCrituraMenuDeseleccion = AnimationCrituraMenuDeseleccion;
      
    }

    public virtual void Estadisticas(int attack, int defence, int speed, int precision, int evation, int EspecialAttack, int DefenceSpecial, 
                                     int Resistence , int Lvl)
    {
        this.attack = attack;
        this.defence = defence;
        this.speed = speed;
        this.precision = precision;
        this.evation = evation;
        this.EspecialAttack = EspecialAttack;
        this.DefenceSpecial = DefenceSpecial;
    }

    public virtual void Acciones()
    {


    }

}

[System.Serializable]
public class Attack
{
    public string Name ="Name Attack";
    [HideInInspector]
    public bool bUsed;
    public StaffStatisticCrewild.AttackType attack;
    public int Cantidad;
    public int Uso;
    public int PointAttack;


  
}


[System.Serializable]
public class StandBrawler
{
    public string Name = "Name Stand";
    [HideInInspector]
    public bool bUsed;

    public enum standEnum
    {
        postura1,
        postura2
      
    }

    public standEnum StandEnum;
    public int consumoMana;
    public int PointEstamine;

}






/// <summary>
/// define cuales el objeto en cuestion
/// 0 = Ene , 1 = player, 2 = ene2, 3 = player2
/// </summary>
public enum DefiniteObject
{
    Enemy,
    Player,
    Enemy2,
    Player2,
    none
}