using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surf : AttacksBase
{
    public Surf()
    {
        this.nombreAtaque = "Surf";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 50;
        this.ValorCansancio = 4f;
        this.probabilidadEfecto = 100;

        this.cantidadDeusosTotales = 15;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.EfectoFueradeBatalla = true;

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
              //  animationBrawler.AddClip(this.animaBattle[0], "Attack");
              //   PosicionRefec = new Vector2(151f, 81f);

            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().imagenePlayer;
               // animationBrawler.AddClip(this.animaBattle[1], "Attack");
              //  PosicionRefec = new Vector2(-236.1f, -115.3f);
            }

        }
        else if (TwovsTwo == true)
        {
            // sin implementaciontodavia
        }


            ObjetoInsta = CrearObjetos.CrearObjeto("Prefac/Batalla/Surf", "Surf", PosicionRefec);
     
            ObjetoInsta.GetComponent<Animation>().Play("Navegacion");

        //audio hit
        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Navegacion");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        yield return new WaitWhile(() => OpcionesAnimation.eventoTrigger == false);
        OpcionesAnimation.eventoTrigger = false;
      
        MonoBehaviour.Destroy(ObjetoInsta.gameObject);


        clip1 = (AudioClip)Resources.Load("Audios/batalla/Hit_Hurt2");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);
        //parpadeo
        BehaviourCall.StartCoroutine(SecuenciasAux.SecunciaDaño(ImagenPersonaje));

        //define el daño y ejecuta el efecto
        float Daño = CalculoDaño.CalcularDaño(QuienAtaca, NombreDelAtaque);
        Daño = (int)Daño;
        Debug.Log("valor del daño :  " + Daño);

        BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDaño(QuienAtaca, Daño));

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
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();

        CrewildChoiseSelect.instanShare.Retroceder();
        CrewildChoiseSelect.instanShare.salirDetodosLosMenus();
        libreriaS.menuInterface.CerrarMenus();
        libreriaS.menuInterface.SalirMenuSeleccion();

        if (movimiento.SharedInstancia.hitRaycast.collider != null)
        {
            if (movimiento.SharedInstancia.hitRaycast.collider.gameObject.layer == 4 )                  
            movimiento.SharedInstancia.MovDirection();
        }
        
       
    }
}
