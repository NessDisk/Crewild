using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlStar_Script : MonoBehaviour {

    public Animation AnimaIntro;
    public Animation AnimationMenu;
    public AudioSource SongGrito, cinematicaInicial;
    public Canvas IntroCanvas, MenuCanvas;
    public Image transitionImagen;
    public string NuevaEscena;
    public GameObject[] Scenas = new GameObject[7];
    public GameObject SeleccionObj, textoPressStar;
    private RectTransform selectorRect;
    public int numPOsSelector;
    public string NombreScenaPrologo;

    public enum EstadosDepantalla
    {
        seleccion,
        intro,
        pressStar
    }

    public EstadosDepantalla Pantallas;
	// Use this for initialization
	void Start ()
    {
        AnimaIntro = GameObject.Find("cinematica inicial").GetComponent<Animation>();
        AnimationMenu = GameObject.Find("menu").GetComponent<Animation>();

        IntroCanvas = GameObject.Find("cinematica inicial").GetComponent<Canvas>();

        MenuCanvas = GameObject.Find("menu").GetComponent<Canvas>();

        transitionImagen = GameObject.Find("menu/transition").GetComponent<Image>();

        SeleccionObj  = GameObject.Find("menu/opciones");
        selectorRect = GameObject.Find("menu/opciones/Select").GetComponent<RectTransform>();
        SeleccionObj.SetActive(false);

        textoPressStar = GameObject.Find("menu/Text");

       
      
        transitionImagen.CrossFadeAlpha(1, 2.0f, false);

        cinematicaInicial = GetComponent<AudioSource>();
        Pantallas = EstadosDepantalla.intro;
        AnimaIntro.Play("Cinematica inicial");

        AnimationMenu.Play("NormalPressStar");

       
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && AnimaIntro.IsPlaying("Cinematica inicial") && Pantallas == EstadosDepantalla.intro)
        {
            ExitIntro();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && AnimaIntro.IsPlaying("Cinematica inicial") == false && Pantallas == EstadosDepantalla.pressStar)
        {

            AccionPressStar();
        }

        else if (Pantallas == EstadosDepantalla.seleccion)
        {
            MovSelectorOpciones();

            if (Input.GetKeyDown(KeyCode.Space))
                AccioneASelecionar();
                    
         }



    }
    /// este metodo permite el fundido de alpha en 0 a 1
    void fundido()
    {
        Color fixedColor = transitionImagen.color;
        fixedColor.a = 1;
        transitionImagen.color = fixedColor;
        transitionImagen.CrossFadeAlpha(0f, 0f, true);
        transitionImagen.CrossFadeAlpha(1, 0.5f, false);
    }
  public  void changeScene()
    {
        SceneManager.LoadScene(informacionCrewild.CargarNombreZona());
    }

    public void CargaEspecifica()
    {
        SceneManager.LoadScene(NombreScenaPrologo);
    }

    public void songCinematiic()
    {

        cinematicaInicial.Play();
    }

    public void playGrito()
    {
        SongGrito.Play();
    }

    public void ExitIntro()
    {
        AnimaIntro.Stop();
        IntroCanvas.enabled = false;
        MenuCanvas.enabled = true;
        cinematicaInicial.Stop();
        SongGrito.Stop();
        Pantallas = EstadosDepantalla.pressStar;
        for (int i = 0; i< 7; i++)
        {
            Scenas[i].SetActive(false);
        }
    }

    void AccionPressStar()
    {
        textoPressStar.transform.gameObject.SetActive(false);
        SeleccionObj.SetActive(true);
        Pantallas = EstadosDepantalla.seleccion;
    }

    void CargaScena()
    {
        AnimationMenu.Play("PressStarEnterGame");
        print("animacion para entrar en nueva escena");

        fundido();
        Invoke("changeScene", 1);
    }

    /// <summary>
    /// carga escena especifica
    /// </summary>
    void CargaScena(string Nombreescena)
    {
        AnimationMenu.Play("PressStarEnterGame");
        print("animacion para entrar en nueva escena");

        fundido();
        NombreScenaPrologo = Nombreescena;
        Invoke("CargaEspecifica", 1);
    }

    void MovSelectorOpciones()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            numPOsSelector++;
        }

        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            numPOsSelector--;
        }

        if (numPOsSelector > 1)
        {
            numPOsSelector = 0;
        }

        else if (numPOsSelector < 0)
        {
            numPOsSelector = 1;
        }
        if (numPOsSelector == 0)
        {
            selectorRect.anchoredPosition3D = new Vector2(-8, 22);
        }
        else if (numPOsSelector ==1)
        {
            selectorRect.anchoredPosition3D = new Vector2(-8, -19);
        }
    }

    void AccioneASelecionar() 
{
        //nueva partida
        if (numPOsSelector == 0)
        {
            ValoresIniciales();
            CargaScena("prologo");
        }
        //load game
        else  if (numPOsSelector ==  1)
        {
            if (informacionCrewild.CargarNombreZona() != "")
            {
                CargaScena();
            }
        }
           
    }

    void  ValoresIniciales()
    {
        informacionCrewild cargardatainicial = new informacionCrewild();

        cargardatainicial.GuardaDataDeinicio();
        PlayerPrefs.DeleteAll();

    }
}
