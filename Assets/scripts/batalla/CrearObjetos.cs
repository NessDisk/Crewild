using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearObjetos 
{
    public static RectTransform CrearObjeto(string Ruta, string NombreObj,Vector3 posicion )
    {
        GameObject VFX = Resources.Load(Ruta) as GameObject;
        //  VFX.name = VFX.name;
        MonoBehaviour.Instantiate(VFX);

        RectTransform transfAux = GameObject.Find( NombreObj + "(Clone)").GetComponent<RectTransform>();
        
        transfAux.parent = GameObject.Find("baltle interfaceC/baltle interface").GetComponent<RectTransform>().transform;
        // transfAux.transform.localPosition = GameObject.Find("baltle interface/baltle interface/zone fight 2/Plataforma").GetComponent<RectTransform>().position;
        transfAux.localPosition = posicion;
        transfAux.localScale = VFX.GetComponent<RectTransform>().localScale;
        //  transfAux.sizeDelta = new Vector2(transfAux.sizeDelta.x -5, transfAux.sizeDelta.y+ 25);
        return transfAux;

    }
}
