using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueclassNPC : MonoBehaviour
{
    public bool EstoyTypeText, saltotexto, triggercompleteTExt, LeyendoTexto;

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


    /// <summary>
    /// hace que el texto se mueva hacia abajo cuando toca el  limite de palabras
    /// </summary>
    /// <param name="speedMoveText"></param>
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


                yield return new WaitForSeconds(letterPause);
            }

        }

        LeyendoTexto = true;
        triggercompleteTExt = false;
        EstoyTypeText = false;
    }




    public IEnumerator LecturaTexto(string message, Text texto ,float TiempodePausa)
    {
        LeyendoTexto = true;
        yield return new WaitForSeconds(0.1f);
        foreach (char letter in message.ToCharArray())
        {
           

            texto.text += letter;
            //completa el texto faltante
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
            else
            {
                yield return new WaitForSeconds(TiempodePausa);
            }
            

        }

        LeyendoTexto = false;
        yield return 0;

    }

    

}
