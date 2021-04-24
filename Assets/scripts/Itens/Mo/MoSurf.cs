using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoSurf : BaseItem
{
    public MoSurf()
    {

        this.Nombre = "MoSurf";
        this.descripcionItem = "Da el MoSurf al Crewild seleccionado";
        this.Coste = 2000;
        this.cantidad = 1;
        this.puntosefectos = 20;
        this.Vendible = false;
        this.TieneEfectoNormal = true;
        this.TieneEfectoEnBatalla = false;
        this.ActivarPanelSeleccionParty = true;
        this.EfectoAlEquiparItenJugador = false;
        this.ImagenIten = MetodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "MoSprite");
        // añadir        
        this.TipoItem = TipoItem.Ataques;
        Animationparticle = null;
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();
    }


    public override GameObject AnimationBrawler()
    {
        throw new System.NotImplementedException();
    }

    public override void EfectoAlequipar()
    {
        throw new System.NotImplementedException();
    }

    public override void EfectoAlequiparJugador()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla)
    {

        throw new System.NotImplementedException();

    }

    public override IEnumerator FuncionNormal(int NumQuienRecaeEfecto, CrewildBase crewildBase)
    {
        bool YaseTieneMo = false;
        CrewildChoiseSelect.instanShare.Cuadrotexto.SetActive(true);
        foreach (AttacksBase AtqBase in crewildBase.ataques)
        {
            if (AtqBase.nombreAtaque == new Surf().nombreAtaque)
            {
                YaseTieneMo = true;
            }
        }


        if (YaseTieneMo == false)
        {
            yield return new WaitForSeconds(0.5f);
            CrewildChoiseSelect.instanShare.BoolSelector = true;
            CrewildChoiseSelect.instanShare.estadoAccionEjecutar = CrewildChoiseSelect.EstadoAccionEjecutar.Eleccion;
            CrewildChoiseSelect.instanShare.Cuadrotexto.SetActive(true);
            CrewildChoiseSelect.instanShare.CajaSelectorObj.SetActive(true);
            CrewildChoiseSelect.instanShare.TexteMensaje.text = "Esta Seguro de agregar MOV";


            yield return new WaitWhile(() => CrewildChoiseSelect.instanShare.BoolSelector == true);

            CrewildChoiseSelect.instanShare.CajaSelectorObj.SetActive(false);

            yield return new WaitForSeconds(0.25f);
            Debug.Log(CrewildChoiseSelect.instanShare.ejecutaAccion);




            if (CrewildChoiseSelect.instanShare.ejecutaAccion == true)
            {

                int contador = 0;
                foreach (AttacksBase AtqBase in crewildBase.ataques)
                {
                    if (AtqBase != null)
                    {
                        contador++;
                    }
                }

                Debug.Log("Entre en la Solucion :" + contador);
                if (contador > 2)
                {
                    yield return new WaitForSeconds(0.25f);
                    CrewildChoiseSelect.instanShare.BoolSelector = true;
                    CrewildChoiseSelect.instanShare.TexteMensaje.text = "Seleccion Mo A Remplazar";
                    CrewildChoiseSelect.instanShare.estadoAccionEjecutar = CrewildChoiseSelect.EstadoAccionEjecutar.cambiarMo;

                    CrewildChoiseSelect.instanShare.TextPanelBox.text = "";
                    foreach (AttacksBase AtqBase in crewildBase.ataques)
                    {
                        Debug.Log(AtqBase.nombreAtaque);
                        CrewildChoiseSelect.instanShare.TextPanelBox.text += "" + AtqBase.nombreAtaque + "\n";
                    }



                    yield return new WaitWhile(() => CrewildChoiseSelect.instanShare.BoolSelector == true);
                    // selecciona habilidad a Remplazar

                    crewildBase.ataques[CrewildChoiseSelect.instanShare.NumSelectorMo] = new Surf();
                    CrewildChoiseSelect.instanShare.estadoAccionEjecutar = CrewildChoiseSelect.EstadoAccionEjecutar.nulo;


                }
                else
                {
                    CrewildChoiseSelect.instanShare.SelectorYesOrNo.gameObject.SetActive(false);
                    crewildBase.ataques[CrewildChoiseSelect.instanShare.NumSelectorMo] = new Surf();
                    // Remplaza habilidad Directamente
                }

            }
        }
        else
        {
            CrewildChoiseSelect.instanShare.TexteMensaje.text = "Este Crewild  ya a aprendido este movimiento";
            CrewildChoiseSelect.instanShare.CajaSelectorObj.SetActive(false);
            yield return new WaitForSeconds(2f);
        }


        CrewildChoiseSelect.instanShare.TexteMensaje.text = "";

        // retrocede
        CrewildChoiseSelect.instanShare.ExitMo();


        yield return 0;
    }
}
