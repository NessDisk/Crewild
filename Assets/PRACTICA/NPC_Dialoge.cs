using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//using UnityEngine.SceneManagement;      //Allows us to use SceneManager




public class NPC_Dialoge : MonoBehaviour {

    public bool stopDialogue;

    public LayerMask Layermask;


    /// <summary>
    /// define en que posicion de texto esta codigo
    /// </summary>
    private bool EstoyTypeText;


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
    public float letterPause = 0.2f;



    public float speedMoveText = 1f;

    public float distance = 1f;

    // posicion a la que el texto se dirige 
    public Vector3 postext;
    Vector3 Initalpostext;

    ///posicion pasiva a la que el texto esta sin modifiaciones.
    //Transform ActualPosText;

    // condicion que activa el salto de lineas cuando las ltas son mayor a 125
    public bool saltotexto = true;


    bool triggercompleteTExt = false;
    // Use this for initialization.


    public DialogueCinema[] dialogue;


    // posicion del texto
    public int numDialogue = 0;
    public int numSentence = 0;

    //script movimiento del jugador.
    private movimiento PlayerScritp;

    private Transform playerTransfor;


    /// <summary>
    /// defina en cual de los rangos de vision esta actualmente el objeto 
    /// O:R,1:L,2:D,3:U
    /// </summary>
    public bool[] obstacle = new bool[4];

    /// <summary>
    /// define si detecta el objeto frente el y la condicion en de movimiento
    /// </summary>
    public bool accion;

    /// <summary>
    /// define cuando  se sale  del ciclo de  dialogo.
    /// </summary>
    public bool exitDialogue;

  

    /// <summary>
    /// se utiliza para desactivar o activar en menu de caja de texto.
    /// [0] = caja de texto universal.
    /// [1] = caja de seleccion de Yes/no.
    /// </summary>
    private GameObject[] boxTextImage = new GameObject[2];

    private Canvas canvas;

    /// <summary>
    /// es el metodo que se utiliza para definir el que tipo de texto se esta usando.
    /// 1 es para un texto de entrada y salida.
    /// 2 es para decir algo una ves para no repetir.
    /// 3 es para definir eleccion yes not
    /// </summary>
    [Range(1, 3)]
    public int MetodoText;

    private RectTransform TransformCorchetes;




    private int numposCorchet = 0;

    /// <summary>
    /// variable que define la accion en el metodo 3 de dialogo.
    /// </summary>
    public bool BoolCondition;

    /// <summary>
    /// define los objetos con los que colision el objetivo.
    /// </summary>
    public RayNpc Raycast_Npc;


    private Npc_move npcMove;



    /// <summary>
    /// define direccion face npc
    /// </summary>
    [Range(0, 3)]
    public int dirFace;

    int auxface;

    /// <summary>
    /// Detiene el objeto en un lugar en especifico.
    /// </summary>
    public bool ActDialogue;

    private bool exitDialoguemetode2 , exitDialoguemetode3;

    public dialogueclassNPC NpcDialogueClass;

    public DialogueCinema[] TextmanagerBatles;

    private int numAuxArrayText;

    public float pauseletter, distanceLetter, SpeedMoveText;

    private RectTransform ActualPosText;




    void Start() {

        texto = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();

        ActualPosText = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<RectTransform>();





        PlayerScritp = GameObject.Find("personaje").GetComponent<movimiento>();

        playerTransfor = GameObject.Find("personaje").GetComponent<Transform>();


        boxTextImage[0] = GameObject.Find("Canvas/box Texto");
        boxTextImage[1] = GameObject.Find("Canvas/box Election");

        TransformCorchetes = GameObject.Find("Canvas/box Election/Corchete").GetComponent<RectTransform>();

        npcMove = GetComponent<Npc_move>();

        canvas = GameObject.Find("Canvas/").GetComponent<Canvas>();

        postext = ActualPosText.position;

        canvas.enabled = false;

        // boxTextImage[0].SetActive(false);
        // boxTextImage[1].SetActive(false);

        NpcDialogueClass = new dialogueclassNPC();

        Initalpostext = ActualPosText.position;

        Raycast_Npc = new RayNpc();

        NpcDialogueClass.initialTexTVar(ActualPosText);


    }




    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate() {

        if (stopDialogue)
        {
            return;
        }


        Raycast_Npc.RayColision(transform, Layermask);

        if (Raycast_Npc.hitdown.Length < 3)
        {

            Raycast_Npc.hitdown = new RaycastHit2D[3];
        }

        if (Raycast_Npc.hitleft.Length < 3)
        {

            Raycast_Npc.hitleft = new RaycastHit2D[3];
        }

        if (Raycast_Npc.hitup.Length < 3)
        {

            Raycast_Npc.hitup = new RaycastHit2D[3];
        }

        if (Raycast_Npc.hitright.Length < 3)
        {

            Raycast_Npc.hitright = new RaycastHit2D[3];
        }

                    

        if (ActDialogue == true)
        {
            NpcDialogueClass.MoveTowardsText(SpeedMoveText);

        }



        

        DialogueText();

        DirFaceNum();
    }



    void DirFaceNum()
    {





        // detecte player
        if (Raycast_Npc.hitright[2].collider != null)
        {

            //     Debug.Log(Raycast_Npc.hitright.collider.name != gameObject.name);


            obstacle[0] = true;




            if (Raycast_Npc.hitright[2].collider.name == "personaje")
            {


                accion = true;

                auxface = 4;


                //  Debug.Log(Raycast_Npc.hitright.collider.name == "personaje");

            }

        }
        else if (Raycast_Npc.hitright[2].collider == null && obstacle[0] == true)
        {

            obstacle[0] = false;
            accion = false;


        }



        if (Raycast_Npc.hitleft[2].collider != null)
        {



            // && Raycast_Npc.hitleft.collider.name != gameObject.name



            obstacle[1] = true;







            if (Raycast_Npc.hitleft[2].collider.name == "personaje")
            {

                auxface = 2;
                accion = true;

                // Debug.Log(Raycast_Npc.hitleft.collider.name == "personaje");
            }

        }
        else if (Raycast_Npc.hitleft[2].collider == null && obstacle[1] == true)
        {

            obstacle[1] = false;
            accion = false;


        }




        if (Raycast_Npc.hitdown[2].collider != null)
        {



            obstacle[2] = true;


            if (Raycast_Npc.hitdown[2].collider.name == "personaje")
            {

                auxface = 1;
                accion = true;

                //  Debug.Log(Raycast_Npc.hitdown.collider.name == "personaje");
            }

        }
        else if (Raycast_Npc.hitdown[2].collider == null && obstacle[2] == true)
        {

            obstacle[2] = false;
            accion = false;

        }



        if (Raycast_Npc.hitup[2].collider != null)
        {


            obstacle[3] = true;


            if (Raycast_Npc.hitup[2].collider.name == "personaje")
            {

                auxface = 3;
                accion = true;

                //   Debug.Log(Raycast_Npc.hitup.collider.name == "personaje");
            }

        }
        else if (Raycast_Npc.hitup[2].collider == null && obstacle[3] == true)
        {

            obstacle[3] = false;
            accion = false;


        }





        
    }

    /// <summary>
    /// systema de dialogo esta dividio en 3 modos de texto, 1 : entra y repite, 2: entra y repite un texto secundario, 
    /// 3: entra pegunta y sale respuesta
    /// </summary>
    void DialogueText()
    {
        if (
            exitDialoguemetode3 == true 
           )
        {
            selectYesOrNo();

        }

        //metodo 2 no repetition
        else if (
                  exitDialoguemetode2 == true && Input.GetKeyDown(KeyCode.Space)
                  ||
                  TextmanagerBatles.Length == numAuxArrayText
                  && Input.GetKeyDown(KeyCode.Space)
                  && MetodoText == 2
            )
        {
            int aux = 0;
            foreach (DialogueCinema i in TextmanagerBatles)
            {
                if (TextmanagerBatles[aux].PlayContinue == true)
                {
                    numAuxArrayText = aux + 1;
                    break;
                }
                aux++;
            }


            PlayerScritp.StopPlayer = false;

            boxTextImage[0].SetActive(false);
            boxTextImage[1].SetActive(false);

            exitDialogue = false;

            canvas.enabled = false;
            texto.text = "";

            ActDialogue = false;
            exitDialoguemetode2 = false;

            return;
        }



        // exit text 

        else if (TextmanagerBatles.Length == numAuxArrayText && Input.GetKeyDown(KeyCode.Space))
        {
            VarsExitDialogue();


        }



        ///re entrace text metodo 1
        else if (
            Input.GetKeyDown(KeyCode.Space)
           && NpcDialogueClass.EstoyTypeText == false
           && NpcDialogueClass.LeyendoTexto == false
           && TextmanagerBatles[numAuxArrayText].sentences != ""
           && exitDialoguemetode2 == false

           && accion == true
           && transform.position == npcMove.pos
           && playerTransfor.position == PlayerScritp.pos

           )
        {

            VarsEntreceDialogue();

            
        }

        //complete imcomplete text
        else if
           (
           Input.GetKeyDown(KeyCode.Space)
           && texto.text.Length < NpcDialogueClass.rangertext
           && NpcDialogueClass.triggercompleteTExt == false
           && NpcDialogueClass.EstoyTypeText == true
           && NpcDialogueClass.postext == NpcDialogueClass.ActualPosText.localPosition
           && NpcDialogueClass.LeyendoTexto == false




           )
        {
            NpcDialogueClass.triggercompleteTExt = true;

        }


        //normaliza array text
        else if
            (

                 NpcDialogueClass.LeyendoTexto == true
            )

        {

            MetodeTalk();

        }

        

        }



    void MetodeTalk()
    {
        if (MetodoText == 1)
        {

            numAuxArrayText++;


            NpcDialogueClass.LeyendoTexto = false;


        }

        else if (MetodoText == 2)
        {


            if (TextmanagerBatles[numAuxArrayText].PlayContinue == true)
            {
                exitDialoguemetode2 = true;
              
            }
            else
            {
                numAuxArrayText++;

            }

            NpcDialogueClass.LeyendoTexto = false;


        }

        else if (MetodoText == 3)
        {

            if (TextmanagerBatles[numAuxArrayText].PlayContinue == true)
            {
                exitDialoguemetode3 = true;
                boxTextImage[1].SetActive(true);
            }
            else
            {
                numAuxArrayText++;

            }

            NpcDialogueClass.LeyendoTexto = false;

          
        

        }

    }


  
    void VarsExitDialogue()
    {
        PlayerScritp.StopPlayer = false;

        boxTextImage[0].SetActive(false);
        boxTextImage[1].SetActive(false);

        exitDialogue = false;

        canvas.enabled = false;
        texto.text = "";
        numAuxArrayText = 0;
        ActDialogue = false;
        return;

    }


    /// <summary>
    /// estado de variable en cuando entra en el metodo dialogo 1 
    /// </summary>
    void VarsEntreceDialogue()
    {
        PlayerScritp.StopPlayer = true;

        boxTextImage[0].SetActive(true);
        boxTextImage[1].SetActive(false);


        canvas.enabled = true;
        ActualPosText.position = Initalpostext;

        postext = Initalpostext;

        ActDialogue = true;

        npcMove.animatorNpc.SetInteger("face", auxface);


        NpcDialogueClass.initialposText();

      

        texto.text = "";
        message = TextmanagerBatles[numAuxArrayText].sentences;

        StartCoroutine(NpcDialogueClass.textMetode(message, texto, pauseletter, distanceLetter));

    }
    /// <summary>
    /// se activa en metodo 3 de  dialogo, activa el cuadro de dialogo
    /// </summary>
    void selectYesOrNo()
    {
       
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //exitDialogue = true;
               

                 exitDialoguemetode3 = false;
                 boxTextImage[1].SetActive(false);
                 numAuxArrayText += 1;

            if (TextmanagerBatles.Length != numAuxArrayText)
            {
                VarsEntreceDialogue();
            }
            else if (TextmanagerBatles.Length == numAuxArrayText)
            {
                VarsExitDialogue();
            }
           


            if (numposCorchet == 0)
                {
                    BoolCondition = false;
                }
                else if (numposCorchet == 1)
                {
                    BoolCondition = true;
                }

                return;
            }



            if (Input.GetKeyDown(KeyCode.W))
            {

                if (numposCorchet == 0)
                {
                    TransformCorchetes.localPosition = new Vector3(0, -36f, 0);
                    numposCorchet++;
                }

                else if (numposCorchet == 1)
                {
                    TransformCorchetes.localPosition = new Vector3(0, 20, 0);
                    numposCorchet--;
                }

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (numposCorchet == 0)
                {
                    TransformCorchetes.localPosition = new Vector3(0, -36f, 0);
                    numposCorchet++;
                }

                else if (numposCorchet == 1)
                {
                    TransformCorchetes.localPosition = new Vector3(0, 20, 0);
                    numposCorchet--;
                }
            }




        
    }

  
   
}

/// <summary>
/// define los objetos a cierta distancia.
/// </summary>
public class RayNpc : MonoBehaviour
{
    /*  public  RaycastHit2D hitup;
      public RaycastHit2D hitdown;
      public RaycastHit2D hitright;
      public RaycastHit2D hitleft; */

    public RaycastHit2D[] hitdown;
    public RaycastHit2D[] hitleft;
    public RaycastHit2D[] hitup;
    public RaycastHit2D[] hitright;

    public RaycastHit2D lookDir;

    /// <summary>
    /// Raycast Define los objetos con los que colisiona
    /// </summary>
    public void RayColision(Transform transF, LayerMask Layer)
    {


        hitdown = Physics2D.RaycastAll(transF.position, Vector2.down, 1, Layer);
        hitleft = Physics2D.RaycastAll(transF.position, Vector2.left, 1, Layer);
        hitup = Physics2D.RaycastAll(transF.position, Vector2.up, 1, Layer);
        hitright = Physics2D.RaycastAll(transF.position, Vector2.right, 1, Layer);


        Debug.DrawLine(transF.position, transF.position + Vector3.left * 1);
        Debug.DrawLine(transF.position, transF.position + Vector3.right * 1);
        Debug.DrawLine(transF.position, transF.position + Vector3.up * 1);
        Debug.DrawLine(transF.position, transF.position + Vector3.down * 1);


    }

    /// <summary>
    /// define si ve al npc jugador  atraves del raycast
    /// </summary>
    /// <param name="transF"> </param>
    /// <param name="dir"></param>
    /// <param name="RangerSee"></param>
    /// <param name="Layer"></param>
    public void dirSeeNpc(Transform transF, Vector3 dir, float RangerSee ,LayerMask Layer)
    {
        
            lookDir = Physics2D.Raycast(transF.position, dir, RangerSee, Layer);

            Debug.DrawLine(transF.position, transF.position + dir * RangerSee, Color.red);
    }

}



