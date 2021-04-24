using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectorMapa : MonoBehaviour
{
    Text texto;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mapa")
        {
            texto.text = "" + collision.transform.gameObject.name;
        }
    
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mapa")
        {
            texto.text = "";
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        texto = GameObject.Find("equipo/equipo/Mapa/zona/Texto").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
