using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adsorver : AttacksBase
{

    public Adsorver()
    {
        this.nombreAtaque = "Adsorver";
        this.PrecisionDelAtaque = 80;
        this.PoderAtaque = 25;
        this.ValorCansancio = 4f;

        this.cantidadDeusosTotales = 20;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.TipoDeAtaque = TipoUniversalEnum.Plant;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/triple impacto/TripleImpactoJugador");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/triple impacto/TripleImpactoEnemigo");

        this.descripcion = "Adsorve un parte del Salud del contrincante";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");



    }
    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
        cantidadDeusos--;
        Vector2 PosicionRefec = Vector2.zero;
        RectTransform ObjetoInsta = null;

        UnityEngine.UI.Image ImagenPersonaje = null;

        string ruta = "", objString = "";

        libreriaDeScrips libreriaS = Object.FindObjectOfType<libreriaDeScrips>();

        PosicionRefec = new Vector2(-167.1f, -115f);

        CrewildBase atacante = null, defensor = null;

        if (TwovsTwo == false)
        {

            //ataca de espalda
            if (QuienAtaca == "Player")
            {
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().ImagenEnemigo;
                animationBrawler.AddClip(this.animaBattle[0], "Attack");
                ruta = "Sprites/vfx/Curacion/absorcion0";
                objString = "absorcion0";
                PosicionRefec = new Vector2(103, 20);

               
                defensor = RecuperaHp.DevuelveCrewildUSed("Enemy");
                atacante = RecuperaHp.DevuelveCrewildUSed("Player");

            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().imagenePlayer;
                animationBrawler.AddClip(this.animaBattle[1], "Attack");
                ruta = "Sprites/vfx/Curacion/absorcion1";
                objString = "absorcion1";

                defensor = RecuperaHp.DevuelveCrewildUSed("Player");
                atacante = RecuperaHp.DevuelveCrewildUSed("Enemy");
            }

        }

        else if (TwovsTwo == true)
        {

            // sin implementaciontodavia
        }
        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Adsorver");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        ObjetoInsta = CrearObjetos.CrearObjeto(ruta, objString, PosicionRefec);

        BehaviourCall.StartCoroutine(RecuperaHp.AdsorverHp(atacante, defensor));

        yield return new WaitForSeconds(4f);
        yield return new WaitWhile(() => RecuperaHp.PausaEjecucionEvento == true);

        animationBrawler.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = "";
        animationBrawler.GetComponent<animationScritpBatle>().text.text = "";
        animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;

        animationBrawler.GetComponent<animationScritpBatle>().pausaIenumerator = false;

        yield return 0;
    }

    public override void MetodoEnBatalla(CrewildBase cw)
    {
        
    }
}
