using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculosAcierto : MonoBehaviour
{
    /// <summary>
    /// calcula el valor de un ataque si acierta o falla en un combate si es menor que 1 es  un ataque fallido
    /// </summary>
    /// <returns></returns>
    public static bool CalculoAcierto(string Atacante, string NombreDelAtaque)
    {
        //define si se ha  acerta o fallado el ataque
        bool AciertoODesacierto;
        float ProbabilidadDeAcierto = 0;
        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();

        float ValorPresicion, ValorEvacion;

        PresicionEvacionValor Valores = RetornaPrecisionEvacion(Atacante);

        ValorPresicion = Valores.ValorPresicion;
        ValorEvacion = Valores.ValorEvacion;

        AttacksBase AtaqueUsado = EncontrarMetodo.EncontrarAtaques(NombreDelAtaque);

        ProbabilidadDeAcierto = AtaqueUsado.PrecisionDelAtaque * (ValorPresicion / ValorEvacion);

        AciertoODesacierto =  AciertoDesacierto(ProbabilidadDeAcierto);


        return AciertoODesacierto;
    }

    static PresicionEvacionValor RetornaPrecisionEvacion(string Atancante)
    {
        PresicionEvacionValor ValorEvacionPresicion = new PresicionEvacionValor();

        libreriaDeScrips LibreriaS = FindObjectOfType<libreriaDeScrips>();

        if (Atancante == "Player")
        {
            ValorEvacionPresicion.ValorPresicion = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].precision;
            ValorEvacionPresicion.ValorEvacion = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].Evacion;

        }

        else if (Atancante == "Enemy")
        {
            ValorEvacionPresicion.ValorPresicion = LibreriaS.Batalla.EnemigosBatalla[LibreriaS.Batalla.ActualSeNumEnemy].precision;
            ValorEvacionPresicion.ValorEvacion = LibreriaS.informacionCrewild.CrewillInstancia[LibreriaS.Batalla.ActualSelNumPlayer].Evacion;

        }

        return ValorEvacionPresicion;

    }

    class PresicionEvacionValor
    {
        public float ValorEvacion, ValorPresicion;
    }

    static bool AciertoDesacierto(float ProbabilidadDeAcertar)
    {
        bool AciertaODesacierto = false;

        ProbabilidadDeAcertar = ProbabilidadDeAcertar / 100;


        if (ProbabilidadDeAcertar >= Random.value)
        {
            AciertaODesacierto = true;
        }

        else if (ProbabilidadDeAcertar < Random.value)
        {
            AciertaODesacierto = false;

        }

        

        return AciertaODesacierto;
    }

}
