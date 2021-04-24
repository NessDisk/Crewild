using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiendaScript : MonoBehaviour
{
    public bool Desactivador, disparador, pruebas;



    public CajaInventario[] ItensAVenderTestin;

    public ListdeInventario listdeInventario;

    public Inventario ClaseInventario;

    Canvas CanvasTienda;

    public Navegacion HubNavegacion;

    bool SeleccionActiva, AceptarCompra, pausa;

    public RectTransform CantidadPanel, eleccionPanel, SelectorCantidad, selectorSioNo;

    public Text DecimalText, UnidadText, ValorText, DescripcionText, SaldoText;

    public int PosSelectorCantida, CantidadDecimal,CantidadUnidad, PosSelectorfinal;

    public libreriaDeScrips LibreriaS;

    // Start is called before the first frame update
    void Start()
    {
        CanvasTienda = GameObject.Find("TiendaC").GetComponent<Canvas>();

        ClaseInventario = new Inventario();

        listdeInventario = new ListdeInventario();

        RectTransform Paneltranf = GameObject.Find("TiendaC/tienda/PanelItens/Mascara/Tabla").GetComponent<RectTransform>();

        RectTransform PosicionReferncia = GameObject.Find("TiendaC/tienda/PanelItens/Mascara/Tabla/referencia").GetComponent<RectTransform>();

       
        RectTransform corcheteTranfor = GameObject.Find("TiendaC/tienda/PanelItens/Mascara/Tabla/Selector").GetComponent<RectTransform>();
       
        //Navegacion

        HubNavegacion = new Navegacion(corcheteTranfor, Paneltranf);

        CantidadPanel = GameObject.Find("TiendaC/tienda/cantidad").GetComponent<RectTransform>() ;

        eleccionPanel = GameObject.Find("TiendaC/tienda/Eleccion").GetComponent<RectTransform>();



        Invoke("invokeEncapsularObj", 1f);

        DecimalText = GameObject.Find("TiendaC/tienda/cantidad/Decimal").GetComponent<Text>();
        UnidadText = GameObject.Find("TiendaC/tienda/cantidad/Unidades").GetComponent<Text>();
        ValorText = GameObject.Find("TiendaC/tienda/cantidad/Valor").GetComponent<Text>();
        DescripcionText = GameObject.Find("TiendaC/tienda/descripcion/Texto").GetComponent<Text>();
        SaldoText = GameObject.Find("TiendaC/tienda/PanelItens/Saldo").GetComponent<Text>();
        SelectorCantidad = GameObject.Find("TiendaC/tienda/cantidad/selector").GetComponent<RectTransform>();


        selectorSioNo = GameObject.Find("TiendaC/tienda/Eleccion/selector").GetComponent<RectTransform>();

        LibreriaS = FindObjectOfType<libreriaDeScrips>();


        // test
        if (pruebas == true)
        {
            listdeInventario = ClaseInventario.DefineListTienda(ItensAVenderTestin, Paneltranf, PosicionReferncia);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Desactivador == true)
        {
            return;
        }

        if (disparador == true && pausa == false)
        {

            saldo();
            retroceder();
            if (AceptarCompra == true)
            {
                NavCosteSioNo();
                SelectorSioNo();
            }

            else if (SeleccionActiva == true)
            {
                // cantidad x coste
                NavCosteCantidad();
                CantidadAComprar();
               
            }
           else if (SeleccionActiva == false)
            {
                SelectorAccion();
                Navegador();
            }
        }
        
    }

   void retroceder()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Retroceder aceptar compra
            if (AceptarCompra == true)
            {
                AceptarCompra = false;
                SeleccionActiva = false;
                eleccionPanel.gameObject.SetActive(false);
            }
            //retroceder
                else if (SeleccionActiva == true)
                {
                SeleccionActiva = false;
                CantidadPanel.gameObject.SetActive(false);
            }
            ///salir tienda
                else if (SeleccionActiva == false)
                {
                Invoke("SalirTienda",0.1f);
            }

            CantidadUnidad = 0;
            CantidadDecimal = 0;

            LibreriaS.audioMenus.Audio.Play();
        }
    }


    void SalirTienda()
    {
        SeleccionActiva = false;
        CantidadPanel.gameObject.SetActive(false);
        SalirCanvas();
        HubNavegacion.ReiniciarValores();
    }

    void SalirCanvas()
    {
        GameObject.Find("TiendaC").GetComponent<Canvas>().enabled = false;
        FindObjectOfType<movimiento>().DisparadorEvento = false;
        disparador = false;
        limpiarListaItens();
    }

    void limpiarListaItens()
    {
        foreach (BaseItem BI in listdeInventario.Item)
        {
            DestroyImmediate(BI.ObjPrefac); 
        }
        listdeInventario.Item.Clear();
    }

    /// <summary>
    /// llama y organiza los objetos en canvas de la tienda.
    /// </summary>
    /// <param name="CajaItens"></param>
  public  void Encapsulatablas(CajaInventario[] CajaItens)
    {
        RectTransform Paneltranf = GameObject.Find("TiendaC/tienda/PanelItens/Mascara/Tabla").GetComponent<RectTransform>();

        RectTransform PosicionReferncia = GameObject.Find("TiendaC/tienda/PanelItens/Mascara/Tabla/referencia").GetComponent<RectTransform>();

        listdeInventario = ClaseInventario.DefineListTienda(CajaItens, Paneltranf, PosicionReferncia);

    }
    void saldo()
    {
      SaldoText.text = "Saldo: $"+FindObjectOfType<Inventario>().Dinero;
    }

    /// <summary>
    /// navega en el primer nivel de seleccion de itens
    /// </summary>
    void Navegador()
    {        
        // al contador se le restan -1 para que el array quede ajustado.
        HubNavegacion.MovimientoDinamico(listdeInventario.Item.Count - 1);

    }

    /// <summary>
    /// selecciona la accion en el panel de movimiento
    /// </summary>
    void SelectorAccion()
    {
        DescripcionText.text = listdeInventario.Item[HubNavegacion.LimiteDemovimientos].descripcionItem;

        if (Input.GetKeyDown(KeyCode.Space) && SeleccionActiva == false)
        {
            CantidadPanel.gameObject.SetActive(true);

            RectTransform AuxTransfor = listdeInventario.Item[HubNavegacion.LimiteDemovimientos].ObjPrefac.GetComponent<RectTransform>();

           

            CantidadPanel.anchoredPosition = AuxTransfor.anchoredPosition;
            CantidadPanel.anchoredPosition += new Vector2(20, 30);


            LibreriaS.audioMenus.Audio.Play();

            SeleccionActiva = true;
        }     

    }

    void NavCosteCantidad()
    {
        if (PosSelectorCantida > 1)
        {
            PosSelectorCantida = 0;
        }

        else if (PosSelectorCantida < 0)
        {
            PosSelectorCantida = 1;
        }

        if (PosSelectorCantida == 0)
        {
            SelectorCantidad.localPosition = new Vector2(26f, -0.21f);
        }
        else if (PosSelectorCantida == 1)
        {
            SelectorCantidad.localPosition = new Vector2(-26.79f, -0.21f);
        }



        if (Input.GetKeyDown(KeyCode.A))
        {
            PosSelectorCantida--;

            LibreriaS.audioMenus.Audio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            PosSelectorCantida++;

            LibreriaS.audioMenus.Audio.Play();
        }     
        
    }


   public void NavCosteSioNo()
    {
        if (PosSelectorfinal > 1)
        {
            PosSelectorfinal = 0;
        }

        else if (PosSelectorfinal < 0)
        {
            PosSelectorfinal = 1;
        }

        if (PosSelectorfinal == 0)
        {
            selectorSioNo.localPosition = new Vector2(24.4f, -0.21f);
        }
        else if (PosSelectorfinal == 1)
        {
            selectorSioNo.localPosition = new Vector2(-24.9f, -0.21f);
        }



        if (Input.GetKeyDown(KeyCode.A))
        {
            PosSelectorfinal--;

            LibreriaS.audioMenus.Audio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            PosSelectorfinal++;


            LibreriaS.audioMenus.Audio.Play();
        }

    }
    void CantidadAComprar()
    {
        DescripcionText.text = "Selecciona cantidad a comprar";

        UnidadText.text = CantidadUnidad + "";
        DecimalText.text = CantidadDecimal + "";
        int CantidadAcomprar = (CantidadDecimal * 10) + CantidadUnidad;
        int CosteTotal = CantidadAcomprar * (int)listdeInventario.Item[HubNavegacion.LimiteDemovimientos].Coste;

        ValorText.text = "$" + CosteTotal;

        //aceptar la compra
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CantidadAcomprar == 0)
            {         
                StartCoroutine(SeleccionaAlmenos1Iten());
            }
            //mesnaje de no se puede Hacer compra
            else if (FindObjectOfType<Inventario>().Dinero < CosteTotal)
            {
                StartCoroutine(NosePudoHacerCompra());

            }
            //continua compra
            else
            {
                eleccionPanel.gameObject.SetActive(true);
                CantidadPanel.gameObject.SetActive(false);
                AceptarCompra = true;
                return;
            }


            LibreriaS.audioMenus.Audio.Play();

        }
        
       //incremento Cantidad unidad
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (PosSelectorCantida == 0)
            {
                CantidadUnidad++;
            }

            else if (PosSelectorCantida == 1)
            {
                CantidadDecimal++;

            }
            LibreriaS.audioMenus.Audio.Play();
        }
        //Decremento Cantidad unidad
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (PosSelectorCantida == 0)
            {
                CantidadUnidad--;
            }

            else if (PosSelectorCantida == 1)
            {
                CantidadDecimal--;

            }
            LibreriaS.audioMenus.Audio.Play();
        }


        if (CantidadUnidad > 9)
        {
            CantidadUnidad = 0;

        }
        else if (CantidadUnidad < 0)
        {
            CantidadUnidad = 9;
        }

        if (CantidadDecimal > 9)
        {
            CantidadDecimal = 0;

        }
        else if (CantidadDecimal < 0)
        {
            CantidadDecimal = 9;
        }


       
    }

    void SelectorSioNo()
    {
        int CantidadAComprar = (CantidadDecimal * 10) + CantidadUnidad;
        int CostoTotal = CantidadAComprar * (int)listdeInventario.Item[HubNavegacion.LimiteDemovimientos].Coste;
        DescripcionText.text = "Estas Seguro que deseas comprar " + CantidadAComprar + " " + listdeInventario.Item[HubNavegacion.LimiteDemovimientos].Nombre +
                               " por valor de " + CostoTotal;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //No
            if (PosSelectorfinal == 0)
            {
                // por defecto devuelve  al menu inicial
            }
            //Si
            else if (PosSelectorfinal == 1)
            {
                // CajaInventario itenAComprar = ItensAVenderTestin[HubNavegacion.LimiteDemovimientos];
                CajaInventario itenAComprar = new CajaInventario();
                itenAComprar.NombreItem = listdeInventario.Item[HubNavegacion.LimiteDemovimientos].Nombre;
                itenAComprar.Cantidad = CantidadAComprar;
                print(itenAComprar.NombreItem);
                FindObjectOfType<Inventario>().DefineList(itenAComprar);
                FindObjectOfType<Inventario>().Dinero -= CostoTotal;
            }
                                 

            eleccionPanel.gameObject.SetActive(false);
            AceptarCompra = false;
            SeleccionActiva = false;
            CantidadUnidad = 0;
            CantidadDecimal = 0;

            LibreriaS.audioMenus.Audio.Play();
        }
        
    }



    /// <summary>
    /// Invoke del panel  de seleccion
    /// </summary>
    void invokeEncapsularObj()
    {
         SeleccionActiva = false;
         AceptarCompra = false;

        CantidadPanel.gameObject.SetActive(false);
        eleccionPanel.gameObject.SetActive(false);
    }

    IEnumerator NosePudoHacerCompra()
    {
        DescripcionText.text = "No tienes suficiente dinero para realizar esta compra.";
        pausa = true;
        yield return new WaitForSeconds(2f);
        pausa = false;
       
    }

    IEnumerator SeleccionaAlmenos1Iten()
    {
        DescripcionText.text = "Seleccion almenos 1 iten para poder continuar con la compra.";
        pausa = true;
        yield return new WaitForSeconds(2f);
        pausa = false;

    }
}
