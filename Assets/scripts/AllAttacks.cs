
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
public class AllAttacks : MonoBehaviour {

   
   
    void Start () {

        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
/// <summary>
/// biblioteca de metodos auxiliares en para la clase abstracta Attacks
/// </summary>
public class MetodosParaAtaque
{

    /// <summary>
    /// este metodo es para tomar un sprite especifico del editor en una textura2d  dentro de su array
    /// </summary>
    /// <param name="RutadeTextura2d"> es la ruta dentro del editor ejemplo ("Assets/Sprites/rpg-pack/atlas_.png)"</param>
    /// <param name="NombreDelSprite">Es el nombre del sprite dentro del array que forma la textura2d </param>
    /// <returns></returns>
    public Sprite DevuelveSprite(string RutadeTextura2d , string NombreDelSprite )
    {

        Sprite auxSprite = null;

        //agrega la ruta de sprita que se desea en este caso seria  a una textura2d
        foreach (Sprite o in Resources.LoadAll<Sprite>(RutadeTextura2d))
        {
         
            //este metodo se puede hacer sabiando el nombre o el numero del array pero es mas conveniente con el nombre 
            // ya que  en una sola textura hay muchos arrays
            if (o.name == NombreDelSprite)
            {
                auxSprite = o;
                break;
            }
            
        }

        return auxSprite;
    }

}




[System.Serializable]
public abstract class AttacksBase 
{
    public string nombreAtaque;
    public float PoderAtaque;
    public float ValorCansancio;
    public float PrecisionDelAtaque;
    public float probabilidadEfecto;

    public bool EfectoFueradeBatalla;

    public int cantidadDeusos, cantidadDeusosTotales;
    // cambiar tipo de ataque 
    public TipoUniversalEnum TipoDeAtaque;

    public string descripcion;

    public Sprite imagenIten;

    public libreriaDeScrips LibreriaS;

    /// <summary>
    /// clase de apoyo que ayuda a para tener una libreria  de metodos utiles las clases de ataques. 
    /// </summary>
    public MetodosParaAtaque metodosAuxiliares = new MetodosParaAtaque();
 
    
    /// <summary>
    /// [0] = ataque de espalda , [1] = ataque de frente
    /// </summary>
    public AnimationClip[] animaBattle = new AnimationClip[2];





    abstract public IEnumerator  AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque , MonoBehaviour BehaviourCall);

    /// <summary>
    /// define cual como que efecto tiene  batalla
    /// </summary>
    abstract public void MetodoEnBatalla(CrewildBase cw);

    
}


[System.Serializable]
public class ataqueBarrida : AttacksBase
{

    public ataqueBarrida()
    {
        this.nombreAtaque = "Barrida";
        this.PrecisionDelAtaque = 100;
        this.PoderAtaque = 40;
        this.ValorCansancio = 4f;

        this.cantidadDeusosTotales = 30;
        this.cantidadDeusos = this.cantidadDeusosTotales;
               
        this.TipoDeAtaque = TipoUniversalEnum.Normal;

        this.animaBattle[0] = Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/barrida/Player/Attack");
        this.animaBattle[1] = Resources.Load<AnimationClip>("animaciones/btl sys/animations/attack/barrida/Enemy/Attack");
        this.descripcion = "ataque con todo el peso del cuerpo";
        this.imagenIten = metodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "ItenAttack");

        this.LibreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();

    }


    public override IEnumerator AnimationBrawler(bool TwovsTwo, string QuienAtaca, Animation animationBrawler, string NombreDelAtaque, MonoBehaviour BehaviourCall)
    {
       
        if (TwovsTwo == false)
        {
            //ataca de espalda
            if (QuienAtaca == "Player")
            {
              
                animationBrawler.AddClip(this.animaBattle[0], "Attack");

               
            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {
                animationBrawler.AddClip(this.animaBattle[1], "Attack");
                
            }

        }

        else if (TwovsTwo == true)
        {

            // sin implementaciontodavia
        }

        animationBrawler.Play("Attack");

        libreriaAnimaciones lAnimacionesAux = animationBrawler.GetComponent<libreriaAnimaciones>();

        yield return new WaitWhile(() => lAnimacionesAux.Disparador == false);

        lAnimacionesAux.Disparador = false;


        // sonido de impacto

        AudioClip clip1 = (AudioClip)Resources.Load("Audios/batalla/Hit_Hurt2");
        animationBrawler.GetComponent<animationScritpBatle>().AudioVfx.PlayOneShot(clip1);

        //define el daño y ejecuta el efecto
        float Daño =  CalculoDaño.CalcularDaño(QuienAtaca, NombreDelAtaque);
        Daño = (int)Daño;
        Debug.Log("valor del daño :  "+ Daño);
       
        BehaviourCall.StartCoroutine(CalculoDaño.EjecutarDaño(QuienAtaca, Daño));

        //salir de la seccuencia
        yield return new WaitWhile(() => CalculoDaño.PausaEjecucionEvento == true);
          yield return new WaitWhile(() => lAnimacionesAux.Disparador == false);
        lAnimacionesAux.Disparador = false;


        animationBrawler.GetComponent<animationScritpBatle>().pausaIenumerator = false;
        yield return 0;
        
    }

    public override void MetodoEnBatalla(CrewildBase cw)
    {


    }
   


}



