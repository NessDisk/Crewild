using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Npc_BrawlerScritp : MonoBehaviour {
    /// <summary>
    /// ActDialogue = se deteine en lugar especifico
    /// </summary>
    public bool Stop , ActDialogue, TriggerBrawler;

    public RayNpc  RaycastMetode;

    public Animator animatorNpc;

    public LayerMask Layermask;

    public int rangerSee;

    private Vector2 dirfaceku;

    public dialogueclassNPC dialogueClass;


    


    //script movimiento del jugador.
    private movimiento PlayerScritp;

    private Transform playerTransf;

    /// <summary>
    /// se utiliza para desactivar o activar en menu de caja de texto.
    /// [0] = caja de texto universal.
    /// [1] = caja de seleccion de Yes/no.
    /// </summary>
    private GameObject[] boxTextImage = new GameObject[2];

    private Canvas canvas , CanvasBrawler;

    private animationScritpBatle scritpBrawler;

    private RectTransform ActualPosText;


    //postext= posicion a la que el texto se dirige 
    private Vector3 postext , Initalpostext;



    private Npc_move npcMove;


    public dialogueclassNPC NpcDialogueClass;



    /// <summary>
    /// es el texto que se va llenando
    /// </summary>
    Text texto;

     private  int auxface, numAuxArrayText;

    /// <summary>
    /// es el mensaje que se le manda 
    /// </summary>
    string message;

    public DialogueCinema[] TextmanagerBatles;

    public float pauseletter = 0.01f, distanceLetter = 95, SpeedMoveText = 200;

    /// <summary>
    /// define cuando  se sale  del ciclo de  dialogo.
    /// </summary>
    public bool exitDialogue;


    // Use this for initialization
    void Start ()
    {
        RaycastMetode = new RayNpc();

        animatorNpc = GetComponent<Animator>();
        dialogueClass = new dialogueclassNPC();

        
        texto = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();

        ActualPosText = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<RectTransform>();

        PlayerScritp = GameObject.Find("personaje").GetComponent<movimiento>();

      
        boxTextImage[0] = GameObject.Find("Canvas/box Texto");
        boxTextImage[1] = GameObject.Find("Canvas/box Election");

        npcMove = GetComponent<Npc_move>();

        canvas = GameObject.Find("Canvas/").GetComponent<Canvas>();

        CanvasBrawler = GameObject.Find("baltle interface/").GetComponent<Canvas>();

        scritpBrawler = GameObject.Find("baltle interface/baltle interface").GetComponent<animationScritpBatle>();

        postext = ActualPosText.position;

        canvas.enabled = false;

        // boxTextImage[0].SetActive(false);
        // boxTextImage[1].SetActive(false);

        NpcDialogueClass = new dialogueclassNPC();

        Initalpostext = ActualPosText.position;

        NpcDialogueClass.initialTexTVar(ActualPosText);

        playerTransf = GameObject.Find("personaje").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Stop == true)
        {
            return;
        }

       // Debug.Log(animatorNpc.GetInteger("face"));

        RaycastMetode.dirSeeNpc(transform, dirfaceku, rangerSee, Layermask);
        DirFace();

        // detecte player
        if (RaycastMetode.lookDir.collider != null)
        {
          
            if (
                RaycastMetode.lookDir.collider.name == "personaje" &&
                PlayerScritp.pos == playerTransf.position&&
                npcMove.pos == transform.position 
               
                )
             {
                EntraceBattle();
                Debug.Log("llegue  aqui ");
                NpcDialogueClass.MoveTowardsText(SpeedMoveText);
            }

        }
        

    }




    void DirFace()
    {
             if (animatorNpc.GetInteger("face") == 1)
        {
            dirfaceku = Vector2.down;
        }
        else if (animatorNpc.GetInteger("face") == 2)
        {
            dirfaceku = Vector2.left;
        }
        else if (animatorNpc.GetInteger("face") == 3)
        {
            dirfaceku = Vector2.up;
        }
        else if (animatorNpc.GetInteger("face") == 4)
        {
            dirfaceku = Vector2.right;
        }
    }

    void EntraceBattle()
    {
        if (TriggerBrawler == false)
        {
           
            PlayerScritp.StopPlayer = true;
            npcMove.NpcMoveBrawlerPlayer();
            TriggerBrawler = true;
        }
        else if (npcMove.triggerEntracebattle == true)
        {
            print("text entrace batlle");
            DialogueText();

          //  npcMove.triggerEntracebattle = false;
        }

    }


    void DialogueText()
    {
        

        // exit text 

         if (TextmanagerBatles.Length == numAuxArrayText )
        {
            print("entrace battle");
            VarsExitDialogue();
           

        }



        ///re entrace text
        else if (
            NpcDialogueClass.EstoyTypeText == false
           && NpcDialogueClass.LeyendoTexto == false
           && TextmanagerBatles[numAuxArrayText].sentences != ""
          

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

            numAuxArrayText++;


            NpcDialogueClass.LeyendoTexto = false;
          
        }



    }



    /// <summary>
    /// estado de variable en cuando entra en el metodo dialogo 1 
    /// </summary>
    void VarsEntreceDialogue()
    {
        //PlayerScritp.StopPlayer = true;

        boxTextImage[0].SetActive(true);
        boxTextImage[1].SetActive(false);


        canvas.enabled = true;
        ActualPosText.position = Initalpostext;

        postext = Initalpostext;

        ActDialogue = true;

     //   npcMove.animatorNpc.SetInteger("face", auxface);


        NpcDialogueClass.initialposText();

        canvas.enabled = true;

        texto.text = "";
        message = TextmanagerBatles[numAuxArrayText].sentences;

        StartCoroutine(NpcDialogueClass.textMetode(message, texto, pauseletter, distanceLetter));

    }

    void VarsExitDialogue()
    {
       

        boxTextImage[0].SetActive(false);
        boxTextImage[1].SetActive(false);

        exitDialogue = false;

        canvas.enabled = false;
        texto.text = "";
        numAuxArrayText = 0;
        ActDialogue = false;

        npcMove.triggerEntracebattle = false;

        CanvasBrawler.enabled = true;

        scritpBrawler.batleManager(animationScritpBatle.enumActionsExecute.begin);
        return;

    }

    // in Vector2Int marca las distancia con metodo manhattan
    public int ManhattanDistance(Vector2Int a, Vector2Int b)
    {
        checked
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}
