using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperaHp : MonoBehaviour
{
    /// <summary>
    /// Da una pausa mientras  en la secuencia para que el ejecucion del daño restado al Hp 
    /// </summary>
    public static bool PausaEjecucionEvento;
    public static float velDaño = 0.2f;

    /// <summary>
    /// se efectua el daño de manera 1 1  con respeto a la tipo de atacante
    /// </summary>
    /// <param name="Atancante"></param>
    /// <param name="ValorArecuperar"></param>
    /// <returns></returns>
    public static IEnumerator EjecutarRecuperaHpInvertido(string Atancante, float ValorArecuperar)
    {
        // se le resta 1 para que el valor de exactamente con la cantidad de daño restada
        ValorArecuperar--;
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();
        RecuperaHp.PausaEjecucionEvento = true;
        float Incrementador = 0;
        if (Atancante == "Enemy")
        {

            //  Time.deltaTime / VelocidadDecremento
            // LibreriaS.Batalla.ImagenEnemigo.sprite = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[1];
            while (ValorArecuperar > Incrementador)
            {
                if (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp >= LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal)
                {
                    break;
                }
                LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp += Time.deltaTime / velDaño;

                LibreriaS.Batalla.HpScrollbar[0].size = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp / LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;

                CalculoDaño.ActualizahpNormal(Atancante);

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
            //LibreriaS.Batalla.imagenePlayer.sprite = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[1];
            while (ValorArecuperar > Incrementador)
            {

                if (LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp >= LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal)
                {
                    break;
                }
                LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp += Time.deltaTime / velDaño;

                LibreriaS.Batalla.HpScrollbar[1].size = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp
                                                             / LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal;
                CalculoDaño.ActualizahpNormal(Atancante);

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
        RecuperaHp.PausaEjecucionEvento = false;
    }

    public static float calculoRecuperacion(string Atancante)
    {
        float ValueReturn = 0;

        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();

        if (Atancante == "Player")
        {
            ValueReturn = (LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal * 0.25f);

        }

        else if (Atancante == "Enemy")
        {
            ValueReturn = (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal * 0.25f);

        }

        return ValueReturn;
    }


    public static void calculoRecuperacion(CrewildBase CW)
    {
        CW.Hp += CW.hpTotal * 0.25f;
        CW.Hp = (int)CW.Hp;
    }

    public static IEnumerator AdsorverHp(CrewildBase  Atancante , CrewildBase defensor)
    {
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();
        RecuperaHp.PausaEjecucionEvento = true;
        float Incrementador = 0, ValorArecuperar = 0;

        ValorArecuperar = cantidadARecuperar(Atancante);

        while (ValorArecuperar > Incrementador)
        {
            if (defensor.Hp <= 0)
            {
                break;
            }

            ActualizaDatosHP(LibreriaS);
            Atancante.Hp += Time.deltaTime / 0.2f;
            defensor.Hp  -= Time.deltaTime / 0.2f;
            Incrementador += Time.deltaTime /0.2f;
            yield return null;
        }


        ajustaFinal(LibreriaS);



        RecuperaHp.PausaEjecucionEvento = false;

        yield return 0;
    }

    public static  void ActualizaDatosHP(libreriaDeScrips LibreriaS)
    {
        LibreriaS.Batalla.HpScrollbar[0].size = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp / LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;

        LibreriaS.Batalla.TextoHP[0].text = "HP: " + (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp + "/" +
                                                   (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].hpTotal;

        LibreriaS.Batalla.HpScrollbar[1].size = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp
                                                            / LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal;

        LibreriaS.Batalla.TextoHP[1].text = "HP: " + (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp + "/" +
                                                           (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].hpTotal;


    }
    public static float cantidadARecuperar(CrewildBase cw)
    {
        float valueRetur = 0;
        valueRetur = cw.hpTotal * 0.25f;
        return valueRetur;
    }

    public static void ajustaFinal(libreriaDeScrips LibreriaS)
    {
        LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp = (int)LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp;

        if (LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Hp > 0)
        {
            LibreriaS.Batalla.imagenePlayer.sprite = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[0];
        }
        else
        {
            LibreriaS.Batalla.imagenePlayer.sprite = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[1];
        }

        LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp = (int)LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp;


        if (LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Hp > 0)
        {
            LibreriaS.Batalla.ImagenEnemigo.sprite = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[0];
        }
        else
        {
            LibreriaS.Batalla.ImagenEnemigo.sprite = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[1];
        }

    }

    public static CrewildBase DevuelveCrewildUSed(string nombre)
    {
        CrewildBase valueReturn  = null;
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();
        if (nombre == "Player")
        {
         
            valueReturn = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer];

        }
        else
        {
            valueReturn = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy];
        }
        return valueReturn;
    }

}
