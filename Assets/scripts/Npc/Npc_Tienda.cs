using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Tienda : MonoBehaviour
{
    public bool desactivador;
    public NPC_Dialogo Dialogo;

    public Canvas CanvasTienda;

    public bool DisparadorEvento;

    /// <summary>
    /// Array de datos los diaogos que se van a usar
    /// </summary>
    public  LineasDeDialogo Dialogos;

    public CajaInventario[] ItensAVender;

    // Start is called before the first frame update
    void Start()
    {
        CanvasTienda = GameObject.Find("TiendaC").GetComponent<Canvas>();
        Dialogo = new NPC_Dialogo();
        Dialogo.Inicializacion();
        Dialogos.Mensaje = "Dime que quieres conseguir";

    }

    // Update is called once per frame
    void Update()
    {
        if (desactivador == true)
        {
            return;
        }

        if (DisparadorEvento == true)
        {

            //ativa canvas
            if (Dialogo.LectorDetexto.LeyendoTexto == true && Dialogo.ValidadorCanvastext == false)
            {
                Dialogo.Canvastext.enabled = true;
                Dialogo.ValidadorCanvastext = true;
                Dialogo.BoxSelecction.SetActive(false);
            }
          
            if (Dialogo.LectorDetexto.LeyendoTexto == false)
            {
            


                //puntero de espera                
                StartCoroutine(Dialogo.Puntero_.Parpaderopuntero(Dialogo.LectorDetexto.LeyendoTexto));

                // Dialogo.EntraATextoSimple(Dialogos.Mensaje);
                EntraATextoSimple(Dialogos.Mensaje);
            }

        }


    }


    /// <summary>
    /// entrada de texto Simplificada
    /// </summary>
    public void EntraATextoSimple(string mensaje)
    {


        if (Dialogo.PrimeraEntrada == true || Input.GetKeyDown(KeyCode.Space))
        {

            if (Dialogo.SegundaEntrada == true)
            {
                ActivadorTienda();
                return;
            }
            Dialogo.SegundaEntrada = true;

            Dialogo.TextoDialogo.text = "";
            print(mensaje);
            StartCoroutine(Dialogo.LectorDetexto.LecturaTexto(mensaje, Dialogo.TextoDialogo, Dialogo.TimepoDePausa));


            Dialogo.PrimeraEntrada = false;
            Dialogo.Contador++;



        }
    }

    void ActivadorTienda()
    {
        Dialogo.TextoDialogo.text = "";

        Dialogo.Canvastext.enabled = false;
        Dialogo.ValidadorCanvastext = false;
        Dialogo.BoxSelecction.SetActive(false);

        Dialogo.PrimeraEntrada = true;
        Dialogo.SegundaEntrada = false;

        CanvasTienda.enabled = true;

        DisparadorEvento = false;

        Invoke("invokeTiendaScrip", 0.1f);
        print("Entra en Tienda");
    }

    void   invokeTiendaScrip()
    {
        FindObjectOfType<TiendaScript>().Encapsulatablas(ItensAVender);
        FindObjectOfType<TiendaScript>().disparador = true;
        FindObjectOfType<TiendaScript>();
    }
}
