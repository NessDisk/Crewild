using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Inventario : MonoBehaviour {
    public bool TriggerInventary, brawlerMode;

    public int Dinero = 2000;

    /// <summary>
    /// cuando esta activa permite la vegacion en panel de invencion
    /// </summary>
    public bool TriggerMoveAction;

    private GameObject Consumible, Attacks, ObjClave, Frutas, Jaulas, Stands, ObjEfects, Accions;




    // posicion del planel
    private Vector3[] pos = new Vector3[7], posInitial = new Vector3[7], panelPos = new Vector3[7], panelPosInitial = new Vector3[7];

    private RectTransform[] CorchetesTranfTablas = new RectTransform[7];


    private RectTransform CorchetesTranfAccion = new RectTransform();

    private Vector3 posAction = new Vector3(), posInitialAction = new Vector3();


    /// <summary>
    /// define el Rectranform de todos los paneles 
    /// 0: consumibles, 1: Ataques , 2: ObjClaves, 3: frutas, 4: espacios o jaulas, 5: posturas, 6: Objefect
    /// </summary>
    private RectTransform[] panel = new RectTransform[7];

    private RectTransform ReferencePanel = new RectTransform();



    private Text TextInfoObj, TextAccions, TextZonaMochila;

    /// <summary>
    ///  define en cual tabla se esta.
    /// 0: Consumibles, 1: Attacks, 2: ObjClaves, 3: Frutas, 4: Jaulas, 5: Stands, 6: ObjEfect
    /// </summary>
    public int levelHorizontal = 0, AuxLvlHorizo, AuxLvlVerti, levelAccion = 0;
    /*    , LvlVerticalConsumible = 1, LvlVerticalAttacks = 1, LvlVerticalObjClave = 1, LvlVerticalFrutas = 1
         , LvlVerticalJaulas = 1, LvlVerticalStands = 1, LvlVerticalObjEfects = 1;*/

    /// <summary>
    /// : Define la posicion del objeto en vetical en este caso cual objeto
    /// </summary>
    public int[] LvlsVertical = new int[7] { 0, 0, 0, 0, 0, 0, 0 };


    public float speed = 10, distance = 50, distanceLvlAccion = 25, limitYUp, limitYDown;


    public enum TablesName

    {

        Consumible,
        Attacks,
        ObjClave,
        Frutas,
        Jaulas,
        Stands,
        ObjEfects
    }

    // public ItenType iten = new ItenType();

    public List<ItemTest> item;


    // public List<Item> ConsumiblesList, AttacksList, ObjClavesList, FrutasList, JaulasList, StandsList, ObjEfectList;



    /// <summary>
    /// 0: Consumibles, 1: Attacks, 2: ObjClaves, 3: Frutas, 4: Jaulas, 5: Stands, 6: ObjEfect
    /// </summary>
    public  ListdeInventario[] listTables;


   

    /// <summary>
    /// capta la acccion para luego devolverla en a la pisina de acciones 
    /// </summary>
    public string AuxCaptadorAccion;

    /// <summary>
    /// catura el numero de la tabla sobre la que se esta llamando
    /// </summary>
    public int NumtablaEnqueSeEsta;

    private animationScritpBatle ModoBatallaScript;

    //--- Mochila ---

    /// <summary>
    /// 0: Consumible 1:ataques, 2: Frutas, 3: espacios, 4: Objetos claves 5: Stands : objefect
    /// </summary>
    public Sprite[] MochilaImagenes = new Sprite[6];

    public Image imagenMochila;


    public List<CajaInventario> CajaItems;

    /// <summary>
    /// otorga acceso a todos los scrips dentro del documento
    /// </summary>
    public libreriaDeScrips LibreriaS ;

    /// <summary>
    ///  Cuando esta activo se da a "usar" en modo seleccion, 
    /// </summary>
    public bool PausaSelector;

    /// <summary>
    /// iten equipado por el personaje
    /// </summary>
    public BaseItem ItenEquipado;


   public 


    // Use this for initialization
    void Start()
    {

      
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

        //lista de item



        for (int i = 0; i < listTables.Length; i++)
        {
            listTables[i].Item = new List<BaseItem>();
        }


        //--- Mochila ---
        imagenMochila = GameObject.Find("objetos/objetos/animacion mochila/mochila").GetComponent<Image>();

        imagenMochila.sprite = MochilaImagenes[0];

        //gameobject

        Consumible = GameObject.Find("objetos/objetos/Consumibles");

        Attacks = GameObject.Find("objetos/objetos/Attacks");

        ObjClave = GameObject.Find("objetos/objetos/ObjClaves");

        Frutas = GameObject.Find("objetos/objetos/Frutas");

        Jaulas = GameObject.Find("objetos/objetos/Jaulas");

        Stands = GameObject.Find("objetos/objetos/Stands");

        ObjEfects = GameObject.Find("objetos/objetos/ObjEfect");

        Accions = GameObject.Find("objetos/objetos/accion");


        // obj tablas 




        //----------------------------
        //tables RecTransfor

        panel[0] = GameObject.Find("objetos/objetos/Consumibles/mask panel/limit object").GetComponent<RectTransform>();

        panel[1] = GameObject.Find("objetos/objetos/Attacks/mask panel/limit object").GetComponent<RectTransform>();

        panel[2] = GameObject.Find("objetos/objetos/ObjClaves/mask panel/limit object").GetComponent<RectTransform>();

        panel[3] = GameObject.Find("objetos/objetos/Frutas/mask panel/limit object").GetComponent<RectTransform>();

        panel[4] = GameObject.Find("objetos/objetos/Jaulas/mask panel/limit object").GetComponent<RectTransform>();

        panel[5] = GameObject.Find("objetos/objetos/Stands/mask panel/limit object").GetComponent<RectTransform>();

        panel[6] = GameObject.Find("objetos/objetos/ObjEfect/mask panel/limit object").GetComponent<RectTransform>();


        //------------------------------
        //Corchetes Rectransfor #7
        CorchetesTranfTablas[0] = GameObject.Find("objetos/objetos/Consumibles/mask panel/limit object/corchetes menu").GetComponent<RectTransform>();

        CorchetesTranfTablas[1] = GameObject.Find("objetos/objetos/Attacks/mask panel/limit object/corchetes menu").GetComponent<RectTransform>();

        CorchetesTranfTablas[2] = GameObject.Find("objetos/objetos/ObjClaves/mask panel/limit object/corchetes menu").GetComponent<RectTransform>();

        CorchetesTranfTablas[3] = GameObject.Find("objetos/objetos/Frutas/mask panel/limit object/corchetes menu").GetComponent<RectTransform>();

        CorchetesTranfTablas[4] = GameObject.Find("objetos/objetos/Jaulas/mask panel/limit object/corchetes menu").GetComponent<RectTransform>();

        CorchetesTranfTablas[5] = GameObject.Find("objetos/objetos/Stands/mask panel/limit object/corchetes menu").GetComponent<RectTransform>();

        CorchetesTranfTablas[6] = GameObject.Find("objetos/objetos/ObjEfect/mask panel/limit object/corchetes menu").GetComponent<RectTransform>();



        // texto  de los objetos

        TextInfoObj = GameObject.Find("objetos/objetos/infoObjeto/text").GetComponent<Text>();

        TextZonaMochila = GameObject.Find("objetos/objetos/zona de la mochila/text").GetComponent<Text>();



        ReferencePanel = GameObject.Find("ObjNameXcantidad").GetComponent<RectTransform>();

        TextAccions = GameObject.Find("objetos/objetos/accion/text").GetComponent<Text>();

        CorchetesTranfAccion = GameObject.Find("objetos/objetos/accion/Corchete").GetComponent<RectTransform>();

        ModoBatallaScript = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>();

        posAction = CorchetesTranfAccion.localPosition; // Take the current position
        posInitialAction = CorchetesTranfAccion.localPosition;









        for (int i = 0; i <= 6; i++)
        {
            pos[i] = CorchetesTranfTablas[i].localPosition; // Take the current position
            posInitial[i] = CorchetesTranfTablas[i].localPosition;
            panelPos[i] = panel[i].localPosition;
            panelPosInitial[i] = panel[i].localPosition;

            DisableObj();

        }

    //    ItemsListaGuardadas = new ListdeInventarioAGuardar[7];

      

        for (int i = 0; i < CajaItems.Count; i++)
        {

           DefineList(CajaItems[i]);

        }



        ItenEquipado = new Botas();
        // entraceSelector();
        Accions.SetActive(false);



    }
    /// <summary>
    /// devuelve el valor en bool si existe
    /// </summary>
    /// <param name="NombreIten"></param>
    /// <returns></returns>
    public bool DefineSiItenExiste(string NombreIten)
    {
     bool   ResultadoAdevolver = false;

        llamarIten Itenllamado = new llamarIten();
        BaseItem ItenInstanciado = Itenllamado.RetornarClase(NombreIten);


        foreach (BaseItem A in listTables[(int)ItenInstanciado.TipoItem].Item)
        {
            if (A.Nombre == ItenInstanciado.Nombre)
            {
                ResultadoAdevolver = true;
                break;
            }

        }

        return ResultadoAdevolver;
    }
    /// <summary>
    /// define la lista de objetos dentro de todas las tablas.
    /// </summary>
    /// <param name="iten"></param>
   public void DefineList(CajaInventario InventarioCaja)
    {
       
        llamarIten Itenllamado = new llamarIten();
        
        // si es true significa que el Iten ya existe por lo que no se necesita
        bool YaExisteEsteIten = false;

        BaseItem ItenInstanciado = Itenllamado.RetornarClase(InventarioCaja.NombreItem);
      

        foreach (BaseItem A in listTables[(int)ItenInstanciado.TipoItem].Item)
        {
            if (A.Nombre == ItenInstanciado.Nombre)
            {
                YaExisteEsteIten = true ;
                break;
            }

        }

        // Anade un nuevo iten a la lista 
        if (YaExisteEsteIten == false)
        {
            ItenInstanciado.cantidad = InventarioCaja.Cantidad;


            cloneText(ItenInstanciado, (int)ItenInstanciado.TipoItem);
            listTables[(int)ItenInstanciado.TipoItem].Item.Add(ItenInstanciado);
        }
        //incrementa la cantidad en lista  ya existente
        else if (YaExisteEsteIten == true)
        {
            for (int i = 0;i< listTables[(int)ItenInstanciado.TipoItem].Item.Count; i++ )
            {
                if (listTables[(int)ItenInstanciado.TipoItem].Item[i].Nombre == ItenInstanciado.Nombre)
                {
                    listTables[(int)ItenInstanciado.TipoItem].Item[i].cantidad += InventarioCaja.Cantidad;
                    listTables[(int)ItenInstanciado.TipoItem].Item[i].cantidadText.text = ""+listTables[(int)ItenInstanciado.TipoItem].Item[i].cantidad;
                    break;
                }
            }

        }
        


    }

    /// <summary>
    /// define la lista de objetos dentro de todas las tablas.
    /// </summary>
    /// <param name="iten"></param>
    public void DefineList(SaveDataIten ItensAcargar)
    {

        llamarIten Itenllamado = new llamarIten();

        // si es true significa que el Iten ya existe por lo que no se necesita
        bool YaExisteEsteIten = false;

        BaseItem ItenInstanciado = Itenllamado.RetornarClase(ItensAcargar.NombreIten);
 

        foreach (BaseItem A in listTables[(int)ItenInstanciado.TipoItem].Item)
        {
            if (A.Nombre == ItenInstanciado.Nombre)
            {
                YaExisteEsteIten = true;
                break;
            }

        }

        // Anade un nuevo iten a la lista 
        if (YaExisteEsteIten == false)
        {
            ItenInstanciado.cantidad = ItensAcargar.Cantidad;


            cloneText(ItenInstanciado, (int)ItenInstanciado.TipoItem);
            listTables[(int)ItenInstanciado.TipoItem].Item.Add(ItenInstanciado);
        }
        //incrementa la cantidad en lista  ya existente
        else if (YaExisteEsteIten == true)
        {
            for (int i = 0; i < listTables[(int)ItenInstanciado.TipoItem].Item.Count; i++)
            {
                if (listTables[(int)ItenInstanciado.TipoItem].Item[i].Nombre == ItenInstanciado.Nombre)
                {
                    listTables[(int)ItenInstanciado.TipoItem].Item[i].cantidad += ItensAcargar.Cantidad;
                    listTables[(int)ItenInstanciado.TipoItem].Item[i].cantidadText.text = "" + listTables[(int)ItenInstanciado.TipoItem].Item[i].cantidad;
                    break;
                }
            }

        }



    }


    /// <summary>
    /// define la lista de objetos dentro de todas las tablas.
    /// </summary>
    /// <param name="iten"></param>
    public ListdeInventario DefineListTienda(CajaInventario[] InventarioCaja, RectTransform panel, RectTransform referencia)
    {

        ListdeInventario inventarioTienda = new ListdeInventario();
        inventarioTienda.Item = new List<BaseItem>();

        llamarIten Itenllamado = new llamarIten();

        foreach (CajaInventario CI in InventarioCaja)
        {
            // si es true significa que el Iten ya existe por lo que no se necesita
            bool YaExisteEsteIten = false;

            BaseItem ItenInstanciado = Itenllamado.RetornarClase(CI.NombreItem);

           
            //ItenInstanciado.Datos();
            if (inventarioTienda.Item != null)
            {
                foreach (BaseItem A in inventarioTienda.Item)
                {
                    if (A.Nombre == ItenInstanciado.Nombre)
                    {
                        YaExisteEsteIten = true;
                        break;
                    }

                }
            }
            

            // Anade un nuevo iten a la lista 
            if (YaExisteEsteIten == false)
            {
                if (inventarioTienda.Item == null)
                {
                    ItenInstanciado = cloneText(ItenInstanciado, panel, referencia, 0);
                }
                else if (inventarioTienda.Item != null)
                {
                    ItenInstanciado = cloneText(ItenInstanciado, panel, referencia, inventarioTienda.Item.Count);
                }
                print(ItenInstanciado.Nombre);
                inventarioTienda.Item.Add(ItenInstanciado);
            }
            //incrementa la cantidad en lista  ya existente
            else if (YaExisteEsteIten == true)
            {


            }

        }

        return inventarioTienda;





    }


    // Update is called once per frame
    void Update()
    {

        if (FindObjectOfType<movimiento>()!=  null)
        {
            //este es el efecto que se le aplica al jugador cuando quiero usar un iten equipable 
            if (ItenEquipado != null && FindObjectOfType<movimiento>().DisparadorEvento == false)
            {
                ItenEquipado.EfectoAlequiparJugador();
            }

        }
               
        if (TriggerInventary == false )
        {
            return;
        }

        entraceSelector();

    }

    void entraceSelector()
    {

        if (LibreriaS == null)
            LibreriaS = FindObjectOfType<libreriaDeScrips>();


        //level 3 accion select
        if (TriggerMoveAction == true && Input.GetKeyDown(KeyCode.C) && brawlerMode == false
            || Input.GetKeyDown(KeyCode.C) && brawlerMode == true && TriggerMoveAction == true
            )
        {
            exitpaneles();
            
            LibreriaS.audioMenus.Audio.Play();
        }

        else if (Input.GetKeyDown(KeyCode.C) && brawlerMode == true && TriggerMoveAction == false)
        {
            if (LibreriaS == null)
             LibreriaS = FindObjectOfType<libreriaDeScrips>();
          
            LibreriaS.Batalla.exitInventaryPanel();
            LibreriaS.audioMenus.Audio.Play();
        }


        else if (TriggerMoveAction == true && PausaSelector == false)
        {
            ///posicion del selector
            if (listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].EfectoAlEquiparItenJugador == false)
            {
                MovSeleccionaAccionNormal();             
            }
            if (listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].EfectoAlEquiparItenJugador == true)
            {

                moveLvlActionEquipar();
                
            }
            ///acciones  a ejecutar
            if (CorchetesTranfAccion.localPosition == posAction)
            {
                ///dependiendo de si  el iten es para el jugador o para la criatura se ejecuta el iten
                if (listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].EfectoAlEquiparItenJugador == false)
                {
                    AccionesNormal();
                }
                else if (listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].EfectoAlEquiparItenJugador == true)
                {
                    AccionEquipar();
                }
               
            }

        }



        else if (TriggerMoveAction == false)
        {
            //movimiento por los item
            MoveHorizontal();

            CorchetesTranfTablas[levelHorizontal].localPosition = Vector3.MoveTowards(CorchetesTranfTablas[levelHorizontal].localPosition, pos[levelHorizontal], speed * Time.deltaTime);  // Move there square braket

            panel[levelHorizontal].localPosition = Vector3.MoveTowards(panel[levelHorizontal].localPosition, panelPos[levelHorizontal], speed * Time.deltaTime);  // Move there panel select

            // movimiento por las tablas
            MoveTable();

            // texto de descripcion
            if (listTables[levelHorizontal].Item.Count != 0)
            {
              
                  TextInfoObj.text = listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].descripcionItem;
            }
            else if (listTables[levelHorizontal].Item.Count == 0)
            {
                TextInfoObj.text = "";
            }


            if (Input.GetKeyDown(KeyCode.Space) && listTables[levelHorizontal].Item.Count != 0 && listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].cantidad != 0)
            {
                DefineTextoAccion();
                Accions.SetActive(true);
                TriggerMoveAction = true;

            }


            //consumible
            if (levelHorizontal == 0)
            {
                //level 2 move en vertical iten
                LvlsVertical[levelHorizontal] = MoveLevelVertival(LvlsVertical[levelHorizontal]);
                TextZonaMochila.text = "Consumible";

                imagenMochila.sprite = MochilaImagenes[0];

                Consumible.SetActive(true);



            }
            //Attacks
            else if (levelHorizontal == 1)
            {
                LvlsVertical[levelHorizontal] = MoveLevelVertival(LvlsVertical[levelHorizontal]);

                TextZonaMochila.text = "Attacks";

                imagenMochila.sprite = MochilaImagenes[1];
                Attacks.SetActive(true);


            }
            //objClaves
            else if (levelHorizontal == 2)
            {
                LvlsVertical[levelHorizontal] = MoveLevelVertival(LvlsVertical[levelHorizontal]);
                TextZonaMochila.text = "ObjClave";
                imagenMochila.sprite = MochilaImagenes[4];
                ObjClave.SetActive(true);
                // Debug.Log();

            }
            //Frutas
            else if (levelHorizontal == 3)
            {

                LvlsVertical[levelHorizontal] = MoveLevelVertival(LvlsVertical[levelHorizontal]);

                TextZonaMochila.text = "Frutas";

                imagenMochila.sprite = MochilaImagenes[2];

                Frutas.SetActive(true);


            }
            //Jaulas
            else if (levelHorizontal == 4)
            {

                LvlsVertical[levelHorizontal] = MoveLevelVertival(LvlsVertical[levelHorizontal]);

                TextZonaMochila.text = "Cartuchos";

                imagenMochila.sprite = MochilaImagenes[3];
                Jaulas.SetActive(true);



            }
            //stand
            else if (levelHorizontal == 5)
            {
                LvlsVertical[levelHorizontal] = MoveLevelVertival(LvlsVertical[levelHorizontal]);

                TextZonaMochila.text = "Stands";


                imagenMochila.sprite = MochilaImagenes[5];
                Stands.SetActive(true);

            }
            //objEfect
            else if (levelHorizontal == 6)
            {
                LvlsVertical[levelHorizontal] = MoveLevelVertival(LvlsVertical[levelHorizontal]);

                TextZonaMochila.text = "ObjEfects";

                imagenMochila.sprite = MochilaImagenes[4];
                ObjEfects.SetActive(true);


            }

        }
        DisableObj();



    }
  
    /// <summary>
    /// El seleccionador tiene permitido moverse en 3 movimientos
    /// </summary>
    void MovSeleccionaAccionNormal()
    {
        CorchetesTranfAccion.localPosition = Vector3.MoveTowards(CorchetesTranfAccion.localPosition, posAction, speed * Time.deltaTime);  // Move there square braket

        levelAccion = moveLvlAction(levelAccion);

    }
    /// <summary>
    /// si el iten tiene activa la opcion de equipar solo se puede mover en 2 opciones
    /// </summary>
    void moveLvlActionEquipar()
    {
        CorchetesTranfAccion.localPosition = Vector3.MoveTowards(CorchetesTranfAccion.localPosition, posAction, speed * Time.deltaTime);  // Move there square braket

        levelAccion = moveLvlActionEquipar(levelAccion);

    }


    void DefineTextoAccion()
    {
        if(TextAccions == null)
            TextAccions = GameObject.Find("objetos/objetos/accion/text").GetComponent<Text>();

        if (listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].EfectoAlEquiparItenJugador == false)
        {
            TextAccions.text = "Usar\n" +
                               "Dar\n" +
                               "Soltar";
        }
        if (listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].EfectoAlEquiparItenJugador == true)
        {
            ///equipa un objeto para el Jugador
            if ( ItenEquipado == null || ItenEquipado.Nombre != listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].Nombre)
            {
                TextAccions.text = "Equipar\n" +
                                   "Salir";
            }
            ///Desequipa un iten Igual Para el Juagdor
            else if (ItenEquipado != null && ItenEquipado.Nombre == listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].Nombre)
            {
                TextAccions.text = "Quitar\n" +
                                   "Salir";
            }
        }
       
    }


    int moveLvlAction(int levelInt)
    {
        if (

            CorchetesTranfAccion.localPosition == posAction
               //&& panel[levelHorizontal - 1].localPosition == panelPos[levelHorizontal - 1]
               )
        {
            // y = (int)Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.W) && levelInt > 0)
            {
                LibreriaS.audioMenus.Audio.Play();
                posAction += Vector3.up * distanceLvlAccion;// Add 1 to pos.x
                levelInt--;
            }
            else if (Input.GetKey(KeyCode.S) && levelInt < 2)
            {
                LibreriaS.audioMenus.Audio.Play();
                posAction += Vector3.down * distanceLvlAccion;// Add 1 to pos.x
                levelInt++;
            }


        }
        checked
        {
            return levelInt;
        }
    }


    int moveLvlActionEquipar(int levelInt)
    {
        if (

            CorchetesTranfAccion.localPosition == posAction
               //&& panel[levelHorizontal - 1].localPosition == panelPos[levelHorizontal - 1]
               )
        {
            // y = (int)Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.W) && levelInt > 0)
            {               
                posAction += Vector3.up * distanceLvlAccion;// Add 1 to pos.x
                levelInt--;

                LibreriaS.audioMenus.Audio.Play();
            }
            else if (Input.GetKey(KeyCode.S) && levelInt < 1)
            {
                posAction += Vector3.down * distanceLvlAccion;// Add 1 to pos.x
                levelInt++;

                LibreriaS.audioMenus.Audio.Play();
            }


        }
        checked
        {
            return levelInt;
        }
    }

    void AccionesNormal()
    {
       
            // usar
            if (Input.GetKeyDown(KeyCode.Space) && levelAccion == 0)
            {


                AuxCaptadorAccion = listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].Nombre;
                NumtablaEnqueSeEsta = levelHorizontal;
                AuxLvlVerti = LvlsVertical[levelHorizontal];
                AuxLvlHorizo = levelHorizontal;

                LibreriaS.audioMenus.Audio.Play();

            if (LibreriaS.Batalla.TriggerEntraceCinema == true && listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].TieneEfectoEnBatalla == true)
                {
                    Invoke("EntraModoUsarItem", 0.1f);
                }
                else if (LibreriaS.Batalla.TriggerEntraceCinema == false && listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].TieneEfectoNormal == true)
                {
                    Invoke("EntraModoNormalUsarItem", 0.1f);

              

            }
                else
                {
                    StartCoroutine("RespuestaNoValidad");
                }


                //  exitpaneles();

            }
            else if (Input.GetKeyDown(KeyCode.Space) && levelAccion == 1)
            {

                print("dar item a ");
            }
            else if (Input.GetKeyDown(KeyCode.Space) && levelAccion == 2)
            {

                print("soltar iten ");
            }
        

    }

    void AccionEquipar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // usar
            if (levelAccion == 0)
            {
                if (LibreriaS.Batalla.TriggerEntraceCinema == true)
                {
                    StartCoroutine("RespuestaNoValidad");
                    return;
                }
                else if (ItenEquipado == null)
                {
                    ItenEquipado = listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]];
                    print("Salir");
                }
                else if (ItenEquipado != null)
                {
                    ItenEquipado = null;

                }


            }
            else if (levelAccion == 1)
            {
                exitpaneles();
                print("Salir");
            }
            exitpaneles();
        }
   /*     else if( LibreriaS.Batalla.TriggerEntraceCinema == true)
        {
            StartCoroutine("RespuestaNoValidad");
        }*/
       
       
    }


    void MoveHorizontal()
    {
        if (
              CorchetesTranfTablas[levelHorizontal].localPosition == pos[levelHorizontal]
           && panel[levelHorizontal].localPosition == panelPos[levelHorizontal]
             )
        {
            if (LibreriaS == null)
            {
                LibreriaS = FindObjectOfType<libreriaDeScrips>() ;
            }  

            if (Input.GetKeyDown(KeyCode.A))
            {           
                    LibreriaS.audioMenus.Audio.Play();
                levelHorizontal--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                LibreriaS.audioMenus.Audio.Play();
                levelHorizontal++;
            }
        }

        if (levelHorizontal <= -1)
        {
            levelHorizontal = 6;
        }

        else if (levelHorizontal >= 7)
        {
            levelHorizontal = 0;
        }
    }

    //aqui hay un valor modificable dependiendo del largo de la lista de objetos es bastate interesante esto metodo
    int MoveLevelVertival(int levelInt)
    {
        if (
              CorchetesTranfTablas[levelHorizontal].localPosition == pos[levelHorizontal]
            )
        {
            // y = (int)Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.W) && levelInt > 0)
            {
                pos[levelHorizontal] += Vector3.up * distance;// Add 1 to pos.x
                levelInt--;
                LibreriaS.audioMenus.Audio.Play();
            }
            else if (Input.GetKey(KeyCode.S) && levelInt < listTables[levelHorizontal].Item.Count - 1)
            {
                pos[levelHorizontal] += Vector3.down * distance;// Add 1 to pos.x
                levelInt++;
                LibreriaS.audioMenus.Audio.Play();
            }

        }
        checked
        {
            return levelInt;
        }

    }

    void DisableObj()
    {
        if (levelHorizontal != 0 && Consumible.activeSelf == true)
        {
            Consumible.SetActive(false);
        }

        else if (levelHorizontal != 1 && Attacks.activeSelf == true)
        {
            Attacks.SetActive(false);
        }

        else if (levelHorizontal != 2 && ObjClave.activeSelf == true)
        {
            ObjClave.SetActive(false);
        }

        else if (levelHorizontal != 3 && Frutas.activeSelf == true)
        {
            Frutas.SetActive(false);
        }
        else if (levelHorizontal != 4 && Jaulas.activeSelf == true)
        {
            Jaulas.SetActive(false);
        }
        else if (levelHorizontal != 5 && Stands.activeSelf == true)
        {
            Stands.SetActive(false);
        }
        else if (levelHorizontal != 6 && ObjEfects.activeSelf == true)
        {
            ObjEfects.SetActive(false);
        }

    }
    /// <summary>
    /// mensajes Adevolvercuando cuando el iten no tiene funcionalidad
    /// </summary>
    IEnumerator RespuestaNoValidad()
    {
        PausaSelector = true;
       

        if (LibreriaS.Batalla.TriggerEntraceCinema == false)
        {
            TextInfoObj.text = "No se puede realizar accion";
        }

        else if (LibreriaS.Batalla.TriggerEntraceCinema == true)
        {
            TextInfoObj.text = "No se puede usar en batalla";

        }
        yield return new WaitForSeconds(3);
        TextInfoObj.text = listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].descripcionItem;
        PausaSelector = false;
    }

    void MoveTable()
    {
        if (CorchetesTranfTablas[levelHorizontal].position.y < limitYDown && panel[levelHorizontal].localPosition == panelPos[levelHorizontal])
        {
            //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
            panelPos[levelHorizontal] += Vector3.up * distance;// Add 1 to pos.x
        }

        else if (CorchetesTranfTablas[levelHorizontal].position.y > limitYUp && panel[levelHorizontal].localPosition == panelPos[levelHorizontal])
        {
            //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
            panelPos[levelHorizontal] += Vector3.down * distance;// Add 1 to pos.x
        }
    }

    /// <summary>
    /// Salida del panel selección . 
    /// </summary>
    public void exitpaneles()
    {
        CorchetesTranfAccion.localPosition = posInitialAction;
        posAction = posInitialAction;
        levelAccion = 0;
        Accions.SetActive(false);
        TriggerMoveAction = false;
        print("estos es una salida");
    }

    /// <summary>
    /// da entrada a al modo  Iten cuando entra a modo de batalla
    /// </summary>
    public void EntraModoUsarItem()
    {
        //si el iten tiene activo el bool  ActivarPanelSeleccionParty entra a modo de seleccion
        if (listTables[levelHorizontal].Item[LvlsVertical[levelHorizontal]].ActivarPanelSeleccionParty == true)
        {
            SobreQuienActuaIten();
        }
        else 
        {
            ItenEfectoSinSeleccion();
        }
    }


    /// <summary>
    /// difine la accion que se va  a ejecutar en modo  de batalla 
    /// </summary>
    void ItenEfectoSinSeleccion()
    {
        LibreriaS.Batalla.ListaAcciones[1] = "Iten";
        LibreriaS.Batalla.NombreItenAUsar[1] = AuxCaptadorAccion;
        
        //SaleDelpanelDeseleecion();
        ExitUsarIten();
    }
    /// <summary>
    /// activa el panel de selecion de Criaturas en el modo de seleccion
    /// </summary>
    void SobreQuienActuaIten()
    {
        LibreriaS.SeleccionDeCriaturas.TriggerChoiseBrawler = true;
        LibreriaS.SeleccionDeCriaturas.ActivateCrewildChoise = true;
        LibreriaS.Batalla.criaturasCanvas.enabled = true;
        LibreriaS.Batalla.objetosCanvas.enabled = false;
        LibreriaS.SeleccionDeCriaturas.AplicarIten = true;
        LibreriaS.SeleccionDeCriaturas.actualizaDatos();
        PausaSelector = true;
    }

    /// <summary>
    /// da entrada a al modo  Iten cuando esta en modo normal
    /// </summary>

    public void EntraModoNormalUsarItem()
    {
        if (LibreriaS.Batalla == null)
        {
            LibreriaS.Batalla = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<animationScritpBatle>();
        }

        LibreriaS.SeleccionDeCriaturas.ActivateCrewildChoise = true;
        LibreriaS.Batalla.criaturasCanvas.enabled = true;
        LibreriaS.Batalla.objetosCanvas.enabled = false;
        LibreriaS.SeleccionDeCriaturas.AplicarIten = true;
        LibreriaS.SeleccionDeCriaturas.actualizaDatos();
        PausaSelector = true;
    } 


        /// <summary>
        /// Da Salida al modo  Iten cuando entra a modo de batalla
        /// </summary>
        public void SaleDelpanelDeseleecion()
    {
        LibreriaS.SeleccionDeCriaturas.TriggerChoiseBrawler = false;
        LibreriaS.SeleccionDeCriaturas.ActivateCrewildChoise = false;
        LibreriaS.Batalla.criaturasCanvas.enabled = false;
        LibreriaS.Batalla.objetosCanvas.enabled = true;
        LibreriaS.SeleccionDeCriaturas.AplicarIten = false;
        PausaSelector = false;

    }

    /// <summary>
    /// cuando se activa sale modo inventario y el modo seleccion deCriaturas
    /// </summary>
    public void ExitUsarIten()
    {
        LibreriaS.SeleccionDeCriaturas.TriggerChoiseBrawler = false;
        LibreriaS.SeleccionDeCriaturas.ActivateCrewildChoise = false;
        LibreriaS.Batalla.criaturasCanvas.enabled = false;
        LibreriaS.Batalla.objetosCanvas.enabled = false;
        LibreriaS.SeleccionDeCriaturas.AplicarIten = false;
        brawlerMode = false;
        TriggerInventary = false;
        PausaSelector = false;
        

        exitpaneles();
    }
    
    /// <summary>
    /// llama  a todas las propiedades de los texto del iten en el inventario
    /// </summary>
    /// <param name="iten"> llama al objeto base del hijo</param>
    /// <param name="numAux">numero de la tabla de en la que se va  añadir Item</param>
    void cloneText(BaseItem iten, int numAux)
    {

        GameObject clone = Instantiate(Resources.Load<Object>("Prefac/Iten") as GameObject) as GameObject;


        GameObject CloneCantidad = clone.transform.Find("cantidad").gameObject;
        GameObject ImagenSprite =  clone.transform.Find("imagen").gameObject;


        //nombre
        clone.name = iten.Nombre;
        RectTransform tranf = clone.GetComponent<RectTransform>();
        iten.NombreText = clone.GetComponent<Text>();
        iten.NombreText.text = iten.Nombre;

        tranf.SetParent(panel[numAux].transform);
        tranf.transform.localPosition = ReferencePanel.transform.localPosition;
        tranf.transform.localScale = ReferencePanel.transform.localScale;

        tranf.transform.localPosition = new Vector2(tranf.transform.localPosition.x, tranf.transform.localPosition.y - 42 * listTables[numAux].Item.Count);

        // prefac obj
        iten.ObjPrefac = clone;

        //cantidad

        iten.cantidadText = CloneCantidad.GetComponent<Text>();
        iten.cantidadText.text = iten.cantidad.ToString();

        //imagen
        iten.CapturaImagen = ImagenSprite.GetComponent<Image>();
        iten.CapturaImagen.sprite = iten.ImagenIten;


    }



    /// <summary>
    /// llama  a todas las propiedades de los texto del iten en el inventario
    /// </summary>
    /// <param name="iten"> llama al objeto base del hijo</param>
    /// <param name="PanaelTranf"> panel al que se instancia iten</param>
    BaseItem cloneText(BaseItem iten, RectTransform PanaelTranf, RectTransform referencia, int tamañoTabla)
    {

        GameObject clone = Instantiate(Resources.Load<Object>("Prefac/Box text menus/iten Tienda") as GameObject) as GameObject;

        GameObject CloneCosto = clone.transform.Find("Costo").gameObject;
        GameObject ImagenSprite = clone.transform.Find("imagen").gameObject;


        //nombre
        clone.name = iten.Nombre;
        RectTransform tranf = clone.GetComponent<RectTransform>();
        iten.NombreText = clone.GetComponent<Text>();
        iten.NombreText.text = iten.Nombre;

        tranf.SetParent(PanaelTranf.transform) ;
        tranf.transform.localPosition = referencia.transform.localPosition;
        tranf.transform.localScale = referencia.transform.localScale;

        tranf.transform.localPosition = new Vector2(tranf.transform.localPosition.x, tranf.transform.localPosition.y - 42 * tamañoTabla);



        // prefac obj
        iten.ObjPrefac = clone;

        //cantidad

        iten.cantidadText = CloneCosto.GetComponent<Text>();
        iten.cantidadText.text = iten.Coste.ToString();

        //imagen
        iten.CapturaImagen = ImagenSprite.GetComponent<Image>();
        iten.CapturaImagen.sprite = iten.ImagenIten;

        return iten;
    }







    ///obsolote borrar
    /// <summary>
    /// retuna el valor del iten 
    /// </summary>
    /// <param name="auxString"></param>
    /// <returns></returns>
    ItemTest.ItenType findItemType(string auxString)
    {
        ItemTest.ItenType a = ItemTest.ItenType.none;
        foreach (ItemTest.ItenType foo in ItemTest.ItenType.GetValues(typeof(ItemTest.ItenType)))
        {
            if (foo.ToString() == auxString)
            {
                a = foo;
                break;
            }
              
        }
        
                
            checked
            {
                return a;
            }


    }

}
/// <summary>
/// iten que se quiere dar + cantidad 
/// </summary>
[System.Serializable]
public class CajaInventario
{
   public string NombreItem;
    public int Cantidad;
}



[System.Serializable]
public class ListdeInventario 
{
    public string Name ;
   
    public bool bUsed;

    /// <summary>
    /// 0: Consumibles, 1: Attacks, 2: ObjClaves, 3: Frutas, 4: Jaulas, 5: Stands, 6: ObjEfect
    /// </summary>
    /// 
    public List<BaseItem> Item;


}



/// <summary>
/// clase  obsolote borrar
/// </summary>
[System.Serializable]
public class ItemTest
{


    public string name;
    public enum ItenType

    {
         none ,
        //consumibles
        posion,
        hierva,
        pierda,
        manzana,
        antidoto,
        megaposion,
        
        // , Attacks
        Barrida,
        Recuperacion,

        //ObjClave
        llave,
        pistola,
        CañadePescar,

        //Frutas
        fruta1,
        fruta2,
        fruta3,
        fruta4,

        //Jaulas
        normal,
        especial, 
        acuatia,
        sol,
        luna,
        rapida,
        recuperacion, 
        solida,
        sinestado,
        Confusion,
        
        
        //Stands
        pasiva,
        agresiva,
        defensiva,
        desgaste,
        aguante,
        PosturaRapida,




        //ObjEfects
        ataqueEfec,
        defensaEfec,
        velocidadEfec,
        AntiEstado,
        MoreExperience,
        antiFinalHit


    }
    public ItenType itenType = new ItenType();
   

    public int cantidad;

    public Text textobj;

    [TextArea(3, 2)]
    public string Descripcion;
}

 

