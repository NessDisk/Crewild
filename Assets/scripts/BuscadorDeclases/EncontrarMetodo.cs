using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Metodo que permite encontrar la clase hija de la clases de variios metodos 
/// </summary>
public class EncontrarMetodo : MonoBehaviour
{
    

    public static CrewildBase EncontrarCrewild(string NombreClase)
    {
        int nivelbase = 2;
        CrewildBase ClaseARetornar = null;
        switch (NombreClase)
        {
            case "Grismon":
                ClaseARetornar = new crear_Crewild_Grismon_Insecto_Energia(nivelbase);
                break;
            case "Eghi":
                ClaseARetornar = new crear_Crewild_Eghi_salvaje_insecto(nivelbase);
                break;

            case "Mixio":
                ClaseARetornar = new Mixio(nivelbase);
                break;

            case "Ouno":
                ClaseARetornar = new Ouno(nivelbase);
                break;
            case "Xilaxi":
                ClaseARetornar = new Xilaxi(nivelbase);
                break;
            case "Kraten":
                ClaseARetornar = new Kraten(nivelbase);
                break;
            case "Ihluv":
                ClaseARetornar = new Ihluv(nivelbase);
                break;

            default:
                Debug.Log("no se ha encontra criatura:" + NombreClase + "nombre a Corregir");
                break;
        }
        //ClaseARetornar.Datos(1);
        return ClaseARetornar;

    }

    /// <summary>
    /// Metodo que devuelve la critatura y segun el nivel
    /// </summary>
    /// <param name="NombreClase"></param>
    /// <param name="nivel"></param>
    /// <returns></returns>
    public static CrewildBase EncontrarCrewild(string NombreClase, int nivel)
    {
        CrewildBase ClaseARetornar = null;
        switch (NombreClase)
        {
            case "Grismon":
                ClaseARetornar = new crear_Crewild_Grismon_Insecto_Energia(nivel);
                break;
            case "Eghi":
                ClaseARetornar = new crear_Crewild_Eghi_salvaje_insecto(nivel);
                break;

            case "Mixio":
                ClaseARetornar = new Mixio(nivel);
                break;

            case "Ouno":
                ClaseARetornar = new Ouno(nivel);
                break;
            case "Xilaxi":
                ClaseARetornar = new Xilaxi(nivel);
                break;

            case "Kraten":
                ClaseARetornar = new Kraten(nivel);
                break;
            case "Ihluv":
                ClaseARetornar = new Ihluv(nivel);
                break;
                //segunda tanda de Crewilds
            case "Keren":
                ClaseARetornar = new Keren(nivel);
                break;
            case "Kanget":
                ClaseARetornar = new Kanget(nivel);
                break;
            case "Artflow":
                ClaseARetornar = new Artflow(nivel);
                break;
            case "Beslin":
                ClaseARetornar = new Beslin(nivel);
                break;
            case "Retolizar":
                ClaseARetornar = new Retolizar(nivel);
                break;
            case "Dechet":
                ClaseARetornar = new Dechet(nivel);
                break;
            case "Silvere":
                ClaseARetornar = new Silvere(nivel);
                break;
            case "Chibull":
                ClaseARetornar = new Chibull(nivel);
                break;
            case "Rochad":
                ClaseARetornar = new Rochad(nivel);
                break;
            case "Vieper":
                ClaseARetornar = new Vieper(nivel);
                break;
            case "Kabat":
                ClaseARetornar = new Kabat(nivel);
                break;
            case "Tmand":
                ClaseARetornar = new Tmand(nivel);
                break;
            case "Ashsa":
                ClaseARetornar = new Ashsa(nivel);
                break;
            default:
                Debug.Log("no se ha encontra criatura:" + NombreClase + "nombre a Corregir");
                break;


        }

        return ClaseARetornar;

    }

    /// <summary>
    /// pasandole el nombre se retornan las clases  hija  a utilizar 
    /// </summary>
    /// <param name="NombreAtaque"></param>
    /// <returns></returns>
    public static AttacksBase EncontrarAtaques(string NombreAtaque)
    {
        AttacksBase ClaseARetornar = null;
        switch (NombreAtaque)
        {
            case "Barrida":
                ClaseARetornar = new ataqueBarrida();
                break;
            case "Disp de Energia":
                ClaseARetornar = new DisparoEnergia();
                break;
            case "Esquive":
                ClaseARetornar = new Esquive();
                break;
            case "Triple Impacto":
                ClaseARetornar = new TripleImpacto();
                break;
            case "PInchosVenenoso":
                ClaseARetornar = new PInchosVenenoso();
                break;

            case "Cura":
                ClaseARetornar = new Cura();
                break;

            case "Adsorver":
                ClaseARetornar = new Adsorver();
                break;

            case "Ataque Electrico":
                ClaseARetornar = new AtaqueElectrico();
                break;
            case "Mega puño":
                ClaseARetornar = new MegaPuño();
                break;
            case "Surf":
                ClaseARetornar = new Surf();                
                break;
            default:

                Debug.Log("no se ha Ataque:" + NombreAtaque + " Corregir o no existe");
                Debug.Break();
                break;


        }

        return ClaseARetornar;
    }


    /// <summary>
    /// este metodo es para tomar un sprite especifico del editor en una textura2d  dentro de su array
    /// </summary>
    /// <param name="RutadeTextura2d"> es la ruta dentro del editor ejemplo ("Assets/Sprites/rpg-pack/atlas_.png)"</param>
    /// <param name="NombreDelSprite">Es el nombre del sprite dentro del array que forma la textura2d </param>
    /// <returns></returns>
    public static Sprite DevuelveSprite(string RutadeTextura2d, string NombreDelSprite)
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



    /// <summary>
    /// se utiliza para retornar el nombre la clase con una variable de texto
    /// </summary>
    /// <param name="NombreDelaclase">Nombre de la clase  a la que sequiere retornar</param>
    /// <returns></returns>
    public static BaseItem EncontrarItem(string NombreDelaclase)
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
            default:
                Debug.Log("Este iten no existe o esta mal escrito =" + NombreDelaclase);
                Debug.Break();
                break;
        }

        return AuxIten;
    }


    /// <summary>
    /// Devuelve un valor  sino hay critarus a mano.
    /// </summary>
    /// <returns></returns>
    public static bool DefineSiHaycriaturas()
    {
        bool ValorARetornar = false;
        libreriaDeScrips LibreriaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();


        for (int i = 0; i < 6; i++)
        {
            if (LibreriaS.informacionCrewild.CrewillInstancia[i] != null)
            {
                ValorARetornar = true;
                break;
            }
        }
        return ValorARetornar;
    }


//--- panel de seleccion si/no o Yes/No

    ///define la posicon del selector cursor 
    public static void definirMovimientoSelector_Yes_no(int numPos)
    {
        RectTransform selector = GameObject.Find("Canvas/box Election/Corchete").GetComponent<RectTransform>();

        if (numPos == 0)
            selector.anchoredPosition = new Vector2(0.5f, 27f);
        else if (numPos == 1)
            selector.anchoredPosition = new Vector2(0.5f, -29f);
    }

    /// <summary>
    /// define el numero de poasicion de cursos  
    /// </summary>
    /// <param name="numAAjustar"></param>
    /// <returns></returns>
    public static int numDefinirNumSelector(int numAAjustar)
    {
        int numARetornar = numAAjustar;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            numARetornar++;

        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            numARetornar--;

        if (numARetornar > 1)
            numARetornar = 0;
        else if (numARetornar < 0)
            numARetornar = 1;

        return numARetornar;
    }

    //  añadir nuevas criaturas

    public static void AnadirABulleWild(CrewildBase CriaturaAañadir)
    {
        int contador = 0;
       libreriaDeScrips  LibreriaS  = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();
        bool validadorAgrupacion = false;
        foreach (CrewildBase Cb in LibreriaS.informacionCrewild.CrewillInstancia)
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
            LibreriaS.informacionCrewild.CrewillInstancia[contador] = CriaturaAañadir;
            //LibreriaS.SeleccionDeCriaturas.BulletTransf[contador].gameObject.SetActive(true);

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
                if (CriaturaAañadir.ataques[i] != null)
                {
                    CriaturaAañadir.ataques[i].cantidadDeusos = CriaturaAañadir.ataques[i].cantidadDeusosTotales;
                }

            }

            LibreriaS.PcScritp.AñadirCriatura(CriaturaAañadir);

            LibreriaS.SeleccionDeCriaturas.actualizaDatos();
        }



    }

}

