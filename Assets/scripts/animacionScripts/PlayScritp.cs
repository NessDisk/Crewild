using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScritp : MonoBehaviour
{
    public bool activador;

    public ModosRepruduccion ModoReproduccion;

    public  bool DetenerAnimacion;
    public float VelocidadAnimacion;
    Image Spriterender;
    public Sprite[] animacion;

    public int frame;
    


    // Start is called before the first frame update
    void Start()
    {
        Spriterender = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activador == true)
        {
            if (ModoReproduccion == ModosRepruduccion.Loop)
            {
                StartCoroutine(indefinida());
                activador = false;
            }
            else if (ModoReproduccion == ModosRepruduccion.Oneplay)
            {
                StartCoroutine(OnePlay());
                activador = false;
            }
            else if (ModoReproduccion == ModosRepruduccion.Frame)
            {
                Frameespecifico();
            }
            
            
        }
    }

    IEnumerator indefinida()
    {
        while (DetenerAnimacion == false)
        {
            for (int i = 0;i < animacion.Length; i++)
            {
                   Spriterender.sprite = animacion[i];
                   yield return new WaitForSeconds(VelocidadAnimacion);
            }
           
        }
        DetenerAnimacion = false;
        yield return 0;
    }

    /// <summary>
    /// recorre una ves el recorrido array con los perosnajes
    /// </summary>
    /// <returns></returns>
    IEnumerator OnePlay()
    {
       
            for (int i = 0; i < animacion.Length; i++)
            {
                Spriterender.sprite = animacion[i];
                yield return new WaitForSeconds(VelocidadAnimacion);
            }      
   
        yield return 0;
    }
    void Frameespecifico()
    {
        Spriterender.sprite = animacion[frame];
    }


}

/// <summary>
/// diferentes modos de reproduccion de animacion via script
/// </summary>
[System.Serializable]
public enum ModosRepruduccion
{
    Oneplay,
    Loop,
    Frame
}