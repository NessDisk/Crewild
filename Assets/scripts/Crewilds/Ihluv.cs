using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Ihluv : CrewildBase
{

    public Ihluv(int LvlCritatura)
    {
        Nombre = "";
        NombreTaxonomico = "Ihluv";
        descripcion = "Crewild de tipo  muy agresivo y hostil de salvaje.";

        genero = metodosAux.GeneroRandon();

        TipoDecrewild[0] = TipoUniversalEnum.Fuego;
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
            //  ataques[i].Datos();
            posturasDeCriatura[i].Datos();
        }

        animaCrewildFrentre[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "frente 1");
        animaCrewildFrentre[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "frente 2");
        animaCrewildFrentre[2] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "frente 3");

        animaCrewildEspalda[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "espalda 1");
        animaCrewildEspalda[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "espalda 2");
        animaCrewildEspalda[2] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "espalda 3");

        SpriteCrewildmenuSelec[0] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "miniatura 1");
        SpriteCrewildmenuSelec[1] = MetodoAuxAtaque.DevuelveSprite("Sprites/HojasDeCrewilds/Ihluv", "miniatura 2");
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
            Auxdefence += Random.Range(2, 4);
            Auxspeed += Random.Range(3, 5);
            Auxprecision += 3;
            Auxevation += 3;
            AuxEspecialAttack += Random.Range(2, 4);
            AuxDefenceSpecial += Random.Range(1, 3);
            AuxResistence += Random.Range(3, 5);

            AuxCansancio += Random.Range(1, 3);




            AuxExp += 50 * (i / 2);
        }

        //pone todo todos los valores de la variables en  acorde al nivel

        this.hpTotal = AuxHpTotal;
        this.Hp = this.hpTotal;

        this.attack = Random.Range(15, 17) + Auxattack;
        this.defence = Random.Range(14, 16) + Auxdefence;
        this.speed = Random.Range(7, 11) + Auxspeed;
        this.precision = 3;
        this.Evacion = 3;
        this.EspecialAttack = Random.Range(7, 3) + AuxEspecialAttack;
        this.EspecialDefensa = Random.Range(7, 9) + AuxDefenceSpecial;
        this.Resistence = Random.Range(4, 7) + AuxResistence;

        this.Cansanciototal = Random.Range(40, 50) + AuxCansancio;
        this.Cansancio = this.Cansanciototal;

        this.experienciaTotal = AuxExp;


    }


    public override void IncrementodeNivel()
    {
        Nivel++;

        hpTotal += Random.Range(4, 6);

        attack += Random.Range(1, 4);
        defence += Random.Range(1, 3);
        speed += Random.Range(2, 4);
        // precision      +=       Random.Range(1, 3);
        //  evation        +=       Random.Range(1, 3);
        EspecialAttack += Random.Range(2, 4);
        EspecialDefensa += Random.Range(1, 3);
        Resistence += Random.Range(2, 4);

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
