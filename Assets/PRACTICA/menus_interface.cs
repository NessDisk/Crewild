using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menus_interface : MonoBehaviour {

    public static menus_interface instanceSharet;

    private RectTransform panelmask;
    RectTransform[] transforcorchete, panel;

    // posicion del player
    Vector3[] pos, posInitial, panelPos, panelPosInitial;

    public float speed;

    public float distance, limitYUp, limitYDown;



    /// <summary>
    /// value  move en axis  Y
    /// </summary>

    ///Value move in y / 
    private int Y_p, numTables;
    public int NumSquareBraquet;


    public Canvas menuDesplegable, MenuCrewilds, MenuEstados, MenuObjetos, MenuJugador, MenuEquipo, MenuGuardado,
        MenuSalirA, MenuSalir, MenuCanvas, transicionesCanvas;

    GameObject VanillaTabla, imagenCriaturacamvas;

    public movimiento PlayerMov;

    public CrewildChoiseSelect CrewildChoiseScritp;

    public informacionCrewild CreWildInfoScript;

    public Inventario InventarioScrip;

    public equipo EquipoScritp;

    public animationScritpBatle BrawlerScript;

    /// <summary>
    /// acceso rapido a todos los scrips
    /// </summary>
    public libreriaDeScrips LibreriaS ;

    /// <summary>
    /// gameobjet del cuadro de seleccion de accion
    /// </summary>
    public GameObject BoxSelecction, BoXCrewild;

    // Use this for initialization
    void Start() {

        instanceSharet = this;

        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

        numTables = 2;

        ScreenLimit();

        BrawlerScript = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>();

        transforcorchete = new RectTransform[numTables];
        panel = new RectTransform[numTables];
        pos = new Vector3[numTables];
        posInitial = new Vector3[numTables];
        panelPos = new Vector3[numTables];
        panelPosInitial = new Vector3[numTables];

        panelmask = GameObject.Find("menu inicial/Panel menu 1/mask panel 1").GetComponent<RectTransform>();

        if (GameObject.Find("menu inicial/Panel menu 1/mask panel 1/limit object/corchetes menu 1") != null)
        {
            //Panel menu 1
            transforcorchete[0] = GameObject.Find("menu inicial/Panel menu 1/mask panel 1/limit object/corchetes menu 1").GetComponent<RectTransform>();
            panel[0] = GameObject.Find("menu inicial/Panel menu 1/mask panel 1/limit object").GetComponent<RectTransform>();
            pos[0] = transforcorchete[0].anchoredPosition3D; // Take the current position
            posInitial[0] = transforcorchete[0].anchoredPosition3D;
            panelPos[0] = panel[0].anchoredPosition3D;
            panelPosInitial[0] = panel[0].anchoredPosition3D;
        }
        if (GameObject.Find("menu inicial/menu inicial/mask panel 1/limit object") != null)
        {
            //menu inicial
            transforcorchete[1] = GameObject.Find("menu inicial/menu inicial/mask panel 1/limit object/corchetes menu 1").GetComponent<RectTransform>();
            panel[1] = GameObject.Find("menu inicial/menu inicial/mask panel 1/limit object").GetComponent<RectTransform>();
            pos[1] = transforcorchete[1].anchoredPosition3D; // Take the current position
            posInitial[1] = transforcorchete[1].anchoredPosition3D;
            panelPos[1] = panel[1].anchoredPosition3D;
            panelPosInitial[1] = panel[1].anchoredPosition3D;


        }

        NumSquareBraquet = 0;

        menuDesplegable = GameObject.Find("menu inicial").GetComponent<Canvas>();

        MenuCrewilds = GameObject.Find("/Crewild").GetComponent<Canvas>();
        MenuEstados = GameObject.Find("/informacion").GetComponent<Canvas>();
        MenuObjetos = GameObject.Find("/objetos").GetComponent<Canvas>();
        MenuJugador = GameObject.Find("/Jugador").GetComponent<Canvas>();
        MenuEquipo = GameObject.Find("/equipo").GetComponent<Canvas>();
        MenuGuardado = GameObject.Find("/guardado").GetComponent<Canvas>();
        MenuSalirA = GameObject.Find("/SalirAMenu").GetComponent<Canvas>();
        MenuCanvas = GameObject.Find("/Canvas").GetComponent<Canvas>();
        transicionesCanvas = GameObject.Find("/transiciones").GetComponent<Canvas>();

        VanillaTabla = GameObject.Find("/Canvas/Vanilla");
        imagenCriaturacamvas = GameObject.Find("/Canvas/cuadroCrewild");


        PlayerMov = GameObject.Find("personaje").GetComponent<movimiento>();


        CrewildChoiseScritp = GameObject.Find("Crewild/Crewild").GetComponent<CrewildChoiseSelect>();
        CreWildInfoScript = GameObject.Find("informacion/estadisticas").GetComponent<informacionCrewild>();

        InventarioScrip = GameObject.Find("objetos/objetos").GetComponent<Inventario>();


        EquipoScritp = GameObject.Find("equipo/equipo").GetComponent<equipo>();

        BoxSelecction = GameObject.Find("Canvas/box Election");
        BoXCrewild = GameObject.Find("Canvas/cuadroCrewild");

        Invoke("InvokeCerrarCanvas", 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MenuEstados == null || CreWildInfoScript == null)
        {
            MenuEstados = GameObject.Find("/informacion").GetComponent<Canvas>();
            CreWildInfoScript = GameObject.Find("informacion/estadisticas").GetComponent<informacionCrewild>();
            return;
        }

        menus();
        //validador para entrada
        if (
            menuDesplegable.enabled == false ||
            InventarioScrip.TriggerMoveAction == true ||
            BrawlerScript.TriggerEntraceCinema == true ||
            LibreriaS.Equipo.MapaEnrtada == true

            )
        {

            return;
        }


        if (transforcorchete[0] != null || transforcorchete[1] != null)
        {
            // este if llama  la script crewild el cual se encarga de mostrar el  sub panel del menu criaturas 
            // en el monto, verificando si la variable que se esta llamando esta activa.
            if (CrewildChoiseScritp.subPanelObj.activeSelf == true || LibreriaS.informacionCrewild.ActivaNavegacion == true)
            {
             //   Debug.Log("CrewildChoiseScritp.subPanelObj.activeSelf o LibreriaS.informacionCrewild.ActivaNavegacion es True");
                return;
            }

            // define si hay algun panel activo para permitir desactivar el panel activo y volver al menu de
            // de seleccion de paneles
            if (

         MenuCrewilds.enabled == true ||
         MenuEstados.enabled == true ||
         MenuObjetos.enabled == true ||
         MenuJugador.enabled == true ||
         MenuEquipo.enabled == true ||
         MenuGuardado.enabled == true ||
         MenuSalirA.enabled == true

               )
            {
                if (
                    Input.GetKeyDown(KeyCode.C)
                   )
                {
                    CerrarMenus();
                }

                return;

            }

            else if (Input.GetKeyDown(KeyCode.Space))
            {
                LibreriaS.audioMenus.Audio.Play();
                ActivaMenus();
            }

            Panels(1);
        }

    }



    public void CerrarMenus()
    {
        Invoke("RetocederMenuDeSeleccion", 0.1f);

        LibreriaS.audioMenus.Audio.Play();
    }

    // este metodo busca abrir los menus y activar cada una de sus funciones
    void menus()
    {
        // abre el menu de seloeccion
        if (
            Input.GetKeyDown(KeyCode.C)
            && menuDesplegable.enabled == false
            && BrawlerScript.TriggerEntraceCinema == false
            && PlayerMov.DisparadorEvento == false
            && PlayerMov.pos == PlayerMov.transform.position
            )
        {
            LibreriaS.audioMenus.Audio.Play();
            Invoke("Abrirmenu", 0.1f);
        }
        else if (
            Input.GetKeyDown(KeyCode.C) && menuDesplegable.enabled == true
            &&
            MenuCrewilds.enabled == false &&
            MenuEstados.enabled == false &&
            MenuObjetos.enabled == false &&
            MenuJugador.enabled == false &&
            MenuEquipo.enabled == false &&
            MenuGuardado.enabled == false &&
            MenuSalirA.enabled == false &&

            BrawlerScript.TriggerEntraceCinema == false



            )
        {
            LibreriaS.audioMenus.Audio.Play();
            Invoke("SalirMenuSeleccion", 0.1f);
        }

    }


    void Abrirmenu()
    {
        PlayerMov.DisparadorEvento = true;
        menuDesplegable.enabled = true;
    }
    /// <summary>
    /// sale del menu de seleccion de paneles
    /// </summary>
 public  void SalirMenuSeleccion()
    {
        PlayerMov.DisparadorEvento = false;
        menuDesplegable.enabled = false;
        RestarCorchete();
    }
    // activa todos menus del jugador 
    void ActivaMenus()
    {        /*
        MenuCrewilds 
        MenuEstados
        MenuObjetos
        MenuJugador
        MenuEquipo
        MenuGuardado
        MenuSalirA
        */
        switch (NumSquareBraquet)
        {
            // menu de criaturas
            case 0:

                Debug.Log("menu de criaturas");

                if (EncontrarMetodo.DefineSiHaycriaturas())
                {
                    Invoke("invokeTimePause", 0.1f);
                    LibreriaS.SeleccionDeCriaturas.SelectPos();
                    LibreriaS.SeleccionDeCriaturas.actualizaDatos();
                }

                break;
            // Menu Estados
            case 1:

                Debug.Log("Menu Estados");

                if (EncontrarMetodo.DefineSiHaycriaturas())
                {
                    MenuEstados.enabled = true;

                    CreWildInfoScript.Active = true;
                    LibreriaS.SeleccionDeCriaturas.actualizaDatos();
                }
                break;
            // Menu Objetos
            case 2:
                Debug.Log("Menu Objetos");


                Invoke("invokeIncentario", 0.1f);
                Invoke("invokeInventarioAux", 0.05f);
                break;

            //  Menu Jugador
            case 3:
                Debug.Log(" Menu Jugador");
                MenuJugador.enabled = true;
                LibreriaS.JugadorInfo.activador = true;
                break;

            //  Menu Equipo
            case 4:
                Debug.Log("Menu Equipo");
                MenuEquipo.enabled = true;

                Invoke("invokeEquipo", 0.1f);

                break;

            //   Menu Guardado
            case 5:
                Debug.Log("  Menu Guardado");
                MenuGuardado.enabled = true;

                Invoke("InvokeGuardado", 0.1f);
                break;

            //  Menu Salir A
            case 6:
                Debug.Log("Menu Salir A");
                SceneManager.LoadScene("Intro");
                MenuSalirA.enabled = true;
                break;
            // Menu Salir del menu
            case 7:
                Debug.Log("Salir");
                SalirMenuSeleccion();
                break;
        }
    }

   
    /// <summary>
    ///  Este metodo lo que hacer es retroceder al menu inicial de seleccion de paneles
    /// </summary>
    public void RetocederMenuDeSeleccion()
    {
         MenuCrewilds.enabled = false;
         MenuEstados.enabled  = false;
         MenuObjetos.enabled  = false;
         MenuJugador.enabled  = false;
         MenuEquipo.enabled   = false;
         MenuGuardado.enabled = false;
         MenuSalirA.enabled   = false;

        CrewildChoiseScritp.ActivateCrewildChoise = false;

        CreWildInfoScript.Active = false;


        InventarioScrip.TriggerInventary = false;


        EquipoScritp.activador = false;

        LibreriaS.JugadorInfo.activador = false;
    }


    void invokeTimePause()
    {
        MenuCrewilds.enabled = true;
        CrewildChoiseScritp.ActivateCrewildChoise = true;

    }

    void invokeIncentario()
    {
        MenuObjetos.enabled = true;

    }
    void invokeInventarioAux()
    {
        InventarioScrip.TriggerInventary = true;

    }
    void invokeEquipo()
    {
        EquipoScritp.activador = true;

    }

    void InvokeGuardado()
    {
        LibreriaS.saveData.activador = true;
    }

    void InvokeCerrarCanvas()
    {
        MenuCanvas.enabled = false;
        VanillaTabla.SetActive(false);
        imagenCriaturacamvas.SetActive(false);

    }
    void Panels(int tableUse)
    {
        //The Current Position = Move To (the current position to the new position by the speed * Time.DeltaTime)

       
        transforcorchete[tableUse].localPosition = Vector3.MoveTowards(transforcorchete[tableUse].localPosition, pos[tableUse], speed * Time.deltaTime);  // Move there square braket

        panel[tableUse].anchoredPosition3D = Vector3.MoveTowards(panel[tableUse].anchoredPosition3D, panelPos[tableUse], speed * Time.deltaTime);  // Move there panel select

        ///esto le da el movimiento en al juagdor
        Y_p = (int)Input.GetAxis("Vertical");

        //ejemplo
        if (tableUse == 0) {
            if (transforcorchete[tableUse].localPosition == pos[tableUse] && Y_p < -0.1f && NumSquareBraquet < 24)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                pos[tableUse] += Vector3.down * distance;// Add 1 to pos.x
                NumSquareBraquet++;
            }

            if (transforcorchete[tableUse].localPosition == pos[tableUse] && Y_p > 0.1f && NumSquareBraquet > 0)

            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                pos[tableUse] += Vector3.up * distance;// Add 1 to pos.x
                NumSquareBraquet--;
            }

            if (transforcorchete[tableUse].anchoredPosition3D.y < limitYDown  && panel[tableUse].anchoredPosition3D == panelPos[tableUse] /*&& NumSquareBraquet >= 24*/)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                panelPos[tableUse] += Vector3.up * distance;// Add 1 to pos.x
                limitYDown -= distance;
                limitYUp -= distance;
            }

            else if (transforcorchete[tableUse].anchoredPosition3D.y > limitYUp && panel[0].anchoredPosition3D == panelPos[tableUse] /* && NumSquareBraquet < 0*/)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                panelPos[tableUse] += Vector3.down * distance;// Add 1 to pos.x
                limitYDown += distance;
                limitYUp += distance;
            }

           
        }
        else if (tableUse == 1)
        {
            if (transforcorchete[tableUse].localPosition == pos[tableUse] && Y_p < -0.1f && NumSquareBraquet < 7)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                pos[tableUse] += Vector3.down * distance;// Add 1 to pos.x
                NumSquareBraquet++;

                LibreriaS.audioMenus.Audio.Play();
            }

            if (transforcorchete[tableUse].localPosition == pos[tableUse] && Y_p > 0.1f && NumSquareBraquet > 0)

            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                pos[tableUse] += Vector3.up * distance;// Add 1 to pos.x
                NumSquareBraquet--;
                LibreriaS.audioMenus.Audio.Play();

            }
          
        }
    }


    void RestarCorchete()
    {
        pos[1] = posInitial[1];
        transforcorchete[1].localPosition = posInitial[1];
        NumSquareBraquet = 0;


    }

    // este metodo lo que hace es ajustar los parametros de movimiebto del selector dependiendo de los el  tamaño de la pantalla
void ScreenLimit()
    {
        if (Screen.height == 217)
        {
            limitYUp = 198;
            limitYDown = 33;
            distance = 45;
          //  panelmask.sizeDelta = new Vector2(panelmask.sizeDelta.x, 545);
        }

        else if (Screen.height == 302)
        {
            limitYUp = 277;
            limitYDown = 37;
            distance = 43.35f;
            //    panelmask.sizeDelta = new Vector2(panelmask.sizeDelta.x, 545);
        }
        else if (Screen.height == 366)
        {
           
            limitYUp = 334;
            limitYDown = 60;
            distance = 44.3f;
            //    panelmask.sizeDelta = new Vector2(panelmask.sizeDelta.x, 545);
        }
        else if (Screen.height == 637)
        {
            limitYUp = 588;
            limitYDown = 107;
            distance = 43.7f;
        }
    }

}
