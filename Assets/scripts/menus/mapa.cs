using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapa : MonoBehaviour
{

    public RectTransform selector, panel;

    // posicion del selector
    public Vector3 pos, panelPos;

    public float speed = 3f;

    private float x, y;
    public float DistanciaRecorrida = 0.2f;
    /// <summary>
    /// rango en que va   cambiar de direccion
    /// </summary>
    private int prioridad = 0;

    public float limitYIsquierdo, limitYDerecho;

    public mapa()
    {
        selector = GameObject.Find("equipo/equipo/Mapa/Mask/fondo/selector").GetComponent<RectTransform>();
        panel = GameObject.Find("equipo/equipo/Mapa/Mask/fondo").GetComponent<RectTransform>();

        pos = selector.anchoredPosition3D; // Take the current position
        panelPos = panel.anchoredPosition3D;
        speed = 150f;
        DistanciaRecorrida = 32.6f;

       

        limitYIsquierdo = pos.x- DistanciaRecorrida * 4;
        limitYDerecho = pos.x + DistanciaRecorrida  * 4;
    }



    public void Acciones()
    {
        selector.localPosition = Vector3.MoveTowards(selector.localPosition, pos, speed * Time.deltaTime);    // Move there

        panel.anchoredPosition3D = Vector3.MoveTowards(panel.anchoredPosition3D, panelPos, speed * Time.deltaTime);  // Move there panel select

        MovimientoDinamico();

        // movimientos
        x = (int)Input.GetAxis("Horizontal");
        y = (int)Input.GetAxis("Vertical");

        PrioridadMov();

        MOV();
    }

     public void PosicionInicial()
    {
        selector.anchoredPosition3D = GameObject.Find("Ciudad 1").GetComponent<RectTransform>().anchoredPosition3D;
        pos = selector.anchoredPosition3D; // Take the current position
        limitYIsquierdo = pos.x - DistanciaRecorrida * 4;
        limitYDerecho = pos.x + DistanciaRecorrida * 4;

        panelPos = new Vector2(87.49983f, -2f);
        panel.anchoredPosition3D = new Vector2(87.49983f, -2f);

    }
    void PrioridadMov()
    {
        if (
 Input.GetKeyUp(KeyCode.A)
|| Input.GetKeyUp(KeyCode.W)
|| Input.GetKeyUp(KeyCode.S)
|| Input.GetKeyUp(KeyCode.D)

      )
        {

            prioridad = 0;
        }

        if (prioridad == 0 && x != 0)
        {
            prioridad = 1;

        }
        else if (prioridad == 0 && y != 0)
        {
            prioridad = 2;
        }

        else if (x == 0 && y == 0)
        {
            prioridad = 0;
        }



        if (prioridad == 1 && x != 0)
        {
            if (y != 0)
            {

                x = 0;

            }
        }

        else if (prioridad == 2 && y != 0)
        {
            if (x != 0)
            {
                y = 0;
            }
        }
    }

    void MOV()
    {
        if (selector.localPosition == pos)
        {
            if (pos.x > -438.0001f)
            {
                // derecha
                if (x < -0.2f && y == 0)
                {
                    pos += Vector3.left * DistanciaRecorrida;// Add -1 to pos.x

                }
            }

            if (pos.x < 442.2f)
            {
                // isquierda
                if (x > 0.2f && y == 0)
                {
                    pos += Vector3.right * DistanciaRecorrida;// Add -1 to pos.x

                }
            }
            
            if (pos.y < 260.1799f)
            {
                // arriba
                if (x == 0 && y > 0.2f)
                {
                    pos += Vector3.up * DistanciaRecorrida;// Add -1 to pos.x

                }
            }
            if (pos.y > -259.1f)
            {
            // abajo
            if (x == 0 && y < -0.2f)
                {
                    pos += Vector3.down * DistanciaRecorrida;// Add -1 to pos.x

                }
            }
        }


    }

    public void MovimientoDinamico()
    {

        if (panelPos.x < 87.49983f)
        {
            if (selector.anchoredPosition3D.x < limitYIsquierdo && panel.anchoredPosition3D == panelPos)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                panelPos += Vector3.right * DistanciaRecorrida;// Add 1 to pos.x
                limitYIsquierdo -= DistanciaRecorrida;
                limitYDerecho -= DistanciaRecorrida;
            }
        }
        if (panelPos.x > -75.50016f)
        {
            if (selector.anchoredPosition3D.x > limitYDerecho && panel.anchoredPosition3D == panelPos)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                panelPos += Vector3.left * DistanciaRecorrida;// Add 1 to pos.x
                limitYIsquierdo += DistanciaRecorrida;
                limitYDerecho += DistanciaRecorrida;

            }
        }
    }
}