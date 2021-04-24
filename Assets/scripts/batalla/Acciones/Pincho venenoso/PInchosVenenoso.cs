using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PInchosVenenoso : AttacksBase
{

    public PInchosVenenoso()
    {
        this.nombreAtaque = "PInchosVenenoso";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 50;
        this.ValorCansancio = 4f;
        this.probabilidadEfecto = 100;

        this.cantidadDeusosTotales = 15;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.TipoDeAtaque = TipoUniversalEnum.Insect;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/Disparo energia/Disparo Energia Jugador");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/barrida/Enemy/Attack");
        this.descripcion = "Dispara De aguja con la probabilidad de Envenenar";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");
    }


    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
        Vector2 PosicionRefec = Vector2.zero;
        RectTransform ObjetoInsta = null;

       

        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();

        string NombrePLayAnimt = "";

        PosicionRefec = new Vector2(-144f, -74f);
        //  animationBrawler.AddClip(this.animaBattle[0], "Attack");
        ObjetoInsta = CrearObjetos.CrearObjeto("Prefac/Batalla/PinchosEnvenenar", "PinchosEnvenenar", PosicionRefec);

        UnityEngine.UI.Image ImagenPersonaje = null;
        if (TwovsTwo == false)
        {

            //ataca de espalda
            if (QuienAtaca == "Player")
            {
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().ImagenEnemigo;
                libreriaS.Batalla.imagenePlayer.sprite = libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[2];
                NombrePLayAnimt = "PinchosSecuencia1";


            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {
                //   animationBrawler.AddClip(this.animaBattle[1], "Attack");
                ImagenPersonaje = animationBrawler.GetComponent<animationScritpBatle>().imagenePlayer;
                libreriaS.Batalla.ImagenEnemigo.sprite = libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[2];
                NombrePLayAnimt = "PinchosSecuencia2";
            }

        }

        else if (TwovsTwo == true)
        {
            // sin implementaciontodavia
        }

        OpcionesAnimation lAnimacionesAux = ObjetoInsta.GetComponent<OpcionesAnimation>();

        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Hit poison");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        ObjetoInsta.GetComponent<Animation>().Play(NombrePLayAnimt);

        yield return new WaitWhile(() => lAnimacionesAux.Disparador == false);

        lAnimacionesAux.Disparador = false;


        SecuenciasAux.DevuelveSpriteBase(QuienAtaca);

        ObjetoInsta.GetComponent<PlayScritp>().animacion = new Sprite[1];

        ObjetoInsta.GetComponent<PlayScritp>().animacion[0] = EncontrarMetodo.DevuelveSprite("Sprites/vfx/disparo de energia/ataque 1", "impacto 1");

        //Parpadeo
        BehaviourCall.StartCoroutine(SecuenciasAux.SecunciaDaño(ImagenPersonaje));

        //audio hit
         clip1 = (AudioClip)Resources.Load("Audios/batalla/Hit_Hurt2");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        //define el daño y ejecuta el efecto
        float Daño = CalculoDaño.CalcularDaño(QuienAtaca, NombreDelAtaque);
        Daño = (int)Daño;
        Debug.Log("valor del daño :  " + Daño);

        BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDaño(QuienAtaca, Daño));

        yield return new WaitForSeconds(1.2f);

        MonoBehaviour.Destroy(ObjetoInsta.gameObject);

        bool Activaefecto = false;

        if (SecuenciasAux.RetornaSitieneEstadoAlterado(TwovsTwo, QuienAtaca) == true)
        {
            Activaefecto = CalculoDaño.ProbailidadEfecto(probabilidadEfecto);

            if (Activaefecto == true)
            {
                Debug.Log("Active envenenamiento ");
                //Efeto Evenenamiento
                CalculoDaño.PausaEjecucionEvento = true;
                BehaviourCall.StartCoroutine(SecuenciasAux.EnvenenamientoEfecto(ImagenPersonaje));
                yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);
                SecuenciasAux.CambiaEstadoCrewild(QuienAtaca, EstadosEnum.poison);
            }
        }
        

        //salir de la seccuencia
        yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);
        yield return new WaitWhile(() => lAnimacionesAux.Disparador == true);

        lAnimacionesAux.Disparador = false;


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

    /// <summary>
    /// activa efecto para reducir vida despues del final secuencuencia batalla
    /// </summary>
    /// <param name="QuienAtaca"></param>
    /// <param name="BehaviourCall"></param>
    /// <param name="Crw"></param>
    /// <returns></returns>
    public static IEnumerator ReduceHp(bool TwovsTwo,string QuienAtaca, MonoBehaviour BehaviourCall, CrewildBase Crw)
    {


        // animacion Veneno
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        libreriaS.Batalla.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = "Envenenamiento reduce la vitalidad";
        libreriaS.Batalla.GetComponent<animationScritpBatle>().text.text = "";

        libreriaS.Batalla.GetComponent<animationScritpBatle>().NoPauseTexto = true;
        libreriaS.Batalla.GetComponent<animationScritpBatle>().dialogue = true;
        yield return new WaitWhile(() => libreriaS.Batalla.GetComponent<animationScritpBatle>().dialogue ==  true);

        BehaviourCall.StartCoroutine(SecuenciasAux.EnvenenamientoEfecto(SecuenciasAux.RetornarObjImageBattleInversa(TwovsTwo, QuienAtaca)));

        CalculoDaño.PausaEjecucionEvento = true;
        yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);

        CalculoDaño.PausaEjecucionEvento = true;
        float Daño = Crw.hpTotal * 0.2f;       
        Daño = (int)Daño;

        BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDañoInvertido(QuienAtaca, Daño));
        yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);
        libreriaS.Batalla.GetComponent<animationScritpBatle>().pausaIenumerator = false;
        libreriaS.Batalla.GetComponent<animationScritpBatle>().NoPauseTexto = false;
    }

    public void EfectoFueraDeBatalla(CrewildBase cw)
    {

    }


}
