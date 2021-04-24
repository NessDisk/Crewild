using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaPuño : AttacksBase
{
    public MegaPuño()
    {
        this.nombreAtaque = "Mega puño";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 50;
        this.ValorCansancio = 4f;
        this.probabilidadEfecto = 100;

        this.cantidadDeusosTotales = 15;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.EfectoFueradeBatalla = false;

        this.TipoDeAtaque = TipoUniversalEnum.Normal;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/MegaGolpe/MeGaPuñoJugador");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/MegaGolpe/MeGaPuñoEnemigo");
        this.descripcion = "Potente golpe electrico que puede provocar paralisis.";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");
    }

    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
        Vector2 PosicionRefec = Vector2.zero;



        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();



        RectTransform ObjetoInsta = null;

        PosicionRefec = new Vector2(-144f, -74f);
        //  animationBrawler.AddClip(this.animaBattle[0], "Attack");


        UnityEngine.UI.Image ImagenPersonaje = null;
        if (TwovsTwo == false)
        {

            //ataca de espalda
            if (QuienAtaca == "Player")
            {
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().ImagenEnemigo;
                animationBrawler.AddClip(this.animaBattle[0], "Attack");
                PosicionRefec = new Vector2(151f, 81f);

            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().imagenePlayer;
                animationBrawler.AddClip(this.animaBattle[1], "Attack");
                PosicionRefec = new Vector2(-236.1f, -115.3f);
            }

        }
        else if (TwovsTwo == true)
        {
            // sin implementaciontodavia
        }
        animationBrawler.Play("Attack");

        yield return new WaitWhile(() => animationBrawler.GetComponent<libreriaAnimaciones>().Disparador == false);
        animationBrawler.GetComponent<libreriaAnimaciones>().Disparador = false;

        //Parpadeo
        BehaviourCall.StartCoroutine(SecuenciasAux.SecunciaDaño(ImagenPersonaje));

        //audio hit
        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Hit_Hurt2");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        ObjetoInsta = CrearObjetos.CrearObjeto("Prefac/Batalla/MegaPuño", "MegaPuño", PosicionRefec);

        //define el daño y ejecuta el efecto
        float Daño = CalculoDaño.CalcularDañoEspecial(QuienAtaca, NombreDelAtaque);
        Daño = (int)Daño;
        Debug.Log("valor del daño :  " + Daño);

        BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDaño(QuienAtaca, Daño));

        yield return new WaitForSeconds(0.25f);
        MonoBehaviour.Destroy(ObjetoInsta.gameObject);

        yield return new WaitForSeconds(1f);
        //salir de la seccuencia
        yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);

        // animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;
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
