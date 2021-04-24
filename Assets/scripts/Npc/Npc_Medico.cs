using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Medico : MonoBehaviour
{
    public bool desactivador, DisparadorEventos;
    public NPC_Dialogo Dialogo;

    Animation AnimationRecupera;

    /// <summary>
    /// Array de datos los diaogos que se van a usar
    /// </summary>
    LineasDeDialogo[] Dialogos;

    bool SeleccionaAccion , Respuesta, Ejecutaproceso, salir;


    SpriteRenderer[] SpriteRender = new SpriteRenderer[6];

    string NombreAnimacion;

    libreriaDeScrips LibreriaS;


    // Start is called before the first frame update
    void Start()
    {
        Dialogo = new NPC_Dialogo();
        Dialogo.Inicializacion();

        Dialogos = new LineasDeDialogo[5];

        Dialogos[0].Mensaje = "Bienvenido al centro medico";
        Dialogos[1].Mensaje = "Deseas que atendamos a Tus criaturas";
        Dialogos[2].Mensaje = "Dame tus Crewild, en un momento iniciamos el proceso";
        Dialogos[3].Mensaje = "Pues volver cuando quieras";

        LibreriaS = FindObjectOfType<libreriaDeScrips>();

        AnimationRecupera = GetComponent<Animation>();

    

SpriteRenderer[] AuxRender =   GetComponentsInChildren<SpriteRenderer>();
        int auxCounter =  0;
        foreach(SpriteRenderer SPR in AuxRender)
        {
            if (SPR.name ==  "capsula"
              || SPR.name == "capsula (1)"
              || SPR.name == "capsula (2)"
              || SPR.name == "capsula (3)"
              || SPR.name == "capsula (4)"
              || SPR.name == "capsula (5)"
              )
            {
                SpriteRender[auxCounter] = SPR;
                SpriteRender[auxCounter].enabled = false;
                auxCounter++;
            }

        }

        NombreAnimacion = "Recuperacion";
    }

    // Update is called once per frame
    void Update()
    {
        if (desactivador == true)
        {
            return;
        }

        if (DisparadorEventos == true)
        {
            //ativa canvas
            if (Dialogo.LectorDetexto.LeyendoTexto == true && Dialogo.ValidadorCanvastext == false )
            {
                Dialogo.Canvastext.enabled = true;
                Dialogo.ValidadorCanvastext = true;
                Dialogo.BoxSelecction.SetActive(false);
            }

            //activacanvas
            if (SeleccionaAccion == true && Dialogo.LectorDetexto.LeyendoTexto == false && Dialogo.BoxSelecction.activeSelf == false && Respuesta == false && salir == false)
            {
                Dialogo.BoxSelecction.SetActive(true);
            }
            else if (Dialogo.BoxSelecction.activeSelf == true && Dialogo.LectorDetexto.LeyendoTexto == false)
            {
                Dialogo.SeleccionaAccion();
                ElegirRespuesta();
               
            }
                                            

           



            if (Dialogo.LectorDetexto.LeyendoTexto == false && SeleccionaAccion == false)
            {

                //puntero de espera                
                StartCoroutine(Dialogo.Puntero_.Parpaderopuntero(Dialogo.LectorDetexto.LeyendoTexto));

                // Dialogo.EntraATextoSimple(Dialogos.Mensaje);
                EntraATextoSimple(Dialogos[Dialogo.Contador].Mensaje);
            }

            
        }
        
    }

    public void EntraATextoSimple(string mensaje)
    {


        if (Dialogo.PrimeraEntrada == true || Input.GetKeyDown(KeyCode.Space))
        {
            
            if (Ejecutaproceso == true)
            {
                ejecutaProceso();
                return;
            }

            else if (Ejecutaproceso == false && Dialogo.Contador == 4)
            {
                Invoke("SalirProceso", 0.2f);
                return;
            }

            
            else  if (Dialogo.SegundaEntrada == true )
            {
                SeleccionaAccion = true;

            }           
           

            Dialogo.TextoDialogo.text = "";
            StartCoroutine(Dialogo.LectorDetexto.LecturaTexto(mensaje, Dialogo.TextoDialogo, Dialogo.TimepoDePausa));


            if (Respuesta == false && salir == false)
            {
                Dialogo.SegundaEntrada = true;
                Dialogo.PrimeraEntrada = false;
                Dialogo.Contador++;
            }
            else if (Respuesta == true)
            {
                Dialogo.SegundaEntrada = false;
                Ejecutaproceso = true;
                SeleccionaAccion = false;
            }

            else if (salir == true)
            {
                Dialogo.Contador = 4;
                SeleccionaAccion = false;
            }



        }


    }

    void ElegirRespuesta()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Si
            if (Dialogo.PosicionNumCorchete == 0)
            {
                // continua  el procedimiento
                Respuesta = true;                
                Dialogo.Contador = 2;
            }
            //No
            else if (Dialogo.PosicionNumCorchete == 1)
            {
                //sale del procedimiento
                salir = true;
                Dialogo.Contador = 3;
            }
            SeleccionaAccion = false;
            Dialogo.BoxSelecction.SetActive(false);
            LibreriaS.audioMenus.Audio.Play();
        }
       
    }

    void ejecutaProceso()
    {
        print("Ejecuta proceso");
        AnimationRecupera.Play(NombreAnimacion);
        Dialogo.Canvastext.enabled = false;
        Ejecutaproceso = false;
        DisparadorEventos = false;
    }

    void SalirProceso()
    {
        print("SalirProceso proceso");

        Dialogo.ValidadorCanvastext = false;
        Dialogo.Canvastext.enabled = false;        
        Dialogo.Contador = 0;
        Dialogo.PrimeraEntrada = true;
        Dialogo.SegundaEntrada = false;
        SeleccionaAccion = false;
        salir = false;
        Respuesta = false;
        Ejecutaproceso = false;
        DisparadorEventos = false;
        Dialogo.TextoDialogo.text = "";
        Dialogo.PosicionNumCorchete = 0;
        Dialogo.SeleccionaAccion();
        FindObjectOfType<movimiento>().DisparadorEvento = false;

    }

    public void Despedida()
    {
        salir = true;

        Dialogo.TextoDialogo.text = "";
        Dialogo.Canvastext.enabled = true;
        string mensaje = "Vuelve cuando quieras";
        StartCoroutine(Dialogo.LectorDetexto.LecturaTexto(mensaje, Dialogo.TextoDialogo, Dialogo.TimepoDePausa));
        DisparadorEventos = true;
        Dialogo.Contador = 4;
        SeleccionaAccion = false;

        for (int i  = 0; i < 6;  i++)
        {
            SpriteRender[i].enabled = false;
        }
       

    }

    public void ActivaImagene()
    {
        StartCoroutine(ActivaCapsulas());

    }

    public IEnumerator ActivaCapsulas()
    {
    /*    int auxCantidadDeCriaturas = 0;
        foreach (CrewildBase Bwl in FindObjectOfType<informacionCrewild>().CrewillInstancia)
        {
            if (Bwl != null)
            {
                auxCantidadDeCriaturas++;
            }

        }*/


        for (int i = 0; i < 5 ; i++)
        {
            if (FindObjectOfType<informacionCrewild>().CrewillInstancia[i] != null)
            {
                SpriteRender[i].enabled = true;

                FindObjectOfType<informacionCrewild>().CrewillInstancia[i].Hp = FindObjectOfType<informacionCrewild>().CrewillInstancia[i].hpTotal;
                FindObjectOfType<informacionCrewild>().CrewillInstancia[i].Cansancio = FindObjectOfType<informacionCrewild>().CrewillInstancia[i].Cansanciototal;
                FindObjectOfType<informacionCrewild>().CrewillInstancia[i].EstadosCrewild = EstadosEnum.None;
                for (int e = 0; e < 4; e++)
                {
                    if (FindObjectOfType<informacionCrewild>().CrewillInstancia[i].ataques[e] != null)
                    {
                        FindObjectOfType<informacionCrewild>().CrewillInstancia[i].ataques[e].cantidadDeusos = FindObjectOfType<informacionCrewild>().CrewillInstancia[i].ataques[e].cantidadDeusosTotales;
                    }

                }
            }
           

            print("AActiva capsulas");

            yield return new WaitForSeconds(0.5f);

        }

        yield return new WaitForSeconds(1);
        AnimationRecupera[NombreAnimacion].speed = 1;
        print("Pista");
    }
}

