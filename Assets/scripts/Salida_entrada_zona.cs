using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida_entrada_zona : MonoBehaviour
{
    public string nombreDeLaScena, NombrePunto;
    static public bool PosicionAlIniciar; 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            SceneManager.LoadScene(nombreDeLaScena);
        }

       
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
