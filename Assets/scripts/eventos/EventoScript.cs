using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventoScript : MonoBehaviour
{

    public static EventoScript SharedInstancia;
    public string nombreEventos;
    public GameObject[] Eventosobjetos;

    public int numEventoActual;

    private void Awake()
    {
        SharedInstancia = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        

        RevisorEventos();
    }

    void RevisorEventos()
    {

        if (PlayerPrefs.HasKey(nombreEventos) == false)
          return;       
        else
          numEventoActual = PlayerPrefs.GetInt(nombreEventos); ;
       

        int NumAux = 0;
        foreach (GameObject i in Eventosobjetos)
        {
            if (NumAux < numEventoActual - 1)
            {
                i.SetActive(false);
            }
            else if (numEventoActual - 1 != Eventosobjetos.Length - 1)
            {
                break;
            }
            else if (NumAux == Eventosobjetos.Length - 1)
            {
                i.SetActive(true);
            }
            NumAux++;
        }
    }

   public GameObject EncentraEventoName(string NombreEvento)
    {
        foreach (GameObject i in Eventosobjetos)
        {
            if (i.name == NombreEvento)
            {
                Debug.Log("No hay problema");
                return i;
            }           
        }
        Debug.Log("Ese Mesnaje Evento No existe dentro de la lista Revisa bien");
        Debug.DebugBreak();
        return null;
    }

    public void GuardaNumEvento()
    {
        PlayerPrefs.SetInt(nombreEventos,numEventoActual);
    }

    public void IncrementoNumActual()
    {
        numEventoActual++;
    }
}
