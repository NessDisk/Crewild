using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DisparoEnergia : AttacksBase
{

    public DisparoEnergia()
    {
        this.nombreAtaque = "Disp de Energia";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 50;
        this.ValorCansancio = 4f;

        this.cantidadDeusosTotales = 15;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.TipoDeAtaque = TipoUniversalEnum.Energia;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/Disparo energia/Disparo Energia Jugador");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/barrida/Enemy/Attack");
        this.descripcion = "Carga de energia disparada desde el portador";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");



    }


    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
        cantidadDeusos--;
           Vector2 PosicionRefec = Vector2.zero;
        RectTransform ObjetoInsta = null;

        UnityEngine.UI.Image ImagenPersonaje = null;

        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();

        string NombrePLayAnimt = "";

        PosicionRefec = new Vector2(-144f, -74f);
        //  animationBrawler.AddClip(this.animaBattle[0], "Attack");
        ObjetoInsta = CrearObjetos.CrearObjeto("Prefac/Batalla/Disparo", "Disparo", PosicionRefec);

        if (TwovsTwo == false)
        {

            //ataca de espalda
            if (QuienAtaca == "Player")
            {                
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().ImagenEnemigo;
                libreriaS.Batalla.imagenePlayer.sprite = libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[2];
                NombrePLayAnimt = "DisparoSecuencia1";


            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {
             //   animationBrawler.AddClip(this.animaBattle[1], "Attack");
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().imagenePlayer;
                libreriaS.Batalla.ImagenEnemigo.sprite = libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[2];
                NombrePLayAnimt = "DisparoSecuencia2";
            }

        }

        else if (TwovsTwo == true)
        {

            // sin implementaciontodavia
        }


        ObjetoInsta.GetComponent<Animation>().Play(NombrePLayAnimt);
        // animationBrawler.Play("Attack");

        OpcionesAnimation lAnimacionesAux = ObjetoInsta.GetComponent<OpcionesAnimation>();

        yield return new WaitWhile(() => lAnimacionesAux.Disparador == false);

        lAnimacionesAux.Disparador = false;

        SecuenciasAux.DevuelveSpriteBase(QuienAtaca);

        // energia disparada
        ObjetoInsta.GetComponent<PlayScritp>().animacion = new Sprite[3];
        for (int i = 0; i < 3; i++)
        {
            ObjetoInsta.GetComponent<PlayScritp>().animacion[i] = EncontrarMetodo.DevuelveSprite("Sprites/vfx/disparo de energia/ataque 1", "Disparo " + (i + 1));
        }

        yield return new WaitWhile(() => lAnimacionesAux.Disparador == false);

        lAnimacionesAux.Disparador = false;

        //impacto  
        ObjetoInsta.GetComponent<PlayScritp>().animacion = new Sprite[1];

        ObjetoInsta.GetComponent<PlayScritp>().animacion[0] = EncontrarMetodo.DevuelveSprite("Sprites/vfx/disparo de energia/ataque 1", "impacto 1");

        //parpadeo
        BehaviourCall.StartCoroutine(SecuenciasAux.SecunciaDaño(ImagenPersonaje));

        //define el daño y ejecuta el efecto
        float Daño = CalculoDaño.CalcularDaño(QuienAtaca, NombreDelAtaque);
        Daño = (int)Daño;
        Debug.Log("valor del daño :  " + Daño);

        BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDaño(QuienAtaca, Daño));

        yield return new WaitForSeconds(1.2f);
               
        MonoBehaviour.Destroy(ObjetoInsta.gameObject);

        //salir de la seccuencia
        yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);
        yield return new WaitWhile(() => lAnimacionesAux.Disparador == true);

        lAnimacionesAux.Disparador = false;

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