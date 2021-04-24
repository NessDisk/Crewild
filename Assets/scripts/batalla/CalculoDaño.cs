using System.Collections;
using UnityEngine;

public class CalculoDaño : MonoBehaviour
{
    /// <summary>
    /// Da una pausa mientras  en la secuencia para que el ejecucion del daño restado al Hp 
    /// </summary>
    public static bool PausaEjecucionEvento;



    public static float velDaño  = 0.2f;


    //devuelve  el valor de cuanto daño recive el oponente
    public static float CalcularDaño(string Atacante, string NombreDelAtaque)
    {
        float Daño = 0;

        float Nivel = 0, Ataque = 0, PoderAtaque = 0, Defensa = 0, Bonificacion = 1, Efectividad = 1;

        int variante = (int)Random.Range(85, 100);

        Capturardatos DatosAUsar = DatosUsar(Atacante);

        Ataque = DatosAUsar.PuntosAtaque;
        Defensa = DatosAUsar.Puntosdefensa;
        Nivel = DatosAUsar.nivel;
        PoderAtaque = EncontrarMetodo.EncontrarAtaques(NombreDelAtaque).PoderAtaque;
        Bonificacion = Bonificaciones(EncontrarMetodo.EncontrarAtaques(NombreDelAtaque).TipoDeAtaque, Atacante);
        Efectividad =  EfectividadValor(EncontrarMetodo.EncontrarAtaques(NombreDelAtaque).TipoDeAtaque, Atacante);
        /* Daño=0.01×B×E×V×((0.2×N+1)×A×P/25×D+2)
         N = Nivel del Pokémon que ataca.
         A = Cantidad de ataque o ataque especial del Pokémon. Si el ataque que utiliza el Pokémon es físico se toma la cantidad de ataque y si es especial se toma la cantidad de ataque especial.
         P = Poder del ataque, el potencial del ataque.
         D = Cantidad de defensa del Pokémon rival. Si el ataque que hemos utilizado es físico cogeremos la cantidad de defensa del Pokémon rival, si el ataque que hemos utilizado es especial, se coge la cantidad de defensa especial del Pokémon rival.
         B = Bonificación. Si el ataque es del mismo tipo que el Pokémon que lo lanza toma un valor de 1.5, si el ataque es de un tipo diferente al del Pokémon que lo lanza toma un valor de 1.
         E = Efectividad. Puede tomar los valores de 0, 0.25, 0.5, 1, 2 y 4.
         V = Variación. Es una variable que comprende todos los valores discretos entre 85 y 100 (ambos incluidos).
         */
        Daño = (0.01f * Bonificacion * Efectividad * variante )* ((((0.2f * (Nivel + 1)) * Ataque * PoderAtaque) / (25 * Defensa)) + 2);

        

        return Daño;
    }

    //devuelve  el valor de cuanto daño recive el oponente
    public static float CalcularDañoEspecial(string Atacante, string NombreDelAtaque)
    {
        float Daño = 0;

        float Nivel = 0, Ataque = 0, PoderAtaque = 0, Defensa = 0, Bonificacion = 1, Efectividad = 1;

        int variante = (int)Random.Range(85, 100);

        Capturardatos DatosAUsar = DatosEspeciales(Atacante);

        Ataque = DatosAUsar.PuntosAtaque;
        Defensa = DatosAUsar.Puntosdefensa;
        Nivel = DatosAUsar.nivel;
        PoderAtaque = EncontrarMetodo.EncontrarAtaques(NombreDelAtaque).PoderAtaque;
        Bonificacion = Bonificaciones(EncontrarMetodo.EncontrarAtaques(NombreDelAtaque).TipoDeAtaque, Atacante);
        Efectividad = EfectividadValor(EncontrarMetodo.EncontrarAtaques(NombreDelAtaque).TipoDeAtaque, Atacante);
        /* Daño=0.01×B×E×V×((0.2×N+1)×A×P/25×D+2)
         N = Nivel del Pokémon que ataca.
         A = Cantidad de ataque o ataque especial del Pokémon. Si el ataque que utiliza el Pokémon es físico se toma la cantidad de ataque y si es especial se toma la cantidad de ataque especial.
         P = Poder del ataque, el potencial del ataque.
         D = Cantidad de defensa del Pokémon rival. Si el ataque que hemos utilizado es físico cogeremos la cantidad de defensa del Pokémon rival, si el ataque que hemos utilizado es especial, se coge la cantidad de defensa especial del Pokémon rival.
         B = Bonificación. Si el ataque es del mismo tipo que el Pokémon que lo lanza toma un valor de 1.5, si el ataque es de un tipo diferente al del Pokémon que lo lanza toma un valor de 1.
         E = Efectividad. Puede tomar los valores de 0, 0.25, 0.5, 1, 2 y 4.
         V = Variación. Es una variable que comprende todos los valores discretos entre 85 y 100 (ambos incluidos).
         */
        Daño = (0.01f * Bonificacion * Efectividad * variante) * ((((0.2f * (Nivel + 1)) * Ataque * PoderAtaque) / (25 * Defensa)) + 2);



        return Daño;
    }


    static Capturardatos DatosUsar(string Atancante)
    {
        Capturardatos Datoscaptados = new Capturardatos();

        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();

        if (Atancante == "Player")
        {
            Datoscaptados.PuntosAtaque = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].attack;
            Datoscaptados.nivel = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Nivel;

            Datoscaptados.Puntosdefensa = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].defence;

        }

        else if (Atancante == "Enemy")
        {
            Datoscaptados.PuntosAtaque = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].attack;
            Datoscaptados.nivel = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Nivel;

            Datoscaptados.Puntosdefensa = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].defence;

        }



        return Datoscaptados;

    }

    static Capturardatos DatosEspeciales(string Atancante)
    {
        Capturardatos Datoscaptados = new Capturardatos();

        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();

        if (Atancante == "Player")
        {
            Datoscaptados.PuntosAtaque = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].EspecialAttack;
            Datoscaptados.nivel = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Nivel;

            Datoscaptados.Puntosdefensa = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].EspecialDefensa;

        }

        else if (Atancante == "Enemy")
        {
            Datoscaptados.PuntosAtaque = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].EspecialAttack;
            Datoscaptados.nivel = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Nivel;

            Datoscaptados.Puntosdefensa = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].EspecialDefensa;

        }



        return Datoscaptados;

    }




    class Capturardatos
    {
        public float nivel,  PuntosAtaque, Puntosdefensa;
    }

    

    // efectua descuento de deño hp desde los impactos del brawler


    public static  IEnumerator EjecutarDaño(string Atancante, float ValorDaño)
    {
        // se le resta 1 para que el valor de exactamente con la cantidad de daño restada
        ValorDaño--;
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();
        CalculoDaño.PausaEjecucionEvento = true;
        float Incrementador = 0;
        if (Atancante == "Player")
        {
           
            //  Time.deltaTime / VelocidadDecremento
            LibreriaS.Batalla.ImagenEnemigo.sprite = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[1];
            while (ValorDaño > Incrementador)
            {
                if (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp <= 0)
                {
                    break;
                }
                LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp -= Time.deltaTime / velDaño;

                LibreriaS.Batalla.HpScrollbar[0].size = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp / LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;

                ActualizahpDañoBatalla(Atancante);


               Incrementador += Time.deltaTime / velDaño;

              

                yield return null;
            }

            LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp = (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp;


            if (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp > 0)
            {
                LibreriaS.Batalla.ImagenEnemigo.sprite = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[0];
             }
            
        }
        else if (Atancante == "Enemy")
        {

            //  Time.deltaTime / VelocidadDecremento
            LibreriaS.Batalla.imagenePlayer.sprite = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[1];
            while (ValorDaño > Incrementador)
            {

                if (LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp <= 0)
                {
                    break;
                }
                LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp -= Time.deltaTime / velDaño;

                LibreriaS.Batalla.HpScrollbar[1].size = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp 
                                                             / LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal;

               ActualizahpDañoBatalla(Atancante);

                Incrementador += Time.deltaTime / velDaño;



                yield return null;
            }


            LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp = (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp;

            if (LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp > 0)
            {
                LibreriaS.Batalla.imagenePlayer.sprite = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[0];
            }

        }
        yield return 0;
        CalculoDaño.PausaEjecucionEvento = false;
    }

    /// <summary>
    /// se efectua el daño de manera 1 1  con respeto a la tipo de atacante
    /// </summary>
    /// <param name="Atancante"></param>
    /// <param name="ValorDaño"></param>
    /// <returns></returns>
    public static IEnumerator EjecutarDañoInvertido(string Atancante, float ValorDaño)
    {
        // se le resta 1 para que el valor de exactamente con la cantidad de daño restada
        ValorDaño--;
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();
        CalculoDaño.PausaEjecucionEvento = true;
        float Incrementador = 0;
        if (Atancante == "Enemy")
        {

            //  Time.deltaTime / VelocidadDecremento
            LibreriaS.Batalla.ImagenEnemigo.sprite = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[1];
            while (ValorDaño > Incrementador)
            {
                if (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp <= 0)
                {
                    break;
                }
                LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp -= Time.deltaTime / velDaño;

                LibreriaS.Batalla.HpScrollbar[0].size = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp / LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;

                ActualizahpNormal(Atancante);

                Incrementador += Time.deltaTime / velDaño;

                yield return null;
            }

            LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp = (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp;


            if (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp > 0)
            {
                LibreriaS.Batalla.ImagenEnemigo.sprite = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[0];
            }

        }
        else if (Atancante == "Player") 
        {
            //  Time.deltaTime / VelocidadDecremento
            LibreriaS.Batalla.imagenePlayer.sprite = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[1];
            while (ValorDaño > Incrementador)
            {

                if (LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp <= 0)
                {
                    break;
                }
                LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp -= Time.deltaTime / velDaño;

                LibreriaS.Batalla.HpScrollbar[1].size = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp
                                                             / LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal;
                ActualizahpNormal(Atancante);

                Incrementador += Time.deltaTime / velDaño;

                yield return null;
            }

            LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp = (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp;

            if (LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp > 0)
            {
                LibreriaS.Batalla.imagenePlayer.sprite = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[0];
            }

        }
        yield return 0;
        CalculoDaño.PausaEjecucionEvento = false;
    }

    /// <summary>
    /// actualiza daño en secuencia de batalla normal
    /// </summary>
    /// <param name="Atancante"></param>
    public static void ActualizahpDañoBatalla(string Atancante)
    {
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();
        if (Atancante == "Player")
        {
            LibreriaS.Batalla.TextoHP[0].text = "HP: " + (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp + "/" +
                                                    (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;        }
        else if (Atancante == "Enemy") 
        {
            LibreriaS.Batalla.TextoHP[1].text = "HP: " + (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp + "/" +
                                                            (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal;
        }       
    }
  
    public static void ActualizahpNormal(string SobrequienActua)
    {
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();
        if (SobrequienActua == "Enemy")
        {
            LibreriaS.Batalla.TextoHP[0].text = "HP: " + (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp + "/" +
                                                    (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;
        }
        else if (SobrequienActua == "Player")
        {
            LibreriaS.Batalla.TextoHP[1].text = "HP: " + (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp + "/" +
                                                            (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal;
        }


    }
    /// <summary>
    /// devuelve el valor si un a ataque tiene bonificacion de 1 o 1.5 dependiendo de si hay o no bonificacion
    /// </summary>
    /// <param name="tiposAtaques"></param>
    /// <param name="Atacante"></param>
    /// <returns></returns>
    public static float Bonificaciones(TipoUniversalEnum tiposAtaques,string Atacante)
    {
        float ValorAdevolver = 1;
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();

        TipoUniversalEnum[] tipoDecrewild = new TipoUniversalEnum[2];

        if (Atacante == "Player")
        {
            tipoDecrewild = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].TipoDecrewild;
        }
        else if (Atacante == "Enemy")
        {
            tipoDecrewild = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].TipoDecrewild;
        }

       
        switch (tiposAtaques)
        {
            case TipoUniversalEnum.Normal:
                if (tipoDecrewild[0] ==  TipoUniversalEnum.Normal
                 || tipoDecrewild[1] ==  TipoUniversalEnum.Normal
                    )
                {
                     ValorAdevolver = 1.5f;
                }

                break;

            case TipoUniversalEnum.Energia:
                if (tipoDecrewild[0] == TipoUniversalEnum.Energia
                 || tipoDecrewild[1] == TipoUniversalEnum.Energia)
                {
                    ValorAdevolver = 1.5f;
                }

                break;

            default:
                ValorAdevolver = 1;
                break;
        }

        return ValorAdevolver;

    }

    public static float EfectividadValor(TipoUniversalEnum tiposAtaques, string Atacado)
    {
        float ValorADevolver = 1;
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();


        TipoUniversalEnum[] tipoDecrewild = new TipoUniversalEnum[2];
        //define el tipo del Crewild
        if (Atacado == "Player")
            tipoDecrewild = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].TipoDecrewild;
        
        else if (Atacado == "Enemy")    
            tipoDecrewild = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].TipoDecrewild;
        
        //define si es 1 o 2 los ciclos para la cantidad de efectividad  en el daño
        int cantidadDeCiclos = 1;
        
        if (tipoDecrewild[1] != TipoUniversalEnum.None)
        {
            cantidadDeCiclos = 2;
        }
        for (int i  = 0;i < cantidadDeCiclos ; i++ )
        {
            ///el daño esta definido en decremento(total/2) o incremento(totalx2) hay algunos que tienen 0 de daño
            switch (tiposAtaques)
            {
                case TipoUniversalEnum.Normal:
                    if (tipoDecrewild[i] == TipoUniversalEnum.Agros
                       )
                    {
                        ValorADevolver = ValorADevolver * 2;
                    }
                    
                    break;

                case TipoUniversalEnum.Energia:
                    if (tipoDecrewild[i] == TipoUniversalEnum.Normal)
                    {
                        ValorADevolver = ValorADevolver * 2;
                    }

                    else if (tipoDecrewild[i] == TipoUniversalEnum.Hielo)
                    {
                        ValorADevolver = ValorADevolver / 2;
                    }

                    break;

                default:
                    ValorADevolver = 1;
                    break;
            }
        }
        



        return ValorADevolver;
    }

    /// <summary>
    /// devuelvo si el efecto o accion se ejecuta en el ataque
    /// </summary>
    /// <param name="porcentajeProbailidad"></param>
    /// <returns></returns>
    public static bool ProbailidadEfecto(float porcentajeProbailidad)
    {
        bool BoolReturn = false;
        float ActualProbabilidad = Random.Range(0,100);

        if (porcentajeProbailidad >= ActualProbabilidad)
            BoolReturn = true;
        else
            BoolReturn = false;

        return BoolReturn;
    }

}

