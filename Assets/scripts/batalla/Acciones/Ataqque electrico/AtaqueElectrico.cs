using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueElectrico : AttacksBase
{
    public AtaqueElectrico()
    {
        this.nombreAtaque = "Ataque Electrico";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 50;
        this.ValorCansancio = 4f;
        this.probabilidadEfecto = 100;

        this.cantidadDeusosTotales = 15;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.EfectoFueradeBatalla = false;

        this.TipoDeAtaque = TipoUniversalEnum.Plant;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/DescargaEnergia/DescargaJugador");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/DescargaEnergia/DescargaEnemigo");
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
        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Electricidad");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        //define el daño y ejecuta el efecto
        float Daño = CalculoDaño.CalcularDaño(QuienAtaca, NombreDelAtaque);
        Daño = (int)Daño;
        Debug.Log("valor del daño :  " + Daño);

        BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDaño(QuienAtaca, Daño));

       

        animationBrawler.GetComponent<libreriaAnimaciones>().Disparador = false;

        ObjetoInsta = CrearObjetos.CrearObjeto("Prefac/Batalla/ImpactoDescarga", "ImpactoDescarga", PosicionRefec);

        yield return new WaitForSeconds(1.2f);
        MonoBehaviour.Destroy(ObjetoInsta.gameObject);

        bool Activaefecto = false;

        if (SecuenciasAux.RetornaSitieneEstadoAlterado(TwovsTwo, QuienAtaca) == true)
        {
            Activaefecto = CalculoDaño.ProbailidadEfecto(probabilidadEfecto);

            if (Activaefecto == true)
            {
                Debug.Log("Active Electrificado ");
                //Efeto Evenenamiento
              
                SecuenciasAux.CambiaEstadoCrewild(QuienAtaca, EstadosEnum.Paralize);

                animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;
                animationBrawler.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = "Descarga Provoca paralisis";

                yield return new WaitWhile(() => animationBrawler.GetComponent<animationScritpBatle>().dialogue == true);
                yield return new WaitForSeconds(0.5f);
                animationBrawler.GetComponent<animationScritpBatle>().text.text = "";
            }
        }

      
       


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
    // <summary>
    /// activa efecto para reducir vida despues del final secuencuencia batalla
    /// </summary>
    /// <param name="QuienAtaca"></param>
    /// <param name="BehaviourCall"></param>
    /// <param name="Crw"></param>
    /// <returns></returns>
    public static IEnumerator EjecutaParalicis(bool TwovsTwo, string QuienAtaca, MonoBehaviour BehaviourCall, CrewildBase Crw)
    {


        // animacion Veneno
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        libreriaS.Batalla.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = Crw.NombreTaxonomico +" Sufre Paralisis no puede atacar";
        libreriaS.Batalla.GetComponent<animationScritpBatle>().text.text = "";
       
        libreriaS.Batalla.GetComponent<animationScritpBatle>().dialogue = true;
        yield return new WaitWhile(() => libreriaS.Batalla.GetComponent<animationScritpBatle>().dialogue == true);

     
        libreriaS.Batalla.GetComponent<animationScritpBatle>().pausaIenumerator = false;
      
    }

}
