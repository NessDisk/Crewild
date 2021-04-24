using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavegacionAgua : MonoBehaviour
{
    #region Members
    private movimiento Mov;
    private bool EstoyEnAgua;

    #endregion


    #region Funciones

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Agua Pantanosa")
        {
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Mov = GetComponent<movimiento>();
    }

 

    #endregion
}
