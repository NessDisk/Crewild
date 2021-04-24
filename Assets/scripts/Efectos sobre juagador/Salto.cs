using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class Salto : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "personaje" && FindObjectOfType<movimiento>().DisparadorEvento == false)
        {

            col.transform.GetComponent<SortingGroup>().sortingOrder = 1;
           // FindObjectOfType<movimiento>().animt.SetBool("jump", true);
          //  FindObjectOfType<movimiento>().animt.SetBool("walk", false);
              FindObjectOfType<movimiento>().pos += FindObjectOfType<movimiento>().RUMBO * FindObjectOfType<movimiento>().DistanciaRecorrida;
              FindObjectOfType<movimiento>().JumpFuncion();
 
  //          FindObjectOfType<movimiento>().DisparadorEvento = true;
  // GameObject.FindObjectOfType<movimiento>().animt.SetFloat("Blend", 0);
  // GameObject.FindObjectOfType<movimiento>().speed = 2;
  // Invoke("PausaInvoke", 1f);

        }
    }

    void PausaInvoke()
    {
        FindObjectOfType<movimiento>().transform.GetComponent<SortingGroup>().sortingOrder = 0;

    }
   
}
