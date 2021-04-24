using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoFueraDeBatalla : MonoBehaviour
{
    private int pasos = 1;

    public static bool VerificadorBool;


    public Pixelate pixelate;

     public  EfectoFueraDeBatalla()
    {
       
    } 
    public static  void verificador()
    {
        bool aux = false;
        foreach (CrewildBase Cw in informacionCrewild.SharedInstancia.CrewillInstancia )
        {
           
            if (Cw != null)
            {
                if (Cw.EstadosCrewild != EstadosEnum.None && Cw.EstadosCrewild != EstadosEnum.Paralize)
                {
                    Debug.Log("hay estado alterados");
                    VerificadorBool = true;
                    aux = true;
                    break;
                }
            }
            
        }

        if (aux == false)
        VerificadorBool = false;
    }


    public  void EjecutaEfecto()
    {
       
        foreach (CrewildBase Cw in informacionCrewild.SharedInstancia.CrewillInstancia)
        {

            if (Cw != null)
            {
                if (Cw.EstadosCrewild != EstadosEnum.None && Cw.EstadosCrewild != EstadosEnum.Paralize )
                {
                    Efecto(Cw); 
                }
            }

        }

        verificador();
    }


    public void Efecto(CrewildBase Cw)
    {
        switch (Cw.EstadosCrewild)
        {
            case EstadosEnum.poison:
                Cw.Hp -= Cw.hpTotal * 0.2f;
                Cw.Hp = (int)Cw.Hp;

                if (Cw.Hp <= 1)
                {
                    Cw.Hp = 1;
                    Cw.EstadosCrewild = EstadosEnum.None;
                }
                break;
        }
    }


    public  IEnumerator ContadorDepasos()
    {

        if (pasos > 3)
        {
            EjecutaEfecto();
          
            pixelate = FindObjectOfType<Pixelate>();
            pasos = 1;
            float tiempo = 0.01f;
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(tiempo);
               
                pixelate.pixelSizeX++;
           
            }
            for (int i = 0; i < 8; i++)
            {

                pixelate.pixelSizeX--;
            yield return new WaitForSeconds(tiempo);
            }
           
        }
        else           
        pasos++;

        yield return 0;
    }

        
    
}
