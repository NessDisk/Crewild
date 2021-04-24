using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvectoSeleccionaCrewildInicial : MonoBehaviour
{

    private BoxCollider2D Collieder;

    // Start is called before the first frame update
    void Start()
    {
        Collieder = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EncontrarMetodo.DefineSiHaycriaturas() == true)
        {
            Collieder.enabled = false;
        }
    }


}
