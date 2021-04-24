using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class CrewildChoiseSelect : MonoBehaviour {

    public static CrewildChoiseSelect instanShare;

    private RectTransform selector, selectorPanel, selectorPanelBrawler;

  [HideInInspector] public RectTransform panelsAccion  ;


    /// <summary>
    /// metodo que creado con obj imagnes para el menu de criaturas
    /// </summary>
    public class ImagenesMenu
    {
        public Sprite[] Secuecuencia = new Sprite[2];
    }

    private bool AuxAnimation;

    private Image[] SpriteRendersMenu = new Image[6];

    private Image[] ImageEstados = new Image[6];
    private Text[] TextEstados = new Text[6];


    public MetodosParaAtaque MetodoAuxAtaque = new MetodosParaAtaque();




    private int posSelectLv2, levels = 1, AuxArrayIntCrewil1, AuxArrayIntCrewil2, AuxLastPosSelect;

   [HideInInspector] public int posSelect = 1;

    private float timePauseStart;

    public GameObject subPanelObj, subPanelObjBrawler;

    private bool[] movebool = new bool[6];

    /// <summary>
    /// ActivateCrewildChoise =  activa since menu part ,  TriggerChoiseBrawler = activate since brawler menu, seleccionrapida = modo batalla permite el cambio rapidamente
    /// </summary>
    public bool ActivateCrewildChoise, TriggerChoiseBrawler, SeleccionRapida;


    /// <summary>
    /// se actica para aplicar un Iten sobre alguna criatura
    /// </summary>
    public bool AplicarIten;

    /// <summary>
    /// activa el movimiento en el segundo panel de seleccion 
    /// </summary>
    private bool MoveLevel2 = true;


    private informacionCrewild infoCrewild;

    private StaffStatisticCrewild auxInfCrewild1, AuxInfCrewild2;


    /// <summary>
    ///variable auxiliar para cambiar de posicion un criatura en el menu de creacion
    /// , rango max 2
    /// </summary>
    public CrewildBase[] AuxCrewill = new CrewildBase[2];
    private bool[] SeleccionCambio = new bool[2];

    private Coroutine UltimaCortina;

    private Canvas infoCanvas, objCanvas;

    private animationScritpBatle scritpAniBattle;

    /// <summary>
    /// acceso rapido a todos los scrips
    /// </summary>
    public libreriaDeScrips LibreriaS;


    /// <summary>
    /// llama   a las barras de salud , cansancio y expreiencia 
    /// </summary>
    public Image[] barrasDeSalud = new Image[6], BarraDeCansancion = new Image[6], BarraExp = new Image[6];


    public Text[] TextoHp = new Text[6], TextoEst = new Text[6], TextoExp = new Text[6];
    //   public Scrollbar[] barrasDeSalud = new Scrollbar[6], BarraDeCansancion = new Scrollbar[6], BarraExp  = new Scrollbar[6];

    public Text[] nombreCrewild = new Text[6];

    public Text TextPanelBox;

    public RectTransform[] BulletTransf = new RectTransform[6];


    /// efectos fuera de batalla
    EfectoFueraDeBatalla efectosfueradebatlla = new EfectoFueraDeBatalla();

    [Header("accion crewild")]
    #region  #accion crewild
    //----  accion crewild
    private bool AccionSecundariaCW;
    private int AuxPosTexto, AuxPosCwAccionSecundaria;
    private bool EjecutaAccionSecundaria;
    [HideInInspector] public DataAccionSecundaria dataAccionSecundaria;
    #endregion

    [Header("Cuandro Texto")]
    #region Seleccion Mo y CuandroTexto
    public GameObject Cuadrotexto;
    public GameObject CajaSelectorObj;
    public Text TexteMensaje;
    public Text TextYesOrNot;

    public RectTransform SelectorYesOrNo;
    private int NumSelectorYesOrNo;
    [HideInInspector] public bool BoolSelector;
    [HideInInspector] public bool ejecutaAccion;

    [HideInInspector] public int NumSelectorMo;

    public enum EstadoAccionEjecutar
    {
        nulo,
        Eleccion,
        SeleccionanMO,
        cambiarMo
    }

    [HideInInspector]
     public  EstadoAccionEjecutar estadoAccionEjecutar;
    private Coroutine cortineItenEfect;


    #endregion

    // Use this for initialization
    public void Start() {

        instanShare = this;

        dataAccionSecundaria = new DataAccionSecundaria();

        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

        selector = GameObject.Find("Crewild/Crewild/under panel/Selector").GetComponent<RectTransform>();

        panelsAccion = GameObject.Find("Crewild/Crewild/AccionSelect").GetComponent<RectTransform>();

        //  panelsAccionBrawler = GameObject.Find("Crewild/Crewild/AccionSelectBrawler").GetComponent<RectTransform>();

        selectorPanel = GameObject.Find("Crewild/Crewild/AccionSelect/Selector").GetComponent<RectTransform>();

        selectorPanelBrawler = GameObject.Find("Crewild/Crewild/AccionSelectBrawler/Selector").GetComponent<RectTransform>();

        subPanelObj = GameObject.Find("Crewild/Crewild/AccionSelect");

        subPanelObjBrawler = GameObject.Find("Crewild/Crewild/AccionSelectBrawler");

        infoCrewild = GameObject.Find("informacion/estadisticas").GetComponent<informacionCrewild>();

        infoCanvas = GameObject.Find("informacion").GetComponent<Canvas>();
        if (TextPanelBox == null)
        {
            TextPanelBox = GameObject.Find("Crewild/AccionSelect/text").GetComponent<Text>();
        }


        //sin utilidad 
        objCanvas = GameObject.Find("informacion").GetComponent<Canvas>();



        scritpAniBattle = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>();


        for (int i = 0; i < LibreriaS.informacionCrewild.CrewillInstancia.Length; i++)
        {

            BulletTransf[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1)).GetComponent<RectTransform>();
            if (LibreriaS.informacionCrewild.CrewillInstancia[i] != null)
            {
                //añadir nombres, nivel Datos numericos de Barras
                barrasDeSalud[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/HP, Estamine fatigue_ player/hp").GetComponent<Image>();
                BarraDeCansancion[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/HP, Estamine fatigue_ player/Fatige").GetComponent<Image>();
                BarraExp[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/HP, Estamine fatigue_ player/Exp").GetComponent<Image>();

                TextoHp[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/HP, Estamine fatigue_ player/hp text").GetComponent<Text>();
                TextoEst[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/HP, Estamine fatigue_ player/Est text").GetComponent<Text>();
                TextoExp[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/HP, Estamine fatigue_ player/Exp text").GetComponent<Text>();

                nombreCrewild[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/panel nombre/text").GetComponent<Text>();


            }
            ImageEstados[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/Estado").GetComponent<Image>();
            TextEstados[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/Estado/Text").GetComponent<Text>();
            /*  if (LibreriaS.informacionCrewild.CrewillInstancia[i] == null)
              {
                  BulletTransf[i].gameObject.SetActive(false);
              }*/

        }



        //captura de imagenes

        int contador = 0;
        foreach (CrewildBase Cb in LibreriaS.informacionCrewild.CrewillInstancia)
        {
            if (Cb != null)
            {
                contador++;
            }
        }
        for (int i = 0; i < contador; i++)
        {
            if (infoCrewild.CrewillInstancia[i] != null)
            {
                SpriteRendersMenu[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/criatura").GetComponent<Image>();
                SpriteRendersMenu[i].sprite = infoCrewild.CrewillInstancia[i].SpriteCrewildmenuSelec[0];

            }


        }

        actualizaDatos();

        subPanelObj.SetActive(false);
        subPanelObjBrawler.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {



        if (ActivateCrewildChoise == true && timePauseStart < Time.time)
        {

            accion();
        }

        ///define si se ejuta Una Accion para MO
        if (estadoAccionEjecutar != EstadoAccionEjecutar.nulo)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ExitMo();
                StopCoroutine(cortineItenEfect);
            }
           
           if (estadoAccionEjecutar == EstadoAccionEjecutar.Eleccion)
            {
              
                SeleecionAccion();
                // define si o no
                Selector(SelectorYesOrNo);
               
            }

            else if (estadoAccionEjecutar == EstadoAccionEjecutar.cambiarMo)
            {
                subPanelObj.SetActive(true);
                // Elige Mo a cambiar
                SeleecionAccion();
                NumSelectorMo = SelectorpanelCentral(NumSelectorMo, selectorPanel);
                print("BUscnado problema selector "+ NumSelectorMo);
            }

        }


    }

    ///define la posicion y movimientos de los paneles
    #region Selectores Posicion

    private void SeleecionAccion()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BoolSelector = false;
            if (NumSelectorYesOrNo == 0)
                ejecutaAccion = true;
            else
                ejecutaAccion = false;
        }

    }
    private void Selector(RectTransform rectTransform)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NumSelectorYesOrNo++;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            NumSelectorYesOrNo--;
        }

        if (NumSelectorYesOrNo > 1)
        {
            NumSelectorYesOrNo = 0;
        }
        else if (NumSelectorYesOrNo < 0)
        {
            NumSelectorYesOrNo = 1;
        }

        if (NumSelectorYesOrNo == 0)
        {
            rectTransform.localPosition = new Vector2(-0.2508545f, 78.09669f);
        }
        else if (NumSelectorYesOrNo == 1)
        {
            rectTransform.localPosition = new Vector2(-0.2508545f, -12f);
        }
    }

    public int SelectorpanelCentral(int NumPos, RectTransform rectTransform)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NumPos--;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            NumPos++;
        }

        if (NumPos > 3)
        {
            NumPos = 0;
        }
        else if (NumPos < 0)
        {
            NumPos = 3;
        }



        if (NumPos == 0)
        {
            rectTransform.localPosition = new Vector2(0, 72);
        }
        else if (NumPos == 1)
        {
            rectTransform.localPosition = new Vector2(0, 38);
        }
        else if (NumPos == 2)
        {
            rectTransform.localPosition = new Vector2(0, 8);
        }
        else if (NumPos == 3)
        {
            rectTransform.localPosition = new Vector2(0, -28);
        }

        return NumPos;
    }

    public void posicionDinamica(int posNum)
    {
        if (AccionSecundariaCW == true)
        {
            if (posNum == 1)
            {
                selectorPanel.localPosition = new Vector2(0, 75);
            }
            else if (posNum == 5)
            {
                selectorPanel.localPosition = new Vector2(0, -60);
            }

        }
        else if (AccionSecundariaCW == false)
        {
            if (posNum == 1)
            {
                selectorPanel.localPosition = new Vector2(0, 75);
            }
            else if (posNum == 4)
            {
                selectorPanel.localPosition = new Vector2(0, -31);
            }

        }

        if (AuxPosTexto > posNum)
        {
            selectorPanel.localPosition += new Vector3(0, 32);
        }

        else if (AuxPosTexto < posNum)
        {
            selectorPanel.localPosition -= new Vector3(0, 32);
        }

        AuxPosTexto = posNum;
    }


    /// <summary>
    /// first level panel select choise crewild
    /// </summary>
    /// <returns></returns>
    public int SelectPos()
    {
        //movimiento por suma
        if (Input.GetKeyDown(KeyCode.D))
        {
            posSelect += 1;
            LibreriaS.audioMenus.Audio.Play();
            salidReproductorSprite();
            rieniciaSprite();


        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            posSelect -= 1;
            LibreriaS.audioMenus.Audio.Play();
            salidReproductorSprite();
            rieniciaSprite();
        }

        //ajuste
        if (posSelect <= 0)
        {
            posSelect = LibreriaS.informacionCrewild.CrewillInstancia.Length;

        }
        else if (posSelect >= LibreriaS.informacionCrewild.CrewillInstancia.Length + 1)
        {

            posSelect = 1;
        }

        ///ajuste de para que la cantidad de criatura no sobrepase el limite
        if (posSelect <= LibreriaS.informacionCrewild.CrewillInstancia.Length)
        {


            if (posSelect == 1)
            {

                selector.anchoredPosition3D = new Vector2(64, 201);

            }

            else if (posSelect == 2)
            {
                selector.anchoredPosition3D = new Vector2(284, 57);

            }



            else if (posSelect == 3)
            {
                selector.anchoredPosition3D = new Vector2(295, -115);

            }

            else if (posSelect == 4)
            {
                selector.anchoredPosition3D = new Vector2(71, -232);

            }

            else if (posSelect == 5)
            {
                selector.anchoredPosition3D = new Vector2(-147, -120);

            }
            else if (posSelect == 6)
            {
                selector.anchoredPosition3D = new Vector2(-147, 47);

            }



        }








        return posSelect;

    }

    /// <summary>
    /// subpanelselection
    /// </summary>
    /// <returns></returns>
    public int SelectPosLv2()
    {
        if (AccionSecundariaCW == false)
        {
            if (posSelectLv2 <= 0)
            {
                posSelectLv2 = 4;

            }
            else if (posSelectLv2 >= 5)
            {

                posSelectLv2 = 1;
            }
        }

        else
        {
            if (posSelectLv2 <= 0)
            {
                posSelectLv2 = 5;

            }

            else if (posSelectLv2 >= 6)
            {

                posSelectLv2 = 1;
            }
        }




        if (Input.GetKeyDown(KeyCode.S))
        {
            posSelectLv2 += 1;
            LibreriaS.audioMenus.Audio.Play();
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            posSelectLv2 -= 1;
            LibreriaS.audioMenus.Audio.Play();
        }

        return posSelect;

    }


    /// <summary>
    /// subpanelselection in brawiler 
    /// </summary>
    /// <returns></returns>
    public int SelectPosLv2Brawler()
    {



        if (posSelectLv2 <= 0)
        {
            posSelectLv2 = 3;


        }
        else if (posSelectLv2 >= 4)
        {

            posSelectLv2 = 1;
        }





        else if (Input.GetKeyDown(KeyCode.S))
        {
            posSelectLv2 += 1;
            LibreriaS.audioMenus.Audio.Play();
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            posSelectLv2 -= 1;
            LibreriaS.audioMenus.Audio.Play();
        }

        return posSelect;

    }

    void accion()
    {



        if (levels == 1)
        {
            SelectPos();


            /// salida basica
            if (TriggerChoiseBrawler == true && Input.GetKeyDown(KeyCode.C) && SeleccionRapida == false && AplicarIten == false)
            {
                LibreriaS.audioMenus.Audio.Play();
                scritpAniBattle.backChoisePanel();

            }
            else if (Input.GetKeyDown(KeyCode.C) && AplicarIten == true)
            {
                LibreriaS.audioMenus.Audio.Play();
                LibreriaS.inventario.SaleDelpanelDeseleecion();
            }
            if (infoCrewild.CrewillInstancia[posSelect - 1] != null)
            {


                if (Input.GetKeyDown(KeyCode.Space))
                {
                    enableSubPanel();

                    posicionUniversalPaneles();

                }
                DefinirSprite();
            }


        }

        else if (levels == 2)
        {


            if (TriggerChoiseBrawler == false)
            {
                AccionLV2();

            }
            else if (TriggerChoiseBrawler == true && SeleccionRapida == true)
            {
                SelectorKOBrawler();

            }
            else if (TriggerChoiseBrawler == true)
            {
                AccionLV2Brawler();
            }
        }
    }
    /// <summary>
    /// condiciones para salir del modo de eleccion despues de un estado KO
    /// </summary>
    void SelectorKOBrawler()
    {
        if (
             infoCrewild.CrewillInstancia[posSelect - 1].EstadosCrewild == EstadosEnum.Ko
            )
        {

            levels = 1;
        }
        else if (
            infoCrewild.CrewillInstancia[posSelect - 1].EstadosCrewild != EstadosEnum.Ko
            )
        {
            scritpAniBattle.ActualSelNumPlayer = posSelect - 1;
            scritpAniBattle.criaturasCanvas.enabled = false;
            scritpAniBattle.PlayerPausaKOSelector = true;

            ActivateCrewildChoise = false;
            TriggerChoiseBrawler = false;
            SeleccionRapida = false;

            print("este es el valor --->" + (posSelect - 1));

            levels = 1;

        }

    }


    void posicionUniversalPaneles()
    {

        panelsAccion.anchoredPosition3D = new Vector2(18, 2.2f);



    }


    #endregion

    #region Exits

    public void ExitMo()
    {
        instanShare.estadoAccionEjecutar = EstadoAccionEjecutar.nulo;
        LibreriaS.Batalla.pausaIenumerator = false;
        LibreriaS.inventario.ExitUsarIten();
        LibreriaS.Batalla.exitInventaryPanel();
        panelsAccion.gameObject.SetActive(false);
        Cuadrotexto.SetActive(false);
        CajaSelectorObj.SetActive(false);

    }

    #endregion

    void DefinirSprite()
    {

        if (infoCrewild.CrewillInstancia[posSelect - 1] != null && AuxAnimation == false)
        {
            UltimaCortina = StartCoroutine(ReproductorSprite(posSelect - 1));
        }
        else if (infoCrewild.CrewillInstancia[posSelect - 1] == null)
        {

        }
    }

    /// <summary>
    /// corutina que hace un ciclo para las miniaturas para las criaturas
    /// </summary>
    /// <param name="numCrawild"> numero en el menu de seleccion dentro de la info de la criatura</param>
    /// <returns></returns>
    IEnumerator ReproductorSprite(int numCrawild)
    {
        AuxAnimation = true;

        while (true)
        {
            SpriteRendersMenu[numCrawild].sprite = infoCrewild.CrewillInstancia[numCrawild].SpriteCrewildmenuSelec[1];
            if (ActivateCrewildChoise == false)
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
            SpriteRendersMenu[numCrawild].sprite = infoCrewild.CrewillInstancia[numCrawild].SpriteCrewildmenuSelec[0];
            if (ActivateCrewildChoise == false)
                break;
            yield return new WaitForSeconds(0.2f);
        }

    }
    void salidReproductorSprite()
    {
        AuxAnimation = false;
        if (UltimaCortina != null)
            StopCoroutine(UltimaCortina);
    }

    void rieniciaSprite()
    {
        for (int i = 0; i < 6; i++)
        {
            if (infoCrewild.CrewillInstancia[i] != null)
            {
                SpriteRendersMenu[i].sprite = infoCrewild.CrewillInstancia[i].SpriteCrewildmenuSelec[0];
            }

        }
    }
    /// <summary>
    /// ajusta la imagen para que cuando la miniatura en el menu cambia vuelva  a imagen original
    /// </summary>
    public void ajusteSprite(int numCrewildInf)
    {
        ///ajuste de para que la cantidad de criatura no sobrepase el limite

        movebool[numCrewildInf] = false;
        StopAllCoroutines();
        AuxAnimation = false;

        if (infoCrewild.CrewillInstancia[numCrewildInf] != null)
        {
            SpriteRendersMenu[numCrewildInf].sprite = infoCrewild.CrewillInstancia[numCrewildInf].SpriteCrewildmenuSelec[0];
        }



    }


    /// <summary>
    /// metodo que llama para reproducir las  animacion en modo seleccion de criatura
    /// </summary>
    /// <param name="NumSelectionCrewild"></param>
    void AnimacionMenus(int NumSelectionCrewild)
    {
        if (AuxAnimation == false && infoCrewild.CrewillInstancia[NumSelectionCrewild] != null)
        {
            //   print("AuxAnimation " + AuxAnimation);
            StartCoroutine(ReproductorSprite(NumSelectionCrewild));

        }
    }

    void AccionLV2()
    {
        if (EjecutaAccionSecundaria == false)
        {
            if (MoveLevel2 == true)
            {
                SelectPosLv2();
                posicionDinamica(posSelectLv2);

            }
            //retroceder
            if (Input.GetKeyDown(KeyCode.C) && MoveLevel2 == true)
            {
                Retroceder();
            }
        }


        //change
        if (posSelectLv2 == 1)
        {


            if (Input.GetKeyDown(KeyCode.Space) && MoveLevel2 == true)
            {
                MoveLevel2 = false;


                LibreriaS.audioMenus.Audio.Play();

                AuxCrewill[0] = infoCrewild.CrewillInstancia[posSelect - 1];

                SeleccionCambio[0] = true;
                AuxArrayIntCrewil1 = posSelect - 1;

                AuxLastPosSelect = posSelect;
            }

            else if (MoveLevel2 == false)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    MoveLevel2 = true;

                    AuxCrewill[0] = null;
                    AuxCrewill[1] = null;

                    posSelect = AuxLastPosSelect;
                    SelectPos();
                }

                SelectPos();
                changeCrewildPosition();

                DefinirSprite();
            }

        }
        // info crewild
        else if (posSelectLv2 == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) && MoveLevel2 == true)
            {
                LibreriaS.audioMenus.Audio.Play();
                EntraEnInforBulletWil();

            }

            else if (Input.GetKeyDown(KeyCode.C) && MoveLevel2 == false && LibreriaS.informacionCrewild.ActivaNavegacion == false)
            {
                MoveLevel2 = true;
                infoCanvas.enabled = false;
                infoCrewild.Active = false;

                infoCrewild.condicionesDeSalida();

                LibreriaS.audioMenus.Audio.Play();

                LibreriaS.Batalla.criaturasCanvas.enabled = true;
            }




        }
        if (AccionSecundariaCW == false)
        {
            // object 
            if (posSelectLv2 == 3)
            {



            }
            //Back
            else if (posSelectLv2 == 4)
            {


                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Retroceder();
                }


            }
        }
        else if (AccionSecundariaCW == true)
        {
            // object
            if (posSelectLv2 == 3)
            {



            }
            // accion secundaria 
            else if (posSelectLv2 == 4)
            {
                if (Input.GetKeyDown(KeyCode.Space) && EjecutaAccionSecundaria == false)
                {
                    LibreriaS.audioMenus.Audio.Play();
                    EjecutaAccionSecundaria = true;
                    AuxPosCwAccionSecundaria = posSelect - 1;
                    dataAccionSecundaria = ejecutaAccionSecundaria();

                }

                else if (EjecutaAccionSecundaria == true)
                {

                    dataAccionSecundaria.crewildBase.ataques[dataAccionSecundaria.numAtaque].MetodoEnBatalla(dataAccionSecundaria.crewildBase);
                }
            }
            //Back
            else if (posSelectLv2 == 5)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Retroceder();
                }
            }
        }

    }

    public DataAccionSecundaria ejecutaAccionSecundaria()
    {
        DataAccionSecundaria AccionReturn = new DataAccionSecundaria();
        for (int i = 0; i < 4; i++)
        {
            if (infoCrewild.CrewillInstancia[AuxPosCwAccionSecundaria].ataques[i] != null)
            {
                if (infoCrewild.CrewillInstancia[AuxPosCwAccionSecundaria].ataques[i].EfectoFueradeBatalla == true)
                {
                    if (infoCrewild.CrewillInstancia[AuxPosCwAccionSecundaria].ataques[i].cantidadDeusos > 0)
                    {
                        infoCrewild.CrewillInstancia[AuxPosCwAccionSecundaria].ataques[i].cantidadDeusos -= 1;
                      //  infoCrewild.CrewillInstancia[AuxPosCwAccionSecundaria].ataques[i].MetodoEnBatalla(infoCrewild.CrewillInstancia[posSelect - 1]);
                      //  dataAccionSecundaria.numAtaque = i;
                        AccionReturn.numAtaque  = i;
                        AccionReturn.crewildBase = infoCrewild.CrewillInstancia[AuxPosCwAccionSecundaria];

                        return AccionReturn;
                    }

                }
            }
        }

        return AccionReturn;
    }

   public class DataAccionSecundaria
    {
       public int numAtaque;
       public  CrewildBase crewildBase;

    }

  public  void Retroceder()
    {
        EjecutaAccionSecundaria = false;
        subPanelObj.SetActive(false);
        levels = 1;
        posSelectLv2 = 1;
        LibreriaS.audioMenus.Audio.Play();
    }

    public void salirDetodosLosMenus()
    {
        scritpAniBattle.backChoisePanel();
    }

    void EntraEnInforBulletWil()
    {
        MoveLevel2 = false;

        infoCrewild.Active = true;
        infoCrewild.LevelCrewild = posSelect - 1;



        LibreriaS.informacionCrewild.disapadorContadorCriaturas = false;

        Invoke("inockeAuxInfoEnable", 0.1f);

    }
    void inockeAuxInfoEnable()
    {
        LibreriaS.Batalla.criaturasCanvas.enabled = false;
        infoCanvas.enabled = true;

    }

    void AccionLV2Brawler()
    {
        if (MoveLevel2 == true)
        {
            SelectPosLv2Brawler();
        }

        if (Input.GetKeyDown(KeyCode.C) && MoveLevel2 == true)
        {
            subPanelObj.SetActive(false);
            levels = 1;
            posSelectLv2 = 1;
            LibreriaS.audioMenus.Audio.Play();
        }

        //Choise Crewild
        if (posSelectLv2 == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space) && MoveLevel2 == true && (posSelect - 1) != scritpAniBattle.AuxCrewildPosInt)
            {
                scritpAniBattle.ListaAcciones[1] = "Change";
                scritpAniBattle.AuxCrewildPosInt = (posSelect - 1);
                scritpAniBattle.backChoisePanel();
                subPanelObj.SetActive(false);
                subPanelObjBrawler.SetActive(false);
                levels = 1;
                posSelectLv2 = 1;
                LibreriaS.audioMenus.Audio.Play();
                Debug.Log("Esperando  cambio");
            }



            selectorPanel.localPosition = new Vector2(0, 75);

        }
        // info crewild
        else if (posSelectLv2 == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) && MoveLevel2 == true)
            {

                MoveLevel2 = false;
                Invoke("InvokeCanvasInfo", 0.1f);



                infoCrewild.Active = true;
                infoCrewild.LevelCrewild = posSelect - 1;
                LibreriaS.informacionCrewild.disapadorContadorCriaturas = false;

                LibreriaS.audioMenus.Audio.Play();
            }

            else if (Input.GetKeyDown(KeyCode.C) && MoveLevel2 == false)
            {
                MoveLevel2 = true;
                infoCanvas.enabled = false;
                infoCrewild.Active = false;
                LibreriaS.Batalla.criaturasCanvas.enabled = true;

                LibreriaS.audioMenus.Audio.Play();
            }


            selectorPanel.localPosition = new Vector2(0, 42);

        }
        //Back
        else if (posSelectLv2 == 3)
        {
            if (Input.GetKeyDown(KeyCode.Space) && MoveLevel2 == true)
            {
                subPanelObj.SetActive(false);
                levels = 1;
                posSelectLv2 = 1;
                LibreriaS.audioMenus.Audio.Play();
            }
            selectorPanel.localPosition = new Vector2(0, 9);

        }



    }

  

    void InvokeCanvasInfo()
    {
        infoCanvas.enabled = true;
        LibreriaS.Batalla.criaturasCanvas.enabled = false;
    }

    /// <summary>
    /// update Values PosSelect
    /// </summary>
    /// <param name="numaAux"></param>
    public void updateVars(int numaAux)
    {
        posSelect = numaAux;
    }

    /// <summary>
    /// activa el panel para ver las opciones disponibles dependiendo si se abre desde el menu o el Brawler.
    /// </summary>
    void enableSubPanel()
    {
        LibreriaS = FindObjectOfType<libreriaDeScrips>();
        //modo de seleccion rapida para cuando el jugador  entra en necesita cambia  la criatura rapidamente despues de  Un  Ko
        if (SeleccionRapida == true)
        {

            posSelectLv2 = 1;
            levels = 2;
        }
        //aplicar en la criatura que esta guardada
        else if (
                   LibreriaS.Batalla.ActualSelNumPlayer != (posSelect - 1) && AplicarIten == true && TriggerChoiseBrawler == true
                   || TriggerChoiseBrawler == false && AplicarIten == true
                   )
        {
            LibreriaS.Batalla.pausaIenumerator = true;



            ///llamar  Inumerator sobre el que se le va afectar los numeros
            cortineItenEfect = StartCoroutine(LibreriaS.inventario.listTables[LibreriaS.inventario.AuxLvlHorizo].Item[LibreriaS.inventario.AuxLvlVerti].FuncionNormal(posSelect - 1, 
                LibreriaS.informacionCrewild.CrewillInstancia[posSelect - 1]));

            //  define si una  se  empieza a revisar el estado de las crewild fuera de  batalla
            EfectoFueraDeBatalla.verificador();

            LibreriaS.inventario.AuxCaptadorAccion = null;
            LibreriaS.inventario.AuxLvlHorizo = 0;
            LibreriaS.inventario.AuxLvlVerti = 0;
            ActivateCrewildChoise = false;
            levels = 1;

        }

        //aplicar item
        else if (AplicarIten == true && LibreriaS.Batalla.pausaIenumerator == false && TriggerChoiseBrawler == true)
        {
            /// aplicar en la criatura utilizada
            if (LibreriaS.Batalla.ActualSelNumPlayer == (posSelect - 1))
            {
                LibreriaS.Batalla.SobrequienRecaeEfectoItem = posSelect - 1;
                LibreriaS.Batalla.ListaAcciones[1] =  "Iten";
                LibreriaS.Batalla.NombreItenAUsar[1] = LibreriaS.inventario.AuxCaptadorAccion;

               
                FindObjectOfType<libreriaDeScrips>().inventario = FindObjectOfType<Inventario>();
                LibreriaS.inventario.AuxCaptadorAccion = null;

                LibreriaS.inventario.ExitUsarIten();
                LibreriaS.Batalla.exitInventaryPanel();
                levels = 1;

            }
        }
        // entrada normal
        else if (TriggerChoiseBrawler == false )
        {

            subPanelObj.SetActive(true);
            posSelectLv2 = 1;
            MoveLevel2 = true;
            levels = 2;
            textoPanales();
        }
        //menu  de modo batalla 
        else if (TriggerChoiseBrawler == true)
        {
            //  subPanelObjBrawler.SetActive(true);
            subPanelObj.SetActive(true);
            posSelectLv2 = 1;
            MoveLevel2 = true;
            levels = 2;
            textoPanales();
           

        }
       
       
    }

    void textoPanales()      
    {
        //TextPanelBox.text = "";


        if (TriggerChoiseBrawler == false)
        {
            if (AccionSecundariaCW = verificadorAccionSecundaria() == true)
            {
                textoAccionCrewildsMod();
            }
            else
            {
                TextPanelBox.text = "Cambiar\n" +
                               "Info\n" +
                               "Objeto\n" +
                               "Atras";
            }

            
        }
       else if (TriggerChoiseBrawler == true)
        {
            TextPanelBox.text = "Cambiar\n" +
                                "Info\n" +
                                "Atras";
        } 

    }

    /// <summary>
    /// define si al revisar los ataques de un Cw  este presenta un ataque que se pueda usar fuera de batalla
    /// </summary>
    /// <returns></returns>
    private bool verificadorAccionSecundaria()
    {
        bool valorARetornar = false;

        for (int i =0; i < 4;  i++)
        {
            if (infoCrewild.CrewillInstancia[posSelect - 1].ataques[i] != null)
            {
                if (infoCrewild.CrewillInstancia[posSelect - 1].ataques[i].EfectoFueradeBatalla == true)
                {
                    valorARetornar = true;
                }
            }


        }

        return valorARetornar;
    }

    private void textoAccionCrewildsMod()
    {
        TextPanelBox.text = "Cambiar\n" +
                               "Info\n" +
                               "Objeto\n";
        for (int i = 0; i < 4; i++)
        {
            if (infoCrewild.CrewillInstancia[posSelect - 1].ataques[i] != null)
            {
                if (infoCrewild.CrewillInstancia[posSelect - 1].ataques[i].EfectoFueradeBatalla == true)
                {
                    TextPanelBox.text += infoCrewild.CrewillInstancia[posSelect - 1].ataques[i].ToString()+ "\n"; 
                }
            }
        }

        TextPanelBox.text += "Atras\n";      
    }


    


    /// <summary>
    /// pause  time before begin entrace in choise since Amimator
    /// </summary>
    public void PauseStart()
    {
       
        timePauseStart = Time.time +0.1f ;
       
    }
    /// <summary>
    /// Actualiza los datos en la barras de Salud, cansancio y exp 
    /// </summary>
  public  void actualizaDatos()
    {
        for (int i = 0; i < LibreriaS.informacionCrewild.CrewillInstancia.Length; i++)
        {
            
                BulletTransf[i].gameObject.SetActive(false);
           
            if (LibreriaS.informacionCrewild.CrewillInstancia[i] != null)
            {
                SpriteRendersMenu[i] = GameObject.Find("Crewild/Crewild/under panel/criatura " + (i + 1) + "/criatura").GetComponent<Image>();
                SpriteRendersMenu[i].sprite = infoCrewild.CrewillInstancia[i].SpriteCrewildmenuSelec[0];
                LibreriaS.informacionCrewild.InfoAllCrewild(i);
                BulletTransf[i].gameObject.SetActive(true);

                if (LibreriaS.informacionCrewild.CrewillInstancia[i] != null)
                {
                    //verifica estados
                    if (infoCrewild.CrewillInstancia[i].EstadosCrewild != EstadosEnum.None)
                    {

                        ImageEstados[i].transform.gameObject.SetActive(true);
                        ImageEstados[i].color = informacionCrewild.RetornAColor(infoCrewild.CrewillInstancia[i].EstadosCrewild);
                        TextEstados[i].text = informacionCrewild.RetornADiminutivo(infoCrewild.CrewillInstancia[i].EstadosCrewild);

                    }
                    else
                    {
                        ImageEstados[i].transform.gameObject.SetActive(false);
                        ImageEstados[i].color = informacionCrewild.RetornAColor(infoCrewild.CrewillInstancia[i].EstadosCrewild);
                        TextEstados[i].text = informacionCrewild.RetornADiminutivo(infoCrewild.CrewillInstancia[i].EstadosCrewild); ;
                        
                    }
                }

            }
          
        }      

    }



    void changeCrewildPosition()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {

            MetodosParaAtaque MetodosAux = new MetodosParaAtaque();
          
            AuxCrewill[1] = infoCrewild.CrewillInstancia[posSelect - 1];
            SeleccionCambio[1] = true;


        }

           if (SeleccionCambio[0] ==  true && SeleccionCambio[1] == true)
            {

                StopAllCoroutines();
                AuxArrayIntCrewil2 = posSelect-1;

                infoCrewild.CrewillInstancia[AuxArrayIntCrewil1] = AuxCrewill[1];
                infoCrewild.CrewillInstancia[AuxArrayIntCrewil2] = AuxCrewill[0];


            //    SpriteRendersMenu[AuxArrayIntCrewil1].sprite = infoCrewild.CrewillInstancia[AuxArrayIntCrewil1].SpriteCrewildmenuSelec[0];
            //  SpriteRendersMenu[AuxArrayIntCrewil2].sprite = infoCrewild.CrewillInstancia[AuxArrayIntCrewil2].SpriteCrewildmenuSelec[0];




            SeleccionCambio[0] = false;
            SeleccionCambio[1] = false;

                AuxCrewill[0] = null;
                AuxCrewill[1] = null;



                MoveLevel2 = true;

                posSelect = AuxLastPosSelect;
                SelectPos();


            actualizaDatos();
            Retroceder();
        }


    }
}
