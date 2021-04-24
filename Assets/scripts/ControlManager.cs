using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    Image FondoNegro;
    Text texto;

   
    /// <summary>
    /// otorga acceso a todos los scrips dentro del documento
    /// </summary>
    public libreriaDeScrips LibreriaS ;


    public DefinirCreaturasAllamar[] Criaturas;



    // Start is called before the first frame update
    void Start()
    {
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

        FondoNegro = GameObject.Find("transiciones/Animation/Negro").GetComponent<Image>();
        texto = GameObject.Find("transiciones/Animation/Negro/texto").GetComponent<Text>();
        TransicionHaciaAlpha();


        foreach ( DefinirCreaturasAllamar def in Criaturas)
        {
            print(def.NombreCriatura);
        }

       
        if (Entrada.BoolSaltoDeScena == true)
        {
          
            Entrada.BoolSaltoDeScena = false;
            Entrada.MoverJugador(Entrada.posicionInstanciaStatic);
            // move depeniendo el vector
            Entrada.StaticSalidaString = null;
        }
    }
    /// <summary>
    /// va de alpha hacia Negro
    /// </summary>
   public void TransicionHaciaNegro()
    {
        Color fixedColor = FondoNegro.color;
        fixedColor.a = 1;
        FondoNegro.color = fixedColor;
        FondoNegro.CrossFadeAlpha(0f, 0f, true);
        FondoNegro.CrossFadeAlpha(1, 1, false);

    }


    public void TransicionHaciaNegroTexto()
    {
        Color fixedColor = texto.color;
        fixedColor.a = 1;
        texto.color = fixedColor;
        texto.CrossFadeAlpha(0f, 0f, true);
        texto.CrossFadeAlpha(1f, 5f, true);
    }


    /// <summary>
    /// va de Negro  hacia alfa
    /// </summary>
    public void TransicionHaciaAlpha()
    {
        Color fixedColor = FondoNegro.color;
        fixedColor.a = 1;
        FondoNegro.color = fixedColor;
        FondoNegro.CrossFadeAlpha(1f, 0f, true);
        FondoNegro.CrossFadeAlpha(0, 1, false);
    }

    void InvokeDeshabilitaCanvasTransicion()
    {
        LibreriaS.menuInterface.transicionesCanvas.enabled = false;
    } 
}
