using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematicaentrada : MonoBehaviour
{

    /// <summary>
    /// Array de datos los diaogos que se van a usar
    /// </summary>
    public LineasDeDialogo[] Dialogos;

    /// <summary>
    ///  Es lector de texto
    /// </summary>
    dialogueclassNPC LectorDetexto;

    /// <summary>
    /// es el tiempo de pausa entre letra y letra
    /// </summary>
    public float TimepoDePausa = 0.2f;

    /// <summary>
    /// activa dialogo en la cinematica
    /// </summary>
    public bool DisparadorDialogo = false;

    Puntero Puntero_;

    // Start is called before the first frame update
    void Start()
    {
        Puntero_ = new Puntero();
    }

    // Update is called once per frame
    void Update()
    {
        if (DisparadorDialogo == true)
        {
            //lectura de mensaje
            if (LectorDetexto.LeyendoTexto == false)
            {
                //puntero de espera
                // puntero();
                StartCoroutine(Puntero_.Parpaderopuntero(LectorDetexto.LeyendoTexto));


                //EntraATexto();
            }
        }
    }


   
}
