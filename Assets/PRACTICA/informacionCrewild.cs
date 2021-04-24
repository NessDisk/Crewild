using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class informacionCrewild : MonoBehaviour {


    public bool Active;

    GameObject GeneralInfoObj, CrewildInfoObj, AttackObj, PosturesObj, breedingObj;

    public int posPanel = 1, LevelCrewild, CantidadTotalDisponeble;

    public bool disapadorContadorCriaturas;

    public static informacionCrewild SharedInstancia;

    /// <summary>
    /// obsoleto pero necesario para arreglar problemas
    /// </summary>
    public StaffStatisticCrewild[] EstadisticasCrewild = new StaffStatisticCrewild[6];

    /// <summary>
    ///  dentro de este metodo estan todos los valores de los crewild y es modificable simplente hayq ue ponerle
    ///  una clase que sea hija de esta.
    /// </summary>
    public CrewildBase[] CrewillInstancia = new CrewildBase[6] ;


    private bool[] ValidatorBool = new bool[5];


    private Text NameCrewild, Nivel;
    private Text Crewild, Nombre, Tipo, Estado, Efecto, TipeMenu, Descripcion;

    private Text ataque, Defence, Evation, Speed, AtaClass, DefClass, Resistence;
    private Text[] AtaqueText = new Text[4];
    private Text Postures1, Postures2, Postures3, Postures4;
  

    private Text[] Tipos = new Text[2];
    private RectTransform RectfTipo2;
    private Image[] imagesTipos = new Image[2];

    private Text TipePanel;


    private Image imagenCriatura, botones;
    private Image estadoImag;

    //panel ataques
    private RectTransform RectfPanelAtaques;
    private int SelectorAtaques;
    public bool ActivaNavegacion;
    private Text DescripcionAtaque;


    private Scrollbar BarraExp, BarraHp;


    //panel Crianza
    private Scrollbar HambreBarra, SueñoBarra, EmpatiaBarra, CorduraBarra;
    private Text HambreText, SueñoText, EmpatiaText, ValiantText, CorduraText;

    private Text SigNvl, HpInfo;


    /// <summary>
    /// acceso rapido a todos los scrips
    /// </summary>
    public libreriaDeScrips LibreriaS;

    public bool cierraPaneles;

    public static Vector3 PosActualJuador;

    public bool CargueDatos;

    void Awake()
    {
        if (SharedInstancia == null)
        {                       
            SharedInstancia = this;
        }
       


       

    }

    private void OnLevelWasLoaded(int level)
    {
        LibreriaS = GameObject.Find("Game Manager/").GetComponent<libreriaDeScrips>();
    }
    // Use this for initialization
    void Start() {
        

        LibreriaS = GameObject.Find("Game Manager/").GetComponent<libreriaDeScrips>();

        CrewillInstancia = new CrewildBase[6];
        int Auxlevel = 10;
          CrewillInstancia[0] = new Beslin(Auxlevel);
          CrewillInstancia[2] = new  crear_Crewild_Grismon_Insecto_Energia(Auxlevel);
        /*     CrewillInstancia[3] = new Kanget(Auxlevel);
             CrewillInstancia[4] = new Beslin(Auxlevel);
             CrewillInstancia[5] = new Retolizar(Auxlevel);*/


        //  barras de estaminas
        //  CrewillInstancia[0].EstadosCrewild = EstadosEnum.Paralize;
        //  CrewillInstancia[1].EstadosCrewild = EstadosEnum.poison;
        //  CrewillInstancia[0].EstadosCrewild = EstadosEnum.Paralize;
        //  CrewillInstancia[0].Hp = CrewillInstancia[0].hpTotal / 2;

        BarraHp = GameObject.Find("informacion/estadisticas/NameBox/Hp").GetComponent<Scrollbar>();
        HpInfo = GameObject.Find("informacion/estadisticas/NameBox/Hp/text").GetComponent<Text>();

        BarraExp = GameObject.Find("informacion/estadisticas/NameBox/Exp").GetComponent<Scrollbar>();
        SigNvl = GameObject.Find("informacion/estadisticas/NameBox/Exp/sig Nvl").GetComponent<Text>();



        GeneralInfoObj = GameObject.Find("informacion/estadisticas/GeneralInfo");
        CrewildInfoObj = GameObject.Find("informacion/estadisticas/CrewildInfo");
        AttackObj      = GameObject.Find("informacion/estadisticas/Attack");
        PosturesObj    = GameObject.Find("informacion/estadisticas/Postures");
        breedingObj    = GameObject.Find("informacion/estadisticas/Breeding");

        //-------- panles Statick info -----------

        NameCrewild = GameObject.Find("informacion/estadisticas/NameBox/NameCrewild").GetComponent<Text>();
        Nivel = GameObject.Find("informacion/estadisticas/NameBox/NivelCriatura").GetComponent<Text>();

       



        //-------- panles general info -----------

        Crewild  = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Crewild").GetComponent<Text>();
        Nombre   = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Nombre").GetComponent<Text>();
        Tipo     = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Tipo").GetComponent<Text>();
       

        Estado   = GameObject.Find("informacion/estadisticas/GeneralInfo/estadosEfecto/Estados/Estado/Texto").GetComponent<Text>();
        estadoImag = GameObject.Find("informacion/estadisticas/GeneralInfo/estadosEfecto/Estados/Estado").GetComponent<Image>();

        Efecto   = GameObject.Find("informacion/estadisticas/GeneralInfo/estadosEfecto/Efectos").GetComponent<Text>();
        Descripcion = GameObject.Find("informacion/estadisticas/GeneralInfo/estadosEfecto/Descripcion").GetComponent<Text>();


        Tipos[0] = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Tipos 1/Texto").GetComponent<Text>();
        Tipos[1] = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Tipos 2/Texto").GetComponent<Text>();

        imagesTipos[0] = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Tipos 1").GetComponent<Image>();
        imagesTipos[1] = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Tipos 2").GetComponent<Image>();

        RectfTipo2 = GameObject.Find("informacion/estadisticas/GeneralInfo/nombreDatos/Tipos 2").GetComponent<RectTransform>();


        //-------- panel CrewildInfo info -----------

        ataque     = GameObject.Find("informacion/estadisticas/CrewildInfo/estadisticas/Ataque").GetComponent<Text>();
        Defence    = GameObject.Find("informacion/estadisticas/CrewildInfo/estadisticas/Defensa").GetComponent<Text>();
        Evation    = GameObject.Find("informacion/estadisticas/CrewildInfo/estadisticas/Velocidad").GetComponent<Text>();
        Speed      = GameObject.Find("informacion/estadisticas/CrewildInfo/estadisticas/Evacion").GetComponent<Text>();
        AtaClass   = GameObject.Find("informacion/estadisticas/CrewildInfo/estadisticas/AtaClass").GetComponent<Text>();
        DefClass   = GameObject.Find("informacion/estadisticas/CrewildInfo/estadisticas/DefClass").GetComponent<Text>();
        Resistence = GameObject.Find("informacion/estadisticas/CrewildInfo/estadisticas/Resistence").GetComponent<Text>();


        //---------- panel Attacks info ---------------

        for (int i = 0; i<4; i++)
        {
            AtaqueText[i] = GameObject.Find("informacion/estadisticas/Attack/Movimientos/Attack"+(i+1)).GetComponent<Text>();
        }

        DescripcionAtaque = GameObject.Find("informacion/estadisticas/Attack/Descripcion/text").GetComponent<Text>();



         //---------- panel Postures info ---------------

         Postures1 = GameObject.Find("informacion/estadisticas/Postures/movimientos/Postura1").GetComponent<Text>();
        Postures2 = GameObject.Find("informacion/estadisticas/Postures/movimientos/Postura2").GetComponent<Text>();
        Postures3 = GameObject.Find("informacion/estadisticas/Postures/movimientos/Postura3").GetComponent<Text>();
        Postures4 = GameObject.Find("informacion/estadisticas/Postures/movimientos/Postura4").GetComponent<Text>();


        //---------- panel crianza info ---------------

        HambreBarra  = GameObject.Find("informacion/estadisticas/Breeding/estados/Hambre").GetComponent<Scrollbar>();
        SueñoBarra   = GameObject.Find("informacion/estadisticas/Breeding/estados/Sueño").GetComponent<Scrollbar>();
        EmpatiaBarra = GameObject.Find("informacion/estadisticas/Breeding/estados/Empatia").GetComponent<Scrollbar>();
        CorduraBarra = GameObject.Find("informacion/estadisticas/Breeding/estados/Cordura").GetComponent<Scrollbar>();

        HambreText  = GameObject.Find("informacion/estadisticas/Breeding/estados/Hambre/text").GetComponent<Text>();
        SueñoText   = GameObject.Find("informacion/estadisticas/Breeding/estados/Sueño/text").GetComponent<Text>();
        EmpatiaText = GameObject.Find("informacion/estadisticas/Breeding/estados/Empatia/text").GetComponent<Text>();
        CorduraText = GameObject.Find("informacion/estadisticas/Breeding/estados/Cordura/text").GetComponent<Text>();

        //---------- panel breeding info ---------------

        TipePanel = GameObject.Find("informacion/estadisticas/panelType/textMenus").GetComponent<Text>();

        
        //foto de criatura
        imagenCriatura = GameObject.Find("informacion/estadisticas/foto criatura/imagenCrewild").GetComponent<Image>();


        //botones

        botones = GameObject.Find("informacion/Boton").GetComponent<Image>();
        botones.enabled = false;
        RectfPanelAtaques = GameObject.Find("informacion/Selector").GetComponent<RectTransform>();
        RectfPanelAtaques.GetComponent<Image>().enabled = false;
        // global text

        GeneralInfoObj.SetActive(true);
        CrewildInfoObj.SetActive(false);
        AttackObj.SetActive(false);
        PosturesObj.SetActive(false);
        breedingObj.SetActive(false);


        if (CargueDatos == false)
        {
           Invoke("InvokeCargar", 0.1f);
           CargueDatos = true;
            
        }

        EfectoFueraDeBatalla.verificador();

    }
    void InvokeCargar()
    {
        print("cargo Datos");
        CargarData();

        //  define si una  se  empieza a revisar el estado de las crewild fuera de  batalla
       
    }
    /// <summary>
    /// Guarda la data de las criaturas disponibles y en reserva , itens; 
    /// </summary>
    public void GuardaData()
    {
        //limpia la data antes de guardar
        SaveSystem.ClearData();



        //criatura en Cillindro de pistola
        for (int i = 0; i < SharedInstancia.CrewillInstancia.Length; i++)
        {
            print("Contador : " + i);
            if (CrewillInstancia[i] != null)
            {
                SaveSystem.SavedataCrewildAMano(SharedInstancia.CrewillInstancia[i], i);
                print("Guardar criatura a mano = " + SharedInstancia.CrewillInstancia[i].NombreTaxonomico);
            }
            
        }

        //criatura en el pc
        for (int i = 0; i < 144; i++)
        {
            if (LibreriaS.PcScritp.CrewildsResguardo[i] != null)
            {
                SaveSystem.SavedataCrewildAGuardada(LibreriaS.PcScritp.CrewildsResguardo[i], i);
                print(" Criatura en resguardo = " + LibreriaS.PcScritp.CrewildsResguardo[i].NombreTaxonomico+ "  pos: "+ i);
            }

        }

        RevisorDeItensAguardar();

        RevisorDatosGlovales();

        if (EventoScript.SharedInstancia != null)
        {
            EventoScript.SharedInstancia.GuardaNumEvento();
            EventoScript.SharedInstancia = null;
        }
       
    }
    /// <summary>
    /// Es la data  con la  que empieza el juagdor  con todos en valor 0 y da un scena a la cual cargar y un 
    /// </summary>
    public void GuardaDataDeinicio()
    {
        //limpia la data antes de guardar
        SaveSystem.ClearData();



        SaveDataInfoGloval Data = new SaveDataInfoGloval();
        Data.Dinero = 0;
        Data.posicion[0] = 150.25f;
        Data.posicion[1] = -1.75f;
        Data.posicion[2] = 0;

        for (int i = 0; i > 8; i++)
        {
            Data.Recompensas[i] = false;
        }

        Data.nombreZona = "Zona 1";

        RevisorDatosGlovales(Data);
    }

    /// <summary>
    /// Carga la data de las criaturas disponibles y en reserva , itens; 
    /// </summary>
    public void CargarData()
    {
        for (int i = 0; i < 6 ; i++)
        {
            CrewillInstancia[i] =null;
        } 
     
        for (int i = 0; i < 6; i++)
        {
            ///Criatuas  en mochila
            SaveDataCrewild data = SaveSystem.loadDataCrewildAmano(i);

           
            if (data != null)
            {
                CrewillInstancia[i] = RetornarCrweild(data);
            }           

        }

        for (int i = 0; i < 144; i++)
        {
            ///Criatuas  en pc
            SaveDataCrewild data = SaveSystem.loadDataCrewildAGuardada(i);


            if (data != null)
            {
              
                LibreriaS.PcScritp.AñadirCriatura(RetornarCrweild(data) , i)  ;
            }

        }


        for (int i = 0; i < 100; i++)
        {
            ///cargaInventario
            SaveDataIten data = SaveSystem.loadDataItem(i);


            if (data != null)
            {
               
                LibreriaS.inventario.DefineList(data);
            }

        }

        cargaDatosGlovales();

    }

    void cargaDatosGlovales()
    {
        SaveDataInfoGloval data = SaveSystem.loadDataDatosGlovales();
        for (int i = 0; i < 8; i++)
        {
            LibreriaS.PanelJugador.carteleImgWanted[i].activadorCarteles = data.Recompensas[i];
        }
        LibreriaS.inventario.Dinero = data.Dinero;

        Vector2 Posvar = new Vector2(data.posicion[0], data.posicion[1]);

        FindObjectOfType<movimiento>().transform.position = Posvar;
        FindObjectOfType<movimiento>().pos = Posvar;

        if (data.itenEquipado != null)
            LibreriaS.inventario.ItenEquipado = EncontrarMetodo.EncontrarItem(data.itenEquipado);
        else
            LibreriaS.inventario.ItenEquipado = null;


    }

    public static string CargarNombreZona()
    {
        SaveDataInfoGloval data = SaveSystem.loadDataDatosGlovales();

        return data.nombreZona;
    }

    void RevisorDeItensAguardar()
    {
        int contador = 0;
        for (int Recorretablas =  0; Recorretablas < 7; Recorretablas++)
        {
            foreach (BaseItem bsIten in LibreriaS.inventario.listTables[Recorretablas].Item)
            {
                SaveSystem.SaveDataItens(bsIten, contador);
                contador++;
            }
        }

      
    }

   void RevisorDatosGlovales()
    {
        SaveDataInfoGloval DatosGlovales = new SaveDataInfoGloval();
       
        for (int i = 0; i < 8; i++)
        {
            DatosGlovales.Recompensas[i] = LibreriaS.PanelJugador.carteleImgWanted[i].activadorCarteles;
        }

        DatosGlovales.Dinero = LibreriaS.inventario.Dinero;

       
        if (PosActualJuador.x != 0 && PosActualJuador.y != 0)
        {
            DatosGlovales.posicion[0] = PosActualJuador.x;
            DatosGlovales.posicion[1] = PosActualJuador.y;
            DatosGlovales.posicion[2] = PosActualJuador.z;
        }
        else 
        { 
            float DistanciaMasCorta = 9999 , DistanciaActual = 0;

           


            foreach (MarcadorPuntoControl Mc in FindObjectsOfType<MarcadorPuntoControl>())
            {
                DistanciaActual = 0;
                DistanciaActual = pathfinderGrid.ManhattanDistance(Mc.transform.position, FindObjectOfType<movimiento>().transform.position);
               

                if (DistanciaActual < DistanciaMasCorta)
                {
                    DistanciaMasCorta = DistanciaActual;
                   
                    DatosGlovales.posicion[0] = Mc.transform.position.x;
                    DatosGlovales.posicion[1] = Mc.transform.position.y;
                    DatosGlovales.posicion[2] = Mc.transform.position.z;
                }

               
            }
       }

       
        DatosGlovales.nombreZona = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (LibreriaS.inventario.ItenEquipado != null)
            DatosGlovales.itenEquipado = LibreriaS.inventario.ItenEquipado.Nombre;
        else
            DatosGlovales.itenEquipado = null;

        SaveSystem.SaveDataInfoGloval(DatosGlovales);
    }

   public void RevisorDatosGlovales(SaveDataInfoGloval datosAEnviar)
    {
        SaveDataInfoGloval DatosGlovales = new SaveDataInfoGloval();

        for (int i = 0; i < 8; i++)
        {
            DatosGlovales.Recompensas[i] = datosAEnviar.Recompensas[i];
        }

        DatosGlovales.Dinero = datosAEnviar.Dinero;

        DatosGlovales.posicion[0] = datosAEnviar.posicion[0];
        DatosGlovales.posicion[1] = datosAEnviar.posicion[1];
        DatosGlovales.posicion[2] = datosAEnviar.posicion[2];

        DatosGlovales.nombreZona = datosAEnviar.nombreZona;

        DatosGlovales.itenEquipado = null;

        SaveSystem.SaveDataInfoGloval(DatosGlovales);
    }
    CrewildBase RetornarCrweild(SaveDataCrewild data)
    {
        CrewildBase CrewildARetornar =  null;

        CrewildARetornar = EncontrarMetodo.EncontrarCrewild(data.nombreTaxonomico, 1);


        //metodo para llamar CrewillInstancia
        CrewildARetornar.Nombre = data.nombre;



        //devuelve el genero
        if (data.Genero.ToString() == GeneroEnum.Macho.ToString())
            CrewildARetornar.genero = GeneroEnum.Macho;

        else if (data.Genero.ToString() == GeneroEnum.Hembra.ToString())
            CrewildARetornar.genero = GeneroEnum.Hembra;


        //devuelve los ataques
        for (int a = 0; a < 4; a++)
        {
            CrewildARetornar.ataques[a] = null;
            
            if (data.Ataques[a] != null)
            {

                CrewildARetornar.ataques[a] = EncontrarMetodo.EncontrarAtaques(data.Ataques[a]);
            }

        }

        CrewildARetornar.Hp = data.hp;
        CrewildARetornar.hpTotal = data.hpTotal;

        //estadisticias
        CrewildARetornar.attack = data.ataque;
        CrewildARetornar.defence = data.defensa;
        CrewildARetornar.speed = data.velocida;
        CrewildARetornar.precision = data.presicion;
        CrewildARetornar.Evacion = data.evacion;
        CrewildARetornar.EspecialAttack = data.ataqueEspecial;
        CrewildARetornar.EspecialDefensa = data.DefensaEspecial;
        CrewildARetornar.Resistence = data.Resistencia;


        // Nivele de la criatura
        CrewildARetornar.Nivel = data.Nivel;

        //experiencia
        CrewildARetornar.experiencia = data.experiencia;
        CrewildARetornar.experienciaTotal = data.experienciaTotal;

        //empatia
        CrewildARetornar.sueño = data.sueño;
        CrewildARetornar.hambre = data.hambre;
        CrewildARetornar.Cordura = data.Cordura;
        CrewildARetornar.empatia = data.empatia;

        CrewildARetornar.sueñoTotal = data.sueñoTotal;
        CrewildARetornar.hambreTotal = data.hambreTotal;
        CrewildARetornar.CorduraTotal = data.CorduraTotal;
        CrewildARetornar.empatiaTotal = data.empatiaTotal;


        print("cargada datos, hp = " + CrewildARetornar.NombreTaxonomico);
        //actualiza los sprites  en el menu de acciones.
        //    LibreriaS.SeleccionDeCriaturas.ajusteSprite(i);


        return CrewildARetornar;
    }

        // Update is called once per frame
        void Update () {
        ///guardar datos
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardaData();


        }

        // cargar  datos
        if (Input.GetKeyDown(KeyCode.H))
        {
            CargarData();


        }

        if (Active == false)
        {
            return;
        }
        if (disapadorContadorCriaturas == false)
        {
            CantidadTotalDisponeble = cantidadTotalDecriaturas();
           
            disapadorContadorCriaturas = true;
        }

        if (Active == true)
        {
          
            Accion();
            if (CrewillInstancia[LevelCrewild] != null)
            {
                TextInfo();
            }
            

        }
      

        //esta condicion devuelve el panel a su  estado inicial
        if (

                posPanel != 1 && Active == false ||
             LevelCrewild != 0 && Active == false

            )
        {

            print("prueba de panel");
            CondicionesIniciales();

        }


    }

     int cantidadTotalDecriaturas()
    {
        int contador = 0;

        foreach (CrewildBase Cbs in LibreriaS.informacionCrewild.CrewillInstancia)
        {
            if (Cbs != null)
            {
                contador++;
            }

        }
       // contador--;
        return contador;
    }


   public void  AcionesPC(CrewildBase crewild)
    {

        if (ActivaNavegacion == false)
        {
            SelectPos();
        }
         
            Secuencias(crewild);

            TextInfo(crewild);


    }


    void Secuencias( CrewildBase crewild)
    {

        if (ActivaNavegacion == true && Input.GetKeyDown(KeyCode.Space) ||
        ActivaNavegacion == true && Input.GetKeyDown(KeyCode.C)

        )
        {
            Invoke("retroceder", 0.2f);
        }

        if (cierraPaneles == true)
        {
            cerraPaneles();
            cierraPaneles = false;
        }
       
        if (posPanel == 1)
        {
            TipePanel.text = "Info";
            GeneralInfoObj.SetActive(true);
            botones.enabled = false;
        }
        else if (posPanel == 2)
        {
            TipePanel.text = "Estadisticas";
            CrewildInfoObj.SetActive(true);
            botones.enabled = false;
        }

        else if (posPanel == 3)
        {
            TipePanel.text = "Ataques";
            AttackObj.SetActive(true);
            botones.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space) && ActivaNavegacion == false)
                ActivaNavegacion = true;
            else if (ActivaNavegacion == true)           
                NavAtaques(crewild);
          
                
        }
        else if (posPanel == 4)
        {
            TipePanel.text = "Posturas";
            PosturesObj.SetActive(true);
            botones.enabled = false;

        }
        else if (posPanel == 5)
        {
            TipePanel.text = "Crianza";
            breedingObj.SetActive(true);
            botones.enabled = false;
         /*   if (Input.GetKeyDown(KeyCode.Space) && ActivaNavegacion == false)
                ActivaNavegacion = true;
            else if (ActivaNavegacion == true)
                NavAtaques(); */
        }
    }


 public   void cerraPaneles()
    {
        GeneralInfoObj.SetActive(false);
        CrewildInfoObj.SetActive(false);
        AttackObj.SetActive(false);
        PosturesObj.SetActive(false);
        breedingObj.SetActive(false);
    }
    public int SelectPos()
    {
       

        if (Input.GetKeyDown(KeyCode.D))
           {
                cierraPaneles = true;
                posPanel++;
                LibreriaS.audioMenus.Audio.Play();
            
           }
         

        else if (Input.GetKeyDown(KeyCode.A))
           {
            cierraPaneles = true;
            posPanel --;
                LibreriaS.audioMenus.Audio.Play();  
             
           }

        if (posPanel >=6)
        {
            posPanel = 1;
        }

        else if (posPanel < 1)
        {
            posPanel = 5;
        }


      
        return posPanel;

    }

  void  Accion()
    {
       
        if (ActivaNavegacion == false)
        {
            SelectPos();
            selectCrewild();          
        }

        if (   ActivaNavegacion == true && Input.GetKeyDown(KeyCode.Space) ||
               ActivaNavegacion == true && Input.GetKeyDown(KeyCode.C)

               )
        {
         Invoke("retroceder",0.2f);
        }

        if (posPanel == 1)
            {
                TipePanel.text = "Info";
                ValidatorBool[0] = true;
                GeneralInfoObj.SetActive(true);
                botones.enabled = false;
        }
             else if (posPanel == 2)
            {
                TipePanel.text = "Estadisticas";
                ValidatorBool[1] = true;
                CrewildInfoObj.SetActive(true);
                botones.enabled = false;
        }

            else if (posPanel == 3)
            {
                TipePanel.text = "Ataques";
                ValidatorBool[2] = true;
                AttackObj.SetActive(true);
                botones.enabled = true;
              if (Input.GetKeyDown(KeyCode.Space) && ActivaNavegacion == false)
                ActivaNavegacion = true;
              else if (ActivaNavegacion == true)
                NavAtaques();
        }
            else if (posPanel == 4)
            {
                TipePanel.text = "Posturas";
                ValidatorBool[3] = true;
                PosturesObj.SetActive(true);
                botones.enabled = false;
            
        }
            else if (posPanel == 5)
            {
                TipePanel.text = "Crianza";
                ValidatorBool[4] = true;
                breedingObj.SetActive(true);
            botones.enabled = true;
         /*   if (Input.GetKeyDown(KeyCode.Space) && ActivaNavegacion == false)
                ActivaNavegacion = true;
            else if (ActivaNavegacion == true)
                NavAtaques();*/
        }


            //-----------------------------------------------------------

            if (posPanel != 1 && ValidatorBool[0] == true)
            {
                GeneralInfoObj.SetActive(false);
                ValidatorBool[0] = false;
            }


            else if (posPanel != 2 && ValidatorBool[1] == true)
            {

                CrewildInfoObj.SetActive(false);
                ValidatorBool[1] = false;

            }



            else if (posPanel != 3 && ValidatorBool[2] == true)
            {
                AttackObj.SetActive(false);
                ValidatorBool[2] = false;

            }

            else if (posPanel != 4 && ValidatorBool[3] == true)
            {
                PosturesObj.SetActive(false);
                ValidatorBool[3] = false;
            }

            else if (posPanel != 5 && ValidatorBool[4] == true)
            {
                breedingObj.SetActive(false);
                ValidatorBool[4] = false;
            }

     
      
            }

   

    void retroceder()
    {
        ActivaNavegacion = false;
        RectfPanelAtaques.GetComponent<Image>().enabled = false;
        DescripcionAtaque.text = "";
       
    }

    void NavAtaques()
    {
        NumSelector();
        PosicionNav(SelectorAtaques);

        InfoAtaques(SelectorAtaques);
    }


    void NavAtaques(CrewildBase crewild)
    {
        NumSelector();
        PosicionNav(SelectorAtaques);

        InfoAtaques(SelectorAtaques, crewild);
    }

    void NavCrianza()
    {
        NumSelector();
        PosicionNav(SelectorAtaques);
    }

   void NumSelector()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            LibreriaS.audioMenus.Audio.Play();
            SelectorAtaques++;

        }


        else if (Input.GetKeyDown(KeyCode.W))
        {
            LibreriaS.audioMenus.Audio.Play();
            SelectorAtaques--;
        }

        if (SelectorAtaques < 0)
        {
            SelectorAtaques = 3;
        }

        else if (SelectorAtaques>= 4)
        {
            SelectorAtaques = 0;
        }
    }

    void PosicionNav(int num)
    {
        RectfPanelAtaques.GetComponent<Image>().enabled = true;
        if (num == 0)
        {
            RectfPanelAtaques.anchoredPosition3D = new Vector3(161.6179f, 204.8994f);
        }
        else if (num == 1)
        {
            RectfPanelAtaques.anchoredPosition3D = new Vector3(161.6179f, 157, 0);
        }
        
        else if (num == 2)
        {
            RectfPanelAtaques.anchoredPosition3D = new Vector3(161.6179f, 120, 0);
        }

        else if (num == 3)
        {
            RectfPanelAtaques.anchoredPosition3D = new Vector3(161.6179f, 83, 0);
        }
    }

    void InfoAtaques(int AtaqueArray)
    {
        if (CrewillInstancia[LevelCrewild].ataques[AtaqueArray] != null)
            DescripcionAtaque.text = CrewillInstancia[LevelCrewild].ataques[AtaqueArray].descripcion;

        else if (CrewillInstancia[LevelCrewild].ataques[AtaqueArray] == null)
            DescripcionAtaque.text = "";
    }
    void InfoAtaques(int AtaqueArray, CrewildBase crewild)
    {
        if (crewild.ataques[AtaqueArray] != null)
            DescripcionAtaque.text = crewild.ataques[AtaqueArray].descripcion;

        else if (crewild.ataques[AtaqueArray] == null)
            DescripcionAtaque.text = "";
    }

    /// <summary>
    /// movimiento en el panel de informador
    /// </summary>
    void selectCrewild()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            LibreriaS.audioMenus.Audio.Play();
            LevelCrewild = NumAsendente(LevelCrewild);
            
        }


        else if (Input.GetKeyDown(KeyCode.W))
        {
            LibreriaS.audioMenus.Audio.Play();
            LevelCrewild = NumDecente(LevelCrewild);


        }
      /*  //ajuste
        if (LevelCrewild < 0)
        {
            LevelCrewild = CantidadTotalDisponeble -1 ;

        }
        else if (LevelCrewild >= CantidadTotalDisponeble  )
        {

            LevelCrewild = 0;
        }*/

      
      
    }

    int NumAsendente(int NumActual)
    {
        int numARetornar = 0;
        numARetornar = NumActual;
        bool escoger =  false;
        while (escoger == false)
        {
            numARetornar++;          
            numARetornar = AjusteDeAlcance(numARetornar);
      
            if (CrewillInstancia[numARetornar] != null)
            {
                escoger = true;
                break;
            }
        }

        return numARetornar;
    }

    int NumDecente(int NumActual)
    {
        int numARetornar = 0;
        numARetornar = NumActual;
        bool escoger = false;
        while (escoger == false)
        {
            --numARetornar;

            numARetornar = AjusteDeAlcance(numARetornar);
            if (CrewillInstancia[numARetornar] != null)
            {
                escoger = true;
                break;
            }
        }

        return numARetornar;
    }

    int AjusteDeAlcance(int num)
    {
        int numARetornar = 0;
        numARetornar = num;
        if (numARetornar < 0)
        {
            numARetornar = 5;
        }
        else if (numARetornar > 5)
        {
            numARetornar = 0;
        }
        return numARetornar;
    }

  

    public  void CondicionesIniciales()
    {
        //LevelCrewild = 0;
        print("ok");
        posPanel = 1;
        disapadorContadorCriaturas = false;
    }

    public void condicionesDeSalida()
    {
        cerraPaneles();
        GeneralInfoObj.SetActive(true);
        posPanel = 1;
    }
    void TextInfo()
    {
        

        //-------- panles Ever Enable info -----------

        NameCrewild.text = CrewillInstancia[LevelCrewild].Nombre;
        Nivel.text = "Nvl :  "+ CrewillInstancia[LevelCrewild].Nivel;

        //barra de hp
        BarraHp.size = CrewillInstancia[LevelCrewild].Hp / CrewillInstancia[LevelCrewild].hpTotal;
        HpInfo.text = "hp:" + CrewillInstancia[LevelCrewild].Hp+"/"+ CrewillInstancia[LevelCrewild].hpTotal;

        //barra de experiencia
        BarraExp.size = CrewillInstancia[LevelCrewild].experiencia / CrewillInstancia[LevelCrewild].experienciaTotal;
        SigNvl.text = "Sig Nvl: " + (CrewillInstancia[LevelCrewild].experienciaTotal - CrewillInstancia[LevelCrewild].experiencia);

        //-------- panles Statick info -----------
        Crewild.text = "Crewild: " + CrewillInstancia[LevelCrewild].NombreTaxonomico;
        Nombre.text =  "Nombre : "+  CrewillInstancia[LevelCrewild].Nombre;
            
       
        
        // define si una criatura tiene  en su clase
        if (
            CrewillInstancia[LevelCrewild].TipoDecrewild[0] != TipoUniversalEnum.None &&
            CrewillInstancia[LevelCrewild].TipoDecrewild[1] == TipoUniversalEnum.None

           )
        {
            //  Tipo.text = "Tipo   : " + CrewillInstancia[LevelCrewild].TipoDecrewild[0];
            Tipos[0].text = CrewillInstancia[LevelCrewild].TipoDecrewild[0].ToString();           
            imagesTipos[0].color = RetornAColor(CrewillInstancia[LevelCrewild].TipoDecrewild[0]);
            RectfTipo2.gameObject.SetActive(false);

        }

        else if 
            (
           CrewillInstancia[LevelCrewild].TipoDecrewild[0] != TipoUniversalEnum.None &&
            CrewillInstancia[LevelCrewild].TipoDecrewild[1] != TipoUniversalEnum.None
            )
        {

            //  Tipo.text = "Tipo   : " + CrewillInstancia[LevelCrewild].TipoDecrewild[0] + " , " + CrewillInstancia[LevelCrewild].TipoDecrewild[1];
            RectfTipo2.gameObject.SetActive(true);
        
            imagesTipos[0].color = RetornAColor(CrewillInstancia[LevelCrewild].TipoDecrewild[0]);
            imagesTipos[1].color = RetornAColor(CrewillInstancia[LevelCrewild].TipoDecrewild[1]);
           Tipos[0].text = CrewillInstancia[LevelCrewild].TipoDecrewild[0].ToString();
            Tipos[1].text = CrewillInstancia[LevelCrewild].TipoDecrewild[1].ToString();
           
        }



        Estado.text = ""+ CrewillInstancia[LevelCrewild].EstadosCrewild;
        estadoImag.color = RetornAColor(CrewillInstancia[LevelCrewild].EstadosCrewild);

        Efecto.text = "Efecto: " + CrewillInstancia[LevelCrewild].EstadosCrewild;
        Descripcion.text = "Descripcíon: "+ CrewillInstancia[LevelCrewild].descripcion;



        //-------- panles CrewildInfo info -----------

        ataque.text     = "Ata   : " + CrewillInstancia[LevelCrewild].attack;
        Defence.text    = "Def   : " + CrewillInstancia[LevelCrewild].defence;
        Evation.text    = "Eva   : " + CrewillInstancia[LevelCrewild].Evacion;
        Speed.text      = "Spe   : " + CrewillInstancia[LevelCrewild].speed;
        AtaClass.text   = "At/cl : " + CrewillInstancia[LevelCrewild].EspecialAttack;
        DefClass.text   = "De/cl : " + CrewillInstancia[LevelCrewild].EspecialDefensa;
        Resistence.text = "Res   : " + CrewillInstancia[LevelCrewild].Resistence;



        //-------- panles Attack info -----------

        for(int  T  = 0; T < AtaqueText.Length; T++)
        {
            if (CrewillInstancia[LevelCrewild].ataques[T] != null)
            {
                AtaqueText[T].text = CrewillInstancia[LevelCrewild].ataques[T].nombreAtaque + " = " + CrewillInstancia[LevelCrewild].ataques[T].cantidadDeusosTotales + " / " + CrewillInstancia[LevelCrewild].ataques[T].cantidadDeusos;
            }
            else
            {
                AtaqueText[T].text = "";
            }

        }

        //---------- panel Postures info ---------------
        Postures1.text = CrewillInstancia[LevelCrewild].posturasDeCriatura[0].nombre + " = " + CrewillInstancia[LevelCrewild].posturasDeCriatura[0].GastoEnCansancio;
        Postures2.text = CrewillInstancia[LevelCrewild].posturasDeCriatura[1].nombre + " = " + CrewillInstancia[LevelCrewild].posturasDeCriatura[1].GastoEnCansancio;
        Postures3.text = CrewillInstancia[LevelCrewild].posturasDeCriatura[2].nombre + " = " + CrewillInstancia[LevelCrewild].posturasDeCriatura[2].GastoEnCansancio;
        Postures4.text = CrewillInstancia[LevelCrewild].posturasDeCriatura[3].nombre + " = " + CrewillInstancia[LevelCrewild].posturasDeCriatura[3].GastoEnCansancio;

        //---------- panel Breeding info ---------------

        HambreBarra.size  =  CrewillInstancia[LevelCrewild].hambre / CrewillInstancia[LevelCrewild].hambreTotal;
        SueñoBarra.size = CrewillInstancia[LevelCrewild].sueño / CrewillInstancia[LevelCrewild].sueño;
        EmpatiaBarra.size = CrewillInstancia[LevelCrewild].empatia / CrewillInstancia[LevelCrewild].empatiaTotal;
        CorduraBarra.size = CrewillInstancia[LevelCrewild].Cordura / CrewillInstancia[LevelCrewild].CorduraTotal;
        HambreText.text =  "Hambre :" + CrewillInstancia[LevelCrewild].hambre + "/"+   CrewillInstancia[LevelCrewild].hambreTotal;
        SueñoText.text =   "Sueño  :" + CrewillInstancia[LevelCrewild].sueño + "/" +   CrewillInstancia[LevelCrewild].sueñoTotal; ;
        EmpatiaText.text = "Empatia:" + CrewillInstancia[LevelCrewild].empatia + "/" + CrewillInstancia[LevelCrewild].empatiaTotal; ;
        CorduraText.text = "Cordura:" + CrewillInstancia[LevelCrewild].Cordura + "/" + CrewillInstancia[LevelCrewild].CorduraTotal; ;


        imagenCriatura.sprite = CrewillInstancia[LevelCrewild].animaCrewildFrentre[0];



    }


  public  void TextInfo(CrewildBase CrewildReceptor)
    {


        //-------- panles Ever Enable info -----------

        NameCrewild.text = CrewildReceptor.Nombre;
        Nivel.text = "Nvl :  " + CrewildReceptor.Nivel;

        //barra de hp
        BarraHp.size = CrewildReceptor.Hp / CrewildReceptor.hpTotal;
        HpInfo.text = "hp:" + CrewildReceptor.Hp + "/" + CrewildReceptor.hpTotal;

        //barra de experiencia
        BarraExp.size = CrewildReceptor.experiencia / CrewildReceptor.experienciaTotal;
        SigNvl.text = "Sig Nvl: " + (CrewildReceptor.experienciaTotal - CrewildReceptor.experiencia);

        //-------- panles Statick info -----------
        Crewild.text = "Crewild: " + CrewildReceptor.NombreTaxonomico;
        Nombre.text = "Nombre : " + CrewildReceptor.Nombre;



        // define si una criatura tiene  en su clase
        if (
            CrewildReceptor.TipoDecrewild[0] != TipoUniversalEnum.None &&
            CrewildReceptor.TipoDecrewild[1] == TipoUniversalEnum.None

           )
        {
            //  Tipo.text = "Tipo   : " + CrewillInstancia[LevelCrewild].TipoDecrewild[0];
            Tipos[0].text = CrewildReceptor.TipoDecrewild[0].ToString();
            imagesTipos[0].color = RetornAColor(CrewildReceptor.TipoDecrewild[0]);
            RectfTipo2.gameObject.SetActive(false);

        }

        else if
            (
           CrewildReceptor.TipoDecrewild[0] != TipoUniversalEnum.None &&
           CrewildReceptor.TipoDecrewild[1] != TipoUniversalEnum.None
            )
        {

            //  Tipo.text = "Tipo   : " + CrewillInstancia[LevelCrewild].TipoDecrewild[0] + " , " + CrewillInstancia[LevelCrewild].TipoDecrewild[1];
            RectfTipo2.gameObject.SetActive(true);

            imagesTipos[0].color = RetornAColor(CrewildReceptor.TipoDecrewild[0]);
            imagesTipos[1].color = RetornAColor(CrewildReceptor.TipoDecrewild[1]);
            Tipos[0].text = CrewildReceptor.TipoDecrewild[0].ToString();
            Tipos[1].text = CrewildReceptor.TipoDecrewild[1].ToString();

        }



        // Estado.text = "Estado: " + CrewillInstancia[LevelCrewild].EstadosCrewild;
        Efecto.text = "Efecto: " + CrewildReceptor.EstadosCrewild;
        Descripcion.text = "Descripcíon: " + CrewildReceptor.descripcion;



        //-------- panles CrewildInfo info -----------

        ataque.text = "Ata   : " +     CrewildReceptor.attack;
        Defence.text = "Def   : " +    CrewildReceptor.defence;
        Evation.text = "Eva   : " +    CrewildReceptor.Evacion;
        Speed.text = "Spe   : " +      CrewildReceptor.speed;
        AtaClass.text = "At/cl : " +   CrewildReceptor.EspecialAttack;
        DefClass.text = "De/cl : " +   CrewildReceptor.EspecialDefensa;
        Resistence.text = "Res   : " + CrewildReceptor.Resistence;



        //-------- panles Attack info -----------

        for (int T = 0; T < AtaqueText.Length; T++)
        {
            if (CrewildReceptor.ataques[T] != null)
            {
                AtaqueText[T].text = CrewildReceptor.ataques[T].nombreAtaque + " = " + CrewildReceptor.ataques[T].cantidadDeusosTotales + " / " + CrewildReceptor.ataques[T].cantidadDeusos;
            }
            else
            {
                AtaqueText[T].text = "";
            }

        }

        //---------- panel Postures info ---------------
        Postures1.text = CrewildReceptor.posturasDeCriatura[0].nombre + " = " + CrewildReceptor.posturasDeCriatura[0].GastoEnCansancio;
        Postures2.text = CrewildReceptor.posturasDeCriatura[1].nombre + " = " + CrewildReceptor.posturasDeCriatura[1].GastoEnCansancio;
        Postures3.text = CrewildReceptor.posturasDeCriatura[2].nombre + " = " + CrewildReceptor.posturasDeCriatura[2].GastoEnCansancio;
        Postures4.text = CrewildReceptor.posturasDeCriatura[3].nombre + " = " + CrewildReceptor.posturasDeCriatura[3].GastoEnCansancio;

        //---------- panel Breeding info ---------------

        HambreBarra.size = CrewildReceptor.hambre / CrewildReceptor.hambreTotal;
        SueñoBarra.size = CrewildReceptor.sueño / CrewildReceptor.sueño;
        EmpatiaBarra.size = CrewildReceptor.empatia / CrewildReceptor.empatiaTotal;
        CorduraBarra.size = CrewildReceptor.Cordura / CrewildReceptor.CorduraTotal;
        HambreText.text = "Hambre :" + CrewildReceptor.hambre + "/" + CrewildReceptor.hambreTotal;
        SueñoText.text = "Sueño  :" + CrewildReceptor.sueño + "/" + CrewildReceptor.sueñoTotal; ;
        EmpatiaText.text = "Empatia:" + CrewildReceptor.empatia + "/" + CrewildReceptor.empatiaTotal; ;
        CorduraText.text = "Cordura:" + CrewildReceptor.Cordura + "/" + CrewildReceptor.CorduraTotal; ;


        imagenCriatura.sprite = CrewildReceptor.animaCrewildFrentre[0];



    }


    /// <summary>
    /// define el los datos de vida  todas criaturas de las barras de salud e todas las criaturas,  cansancio y exp
    /// </summary>
    public void InfoAllCrewild(int IntCriatura)
    {
        LibreriaS = FindObjectOfType<libreriaDeScrips>();

        /// cuando se da el salto entre escenas vuelve a a otorgarle a las variables de nuevos sus valores iniciales
        if (LibreriaS.SeleccionDeCriaturas.barrasDeSalud[IntCriatura] == null || LibreriaS.SeleccionDeCriaturas.barrasDeSalud[IntCriatura] == null)
        {
            
           // Start();
            LibreriaS.SeleccionDeCriaturas.Start();
        }

         

        LibreriaS.SeleccionDeCriaturas.barrasDeSalud[IntCriatura].fillAmount = CrewillInstancia[IntCriatura].Hp / CrewillInstancia[IntCriatura].hpTotal;
        LibreriaS.SeleccionDeCriaturas.BarraDeCansancion[IntCriatura].fillAmount = CrewillInstancia[IntCriatura].Cansancio / CrewillInstancia[IntCriatura].Cansanciototal;
        LibreriaS.SeleccionDeCriaturas.BarraExp[IntCriatura].fillAmount = CrewillInstancia[IntCriatura].experiencia / CrewillInstancia[IntCriatura].experienciaTotal;

        LibreriaS.SeleccionDeCriaturas.TextoHp[IntCriatura].text = "HP: " + (int)CrewillInstancia[IntCriatura].Hp + "/" + (int)CrewillInstancia[IntCriatura].hpTotal;

        LibreriaS.SeleccionDeCriaturas.nombreCrewild[IntCriatura].text = CrewillInstancia[IntCriatura].NombreTaxonomico; 


    }

    public Color RetornAColor(TipoUniversalEnum Tipo)
    {
        Color ColorARetornar = new Color(0.0f, 0.0f, 0.0f);

        switch (Tipo)
        {
            case TipoUniversalEnum.Insect:
                //verde
                ColorARetornar = new Color(0.375534f, 0.9150943f, 0.3991772f) ;
            break;
            case TipoUniversalEnum.Normal:
                //Neutro
                ColorARetornar = Color.white;
                break;
            case TipoUniversalEnum.Energia:
                //Amarrillo
                ColorARetornar = new Color(0.910404f, 0.9137255f, 0.3764706f);
                break;
            case TipoUniversalEnum.Plant:
                //verde oscuro
                ColorARetornar = new Color(0.2616917f, 0.764151f, 0.1549929f);
                break;
            case TipoUniversalEnum.Agros:
                //naranja oscuro
                ColorARetornar = new Color(0.8773585f, 0.5604118f, 0.1448469f);
                break;
            case TipoUniversalEnum.Ave:
                //naranja claro
                ColorARetornar = new Color(0.9339623f, 0.798476f, 0.621173f);
                break;
            case TipoUniversalEnum.Explosiva:
                //rojo oscuro o marron
                ColorARetornar = new Color(0.4811321f, 0.07943217f, 0.08698845f);
                break;
            case TipoUniversalEnum.Espectro:
                //rojo oscuro o marron
                ColorARetornar = new Color(0.5f, 0.2004717f, 0.4466064f);
                break;

            case TipoUniversalEnum.Agua:
                //rojo oscuro o marron
                ColorARetornar = new Color(0.1839623f, 0.7034869f, 1f);
                break;
            case TipoUniversalEnum.Fuego:
                //rojo oscuro o marron
                ColorARetornar = new Color(1f, 0.1927328f, 0.1843137f);
                break;

        }

       


                    return ColorARetornar;
    }

    public static Color RetornAColor(EstadosEnum Tipo)
    {
        Color ColorARetornar = new Color(0.0f, 0.0f, 0.0f);

        switch (Tipo)
        {
            case EstadosEnum.None:
                //verde
                ColorARetornar = new Color(1, 0.9150943f, 0.3991772f);
                break;
            case EstadosEnum.poison:
                //Morado
                ColorARetornar = new Color(1f, 0.2f, 1f);
                break;

            case EstadosEnum.Paralize:
                //amarillo
                ColorARetornar = new Color(1f, 1, 0f);
                break;
        }

        return ColorARetornar;
    }


    public static string RetornADiminutivo(EstadosEnum Tipo)
    {
        string ColorARetornar =  null;

        switch (Tipo)
        {
            case EstadosEnum.None:
                //verde
                ColorARetornar = "";
                break;
            case EstadosEnum.poison:
                //Morado
                ColorARetornar = "VEn";
                break;
        }

        return ColorARetornar;
    }

}



