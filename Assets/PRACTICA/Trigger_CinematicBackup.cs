using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using UnityEngine.SceneManagement;      //Allows us to use SceneManager

public class Trigger_CinematicBackup : MonoBehaviour
{
   /*
  public bool activaCinema;
  public Animation animaManager;
    private Transform player, manager;

    public Transform[] ObjCinematic;

    public movimiento playermove;

    public string cinematicName;

    public DialogueCinema dielogueCinema;

    ScritpCinematicaPlayer scritpPlayer;

    dialogueclass dialogueclas = new dialogueclass();

    private bool dialogobool ;

    private void Awake()
    {
        
        dialogueclas.Awake();
        
    }
    // Use this for initialization
    void Start()
    {
        animaManager = GameObject.Find("Game Manager").GetComponent<Animation>();
        player = GameObject.Find("personaje").GetComponent<Transform>();
        manager = GameObject.Find("Game Manager").GetComponent<Transform>();

        playermove = GameObject.Find("personaje").GetComponent<movimiento>();

        scritpPlayer = GameObject.Find("personaje").GetComponent<ScritpCinematicaPlayer>();

        dialogueclas.start(dielogueCinema);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "personaje")
        {
           
           // Debug.Log("personaje");
            player.transform.parent =  manager.transform;

            for (int i  = 0 ; i < ObjCinematic.Length; i++)
            {

                ObjCinematic[i].transform.parent = manager.transform;

            }

            playermove.targerName = gameObject.name;

            playermove.StopPlayer = true;

            StartCoroutine(moveplayer());
            


            activaCinema = true;

            GetComponent<BoxCollider2D>().enabled = false;

        }
    }

   
	
	// Update is called once per frame
	void Update () {
        if (dialogobool == true)
        {
         //   dialogueclas=  new dialogueclass();

           dialogueclas.FixedUpdate();

            if (dialogueclas.courtine == true)
            {
                StartCoroutine(dialogueclas.TypeText());
                dialogueclas.courtine = false;
            }

            if (dialogueclas.play == true)

            {
                playCinema();
                dialogueclas.play = false;
                dialogobool = false;
            }
        }
    }

    IEnumerator moveplayer()
    {

        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => player.position != playermove.pos);
     
        playermove.followpath();
        
        yield return new WaitWhile(() => playermove.path.ruteNum.Count != 0);

        playermove.enabled = false;
        animaManager.Play(cinematicName);
    }

    public void dialogueCinema()
    {
     
        dialogobool = true;
    }


    void playCinema()
    {
        animaManager[cinematicName].speed  = 1;
    }

    public void exit()
    {
        playermove.StopPlayer = false;
        playermove.enabled = true;

        player.transform.parent = null;
        for (int i  = 0 ; i < ObjCinematic.Length; i++)
            {

            Destroy(GameObject.Find(ObjCinematic[i].name));

            }
    }
}

public class dialogueclass : MonoBehaviour
{

    /// <summary>
    /// define cuando algo esta dentro del metodo text
    /// </summary>
    public bool EstoyTypeText;

    public bool saltotexto;

    public bool courtine;

    public string message;

     Text texto;

    bool triggercompleteTExt;

    public float distance;
    public float letterPause;


    
    public int numSentence = 0;

    /// <summary>
    /// se utiliza para desactivar o activar en menu de caja de texto.
    /// [0] = caja de texto universal.
    /// [1] = caja de seleccion de Yes/no.
    /// </summary>
    private GameObject[] boxTextImage = new GameObject[2];

    ///posicion pasiva a la que el texto esta sin modifiaciones.
    RectTransform ActualPosText;

    private RectTransform TransformCorchetes;

    // posicion a la que el texto se dirige 
    public Vector3 postext;

    Vector3 Initalpostext;

    DialogueCinema DialogueCinema = new DialogueCinema();


    /// define cuando  se sale  del ciclo de  dialogo.
    /// y tambien el play en el cinema.
    /// </summary>
    public bool exitDialogue, play;


   public  void Awake()
    {

        ActualPosText = GameObject.Find("Text").GetComponent<RectTransform>();
        
        boxTextImage[0] = GameObject.Find("box Texto");

        boxTextImage[1] = GameObject.Find("box Election");

        texto = GameObject.Find("Text").GetComponent<Text>();

        TransformCorchetes = GameObject.Find("Corchete").GetComponent<RectTransform>();

      
    }

    public void start(DialogueCinema dialogueC)
    {
        Initalpostext = ActualPosText.position;

        DialogueCinema = dialogueC;

    }   

    public void FixedUpdate()
    {


       
        //exit dialogo
        if (Input.GetKeyDown(KeyCode.Space) && exitDialogue == true)
        {
           
            

            boxTextImage[0].SetActive(false);
            boxTextImage[1].SetActive(false);
            play = true;
            exitDialogue = false;
            return;
        }


        //enter accion dialogo
        if (
            Input.GetKeyDown(KeyCode.Space)
            && EstoyTypeText == false  
            )
        {


           

        boxTextImage[0].SetActive(true);

            ActualPosText.position = Initalpostext;

            postext = Initalpostext;

            texto.text = "";

            message = DialogueCinema.sentences[numSentence];

            courtine = true;



        }

    }


  public  IEnumerator TypeText()

    {
      
        EstoyTypeText = true;

        saltotexto = true;

        //metodo para la lectura del texto.
        foreach (char letter in message.ToCharArray())
        {


            texto.text += letter;

            if (triggercompleteTExt == true && texto.text.Length == 119)
            {
                triggercompleteTExt = false;
                yield return new WaitForSeconds(0.1f);
            }


            else if (triggercompleteTExt == false)
            {

                yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Space) == false && texto.text.Length == 120);



                if (texto.text.Length == 120 && saltotexto == true)
                {

                    //activa el moviemiento 
                    postext = ActualPosText.position;

                    postext += Vector3.up * distance;// Add -1 to pos.x

                    saltotexto = false;
                    Debug.Log("tamaño del texto =" + texto.text.Length);

                }


                //	if (typeSound1 && typeSound2)
                //		SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);

                //sonido.Play();

                yield return 0;
                yield return new WaitForSeconds(letterPause);
            }

        }


        triggercompleteTExt = false;

       
        Debug.Log(numSentence);
        if (DialogueCinema.sentences.Length > numSentence)
        {

        }

        for (int i = 0; i< DialogueCinema.NumPlayCinema.Length; i++ )
        {
            if (DialogueCinema.NumPlayCinema[i] == numSentence)
            {
                
                exitDialogue = true;
                ++numSentence;
                break;
            }
        }
        if (exitDialogue == false)
        {
            ++numSentence;
        }
      
        if (DialogueCinema.sentences.Length <= numSentence )
        {

            numSentence = 0;
          

            exitDialogue = true;
         
        }

        EstoyTypeText = false;
    }
    */
}
    