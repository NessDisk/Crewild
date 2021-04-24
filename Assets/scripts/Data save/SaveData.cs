using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveDataCrewild
{

    public string nombre, nombreTaxonomico, Genero;

    public string[] Ataques = new string[4];

    public float hp, hpTotal;

        /// <summary>
        /// estadisisticas
        /// </summary>
    public int ataque, defensa, velocida, ataqueEspecial, DefensaEspecial, Resistencia;

    public float presicion, evacion;

    public int Nivel;

    public float experiencia, experienciaTotal;

    public float Cansancio , Cansanciototal;

    //crianza
    public int sueño, hambre,  Cordura, empatia, sueñoTotal, hambreTotal, CorduraTotal, empatiaTotal;


    public  SaveDataCrewild(CrewildBase DataCrewild)
    {
         nombre = DataCrewild.Nombre;
         nombreTaxonomico = DataCrewild.NombreTaxonomico;
         Genero = DataCrewild.genero.ToString();

        
        for (int i = 0 ;i < DataCrewild.ataques.Length; i++)
        {
            if (DataCrewild.ataques[i]!= null)
            {
                Ataques[i] = DataCrewild.ataques[i].nombreAtaque;
            }
            
        }
       
          hp = (int)DataCrewild.Hp;
        
        hpTotal = DataCrewild.hpTotal;


        //estadisticias
        ataque = DataCrewild.attack;
        defensa = DataCrewild.defence;
        velocida = DataCrewild.speed;
        presicion = DataCrewild.precision;
        evacion = DataCrewild.Evacion;
        ataqueEspecial = DataCrewild.EspecialAttack;
        DefensaEspecial = DataCrewild.EspecialDefensa;
        Resistencia = DataCrewild.Resistence;

        Nivel = (int)DataCrewild.Nivel;

        experiencia = DataCrewild.experiencia;
        experienciaTotal = DataCrewild.experienciaTotal;

        Cansancio = DataCrewild.Cansancio;
        Cansanciototal = DataCrewild.Cansanciototal;

        sueño = DataCrewild.sueño;
        hambre = DataCrewild.hambre;
        Cordura = DataCrewild.Cordura;
        empatia = DataCrewild.empatia;

        sueñoTotal = DataCrewild.sueñoTotal;
        hambreTotal = DataCrewild.hambreTotal;
        CorduraTotal = DataCrewild.CorduraTotal;
        empatiaTotal = DataCrewild.empatiaTotal;

    }

}


[System.Serializable]
public class SaveDataIten
{
 public   string NombreIten;
 public   int Cantidad;
    

   public SaveDataIten(BaseItem ItenBase)
    {
        Debug.Log("Estoy guardando: "+ ItenBase.Nombre);
        NombreIten = ItenBase.Nombre;
        Cantidad = ItenBase.cantidad;
    }
}

[System.Serializable]
public class SaveDataInfoGloval
{
    public bool[] Recompensas = new bool[8];
    public int Dinero;
    /// <summary>
    /// posicion  relativa del personaje siendo  0 = x , 1 = y , 2 = z
    /// </summary>
    public float[] posicion;
    public string nombreZona;
    public string itenEquipado;

    public SaveDataInfoGloval(SaveDataInfoGloval InfoGloval)
    {
        Debug.Log("Estoy guardando datos Glovales: ");
        for (int i = 0; i < 8 ; i++)
        {
            Recompensas[i] = InfoGloval.Recompensas[i];
        }
        Dinero = InfoGloval.Dinero;
        posicion = InfoGloval.posicion;
        nombreZona = InfoGloval.nombreZona;

        if (InfoGloval.itenEquipado != null)
            itenEquipado = InfoGloval.itenEquipado;
        else
            itenEquipado = null;


    }

    public SaveDataInfoGloval()
    {
        Recompensas = new bool[8];
        posicion = new float[3];
    }
}