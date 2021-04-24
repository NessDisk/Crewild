using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MensajesGlovales 
{

    /// <summary>
    /// devuelve un string con la cantidad de el nombre del Iten
    /// </summary>
    /// <param name="cantidad"></param>
    /// <param name="NombreIten"></param>
    /// <returns></returns>
    public static string EntregarIten(CajaInventario Infoiten)
    {
        string Mensajes = "Optiene " + Infoiten.Cantidad + " " + Infoiten.NombreItem;
        return Mensajes;
    }

    /// <summary>
    /// recive los valores de las variables de resistencia a la captura mas puntos de captura y devuelve un dato mayor o menor que se 0
    /// </summary>
    /// <param name="Pcaptura"></param>
    /// <param name="Resistencia"></param>
    /// <returns></returns>
    public static float ResitenciaCriaturavPCaptura(float Pcaptura, float Resistencia)
    {
        float FloatARetornar = 0, ValorPCaptura, ValorPResistencia;

        ValorPCaptura = Random.Range(1, Pcaptura);
        ValorPResistencia = Random.Range(1, Resistencia);

        FloatARetornar = ValorPCaptura - ValorPResistencia;

        Debug.Log("Valor de captura"+ FloatARetornar);
        return FloatARetornar;

    }

}
