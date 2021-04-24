using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using UnityEngine.SceneManagement;      //Allows us to use SceneManager

public class Trigger_Cinematic : MonoBehaviour {

  public bool activaCinema;
  public Animation animaManager;
    private Transform player, manager;

    public Transform[] ObjCinematic;

    public movimiento playermove;

    public string cinematicName;

    public DialogueCinema[] TextmanagerBatles;

    ScritpCinematicaPlayer scritpPlayer;

    dialogueclass textclass = new dialogueclass();

    private bool dialogobool , dialogue;

    private int numAuxArrayText;

    private Text text;

    private string message;

    public float pauseletter, distanceLetter, SpeedMoveText;

    private Canvas canvas;

    private RectTransform ActualPosText;

    /// <summary>
    /// se utiliza para desactivar o activar en menu de caja de texto.
    /// [0] = caja de texto universal.
    /// [1] = caja de seleccion de Yes/no.
    /// </summary>
    private GameObject[] boxTextImage = new GameObject[2];

 

    // Use this for initialization
    void Start()
    {
        animaManager = GameObject.Find("Game Manager").GetComponent<Animation>();
        player = GameObject.Find("personaje").GetComponent<Transform>();
        manager = GameObject.Find("Game Manager").GetComponent<Transform>();

        playermove = GameObject.Find("personaje").GetComponent<movimiento>();

        scritpPlayer = GameObject.Find("personaje").GetComponent<ScritpCinematicaPlayer>();

        text = GameObject.Find("Canvas/box Texto/mask/Text").GetComponent<Text>();

        // dialogueclas.start(dielogueCinema[]);
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        ActualPosText = GameObject.Find("baltle interface/barras de texto btl/text").GetComponent<RectTransform>();

        boxTextImage[0] = GameObject.Find("Canvas/box Texto");
        boxTextImage[1] = GameObject.Find("Canvas/box Election");

        textclass.initialTexTVar(ActualPosText);
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

          //  playermove.TargerPath = gameObject.name;

            playermove.StopPlayer = true;

            StartCoroutine(moveplayer());
            


            activaCinema = true;

            GetComponent<BoxCollider2D>().enabled = false;

        }
    }

   
	
	// Update is called once per frame
	void Update () {


        

        if (
               dialogobool == true
            
            )
        {

            textclass.MoveTowardsText(SpeedMoveText);

            DialogueText();


        }
    }


    void DialogueText()
    {
        // exit text 
        if (TextmanagerBatles.Length == numAuxArrayText )
        {



            canvas.enabled = false;
            text.text = "";
            numAuxArrayText = 0;
            dialogobool = false;
            return;
        }

      

        ///re entrace text
        if (
           textclass.EstoyTypeText == false
           && textclass.EndReadText == false
           && TextmanagerBatles[numAuxArrayText].sentences != ""
           && animaManager[cinematicName].speed != 1
           )
        {

            boxTextImage[1].SetActive(false);

            text.text = "";
            textclass.initialposText();

            canvas.enabled = true;


            message = TextmanagerBatles[numAuxArrayText].sentences;

            StartCoroutine(textclass.textMetode(message, text, pauseletter, distanceLetter));
        }

        //complete imcomplete text
        else if
           (
           Input.GetKeyDown(KeyCode.Space)
           && text.text.Length < textclass.rangertext
           && textclass.triggercompleteTExt == false
           && textclass.EstoyTypeText == true
           && textclass.postext == textclass.ActualPosText.localPosition
           && textclass.EndReadText == false
           )
        {
            textclass.triggercompleteTExt = true;

        }


        //normaliza array text
        else if (

                 textclass.EndReadText == true && Input.GetKeyDown(KeyCode.Space) 
                )
        {
            if (TextmanagerBatles[numAuxArrayText].PlayContinue)
            {
                playCinema();
            }
            numAuxArrayText++;
            textclass.EndReadText = false;
          
        }
    }


    IEnumerator moveplayer()
    {

        yield return new WaitForSeconds(0.5f);
        yield return new WaitWhile(() => player.position != playermove.pos);
     
       // playermove.followpath();
        
        yield return new WaitWhile(() => playermove.path.ruteNum.Count != 0);

        playermove.enabled = false;

        print("aqui esta el punto de arranque");
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
    public bool EstoyTypeText, saltotexto, triggercompleteTExt, EndReadText;

    //initalposition is optional var is only one referencia for metode MoveTowards
    public Vector3 postext, initalpostion;

    public RectTransform ActualPosText;

    public int rangertext;

    public void initialTexTVar(RectTransform actualposition)
    {
        ActualPosText = actualposition;
        postext = ActualPosText.localPosition;
        initalpostion = ActualPosText.localPosition;
    }


    public void initialposText()
    {
        postext = initalpostion;
        ActualPosText.localPosition = initalpostion;

    }



    public void MoveTowardsText(float speedMoveText)
    {
        ActualPosText.localPosition = Vector3.MoveTowards(ActualPosText.localPosition, postext, speedMoveText * Time.deltaTime);
    }


    public IEnumerator textMetode(string message, Text text, float letterPause, float distance)
    {
        //Debug.Log("Accion hablar class.");

        EstoyTypeText = true;

        saltotexto = true;

        rangertext = 86;

        //metodo para la lectura del texto.
        //metodo para la lectura del texto.
        foreach (char letter in message.ToCharArray())
        {


            text.text += letter;

            if (triggercompleteTExt == true && text.text.Length == rangertext - 1)
            {
                triggercompleteTExt = false;
                saltotexto = true;
                yield return new WaitForSeconds(letterPause);
            }


            else if (triggercompleteTExt == false)
            {
                float auxTIme;
                auxTIme = Time.deltaTime + 5f;

                yield return new WaitWhile(() => Input.GetKeyDown(KeyCode.Space) == false && text.text.Length == rangertext);

                //de alguna extraña razon  que desconosco este pedazo funciona.

                if (text.text.Length == rangertext && saltotexto == true)
                {

                    //activa el moviemiento 
                    postext = ActualPosText.localPosition;

                    postext += Vector3.up * distance;// Add -1 to pos.x

                    rangertext += rangertext;
                    saltotexto = false;

                    Debug.Log("tamaño del texto =" + text.text.Length);

                }


                //	if (typeSound1 && typeSound2)
                //		SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);

                //sonido.Play();

                yield return 0;
                yield return new WaitForSeconds(letterPause);
            }

        }

        EndReadText = true;
        triggercompleteTExt = false;
        EstoyTypeText = false;
    }


    



}
