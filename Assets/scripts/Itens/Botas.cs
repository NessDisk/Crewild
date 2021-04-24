using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class Botas : BaseItem
{

    public Botas()
    {
        this.Nombre = "Botas";
        this.descripcionItem = "Si estan equipas permite Correr presionando X";
        this.Coste = 0;
        this.cantidad = 1;
        this.puntosefectos = 0;
        this.Vendible = true;
        this.TieneEfectoNormal = false;
        this.TieneEfectoEnBatalla = false;
        this.ActivarPanelSeleccionParty = false;
        this.EfectoAlEquiparItenJugador = true;
        this.ImagenIten = MetodosAuxiliares.DevuelveSprite("Sprites/Itens/lista Itens", "Botas");
        // añadir
        this.TipoItem = TipoItem.ObjClave;
        Animationparticle = Resources.Load<Object>("Sprites/vfx/Estrella/Pocima") as GameObject;
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

    }
  

    /// <summary>
    /// efecto al equipar en un aciatura si el Bool equipar esta activo
    /// </summary>
    public override void EfectoAlequipar()
    {
        
         
    }


    public override void EfectoAlequiparJugador()
    {
        if (Input.GetKey(KeyCode.X))
        {
            GameObject.FindObjectOfType<movimiento>().animt.SetFloat("Blend", 1);
            GameObject.FindObjectOfType<movimiento>().speed = 4;
        }
        else if (GameObject.FindObjectOfType<movimiento>().animt.GetFloat("Blend") == 1)
        {
            GameObject.FindObjectOfType<movimiento>().animt.SetFloat("Blend", 0);
            GameObject.FindObjectOfType<movimiento>().speed = 2;
        }
    }

    /// <summary>
    /// animaciones en modo  de batalla
    /// </summary>
    /// <param name="TwovsTwo"></param>
    /// <param name="QuienAtaca"></param>
    /// <param name="animationBrawler"></param>
    public override GameObject AnimationBrawler()
    {

        return Animationparticle;

    }

    /// <summary>
    /// efectos en modo normal
    /// </summary>
    public override IEnumerator FuncionNormal(int NumQuienRecaeEfecto, CrewildBase crewildBase)
    {
        yield return new WaitForSeconds(0.5f);
        yield return null;
    }

    /// <summary>
    /// efcctos en modo batalla
    /// </summary>
    public override IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla)
    {
        yield return new WaitForSeconds(0.5f);
        yield return null;

    }
}
