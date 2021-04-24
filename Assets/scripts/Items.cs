

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor;

public class Items : MonoBehaviour
{


   
}


/// <summary>
/// se utiliza para llamar las clases hijas por string  de los hijo de ItemBase 
/// </summary>
public class llamarIten
{
    /// <summary>
    /// se utiliza para retornar el nombre la clase con una variable de texto
    /// </summary>
    /// <param name="NombreDelaclase">Nombre de la clase  a la que sequiere retornar</param>
    /// <returns></returns>
    public BaseItem RetornarClase(string NombreDelaclase)
    {
        BaseItem AuxIten = null;
        switch (NombreDelaclase)
        {
              case "pocima":
                AuxIten = new pocima();
                break;

            case "manzana":
                AuxIten = new manzana();
                break;
            case "Espada":
                AuxIten = new Espada();
                break;

            case "Escudo":
                AuxIten = new Escudo();
                break;
            case "BullBox":
                AuxIten = new BullBox();
                break;
            case "Botas":
                AuxIten = new Botas();
                break;
            case "MoCura":
                AuxIten = new MoCura();
                break;
            case "MoSurf":
                AuxIten = new MoSurf();
                break;
            case "MoAtaElec":
                AuxIten = new MoAtaqueElectrico();
                break;
            default:
                Debug.Log("Este iten no existe o esta mal escrito ="+ NombreDelaclase);               
                Debug.Break();
                break;
        }
       
        return AuxIten;
    }




}

public enum TipoItem
{

    consumibles,
    Ataques,
    ObjClave,
    Frutas,
    Espacios,
    Posturas,
    ObjEfects,

}

[System.Serializable]
public abstract class BaseItem /*: MonoBehaviour*/
{
    public string Nombre, descripcionItem;
    public float Coste, puntosefectos;
    public int cantidad;
    public TipoItem TipoItem;

    public GameObject ObjPrefac;

    public Sprite ImagenIten;
    public Image CapturaImagen;
    public RectTransform TransItenMenu = new RectTransform();
    public Text NombreText, cantidadText;

    public GameObject Animationparticle;

    public AnimationClip AnimationClip;

    /// <summary>
    /// libreria  donde llammar rapida mente a todos los scripts
    /// </summary>
    public libreriaDeScrips LibreriaS = new libreriaDeScrips();

    /// <summary>
    /// si esta activo significa que tiene una porpiedad poner como equipo en las criatura
    /// </summary>
    public bool TieneEfectoAlEquipar, TieneEfectoNormal, TieneEfectoEnBatalla, ActivarPanelSeleccionParty, Vendible, EfectoAlEquiparItenJugador;


    /// <summary>
    /// Hay metodo utiles para este clase como llamada de sprite en el proyecto
    /// </summary>
    public MetodosParaAtaque MetodosAuxiliares = new MetodosParaAtaque();


    /// <summary>
    /// [0] = ataque de espalda , [1] = ataque de frente
    /// </summary>
    public AnimationClip[] animaBattle = new AnimationClip[2];





    /// <summary>
    /// define los datos del objeto
    /// </summary>
    abstract public IEnumerator FuncionNormal(int NumQuienRecaeEfecto, CrewildBase crewildBase);


    /// <summary>
    /// devuelve la animacion que se va a usar el modo de batalla
    /// </summary>
    /// <param name="TwovsTwo"></param>
    /// <param name="QuienAtaca"></param>
    /// <returns></returns>
    abstract public GameObject AnimationBrawler();

    /// <summary>
    /// Define los efectos al del objeto al ser equipado por una criatura
    /// </summary>
    abstract public void EfectoAlequipar();

    /// <summary>
    /// Define los efectos al del objeto al ser equipado por el Jugador
    /// </summary>
    abstract public void EfectoAlequiparJugador();


    abstract public IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla);



}






[System.Serializable]
public class BullBox : BaseItem 
{

   public BullBox()
    {
        this.Nombre = "BullBox";
        this.descripcionItem = "Espacio personal de cada Bullwill atrapado";
        this.Coste = 100;
        this.cantidad = 1;
        this.Vendible = true;
        this.TieneEfectoNormal = false;
        this.TieneEfectoEnBatalla = true;
        this.ActivarPanelSeleccionParty = false;
        this.EfectoAlEquiparItenJugador = false;
        this.puntosefectos = 4;
        this.ImagenIten = MetodosAuxiliares.DevuelveSprite("Sprites/Itens/hojas Itens 1", "BullBox");
        // añadir
        //this.animaBattle = ;
        this.TipoItem = TipoItem.Espacios;
        Animationparticle = Resources.Load<Object>("Sprites/vfx/Estrella/Pocima") as GameObject;
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();
    }

 

    /// <summary>
    /// efecto al equipar en un aciatura si el Bool equipar esta activo
    /// </summary>
    public override void EfectoAlequipar()
    {
     
    }

    public override void EfectoAlequiparJugador()
    {
       
    }


    /// <summary>
    /// animaciones en modo  de batalla
    /// </summary>
    /// <param name="TwovsTwo"></param>
    /// <param name="QuienAtaca"></param>
    /// <param name="animationBrawler"></param>
    public override GameObject AnimationBrawler()
    {
       
        return Animationparticle;
       
    }





    /// <summary>
    /// efectos en modo normal
    /// </summary>
    public override IEnumerator FuncionNormal(int NumQuienRecaeEfecto, CrewildBase crewildBase)
    {
        yield return 0;
    }

    /// <summary>
    /// efcctos en modo batalla
    /// </summary>
    public override IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla)
    {

        //Incrementador item

        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();
        if (quienEsAtacante == DefiniteObject.Player)
        {
            cantidad--;
            cantidadText.text = "" + cantidad;

            Debug.Log("Estoy en BullBox");
            LibreriaS.Batalla.animationCinema.AddClip(Resources.Load<AnimationClip>("animaciones/Itens/Bullwill/Attack"), "Attack");
            LibreriaS.Batalla.animationCinema.Play("Attack");

            // encuentra proyectil
            RectTransform AuxrectTransf ;
             
             yield return new WaitWhile(() => GameObject.FindObjectOfType<libreriaAnimaciones>().Disparador == false);

            GameObject.FindObjectOfType<libreriaAnimaciones>().Disparador = false;
            AuxrectTransf = CrearObjetos();

            AuxrectTransf.GetComponent<Animation>().Play("Caida");

            yield return new WaitWhile(() => AuxrectTransf.GetComponent<OpcionesAnimation>().Disparador == false);
            AuxrectTransf.GetComponent<OpcionesAnimation>().Disparador = false;

            for (int i = 0, captura = 0; i < 3;i ++)
            {
                captura++;
                yield return new WaitForSeconds(1f);

                float Valor = -1;

                if (LibreriaS.Batalla.randomEncounter == true)
                {
                    float PorcentajeResistencia, ResistenciaActual;

                    PorcentajeResistencia = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp /
                                           LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;

                    ResistenciaActual = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].ResistenciaAlacaptura * PorcentajeResistencia;

                    Valor = MensajesGlovales.ResitenciaCriaturavPCaptura(this.puntosefectos, ResistenciaActual);
                }

                else
                {
                    Valor = -1;
                }

                Valor = 3;
                captura = 3;
                if (Valor > 0)
                {

                   
                    AuxrectTransf.GetComponent<Animation>().Play("Intento");

                    yield return new WaitWhile(() => AuxrectTransf.GetComponent<OpcionesAnimation>().Disparador == false);
                    AuxrectTransf.GetComponent<OpcionesAnimation>().Disparador = false;
                    yield return new WaitForSeconds(0.5f);

                    if (captura == 3)
                    {

                        AnadirABulleWild();
                        LibreriaS.Batalla.GameOverBool = true;


                        Debug.Log("Se realiza captura");
                        LibreriaS.Batalla.dialogue = true;
                        string nombreBulleWild = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Nombre;

                        LibreriaS.Batalla.TextoDeBatalla[0] = "La captura de "+ nombreBulleWild + " Bullewild";

                        yield return new WaitWhile(() => LibreriaS.Batalla.dialogue == true);
                        yield return new WaitForSeconds(0.5f);

                        LibreriaS.Batalla.dialogue = true;
                      



                        LibreriaS.Batalla.TextoDeBatalla[0] = "Quieres ponerle un Nombre a tu Bullewild";
                        yield return new WaitWhile(() => LibreriaS.Batalla.dialogue == true);                       
                        LibreriaS.Batalla.CajadeEleccion = true;
                        LibreriaS.Batalla.NoPauseTexto = false;


                        LibreriaS.Batalla.text.text = "Quieres ponerle un Nombre a tu Bullewild";

                        yield return new WaitWhile(() => LibreriaS.Batalla.CajadeEleccion == true);

                        LibreriaS.Batalla.text.text = "";
                        if (LibreriaS.Batalla.RespuestaADevolver == true)
                        {
                            Debug.Log("Respuesta Positiva");



                        }

                        else if (LibreriaS.Batalla.RespuestaADevolver == false)
                        {
                            Debug.Log("Respuesta Negativa");

                        }

                        //Efectua respuesta
                        //    yield return new WaitWhile(() => true);

                        //    yield return new WaitWhile(() => LibreriaS.Batalla.dialogue == true);
                        // AuxrectTransf.GetComponent<Animation>().Play("CapturaExitosa");
                        LibreriaS.Batalla.GameOverBool = true;
                        LibreriaS.Batalla.ObjAttacked = DefiniteObject.Enemy;

                        LibreriaS.Batalla.pausaIenumerator = false;
                        yield return new WaitWhile(() => LibreriaS.Batalla.TriggerEntraceCinema == true);
                        MonoBehaviour.Destroy(AuxrectTransf.gameObject);
                        break;
                    }

                }
                else if (Valor < 0)
                {
                    Debug.Log("sali de captura");
                    LibreriaS.Batalla.Animationbrawler(quienEsAtacado.ToString(), "Change");
                    AuxrectTransf.GetComponent<Animation>().Play("Destruccion");

                    yield return new WaitWhile(() => AuxrectTransf.GetComponent<OpcionesAnimation>().Disparador == false);
                        MonoBehaviour.Destroy(AuxrectTransf.gameObject);
                    break;


                }
               
            }





            // continua batalla
            LibreriaS.Batalla.pausaIenumerator = false;


        } 
        else if (quienEsAtacante == DefiniteObject.Enemy)
        {

        }

        yield return new WaitForSeconds(0.5f);
        yield return null;
 
    }



    RectTransform CrearObjetos()
    {

        GameObject VFX = Resources.Load("Prefac/Batalla/BullboxNormal") as GameObject;
      //  VFX.name = VFX.name;
        MonoBehaviour.Instantiate(VFX);

        RectTransform transfAux = GameObject.Find("BullboxNormal(Clone)").GetComponent<RectTransform>();

        transfAux.parent = GameObject.Find("baltle interfaceC/baltle interface/zone fight 2").GetComponent<RectTransform>().transform;
        // transfAux.transform.localPosition = GameObject.Find("baltle interface/baltle interface/zone fight 2/Plataforma").GetComponent<RectTransform>().position;
        transfAux.localPosition = new Vector2(-14.2f, 34.6f);
        transfAux.localScale = VFX.GetComponent<RectTransform>().localScale;
      //  transfAux.sizeDelta = new Vector2(transfAux.sizeDelta.x -5, transfAux.sizeDelta.y+ 25);
        return transfAux;
    }


    void AnadirABulleWild()
    {
           int contador = 0;
        bool validadorAgrupacion = false;
            foreach(CrewildBase Cb in LibreriaS.informacionCrewild.CrewillInstancia)
            {
                if (Cb == null)
                {
                validadorAgrupacion = true;
                break;
                }
            contador++;
        }
            Debug.Log(contador);

        if (validadorAgrupacion == true)
        {
            LibreriaS.informacionCrewild.CrewillInstancia[contador] = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy];
            LibreriaS.SeleccionDeCriaturas.BulletTransf[contador].gameObject.SetActive(true);

            //Reinicia Ataques
            for (int i = 0; i < 4; i++)
            {
                if (LibreriaS.informacionCrewild.CrewillInstancia[contador].ataques[i] != null)
                {
                    LibreriaS.informacionCrewild.CrewillInstancia[contador].ataques[i].cantidadDeusos = LibreriaS.informacionCrewild.CrewillInstancia[contador].ataques[i].cantidadDeusosTotales;
                }

            }


            LibreriaS.SeleccionDeCriaturas.actualizaDatos();
        }

        else
        {
            //Reinicia Ataques
            for (int i = 0; i < 4; i++)
            {
                if (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].ataques[i] != null)
                {
                    LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].ataques[i].cantidadDeusos = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].ataques[i].cantidadDeusosTotales;
                }

            }

            LibreriaS.PcScritp.AñadirCriatura(LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy]);

            LibreriaS.SeleccionDeCriaturas.actualizaDatos();
        }
          
       

    }

}

[System.Serializable]
public class manzana : BaseItem
{

    public manzana()
    {
        this.Nombre = "manzana";
        this.descripcionItem = "Recupera la salud Del Crewild 10 HP";
        this.Coste = 50;
        this.cantidad = 1;

        this.cantidad = 1;
        this.Vendible = true;
        this.TieneEfectoNormal = false;
        this.TieneEfectoEnBatalla = false;
        this.ActivarPanelSeleccionParty = false;
        this.EfectoAlEquiparItenJugador = false;
        this.puntosefectos = 4;


        this.ImagenIten = MetodosAuxiliares.DevuelveSprite("Sprites/rpg-pack/atlas_", "manzana");
        // añadir
        //this.animaBattle = ;
        this.TipoItem = TipoItem.consumibles;

        // añadir
        //this.animaBattle = ;
        this.TipoItem = TipoItem.Espacios;
        Animationparticle = Resources.Load<Object>("Sprites/vfx/Estrella/Pocima") as GameObject;
        LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();

    }
 

    /// <summary>
    /// efecto al equipar en un aciatura si el Bool equipar esta activo
    /// </summary>
    public override void EfectoAlequipar()
    {

    }


    public override void EfectoAlequiparJugador()
    {

    }

   
    public override GameObject AnimationBrawler()
    {
        return Animationparticle;
    }



    /// <summary>
    /// efectos en modo normal
    /// </summary>
    public override IEnumerator FuncionNormal(int NumQuienRecaeEfecto, CrewildBase crewildBase)
    {
        yield return new WaitForSeconds(0.5f);
        yield return null;
    }

    IEnumerable AuxEfect()
    {
        yield return 0;
        yield return new WaitForSeconds(0);

    }
    /// <summary>
    /// efcctos en modo batalla
    /// </summary>
    public override IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla)
    {

        yield return new WaitForSeconds(0.5f);
        yield return null;

    }

}

[System.Serializable]
public class Espada : BaseItem
{
    public Espada()
    {
        this.Nombre = "Espada";
        this.descripcionItem = "se puede aprender";
        this.Coste = 50;
        this.cantidad = 1;
        this.ImagenIten = MetodosAuxiliares.DevuelveSprite("Assets/Sprites/rpg-pack/atlas_.png", "espada");
        // añadir
        //this.animaBattle = ;
        this.TipoItem = TipoItem.Ataques;
    }

  

    /// <summary>
    /// efecto al equipar en un aciatura si el Bool equipar esta activo
    /// </summary>
    public override void EfectoAlequipar()
    {

    }

    public override void EfectoAlequiparJugador()
    {

    }
    /// <summary>
    /// animaciones en modo  de batalla
    /// </summary>
    /// <param name="TwovsTwo"></param>
    /// <param name="QuienAtaca"></param>
    /// <param name="animationBrawler"></param>
    public override GameObject AnimationBrawler()
    {
        return Animationparticle;
    }


    /// <summary>
    /// efectos en modo normal
    /// </summary>
    /// <summary>
    /// efectos en modo normal
    /// </summary>
    public override IEnumerator FuncionNormal(int NumQuienRecaeEfecto, CrewildBase crewildBase)
    {
        yield return new WaitForSeconds(0.5f);
        yield return null;
    }

    /// <summary>
    /// efcctos en modo batalla
    /// </summary>
    public override IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla)
    {

        yield return new WaitForSeconds(0.5f);
        yield return null;

    }
}

[System.Serializable]
public class Escudo : BaseItem
{
    public Escudo()
    {
        this.Nombre = "escudo";
        this.descripcionItem = "Ataque se puede aprender 1";
        this.Coste = 100;
        this.cantidad = 1;
        this.Vendible = true;
        this.TieneEfectoNormal = false;
        this.TieneEfectoEnBatalla = false;
        this.ActivarPanelSeleccionParty = false;
        this.EfectoAlEquiparItenJugador = false;
        this.puntosefectos = 4;
        this.ImagenIten = MetodosAuxiliares.DevuelveSprite("Assets/Sprites/rpg-pack/atlas_.png", "escudo");
        // añadir
        //this.animaBattle = ;
        this.TipoItem = TipoItem.Ataques;
    }

  

    /// <summary>
    /// efecto al equipar en un aciatura si el Bool equipar esta activo
    /// </summary>
    public override void EfectoAlequipar()
    {

    }


    /// <summary>
    /// animaciones en modo  de batalla
    /// </summary>
    /// <param name="TwovsTwo"></param>
    /// <param name="QuienAtaca"></param>
    /// <param name="animationBrawler"></param>
    public override GameObject AnimationBrawler()
    {
        return Animationparticle;
    }



    /// <summary>
    /// efectos en modo normal
    /// </summary>
    /// <summary>
    /// efectos en modo normal
    /// </summary>
    public override IEnumerator FuncionNormal(int NumQuienRecaeEfecto, CrewildBase crewildBase)
    {
        yield return new WaitForSeconds(0.5f);
        yield return null;
    }

    /// <summary>
    /// efcctos en modo batalla
    /// </summary>
    public override IEnumerator Funcionbatalla(DefiniteObject quienEsAtacante, DefiniteObject quienEsAtacado, Animation AnimationBatalla)
    {

        yield return new WaitForSeconds(0.5f);
        yield return null;

    }

    public override void EfectoAlequiparJugador()
    {
       
    }
}


