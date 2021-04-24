using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

/// terminar de implentar plantilla
[System.Serializable]
public class pocima : BaseItem
{
    public MonoBehaviour monoBehavior;

    public pocima()
    {
        this.Nombre = "pocima";
        this.descripcionItem = "Recupera la salud Del Crewild 20 HP";
        this.Coste = 100;
        this.cantidad = 1;
        this.puntosefectos = 20;
        this.Vendible = true;
        this.TieneEfectoNormal = true;
        this.TieneEfectoEnBatalla = true;
        this.ActivarPanelSeleccionParty = true;
        this.EfectoAlEquiparItenJugador = false;
        this.ImagenIten = MetodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "Pocima");
        // añadir        
        this.TipoItem = TipoItem.consumibles;
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
        cantidad--;
        cantidadText.text = "" + cantidad;

        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

        //Incrementador item
        float auxIncrfementador = 0;
        // Recuperador Hp
        while (auxIncrfementador <= this.puntosefectos)
        {
            //sale si el p es mayor o igual 0
            if (
                LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].Hp >=
                LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].hpTotal
                )
            {
                break;
            }

            LibreriaS.SeleccionDeCriaturas.barrasDeSalud[NumQuienRecaeEfecto].fillAmount = LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].Hp /
                LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].hpTotal;

            LibreriaS.SeleccionDeCriaturas.TextoHp[NumQuienRecaeEfecto].text = "HP: " + (int)LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].Hp + "/" +
                                                                                        (int)LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].hpTotal;


            auxIncrfementador += 0.4f + Time.deltaTime;
            LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].Hp += 0.4f + Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            yield return null;
        }

        //regulador de Hp
        LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].Hp = (int)LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].Hp;

        LibreriaS.Batalla.HpScrollbar[1].size = LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].Hp /
                                                LibreriaS.informacionCrewild.CrewillInstancia[NumQuienRecaeEfecto].hpTotal;
        auxIncrfementador = 0;

        // continua batalla
        LibreriaS.Batalla.pausaIenumerator = false;


        LibreriaS.Batalla.ListaAcciones[1] = "null";

        if (LibreriaS.Batalla.TriggerEntraceCinema == true)
        {


        }
        LibreriaS.inventario.ExitUsarIten();
        LibreriaS.Batalla.exitInventaryPanel();
    }

    /// <summary>
    /// efcctos en modo batalla
    /// </summary>
    public override IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla)
    {
        cantidad--;
        cantidadText.text = "" + cantidad;

        metodosItens.InstanciaAnimacionObjeto(quienEsAtacante, this.Animationparticle);

        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Recuperacion de energia");
        AnimationBatalla.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);


        //Incrementador item
        float auxIncrfementador = 0;


        if (quienEsAtacante == DefiniteObject.Player)
        {

            // Recuperador Hp
            while (auxIncrfementador <= this.puntosefectos)
            {
                //salle si el p es mayor o igual 0
                if (
                    LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].Hp >=
                    LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].hpTotal
                    )
                {
                    break;
                }

                LibreriaS.Batalla.HpScrollbar[1].size = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].Hp /
                    LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].hpTotal;
                CalculoDaño.ActualizahpNormal(quienEsAtacante.ToString());

                auxIncrfementador += 0.4f + Time.deltaTime;
                LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].Hp += 0.4f + Time.deltaTime;
                yield return new WaitForSeconds(0.1f);

            }

            CalculoDaño.ActualizahpNormal(quienEsAtacante.ToString());
 

            //regulador de Hp
            LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].Hp = (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].Hp;

            LibreriaS.Batalla.HpScrollbar[1].size = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].Hp /
                                                    LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.SobrequienRecaeEfectoItem].hpTotal;
            auxIncrfementador = 0;

            // continua batalla
            LibreriaS.Batalla.pausaIenumerator = false;


        }
        else if (quienEsAtacante == DefiniteObject.Enemy)
        {

        }

        yield return new WaitForSeconds(0.5f);
        yield return null;

    }

}


public static class metodosItens
{

    public static void InstanciaAnimacionObjeto(DefiniteObject Usuario, GameObject ItenObjInstancia)
    {
        if (Usuario == DefiniteObject.Player)
        {

           MonoBehaviour.Instantiate(ItenObjInstancia);

            RectTransform transfAux = GameObject.Find(ItenObjInstancia.name+ "(Clone)").GetComponent<RectTransform>();

            transfAux.parent = GameObject.Find("baltle interfaceC/baltle interface/zone fight/Mask Player/crewild player").GetComponent<RectTransform>().transform;

            transfAux.transform.localPosition = GameObject.Find("baltle interfaceC/baltle interface/zone fight/Mask Player/crewild player").GetComponent<RectTransform>().anchoredPosition;
            transfAux.localScale = new Vector3(1, 1, 1);

            Debug.Log("instancie");
        }
        else if (Usuario == DefiniteObject.Enemy)
        {


        }

    }


} 