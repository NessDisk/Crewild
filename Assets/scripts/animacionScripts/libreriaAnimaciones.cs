using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class libreriaAnimaciones : MonoBehaviour
{
    public bool Disparador;
    public void ParticulasEntrada()
    {
       // GameObject VFX = ;
        Instantiate(Resources.Load("Sprites/vfx/particula/aparecer") as GameObject);

        RectTransform transfAux = GameObject.Find("aparecer(Clone)").GetComponent<RectTransform>();

        transfAux.parent = GameObject.Find("baltle interfaceC/baltle interface/zone fight/plataforma").GetComponent<RectTransform>().transform;
        transfAux.transform.localPosition = GameObject.Find("baltle interfaceC/baltle interface/zone fight/plataforma").GetComponent<RectTransform>().position;
        transfAux.localScale = new Vector3(1, 1, 1);
    }

  

    public void ParticulasEntrada2()
    {
        Instantiate(Resources.Load("Sprites/vfx/particula/aparecer 2") as GameObject);

        RectTransform transfAux = GameObject.Find("aparecer 2(Clone)").GetComponent<RectTransform>();

        transfAux.parent = GameObject.Find("baltle interfaceC/baltle interface/zone fight 2/Plataforma").GetComponent<RectTransform>().transform;
        transfAux.transform.localPosition = GameObject.Find("baltle interfaceC/baltle interface/zone fight 2/Plataforma").GetComponent<RectTransform>().position;
        transfAux.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    /// <summary>
    /// Dispara la los trigger de los itens cuando se llaman
    /// </summary>/
    public void DisparadorEvento()
    {
        Disparador = true;
    }

    
}
