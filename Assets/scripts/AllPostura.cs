
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPostura : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}



public abstract class posturas
    {
    public string nombre, descripcion;

    public int GastoEnCansancio;

    public Sprite imagenMenu;

    public abstract void Datos();

    public MetodosParaAtaque metodoAuxdeAtaque = new MetodosParaAtaque();
    }


[System.Serializable]
public class PosturaBasica : posturas
{

    public override void Datos()
    {
        nombre = "Normal";
        descripcion = "Esta postura normal de combate no exige mucho esfuerzo ";
        GastoEnCansancio = 1;
        imagenMenu = metodoAuxdeAtaque.DevuelveSprite("Assets/Sprites/rpg-pack/atlas_.png","postura_1");
    }
       
}