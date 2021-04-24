using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleImpacto : AttacksBase
{

    public TripleImpacto()
    {
        this.nombreAtaque = "Triple Impacto";
        this.PrecisionDelAtaque = 200;
        this.PoderAtaque = 25;
        this.ValorCansancio = 4f;

        this.cantidadDeusosTotales = 20;
        this.cantidadDeusos = this.cantidadDeusosTotales;

        this.TipoDeAtaque = TipoUniversalEnum.Normal;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/triple impacto/TripleImpactoJugador");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/AccionesBatalla/triple impacto/TripleImpactoEnemigo");

        this.descripcion = "Puede hacer entre 1 a 3 impactos";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");



    }


    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
        cantidadDeusos--;
        Vector2 PosicionRefec = Vector2.zero;
        RectTransform ObjetoInsta = null;

        UnityEngine.UI.Image ImagenPersonaje = null;

        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();

        PosicionRefec = new Vector2(-167.1f, -115f);
        

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

        for (int i = 0; i<3;i++)
        {
            //como minimo la secuencia se ejecuta una ves en luego de eso define en un 50/50 desde el segundo ciclo si se ejecuta
            if (i >0)
            {
                //sale del ciclo si no se ejecuta se dispara este valor
                if (DevuelveResultado() == false)
                {
                    break;
                }
            }

            animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;

            animationBrawler.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = TextoImpacto(i);

            yield return new WaitWhile(() => animationBrawler.GetComponent<animationScritpBatle>().dialogue == true);
            yield return new WaitForSeconds(0.5f);
            animationBrawler.GetComponent<animationScritpBatle>().text.text = "";



            animationBrawler.Play("Attack");


            yield return new WaitWhile(() => animationBrawler.GetComponent<libreriaAnimaciones>().Disparador == false);

            animationBrawler.GetComponent<libreriaAnimaciones>().Disparador = false;

            ObjetoInsta = CrearObjetos.CrearObjeto("Prefac/Batalla/impacto", "impacto", PosicionRefec);

            //si el atacante es enemigo corrige posicion de ejecuicion de secuencia atraves de esta anima de personaje
            if (QuienAtaca == "Enemy")
                ObjetoInsta.GetComponent<Animation>().Play("ImpactoEnemigo");

            //parpadeo
            BehaviourCall.StartCoroutine(SecuenciasAux.SecunciaDaño(ImagenPersonaje));

            //define el daño y ejecuta el efecto
            float Daño = CalculoDaño.CalcularDaño(QuienAtaca, NombreDelAtaque);
            Daño = (int)Daño;
            Debug.Log("valor del daño :  " + Daño);

            BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDaño(QuienAtaca, Daño));

            yield return new WaitForSeconds(1f);
            MonoBehaviour.Destroy(ObjetoInsta.gameObject);

            //salir de la seccuencia
            yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);
        }
       
       // yield return new WaitWhile(() => lAnimacionesAux.Disparador == true);

        // animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;
        animationBrawler.GetComponent<animationScritpBatle>().TextoDeBatalla[0] = "";
        animationBrawler.GetComponent<animationScritpBatle>().text.text = "";
        animationBrawler.GetComponent<animationScritpBatle>().dialogue = true;

        animationBrawler.GetComponent<animationScritpBatle>().pausaIenumerator = false;


    }

    public string TextoImpacto(int NumeroDeimpactos)
    {
        string TextoARetornar = "";

        if (NumeroDeimpactos == 0)
        {
            TextoARetornar = "Primer Impacto.";
        }
        else if (NumeroDeimpactos == 1)
        {
            TextoARetornar = "Segundo Impacto.";
        }
        else if (NumeroDeimpactos == 2)
        {
            TextoARetornar = "Tercer Impacto.";
        }

        return TextoARetornar;
    }

    public bool DevuelveResultado()
    {
        bool Resultado;
        if (Random.value > 0.5f)
        {
            Resultado = true;
        }
        else
        {
            Resultado = false;
        }
        return Resultado;
    }
    public override void MetodoEnBatalla(CrewildBase cw)
    {


    }



}