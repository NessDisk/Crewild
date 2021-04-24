using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esquive : AttacksBase
{

    public Esquive()
    {
        this.nombreAtaque = "Esquive";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 50;
        this.ValorCansancio = 4f;

        this.cantidadDeusosTotales = 15;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.TipoDeAtaque = TipoUniversalEnum.Normal;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/Esquive/EsquivePlayer");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/Esquive/EsquiveEnemigo");
        
        this.descripcion = "Incrementa en la evación en 1";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");



    }


    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
        cantidadDeusos--;
        Vector2 PosicionRefec = Vector2.zero;
        RectTransform ObjetoInsta = null;

       

        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();



        if (TwovsTwo == false)
        {

            //ataca de espalda
            if (QuienAtaca == "Player")
            {                
                animationBrawler.AddClip(this.animaBattle[0],"Attack");
               
                libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].Evacion +=  1;
            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {

                animationBrawler.AddClip(this.animaBattle[1],"Attack");
                libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].Evacion += 1;
            }

        }

        else if (TwovsTwo == true)
        {

            // sin implementaciontodavia
        }


       // animationBrawler[animaBattle[0].name].name  = "Attack";

        animationBrawler.Play("Attack");


        yield return new WaitWhile(() => animationBrawler.GetComponent<libreriaAnimaciones>().Disparador == false);

        animationBrawler.GetComponent<libreriaAnimaciones>().Disparador = false;

       

        // animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;
        animationBrawler.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = "";
        animationBrawler.GetComponent<animationScritpBatle>().text.text = "";
        animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;

        animationBrawler.GetComponent<animationScritpBatle>().pausaIenumerator = false;


    }

    public override void MetodoEnBatalla(CrewildBase cw)
    {


    }



}