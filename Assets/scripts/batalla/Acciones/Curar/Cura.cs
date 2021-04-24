using System.Collections;
using UnityEngine;

public class Cura : AttacksBase
{
    

    public Cura()
    {
        this.nombreAtaque = "Cura";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 50;
        this.ValorCansancio = 4f;
        this.probabilidadEfecto = 100;

        this.cantidadDeusosTotales = 15;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.EfectoFueradeBatalla = true;

        this.TipoDeAtaque = TipoUniversalEnum.Plant;
        
        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/Cura/Cura player");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/Cura/Cura enemy");
        this.descripcion = "Cura 25%  de la salud del jugador.";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");
    }

    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
        Vector2 PosicionRefec = Vector2.zero;
        



        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();

        string NombrePLayAnimt = "";

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
            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {            
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().imagenePlayer;                             
                animationBrawler.AddClip(this.animaBattle[1], "Attack");
            }

        }
        else if (TwovsTwo == true)
        {
            // sin implementaciontodavia
        }

        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/curacion");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        animationBrawler.Play("Attack");

        float HpARecuperar = RecuperaHp.calculoRecuperacion(QuienAtaca);

        BehaviourCall.StartCoroutine(RecuperaHp.EjecutarRecuperaHpInvertido(QuienAtaca, HpARecuperar));


        yield return new WaitWhile(() => animationBrawler.GetComponent<libreriaAnimaciones>().Disparador == false);
        animationBrawler.GetComponent<libreriaAnimaciones>().Disparador = false;

        yield return new WaitWhile(() => RecuperaHp.PausaEjecucionEvento == true);

        // yield return new WaitWhile(() => lAnimacionesAux.Disparador == true);

        // animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;
        animationBrawler.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = "";
        animationBrawler.GetComponent<animationScritpBatle>().text.text = "";
        animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;

        animationBrawler.GetComponent<animationScritpBatle>().pausaIenumerator = false;


        yield return 0;

        

    }

    public override void MetodoEnBatalla(CrewildBase cw)
    {      
        CrewildChoiseSelect.instanShare.SelectPos();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecuperaHp.calculoRecuperacion(cw);

            CrewildChoiseSelect.instanShare.ejecutaAccionSecundaria();
            CrewildChoiseSelect.instanShare.actualizaDatos();
            CrewildChoiseSelect.instanShare.Retroceder();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {

            CrewildChoiseSelect.instanShare.Retroceder();
        }
    }





}
