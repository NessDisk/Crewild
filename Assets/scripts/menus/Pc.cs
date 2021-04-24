using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pc : MonoBehaviour
{
    public bool Activador;

    private RectTransform Selector;

    private Vector2 posicionInicial;

    public float DistanciaRecorrida = 0.2f;

    public float speed = 3f;
    // posicion del selector
    public Vector3 pos;

    private float x, y;

    /// rango en que va   cambiar de direccion
    /// </summary>
    private int prioridad = 0;

    public int NumPos = 0, NUmposCambio = 0, seleccionanterior, OriginalPos, selectorSacarCriatura;

   
    public CrewildBase[] CrewildsResguardo = new CrewildBase[144];

    private Image[] imagenes = new Image[144];
    public int NumPanlesActivos = 0;


    private Image ImgCrewild;
    private Text NombreText, apodo, nivel;


    private GameObject opcionesObj;

    public bool navOpciones, moverValidador, infoCriatura, sacarvalidador, SeleccionarGuardar;

    private RectTransform SelectorOpciones;

    private int PosSelector;

    private CrewildBase CriaturaAcambiar;

    private RectTransform[] PnalesImagenes = new RectTransform[2], NumTranfTabla = new RectTransform[2];

    private libreriaDeScrips LibreriaS;

    private RectTransform objCriaturasEquipadas, ObjSelectorSacarCriatura;

    private Image[] ImagenesCriaturasASacar = new Image[6];

    private Text[] TextoLvl = new Text[6];

    // Start is called before the first frame update
    void Start()
    {
        LibreriaS = FindObjectOfType<libreriaDeScrips>();

        Selector = GameObject.Find("PcC/pc/caja/Selector").GetComponent<RectTransform>();

        pos = Selector.anchoredPosition3D; // Take the current position
        speed = 150f;
        DistanciaRecorrida = 70f;

        imagenes = new Image[144];
        for (int i = 0; i < 72; i++)
        {
            int r = i + 72;
            imagenes[i] = GameObject.Find("PcC/pc/caja/listaCriaturas0/Criaturas " + i).GetComponent<Image>();
            imagenes[r] = GameObject.Find("PcC/pc/caja/listaCriaturas1/Criaturas " + r).GetComponent<Image>();
        }

        PnalesImagenes = new RectTransform[2];
        for (int e = 0; e < 2; e++)
        {
            PnalesImagenes[e] = GameObject.Find("PcC/pc/caja/listaCriaturas" + e).GetComponent<RectTransform>();
            NumTranfTabla[e] = GameObject.Find("PcC/pc/num/tabla pos" + e).GetComponent<RectTransform>();
        }
        PnalesImagenes[1].gameObject.SetActive(false);

        NumTranfTabla[0].anchoredPosition3D = new Vector2(NumTranfTabla[0].anchoredPosition3D.x, 7);



     /*  AñadirCriatura(new crear_Crewild_Grismon_Insecto_Energia(5));
        AñadirCriatura(new Xilaxi(4));
        AñadirCriatura(new crear_Crewild_Eghi_salvaje_insecto(3)); */


        NombreText = GameObject.Find("PcC/pc/info/nombre").GetComponent<Text>();
        apodo = GameObject.Find("PcC/pc/info/Mote").GetComponent<Text>();
        nivel = GameObject.Find("PcC/pc/info/nivel").GetComponent<Text>();

        ImgCrewild = GameObject.Find("PcC/pc/info/imagen/Crewild").GetComponent<Image>();

        opcionesObj = GameObject.Find("PcC/pc/Opciones");
        SelectorOpciones = GameObject.Find("PcC/pc/Opciones/Selector").GetComponent<RectTransform>();
        opcionesObj.SetActive(false);

        objCriaturasEquipadas = GameObject.Find("PcC/pc/CriaturasEquipadas").GetComponent<RectTransform>();

        ImagenesCriaturasASacar = new Image[6];
        for (int i = 0; i < 6; i++)
        {
            ImagenesCriaturasASacar[i] = GameObject.Find("PcC/pc/CriaturasEquipadas/espacio" + i + "/Criatura").GetComponent<Image>();

            TextoLvl[i] = GameObject.Find("PcC/pc/CriaturasEquipadas/espacio" + i + "/Texto Lvl").GetComponent<Text>();
        }
        ObjSelectorSacarCriatura = GameObject.Find("PcC/pc/CriaturasEquipadas/selector").GetComponent<RectTransform>();
        objCriaturasEquipadas.gameObject.SetActive(false);

        posicionInicial = Selector.anchoredPosition3D;
    }

    // Update is called once per frame
    void Update()
    {
        if (Activador == false)
        {
            return;
        }
        if (navOpciones == false && infoCriatura == false && sacarvalidador == false || moverValidador == true)
        {


            MovimientoSelectorinicial();

            //seleccion inicial
            if (Input.GetKeyDown(KeyCode.Space)
             && pos == Selector.localPosition
             && moverValidador == false
             && CrewildsResguardo[NumPos] != null
             //   && Resguardo[0].Crewilds[NumPos] != null
             )
            {
                activadorOpciones();
            }
            //cambio de posicion 
            if (moverValidador == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    cambioPosiciones();
                    reinicioVariableIciales();
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    posicionesSincambios();
                    reinicioVariableIciales();
                    return;
                }
                NavImagencCambioOpcion();
            }

        }
        //opciones
        else if (navOpciones == true && moverValidador == false)
        {
            NavOpciones();
            PosSelectorOpciones();

            if (Input.GetKeyDown(KeyCode.Space))
                AccioneOpciones();

            else if (Input.GetKeyDown(KeyCode.C))
                reinicioVariableIciales();

        }
        //info
        else if (infoCriatura == true && moverValidador == false)
        {

            if (Input.GetKeyDown(KeyCode.C) && LibreriaS.informacionCrewild.ActivaNavegacion == false)
                reinicioVariableIciales();

            //   LibreriaS.informacionCrewild.AcionesPC(Resguardo[0].Crewilds[NumPos]);
            LibreriaS.informacionCrewild.AcionesPC(CrewildsResguardo[NumPos]);
        }

        // sacar
        else if (sacarvalidador == true)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SacarCriaturaEncaja();
                reinicioVariableIciales();
                return;
            }

            else if (Input.GetKeyDown(KeyCode.C))
            {
                reinicioVariableIciales();
                return;
            }
            MovSelectorSacarCriatura();
            posSelectorSacarCriatura();
        }

    }



    public void GuardarCriatura()
    {
        if (SeleccionarGuardar == false)
        {
            MovSelectorSacarCriatura();
            posSelectorSacarCriatura();

            if (Input.GetKeyDown(KeyCode.Space))
                SeleccionarGuardar = true;


        }
        else if (SeleccionarGuardar == true)
        {
            MovimientoSelectorinicial();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SacarCriaturaEncaja();
                SeleccionarGuardar = false;
                infoNull();
                return;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Invoke("AuxSalida", 0.2f);
                return;
            }
        }


    }

    void MovimientoSelectorinicial()
    {

        if (
              Input.GetKeyDown(KeyCode.X)
              && pos == Selector.localPosition
             )
        {
            CambioPanel();
            return;
        }
        //info de los cuadros
        if (moverValidador == false)
        {
            if (            
                CrewildsResguardo[NumPos] != null
                )
                Datos(CrewildsResguardo[NumPos]);
            else
                infoNull();
        }
        // movimientos del selector
        x = (int)Input.GetAxis("Horizontal");
        y = (int)Input.GetAxis("Vertical");

        posSelector();
        PrioridadMov();
        MOV();
    }
    void AuxSalida()
    {
        SeleccionarGuardar = false;
        infoNull();
    }
    public void condicionesInicialesGuardar()
    {
        objCriaturasEquipadas.gameObject.SetActive(true);
        DefineListaASacar();
    }

    void SacarCriaturaEncaja()
    {
        CrewildBase AuxCriatura = null, AuxCriatura2 = null;

        AuxCriatura = CrewildsResguardo[NumPos];
        AuxCriatura2 = LibreriaS.informacionCrewild.CrewillInstancia[selectorSacarCriatura];

        CrewildsResguardo[NumPos] = AuxCriatura2;
        LibreriaS.informacionCrewild.CrewillInstancia[selectorSacarCriatura] = AuxCriatura;

        DefineListaASacar();

        if (CrewildsResguardo[NumPos] != null)
        {
            imagenes[NumPos].enabled = true;
            imagenes[NumPos].sprite = CrewildsResguardo[NumPos].SpriteCrewildmenuSelec[0];
        }
        if (CrewildsResguardo[NumPos] == null)
        {
            imagenes[NumPos].sprite = null;
            imagenes[NumPos].enabled = false;
        }


    }




    void DefineListaASacar()
    {
        for (int i = 0; i < 6; i++)
        {
            if (LibreriaS.informacionCrewild.CrewillInstancia[i] != null)
            {
                ImagenesCriaturasASacar[i].enabled = true;
                ImagenesCriaturasASacar[i].sprite = LibreriaS.informacionCrewild.CrewillInstancia[i].animaCrewildFrentre[0];
                TextoLvl[i].enabled = true;
                TextoLvl[i].text = "lvl: " + LibreriaS.informacionCrewild.CrewillInstancia[i].Nivel;
            }
            else if (LibreriaS.informacionCrewild.CrewillInstancia[i] == null)
            {
                ImagenesCriaturasASacar[i].sprite = null;
                TextoLvl[i].text = "lvl: ";
                ImagenesCriaturasASacar[i].enabled = false;
                TextoLvl[i].enabled = false;
            }

        }
    }

    void MovSelectorSacarCriatura()
    {
        if (Input.GetKeyDown(KeyCode.A))
            selectorSacarCriatura--;

        else if (Input.GetKeyDown(KeyCode.D))
            selectorSacarCriatura++;


        if (selectorSacarCriatura > 5)
            selectorSacarCriatura = 0;

        else if (selectorSacarCriatura < 0)
            selectorSacarCriatura = 5;
    }

    void posSelectorSacarCriatura()
    {
        if (selectorSacarCriatura == 0)
            ObjSelectorSacarCriatura.anchoredPosition3D = new Vector2(-333.1f, -2.79f);

        else if (selectorSacarCriatura == 1)
            ObjSelectorSacarCriatura.anchoredPosition3D = new Vector2(-232f, -2.79f);

        else if (selectorSacarCriatura == 2)
            ObjSelectorSacarCriatura.anchoredPosition3D = new Vector2(-130f, -2.79f);

        else if (selectorSacarCriatura == 3)
            ObjSelectorSacarCriatura.anchoredPosition3D = new Vector2(-31f, -2.79f);

        else if (selectorSacarCriatura == 4)
            ObjSelectorSacarCriatura.anchoredPosition3D = new Vector2(71f, -2.79f);

        else if (selectorSacarCriatura == 5)
            ObjSelectorSacarCriatura.anchoredPosition3D = new Vector2(171f, -2.79f);


    }
    void CambioPanel()
    {
        CierraTodosLospaneles();
        reinicionspaletasPosTabla();
        if (NumPanlesActivos == 0)
        {
            PnalesImagenes[1].gameObject.SetActive(true);
            NumTranfTabla[1].anchoredPosition3D = new Vector2(NumTranfTabla[1].anchoredPosition3D.x, 7);
            NumPanlesActivos++;
            NumPos += 72;

        }
        else if (NumPanlesActivos == 1)
        {
            PnalesImagenes[0].gameObject.SetActive(true);
            NumTranfTabla[0].anchoredPosition3D = new Vector2(NumTranfTabla[0].anchoredPosition3D.x, 7);
            NumPanlesActivos--;
            NumPos -= 72;
        }
    }

    void CierraTodosLospaneles()
    {
        PnalesImagenes[0].gameObject.SetActive(false);
        PnalesImagenes[1].gameObject.SetActive(false);
    }

    void reinicionspaletasPosTabla()
    {
        NumTranfTabla[0].anchoredPosition3D = new Vector2(NumTranfTabla[0].anchoredPosition3D.x, -6.4f);
        NumTranfTabla[1].anchoredPosition3D = new Vector2(NumTranfTabla[1].anchoredPosition3D.x, -6.4f);
    }

    void activadorOpciones()
    {
        opcionesObj.SetActive(true);
        navOpciones = true;
    }

    void NavOpciones()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PosSelector--;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            PosSelector++;
        }


        if (PosSelector > 3)
        {
            PosSelector = 0;
        }
        else if (PosSelector < 0)
        {
            PosSelector = 3;
        }
    }

    void PosSelectorOpciones()
    {
        if (PosSelector == 0)
        {
            SelectorOpciones.anchoredPosition3D = new Vector2(0.31f, 44.7f);
        }
        else if (PosSelector == 1)
        {
            SelectorOpciones.anchoredPosition3D = new Vector2(0.31f, 16.9f);
        }
        else if (PosSelector == 2)
        {
            SelectorOpciones.anchoredPosition3D = new Vector2(0.31f, -10.9f);
        }
        else if (PosSelector == 3)
        {
            SelectorOpciones.anchoredPosition3D = new Vector2(0.31f, -38.7f);
        }
    }

    void AccioneOpciones()
    {
        // Sacar de lista 
        if (PosSelector == 0)
        {

            objCriaturasEquipadas.gameObject.SetActive(true);
            DefineListaASacar();
            sacarvalidador = true;
            navOpciones = false;
        }
        //mover
        else if (PosSelector == 1)
        {
            mover();
            moverValidador = true;
        }
        //info
        else if (PosSelector == 2)
        {
            infoCriatura = true;
            navOpciones = false;
            LibreriaS.menuInterface.MenuEstados.enabled = true;
            LibreriaS.informacionCrewild.posPanel = 1;
        }
        //salir
        else if (PosSelector == 3)
        {
            reinicioVariableIciales();
        }

    }

    void mover()
    {
        OriginalPos = NumPos;
        CriaturaAcambiar = CrewildsResguardo[NumPos];
        seleccionanterior = NumPos;

    }


    void NavImagencCambioOpcion()
    {

        imagenes[seleccionanterior].sprite = null;
        imagenes[seleccionanterior].enabled = false;

        if (CrewildsResguardo[seleccionanterior] != null)
        {
            imagenes[seleccionanterior].sprite = CrewildsResguardo[seleccionanterior].SpriteCrewildmenuSelec[0];
            imagenes[seleccionanterior].enabled = true;
        }

        if (CrewildsResguardo[NumPos] != null && pos != Selector.localPosition)
        {
            imagenes[OriginalPos].enabled = true;
            imagenes[OriginalPos].sprite = CrewildsResguardo[NumPos].SpriteCrewildmenuSelec[0];
        }
        if (CrewildsResguardo[NumPos] == null)
        {
            imagenes[OriginalPos].enabled = false;
            imagenes[OriginalPos].sprite = null;
        }

        imagenes[NumPos].sprite = CriaturaAcambiar.SpriteCrewildmenuSelec[0];
        imagenes[NumPos].enabled = true;
        seleccionanterior = NumPos;



    }

    void posicionesSincambios()
    {
        imagenes[OriginalPos].sprite = CrewildsResguardo[OriginalPos].SpriteCrewildmenuSelec[0];
        imagenes[OriginalPos].enabled = true;

        if (CrewildsResguardo[NumPos] != null)
        {
            imagenes[NumPos].sprite = CrewildsResguardo[NumPos].SpriteCrewildmenuSelec[0];
        }
        else
        {
            imagenes[NumPos].sprite = null;
            imagenes[NumPos].enabled = false;
        }
    }

    void cambioPosiciones()
    {
        CrewildsResguardo[OriginalPos] = CrewildsResguardo[NumPos];
        CrewildsResguardo[NumPos] = CriaturaAcambiar;
    }

    void reinicioVariableIciales()
    {
        navOpciones = false;
        moverValidador = false;
        opcionesObj.SetActive(false);
        PosSelector = 0;
        SelectorOpciones.anchoredPosition3D = new Vector2(0.31f, 44.7f);

        infoCriatura = false;
        LibreriaS.menuInterface.MenuEstados.enabled = false;
        LibreriaS.informacionCrewild.cerraPaneles();
        LibreriaS.informacionCrewild.posPanel = 1;

        sacarvalidador = false;
        objCriaturasEquipadas.gameObject.SetActive(false);
        selectorSacarCriatura = 0;
    }

    public void condionesInicialesSalida()
    {


        NumPanlesActivos = 1;
        CambioPanel();
        Selector.anchoredPosition3D = posicionInicial;
        pos = posicionInicial;
        NumPos = 0;
        selectorSacarCriatura = 0;
        posSelectorSacarCriatura();
    }

    void posSelector()
    {

        Selector.localPosition = Vector3.MoveTowards(Selector.localPosition, pos, 3 * speed * Time.deltaTime);    // Move there
    }



    void PrioridadMov()
    {
        if (
 Input.GetKeyUp(KeyCode.A)
|| Input.GetKeyUp(KeyCode.W)
|| Input.GetKeyUp(KeyCode.S)
|| Input.GetKeyUp(KeyCode.D)

      )
        {

            prioridad = 0;
        }

        if (prioridad == 0 && x != 0)
        {
            prioridad = 1;

        }
        else if (prioridad == 0 && y != 0)
        {
            prioridad = 2;
        }

        else if (x == 0 && y == 0)
        {
            prioridad = 0;
        }



        if (prioridad == 1 && x != 0)
        {
            if (y != 0)
            {

                x = 0;

            }
        }

        else if (prioridad == 2 && y != 0)
        {
            if (x != 0)
            {
                y = 0;
            }
        }
    }

    void MOV()
    {
        if (Selector.localPosition == pos)
        {
            if (pos.x > -276.2099f)
            {
                // derecha
                if (x < -0.2f && y == 0)
                {
                    pos += Vector3.left * (DistanciaRecorrida - 20);// Add -1 to pos.x

                    NumPos -= 1;
                }
            }

            if (pos.x < 273.7901f)
            {
                // isquierda
                if (x > 0.2f && y == 0)
                {
                    pos += Vector3.right * (DistanciaRecorrida - 20);// Add -1 to pos.x
                    NumPos += 1;
                }
            }

            if (pos.y < 170.8f)
            {
                // arriba
                if (x == 0 && y > 0.2f)
                {
                    pos += Vector3.up * DistanciaRecorrida;// Add -1 to pos.x
                    NumPos -= 12;
                }
            }
            if (pos.y > -179.2f)
            {
                // abajo
                if (x == 0 && y < -0.2f)
                {
                    pos += Vector3.down * DistanciaRecorrida;// Add -1 to pos.x
                    NumPos += 12;
                }
            }
        }


    }

    void Datos(CrewildBase crewild)
    {
        NombreText.text = "Nombre:\n" +
        crewild.NombreTaxonomico;

        apodo.text = "Apodo:\n" +
       crewild.Nombre;

        nivel.text = "nivel_\n" +
        crewild.Nivel;

        ImgCrewild.sprite = crewild.animaCrewildFrentre[0];
        ImgCrewild.enabled = true;
    }

    void infoNull()
    {
        NombreText.text = "Nombre:";

        apodo.text = "Apodo:";

        nivel.text = "nivel:";

        ImgCrewild.enabled = false;
    }


    /// <summary>
    /// Añade criaturas en orden asencendente
    /// </summary>
    /// <param name="cbs"></param>
    public void AñadirCriatura(CrewildBase cbs)
    {


        for (int i = 0; i < 144; i++)
        {
          
            if (CrewildsResguardo[i] == null)
            {
                CrewildsResguardo[i] = cbs;
                imagenes[i].sprite = CrewildsResguardo[i].SpriteCrewildmenuSelec[0];
                imagenes[i].enabled = true;
                break;
            }
        }



    }

    /// <summary>
    /// añade criaturas en una posicion especifica
    /// </summary>
    /// <param name="cbs"></param>
    /// <param name="PosEspecifica"></param>
    public void AñadirCriatura(CrewildBase cbs, int PosEspecifica)
    {
       
        if (CrewildsResguardo[PosEspecifica] == null)
        {
            CrewildsResguardo[PosEspecifica] = cbs;
            imagenes[PosEspecifica].sprite = CrewildsResguardo[PosEspecifica].SpriteCrewildmenuSelec[0];
            imagenes[PosEspecifica].enabled = true;

        }
    }
}



