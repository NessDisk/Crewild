using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC_Dialogo : MonoBehaviour

{

    public bool Desactivador;


    /// <summary>
    /// cuando el jugador detecta al Npc con el que quiere interactuar  activa este disparador
    /// </summary>
    public bool DisparadorDialogo = false;


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
    public float TimepoDePausa = 0.01f;



    public float speedMoveText = 1f;

    public float distance = 1f;


    // Canvas

    public Canvas Canvastext;

    /// <summary>
    ///  activa en un ciclo el canvas y cierra la segunda extrada hasta que se  reinicia
    /// </summary>
    public bool ValidadorCanvastext = false;

    public Text TextoDialogo;

    /// <summary>
    /// Array de datos los diaogos que se van a usar
    /// </summary>
    public LineasDeDialogo[] Dialogos;

    /// <summary>
    ///  Es lector de texto
    /// </summary>
    public dialogueclassNPC LectorDetexto;

    /// <summary>
    /// define el numero del ciclo al cual  el dialogo esta
    /// </summary>
    public int Contador;

    /// <summary>
    /// gameobjet del cuadro de seleccion de accion
    /// </summary>
    public GameObject BoxSelecction, ObJCuadroTexto;

    /// <summary>
    /// recoge el compnente imagen del obj Puntero el canvas
    /// </summary>
    Image punteroImagen;

    /// <summary>
    /// valida la entrada al metodo puntero
    /// </summary>
    bool ValidadorPuntero;

    /// <summary>
    /// se desactiva depues de la primera entrada y se  reactiva al reiniciar el contador
    /// </summary>
    public bool PrimeraEntrada = true, SegundaEntrada;

    /// <summary>
    /// tranfor de el corchete de seleccion de acccion
    /// </summary>
    public RectTransform Corchete;

    /// <summary>
    /// posicion en int de corchete: 0= YES , 1 = NO 
    /// </summary>
    public int PosicionNumCorchete;

    /// <summary>
    /// cuando se activa el metodo compormiento se ejecuta y permite leer el texto una primera ves antes de ejecutar el comportamiento
    /// </summary>
    bool PrimeraLecturaComportamiento;

    movimiento MovPlayer;
    public libreriaDeScrips LibreriaS;

    public Puntero Puntero_;

  
    // Start is called before the first frame update
    void Start()
    {
        LibreriaS = FindObjectOfType<libreriaDeScrips>() ;


        if (Corchete == null)
        {
            if (GameObject.Find("Canvas/box Election/Corchete") == null)
            {
                Corchete = FindObjectOfType<NPC_Dialogo>().Corchete;
            }
            else
                Corchete = GameObject.Find("Canvas/box Election/Corchete").GetComponent<RectTransform>();
        }



        TextoDialogo = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();
        punteroImagen = GameObject.Find("Canvas/box Texto/mask/Puntero").GetComponent<Image>();
        Canvastext = GameObject.Find("Canvas/").GetComponent<Canvas>();
        LectorDetexto = new dialogueclassNPC();
        //  punteroImagen.enabled = false;
        MovPlayer = GameObject.FindObjectOfType<movimiento>();
        Puntero_ = new Puntero();
        BoxSelecction = GameObject.Find("/Canvas/box Election");
    }

    public void Inicializacion()
    {
        LibreriaS = FindObjectOfType<libreriaDeScrips>();

        if (Corchete == null)
        {
            if (GameObject.Find("Canvas/box Election/Corchete") == null)
            {
                Corchete = FindObjectOfType<NPC_Dialogo>().Corchete;
            }
            else
                Corchete = GameObject.Find("Canvas/box Election/Corchete").GetComponent<RectTransform>();
        }

        TextoDialogo = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();
       
        punteroImagen = GameObject.Find("Canvas/box Texto/mask/Puntero").GetComponent<Image>();
        Canvastext = GameObject.Find("Canvas/").GetComponent<Canvas>();
        LectorDetexto = new dialogueclassNPC();
    //  punteroImagen.enabled = false;
        MovPlayer = GameObject.FindObjectOfType<movimiento>();
        Puntero_ = new Puntero();
        BoxSelecction = GameObject.Find("/Canvas/box Election");
        ObJCuadroTexto = GameObject.Find("/Canvas/box Texto");
        


    }


    // Update is called once per frame
    void Update()
    {
        if (Desactivador == true)
        {
            return;
        }

        if (DisparadorDialogo == true)
        {
            //ativa canvas
            if (LectorDetexto.LeyendoTexto == true && ValidadorCanvastext == false)
            {
                Canvastext.enabled = true;
                ValidadorCanvastext = true;
            }

            //activacadro de seleccion
            if (Contador != 0)
            {
                ActivadorCuadroDeSeleccion();
            }
            //define cual es la accion a seleccionar
            if (BoxSelecction == null )
            {
                BoxSelecction = FindObjectOfType<menus_interface>().BoxSelecction;
                
                if (BoxSelecction.activeSelf == true)
                     SeleccionaAccion();               
            }
            else if (BoxSelecction.activeSelf == true)
                     SeleccionaAccion();

            //lectura de mensaje
            if (LectorDetexto.LeyendoTexto == false)
            {
                //puntero de espera                
                StartCoroutine(Puntero_.Parpaderopuntero(LectorDetexto.LeyendoTexto));

                EntraATexto();
                
                
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

                //selecciona una respuesta y genera el resultado de la consulta
                if (Dialogos[Contador - 1].seleccion.EligeAccion == true)
                {
                    retornaRespuesta();
                }

                 if (Dialogos[Contador - 1].Comportamiento != ComportamientoEnrespuesta.nulo && PrimeraLecturaComportamiento == false && PrimeraEntrada == false)
                {
                    Comportamientos();
                    return;
                }
                //Reinicia recorrido
                else if (Contador == Dialogos.Length)
                {
                    reinicioArray();
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

            

            PrimeraLecturaComportamiento = false;
            PrimeraEntrada = false;
            Contador++;



        }
    }

    /// <summary>
    /// cuando esta activo hace que el puntero este intermitente
    /// </summary>
    /// <returns></returns>
    public IEnumerator Parpaderopuntero()
    {
        while (LectorDetexto.LeyendoTexto == false)
        {
            punteroImagen.enabled = false;
            yield return new WaitForSeconds(1f);
            punteroImagen.enabled = true;
            yield return new WaitForSeconds(1f);
        }
        punteroImagen.enabled = false;
        ValidadorPuntero = false;
        yield return 0;
    }

    /// <summary>
    /// Marcador que  parpadea cuando el texto esta en espera
    /// </summary>
    void puntero()
    {
        if (LectorDetexto.LeyendoTexto == false && ValidadorPuntero == false)
        {
            ValidadorPuntero = true;
            StartCoroutine(Parpaderopuntero());

        }

    }

    /// <summary>
    /// reinicia los valores de los e los objetos relacionados
    /// </summary>
    void Reiniciavalores()
    {
        Canvastext.enabled = false;        
        TextoDialogo.text = "";
        punteroImagen.enabled = false;
        DisparadorDialogo = false;
        MovPlayer.DisparadorEvento = false;
        BoxSelecction.SetActive(false);
        ValidadorCanvastext = false;
        ValidadorPuntero = false;
        PrimeraEntrada = true;


    }



    /// <summary>
    /// se llama para poder apagar algunos metodos
    /// </summary>
    void InvokeAuxiliar()
    {
        BoxSelecction.SetActive(false);
    }

    /// <summary>
    ///  metodo auxiliar para Incrementar a +1 el contador;
    /// </summary>
    void InvokeIncrementaContador()
    {
        Contador++;
    }
    
    /// <summary>
    /// metodo auxiliar para  restar en -1 el contador;
    /// </summary>
    void InvokeRestaContador()
    {
        Contador--;
    }

    /// <summary>
    /// metodo auxiliar para igualar a 0 el contador;
    /// </summary>
    void InvokeContadorACero()
    {
        Contador = 0;
    }

    /// <summary>
    /// activa el cadro de texto cuando el indicador en los dialogos de texto se activa 
    /// </summary>
   public void ActivadorCuadroDeSeleccion()
    {
        if (Dialogos[Contador - 1].seleccion.EligeAccion == true)
        {
            BoxSelecction.SetActive(true);
        }

        else if (Dialogos[Contador - 1].seleccion.EligeAccion == false)
        {
            BoxSelecction.SetActive(false);
        }
    }


    /// <summary>
    /// define el comportamiento en una respuesta de si o no para la seleccion de accion
    /// </summary>
    public  void SeleccionaAccion()
    {
        if (Corchete == null)
        {
            if (GameObject.Find("Canvas/box Election/Corchete") == null)
            {
                Corchete = FindObjectOfType<NPC_Dialogo>().Corchete;
            }
            else
                Corchete = GameObject.Find("Canvas/box Election/Corchete").GetComponent<RectTransform>();
        }
        //transfor  de la posicion
        if (PosicionNumCorchete == 0)
        {
            //YES
            Corchete.localPosition = new Vector2(0, 20);
        }

        else if (PosicionNumCorchete == 1)
        {
            // NO
            Corchete.localPosition = new Vector2(0, -31);
        }

        //seleccion lugar
        if (Input.GetKeyDown(KeyCode.S))
        {
            PosicionNumCorchete++;
            LibreriaS.audioMenus.Audio.Play();
        }
            

        else if (Input.GetKeyDown(KeyCode.W))
        {
            PosicionNumCorchete--;
            LibreriaS.audioMenus.Audio.Play();
        }
            


        // Ajuste en caso de salidas del rango de selecccion
        if (PosicionNumCorchete < 0)
            PosicionNumCorchete = 1;

        else if (PosicionNumCorchete > 1)
            PosicionNumCorchete = 0;
    }


    /// <summary>
    /// Respuesta a la conversacion Actual
    /// </summary>
    void retornaRespuesta()
    {
        if (PosicionNumCorchete == 0)
        {
            Dialogos[Contador - 1].seleccion.Respuesta = true;
            Contador = Dialogos[Contador - 1].seleccion.RespuestaPositiva - 1;
        }
        else if (PosicionNumCorchete == 1)
        {
            Dialogos[Contador - 1].seleccion.Respuesta = false;
            Contador = Dialogos[Contador - 1].seleccion.RespuestaNegativa - 1;
        }

        PrimeraLecturaComportamiento = true;
    }

    /// <summary>
    /// Clase usada para retornar la respuesta a la pregunta hecha
    /// </summary>
    /// <returns></returns>
    public bool RespuestaBool()

    {
      
            bool RepuestaAretornar = false;
            if (PosicionNumCorchete == 0)
            {
                return RepuestaAretornar = true;
            }
            else if (PosicionNumCorchete == 1)
            {
                return RepuestaAretornar = false;
            }

        return RepuestaAretornar;

    }


    /// <summary>
    /// define los comportamientos de los dialgos
    /// </summary>
    void Comportamientos()
    {
        if (Dialogos[Contador - 1].Comportamiento == ComportamientoEnrespuesta.RepetirRespuesta)
        {
            repetirRespuesta();

        }

        else if (Dialogos[Contador - 1].Comportamiento == ComportamientoEnrespuesta.IrAlInicioMenssage)
        {
            reinicioArray();
        }

        else if (Dialogos[Contador - 1].Comportamiento == ComportamientoEnrespuesta.salirDeLectura)
        {

            SalirDeLinea();
        }
    }

    /// <summary>
    /// reinicia los valores del Array
    /// </summary>
    void reinicioArray()
    {
        Invoke("Reiniciavalores", 0.2f);       
        Invoke("InvokeContadorACero", 0.2f);
    }

    /// <summary>
    /// repite respuesta 
    /// </summary>
    void repetirRespuesta()
    {
        Invoke("Reiniciavalores", 0.2f);       
        Invoke("InvokeRestaContador", 0.2f);
       
    }

    /// <summary>
    /// Sale del lectura
    /// </summary>
    void SalirDeLinea()
    {
        Invoke("Reiniciavalores", 0.2f);             
        
    }

    /// <summary>
    /// entra un Iten al Jugador
    /// </summary>
    void darIten()
    {
        Dialogos[Contador].Mensaje = MensajesGlovales.EntregarIten(Dialogos[Contador].Itens.ItenInfo);

        TextoDialogo.text = "";
        StartCoroutine(LectorDetexto.LecturaTexto(Dialogos[Contador].Mensaje, TextoDialogo, TimepoDePausa));
        LibreriaS.inventario.DefineList(Dialogos[Contador].Itens.ItenInfo);
    }
}

    /// <summary>
    /// argupacion de variables para las opciones de dialogo
    /// </summary>
    [System.Serializable]
    public struct LineasDeDialogo
    {
        public string name;

        /// <summary>
        /// barrqaa del dialogo que se esta usando
        /// </summary>
        [TextArea(3, 10)]
        public string Mensaje;



        [System.Serializable]
        public struct seleccionaRespuesta
        {
            /// <summary>
            /// definde si durante un momento del  recorrido del Array hay eleccion de  del Array
            /// </summary>
            public bool EligeAccion;

            /// <summary>
            /// respuesta que se envia  para definir la eleccion del personaje 
            /// </summary>
            public bool Respuesta;

            /// <summary>
            /// salto en el Array en caso de que la respuesta sea  positiva
            /// </summary>
            public int RespuestaPositiva;

            /// <summary>
            /// salto en el Array en caso de que la respuesta sea  Negativa
            /// </summary>
            public int RespuestaNegativa;

        }



        /// <summary>
        /// cuando esta activa permite la seleccion de un valor 
        /// </summary>
        public seleccionaRespuesta seleccion;



    /// <summary>
    /// solo tiene funcionalidades en modo cinematica
    /// </summary>
    [System.Serializable]
    public struct cineamtica
    {
        /// <summary>
        /// disparador que continua dialogo  en modo cinamtica
        /// </summary>
        public bool continuarDialogo;
        /// <summary>
        /// permite activa el modo braulwe no tiene ninguna funcionalidad en el script dialogo_npc
        /// </summary>
        public bool DipararBrawler;

        /// <summary>
        /// cuando esta activo sale de la cinematica
        /// </summary>
        public bool salirCinematica;
    }

    /// <summary>
    /// recoge la variables usadas en el modo cinematico
    /// </summary>
    public cineamtica cinema;
       


        /// <summary>
        /// organiza la seleccion de Itens
        /// </summary>
        [System.Serializable]
        public struct itensADar
        {
            /// <summary>
            /// cuando esta activo se entregan itens al personaje
            /// </summary>
            public bool Dariten;
         /// <summary>
          /// itens que se quieren dar + la cantidad
          /// </summary>
           public CajaInventario ItenInfo;
    }

        /// <summary>
        /// define si en este ciclo del array devuelve Iten Jugador
        /// </summary>
        public itensADar Itens;




        /// <summary>
        /// define cual es comportamiento se ejecuta en el Array de Dialogo ciclo 
        /// </summary>
        public ComportamientoEnrespuesta Comportamiento;


    }
    [System.Serializable]
    public enum ComportamientoEnrespuesta
    {
        nulo,
        RepetirRespuesta,
        IrAlInicioMenssage,
        salirDeLectura
    }
[System.Serializable]
/// <summary>
/// classe que obliga al puntero a parpadear cuando no esta lleyendo texto
/// </summary>
public class Puntero : MonoBehaviour
{
    /// <summary>
    /// recoge el compnente imagen del obj Puntero el canvas
    /// </summary>
    Image punteroImagen;
    /// <summary>
    /// valida la entrada al metodo puntero
    /// </summary>
 public   bool ValidadorPuntero;

   
   public Puntero()
    {
      
        punteroImagen = GameObject.Find("Canvas/box Texto/mask/Puntero").GetComponent<Image>();
    }

    /// <summary>
    /// cuando esta activo hace que el puntero este intermitente
    /// </summary>
    /// <returns></returns>
    /// 

    public  IEnumerator Parpaderopuntero(bool LeyendoTexto)
    {

        if (LeyendoTexto == false && ValidadorPuntero == false)
        {
            ValidadorPuntero = true;

            
            
            ///mientas no esta leyendo el puntero esta activado
            while (LeyendoTexto == false)
            {
               
                punteroImagen.enabled = false;
                yield return new WaitForSeconds(1f);
                if (LeyendoTexto == true)
                    break;
                punteroImagen.enabled = true;
                yield return new WaitForSeconds(1f);
                
                    
            }
            punteroImagen.enabled = false;
            ValidadorPuntero = false;
           

           // StartCoroutine(Parpaderopuntero(LeyendoTexto));

        }
        yield return 0;
    }


  

}