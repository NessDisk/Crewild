using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navegacion : MonoBehaviour
{

    public RectTransform panelmask;
    public  RectTransform transforcorchete, panel;

    // posicion del player
    public Vector3 pos, posInitial, panelPos, panelPosInitial;

    public float speed;

    public float distance = 43.7f, limitYUp, limitYDown;

    public int LimiteDemovimientos;

    public int y;

    /// <summary>
    /// define el limite de movimi
    /// </summary>
    public int AlcanceMaximo;

    public libreriaDeScrips LibreriaS;

    public Navegacion(RectTransform corchete, RectTransform Panel)
    {
        transforcorchete = new RectTransform();
        panel = new RectTransform();
        pos = new Vector3();
        posInitial = new Vector3();
        panelPos = new Vector3();
        panelPosInitial = new Vector3();

        transforcorchete = corchete;
        panel = Panel;
        pos = transforcorchete.anchoredPosition3D; // Take the current position
        posInitial = transforcorchete.anchoredPosition3D;
        panelPos = panel.anchoredPosition3D;
        panelPosInitial = panel.anchoredPosition3D;

        limitYUp   = corchete.anchoredPosition3D.y +0.7f;
        limitYDown = corchete.anchoredPosition3D.y - ((7* distance)- 0.7f);

        LibreriaS = FindObjectOfType<libreriaDeScrips>();

        speed = 200;
    }


   public void MovimientoDinamico(int CantidadMaximaDeMov)
    {

        MoveForawar();

        y = (int)Input.GetAxis("Vertical");
        

        if (transforcorchete.localPosition == pos && y < -0.1f && LimiteDemovimientos < CantidadMaximaDeMov)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                pos += Vector3.down * distance;// Add 1 to pos.x
                LimiteDemovimientos++;
                LibreriaS.audioMenus.Audio.Play();
        }

            if (transforcorchete.localPosition == pos && y > 0.1f && LimiteDemovimientos > 0)

            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                pos += Vector3.up * distance;// Add 1 to pos.x
                LimiteDemovimientos--;
                LibreriaS.audioMenus.Audio.Play();
        }

            if (transforcorchete.anchoredPosition3D.y < limitYDown && panel.anchoredPosition3D == panelPos )
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                panelPos += Vector3.up * distance;// Add 1 to pos.x
                limitYDown -= distance;
                limitYUp -= distance;
            }

            else if (transforcorchete.anchoredPosition3D.y > limitYUp && panel.anchoredPosition3D == panelPos)
            {
                //   transforcorchete.Translate(Vector3.down * Time.deltaTime);
                panelPos += Vector3.down * distance;// Add 1 to pos.x
                limitYDown += distance;
                limitYUp += distance;
            }


        
    }


    public void ReiniciarValores()
    {
        pos = posInitial;
        transforcorchete.anchoredPosition3D = posInitial;
        panel.anchoredPosition3D = panelPos;
        LimiteDemovimientos = 0;
    }

    public void MoveForawar()
    {
        transforcorchete.localPosition = Vector3.MoveTowards(transforcorchete.localPosition, pos, speed * Time.deltaTime);  // Move there square braket

        panel.anchoredPosition3D = Vector3.MoveTowards(panel.anchoredPosition3D, panelPos, speed * Time.deltaTime);  // Move there panel select
    }
}
