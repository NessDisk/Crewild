using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AllCrewild : MonoBehaviour
{

   
    void Star()
    {
        float ran = Random.value;

      
    }

}
/// <summary>
/// define  los tipos de criatura  asi como los tipos de ataques
/// </summary>
public enum TipoUniversalEnum
{
  
    None,
    Normal,
    Insect,
    Agros,
    Hielo,
    Agua,
    Plant,
    Energia,
    Espectro,
    Ave,
    Fuego,
    Explosiva,
    bestia
}

public enum GeneroEnum
{
    Macho,
    Hembra,
}

public enum EstadosEnum
{
    None,
    poison,
    Paralize,
    Fire,
    Ice,
    Bleeding,
    Confuse,
    Ko,
    Dead

}

public enum EmpatiaEnum
{
    Respete,
    leal,
    Normal,
    Molesto,
    Arisco,
    Bleeding,
    Odio,
    perturvado,
    cobarde

}

public class metodosAuxCrewildBase
{

    /// <summary>
    /// Define aleatoriamente cual es el genero de la crewild
    /// </summary>
    /// <returns></returns>
    public GeneroEnum GeneroRandon()
    {
        GeneroEnum GeneroAux;

        //Baron
        if (Random.value > 0.5f)
        {
            GeneroAux = GeneroEnum.Macho;

        }
        //Hembra
        else
        {

            GeneroAux = GeneroEnum.Hembra;

        }

        return GeneroAux;
    }

}





[System.Serializable]
public abstract class CrewildBase
{
    public string Nombre, NombreTaxonomico, descripcion;


    /// <summary>
    /// clip de animacion que define la postura(frente o espalda) de la criatura en el modo brauler.
    ///0 = estatico, 1 = Daño
    /// </summary>
    public Sprite[] animaCrewildFrentre = new Sprite[3], animaCrewildEspalda = new Sprite[3];


    /// <summary>
    /// define la animacion de la miniatura en el menu de selección
    /// </summary>
    public Sprite[] SpriteCrewildmenuSelec = new Sprite[2];



    public GeneroEnum genero = new GeneroEnum();


    public AttacksBase[] ataques = new AttacksBase[4];

    /// <summary>
    /// La vida del la criatura
    /// </summary>
    public float Hp, hpTotal = 50;

    /// <summary>
    /// Define el  nivel de raresa de las criaturas entre mas nivel de raresa mas dificil de encontrar
    /// </summary>
    public float NivelRaresa;

    //estadisticas de batalla
    public int attack, defence, speed, EspecialAttack, EspecialDefensa, Resistence;
    public float precision, Evacion;
    public float Nivel;

    public float ResistenciaAlacaptura;
    /// <summary>
    /// en el nivel 1 la experincia es de 50
    /// </summary>
    public float experiencia, experienciaTotal = 50;

    public float Cansancio = 30, Cansanciototal;
    //crianza
    public int sueño , hambre, Cordura, empatia, sueñoTotal, hambreTotal, CorduraTotal, empatiaTotal;

    public TipoUniversalEnum[] TipoDecrewild = new TipoUniversalEnum[2];

    public EstadosEnum EstadosCrewild = new EstadosEnum();

    public posturas[] posturasDeCriatura = new posturas[4]; 

    //--metodos Auxiliares;

    public metodosAuxCrewildBase metodosAux = new metodosAuxCrewildBase();

    public MetodosParaAtaque MetodoAuxAtaque = new MetodosParaAtaque();
    //aqui viene la clase Estilos o posturas de batalla





    /// <summary>
    /// define las estadisticas de batalla de la critaura
    /// </summary>
    abstract public void EstadisticasDeBatalla(int nivel);


    /// <summary>
    /// define las estadisticas de crianza  de la criatura
    /// </summary>
    abstract public void EstadisticasDecrianza();

    /// <summary>
    /// varieble que define como es el  incremento en los valores de incremento de nivel en las estadisticas
    /// </summary>
    abstract public void IncrementodeNivel();

    abstract public void valorDeLaExperiencia();

}


[System.Serializable]
public class crear_Crewild_Grismon_Insecto_Energia : CrewildBase
{

    public crear_Crewild_Grismon_Insecto_Energia(int LvlCritatura)
    {
        Nombre = "";
        NombreTaxonomico = "Grismon";
        descripcion = "Critura de los bosques tiene una punteria execlente";

        genero = metodosAux.GeneroRandon();

        TipoDecrewild[0] = TipoUniversalEnum.Plant;
        TipoDecrewild[1] = TipoUniversalEnum.None;

        /*  ataques[0] = new TripleImpacto();
          ataques[1] = new DisparoEnergia();
          ataques[2] = new Esquive();
          ataques[3] = null; */

        ataques[0] = new ataqueBarrida();
        ataques[1] = new DisparoEnergia();
        ataques[2] = new Esquive();
        ataques[3] = null;

        posturasDeCriatura[0] = new PosturaBasica();
        posturasDeCriatura[1] = new PosturaBasica();
        posturasDeCriatura[2] = new PosturaBasica();
        posturasDeCriatura[3] = new PosturaBasica();

        for (int i = 0; i < 4; i++)
        {
          //  ataques[i].Datos();
            posturasDeCriatura[i].Datos();
        }

        animaCrewildFrentre[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Grismon", "Grismon_frente_estatico");
        animaCrewildFrentre[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Grismon", "Grismon_frente_daño");
        animaCrewildFrentre[2] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Grismon", "Grismon_frente_Apunta");

        animaCrewildEspalda[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Grismon", "Grismon_espalda_estatico");
        animaCrewildEspalda[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Grismon", "Grismon_espalda_daño");
        animaCrewildEspalda[2] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Grismon", "Grismon_espalda_Apunta");

        SpriteCrewildmenuSelec[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/menus/MenusCriaturas", "Grismon_0");
        SpriteCrewildmenuSelec[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/menus/MenusCriaturas", "Grismon_1");
        EstadisticasDeBatalla(LvlCritatura);

        EstadisticasDecrianza();

        EstadosCrewild = EstadosEnum.None;

        NivelRaresa = 0.3f;

        ResistenciaAlacaptura = 10;

    }

   


   /// <summary>
   /// todas las estadisticas se incrementan desde el nivel cuando se instancia la critura
   /// </summary>
    public override void EstadisticasDeBatalla(int LevelCritatura)
    {
        Nivel = LevelCritatura;
        int AuxExp = 0, Auxattack = 0, Auxdefence = 0, Auxspeed = 0, Auxprecision = 0, Auxevation = 0, AuxEspecialAttack = 0,
            AuxDefenceSpecial = 0, AuxResistence = 0, AuxCansancio = 0,  AuxHpTotal = 0;


        //ajusta las variables acorde al nivel en variables aux
        for (int i = 1; i < LevelCritatura; i++)
        {

            AuxHpTotal +=  Random.Range(5, 7);

            Auxattack         +=  Random.Range(3, 5);
            Auxdefence        +=  Random.Range(1, 3);
            Auxspeed          +=  Random.Range(2, 4);
            Auxprecision      +=  3;
            Auxevation        +=  3;
            AuxEspecialAttack +=  Random.Range(1, 3);
            AuxDefenceSpecial +=  Random.Range(1, 3);
            AuxResistence     +=  Random.Range(3, 5);

            AuxCansancio      +=  Random.Range(1, 3);

            
          

            AuxExp +=  50 * (i/2);
        }

        //pone todo todos los valores de la variables en  acorde al nivel

        this.hpTotal = AuxHpTotal;
        this.Hp = this.hpTotal;

        this.attack =         Random.Range(15, 17)   + Auxattack;
        this.defence =        Random.Range(14, 16)   + Auxdefence;
        this.speed =          Random.Range(7,  11)   + Auxspeed;
        this.precision = 3;
        this.Evacion =   3;
        this.EspecialAttack = Random.Range(7,   3)   + AuxEspecialAttack;
        this.EspecialDefensa = Random.Range(7,   9)   + AuxDefenceSpecial;
        this.Resistence =     Random.Range(4,   7)   + AuxResistence;

        this.Cansanciototal      = Random.Range(40, 50)   + AuxCansancio;
        this.Cansancio = this.Cansanciototal;

        this.experienciaTotal = AuxExp;


    }


    public override void IncrementodeNivel()
    {
        Nivel++;

        hpTotal        +=       Random.Range(4,  6);

        attack         +=       Random.Range(1, 4);
        defence        +=       Random.Range(1, 3);
        speed          +=       Random.Range(2, 4);
      // precision      +=       Random.Range(1, 3);
      //  evation        +=       Random.Range(1, 3);
        EspecialAttack +=       Random.Range(2, 4);
        EspecialDefensa +=       Random.Range(1, 3);
        Resistence     +=       Random.Range(2, 4);

        Cansanciototal += Random.Range(4, 6);
        Cansancio = Cansanciototal;


        experienciaTotal += Nivel * 50;
        Hp = hpTotal;
    }


    public override void valorDeLaExperiencia()
    {
        //experienciaTotal = experienciaTotal * (1 / 2);

    }
    
    public override void EstadisticasDecrianza()
    {
        hambre        =  100;
        sueño         =  100;
        empatia       =  100;
      
        Cordura       =  100;
        hambreTotal   = 100;
        sueñoTotal    = 100;
        empatiaTotal  = 100;
    
        CorduraTotal  = 100;
    }

}


public class crear_Crewild_Eghi_salvaje_insecto : CrewildBase
{

    public crear_Crewild_Eghi_salvaje_insecto(int LvlCritatura)
    {

        Nombre = "";
        NombreTaxonomico = "Eghi";
        descripcion = "Criatura con muchas bocas, con apetito  boras";

        genero = metodosAux.GeneroRandon();

        TipoDecrewild[0] = TipoUniversalEnum.Normal;
        TipoDecrewild[1] = TipoUniversalEnum.None;

        ataques[0] = new ataqueBarrida();
        ataques[1] = new Esquive();
        ataques[2] = null;
        ataques[3] = null;

        posturasDeCriatura[0] = new PosturaBasica();
        posturasDeCriatura[1] = new PosturaBasica();
        posturasDeCriatura[2] = new PosturaBasica();
        posturasDeCriatura[3] = new PosturaBasica();



        for (int i = 0; i < 4; i++)
        {
           // ataques[i].Datos();
            posturasDeCriatura[i].Datos();
        }

        animaCrewildFrentre[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Eghi", "Eghi_frente_estatico");
        animaCrewildFrentre[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Eghi", "Eghi_frente_daño");

        animaCrewildEspalda[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Eghi", "Eghi_espalda_estatico");
        animaCrewildEspalda[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Eghi", "Eghi_espalda_daño");

        SpriteCrewildmenuSelec[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/menus/MenusCriaturas", "Eghi_0");
        SpriteCrewildmenuSelec[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/menus/MenusCriaturas", "Eghi_1");

        EstadisticasDeBatalla(LvlCritatura);

        EstadisticasDecrianza();

        EstadosCrewild = EstadosEnum.None;

        NivelRaresa = 0.2f;

        ResistenciaAlacaptura = 10;
    }

  


    /// <summary>
    /// todas las estadisticas se incrementan desde el nivel cuando se instancia la critura
    /// </summary>
    public override void EstadisticasDeBatalla(int LevelCritatura)
    {
        Nivel = LevelCritatura;
        int AuxExp = 0, Auxattack = 0, Auxdefence = 0, Auxspeed = 0, Auxprecision = 0, Auxevation = 0, AuxEspecialAttack = 0,
            AuxDefenceSpecial = 0, AuxResistence = 0, AuxCansancio = 0, AuxHpTotal = 0;

        //ajusta las variables acorde al nivel en variables aux
        for (int i = 1; i < LevelCritatura; i++)
        {
            AuxHpTotal += Random.Range(5, 7);

            Auxattack += Random.Range(3, 5);
            Auxdefence += Random.Range(1, 3);
            Auxspeed += Random.Range(2, 4);
          //  Auxprecision += Random.Range(1, 3);
          //  Auxevation += Random.Range(1, 3);
            AuxEspecialAttack += Random.Range(1, 3);
            AuxDefenceSpecial += Random.Range(1, 3);
            AuxResistence += Random.Range(3, 5);

            AuxCansancio += Random.Range(1, 3);

           

            AuxExp += 50 * (i/2);
        }

        //pone todo todos los valores de la variables en  acorde al nivel
        this.hpTotal = AuxHpTotal;
        this.Hp = this.hpTotal;

        this.attack = Random.Range(15, 17) + Auxattack;
        this.defence = Random.Range(14, 16) + Auxdefence;
        this.speed = Random.Range(7, 11) + Auxspeed;
        this.precision = 3;
        this.Evacion =   3;
        this.EspecialAttack = Random.Range(7, 3) + AuxEspecialAttack;
        this.EspecialDefensa = Random.Range(7, 9) + AuxDefenceSpecial;
        this.Resistence = Random.Range(4, 7) + AuxResistence;

        this.Cansanciototal = Random.Range(40, 50) + AuxCansancio;
        this.Cansancio = this.Cansanciototal;
       
        this.experienciaTotal = AuxExp;


    }


    public override void IncrementodeNivel()
    {
        this.Nivel++;
        this.hpTotal += Random.Range(5, 7);

        attack += Random.Range(1, 4);
        defence += Random.Range(1, 3);
        speed += Random.Range(2, 4);
      //  precision += Random.Range(1, 3);
      //  evation += Random.Range(1, 3);
        EspecialAttack += Random.Range(2, 4);
        EspecialDefensa += Random.Range(1, 3);
        Resistence += Random.Range(2, 4);



        Cansanciototal += Random.Range(4, 6);
        Cansancio = Cansanciototal;
        experienciaTotal += experienciaTotal * (1 / 2);
        Hp = hpTotal;
    }


    public override void valorDeLaExperiencia()
    {


    }

    public override void EstadisticasDecrianza()
    {
        hambre = 100;
        sueño = 100;
        empatia = 100;        
        Cordura = 100;
        hambreTotal = 100;
        sueñoTotal = 100;
        empatiaTotal = 100;    
        CorduraTotal = 100;
    }

}
