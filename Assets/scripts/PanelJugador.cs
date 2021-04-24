using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelJugador : MonoBehaviour {

    public bool activador;

    public libreriaDeScrips LibreriaS;

    [System.Serializable]
    public class Carteles
    {
        public string medalla = "medalla";
        /// <summary>
        /// esta variable tiene todas las imagenes de carteles de se busca
        /// </summary>
        public Image cateles;

        public bool activadorCarteles;


    }
    public Carteles[] carteleImgWanted = new Carteles[8];

        Text DineroText, ObjetivoMisionText;

	// Use this for initialization
	void Start () {

        LibreriaS = FindObjectOfType<libreriaDeScrips>();

        carteleImgWanted[0].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (1)").GetComponent<Image>();
        carteleImgWanted[1].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (2)").GetComponent<Image>();
        carteleImgWanted[2].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (3)").GetComponent<Image>();
        carteleImgWanted[3].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (4)").GetComponent<Image>();
        carteleImgWanted[4].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (5)").GetComponent<Image>();
        carteleImgWanted[5].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (6)").GetComponent<Image>();
        carteleImgWanted[6].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (7)").GetComponent<Image>();
        carteleImgWanted[7].cateles = GameObject.Find("Jugador/Jugador/carteles/carteles (8)").GetComponent<Image>();

        DineroText = GameObject.Find("Jugador/Jugador/Dinero y datos/text").GetComponent<Text>();
        ObjetivoMisionText = GameObject.Find("Jugador/Jugador/Dinero y datos/textObjetivo").GetComponent<Text>();

        activadorCarteles();

        //etsas variables por el momento no solo adsorven los texto mas no clen alguna funcion
        
        objetivoMision();
    }
	
	// Update is called once per frame
	void Update () {

        if (activador == false)
        {
            return;
        }
        dineroActual();
        objetivoMision();


    }


   public void activadorCarteles()
        {
        for (int i = 0;i<8 ; i++)
        {
            if (carteleImgWanted[i].activadorCarteles == true)
            {
                carteleImgWanted[i].cateles.enabled = true;
            }

        }
        
        }

    void dineroActual()
    {
        DineroText.text = "Dinero :  " + LibreriaS.inventario.Dinero+"$";
    }

    void objetivoMision()
    {
        ObjetivoMisionText.text = "Captura  a la banda de llesca.";
    }
}
