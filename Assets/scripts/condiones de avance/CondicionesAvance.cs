using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondicionesAvance : MonoBehaviour
{
    public DefineCondicion CondicionTipo;
    string NombreObjeto;
    public bool CumpleCondicion;
    libreriaDeScrips LibreriaS;
    // Start is called before the first frame update
    void Start()
    {
        LibreriaS = GameObject.Find("Game Manager/").GetComponent<libreriaDeScrips>();
    }


    public void RevisaCondiones()
    {
        bool Destruir = false;
        if (CondicionTipo == DefineCondicion.Iten)
        {
            Inventario inventa = new Inventario();

            Destruir = inventa.DefineSiItenExiste(NombreObjeto);
        }

        else if (CondicionTipo == DefineCondicion.Crewild)
        {
            for (int i = 0; i < 6;i ++)
            {
                if (LibreriaS.informacionCrewild.CrewillInstancia[i] != null)
                {
                    Destruir = true;
                }
            }
        }
        else if (CondicionTipo == DefineCondicion.accion)
        {

        }
        if (Destruir == true)
        {
            DestroyImmediate(gameObject);
        }
    }
}

[System.Serializable]
public enum DefineCondicion
{
    /// <summary>
    /// se le tiene que dar nombre
    /// </summary>
    Iten,
    /// <summary>
    /// solo se usa en la condicion inicial para definir si el jugador tiene criaturas antes de salir del  primer pueblo
    /// </summary>
    Crewild,
    /// <summary>
    /// define si se cumplio con cierta condicion
    /// </summary>
    accion
}