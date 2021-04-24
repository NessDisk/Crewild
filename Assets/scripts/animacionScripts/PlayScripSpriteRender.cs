using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScripSpriteRender : MonoBehaviour
{
    public bool activador;

    public ModosRepruduccion ModoReproduccion;

    public bool DetenerAnimacion;
    public bool DestruirAlFinalizar;
    public float VelocidadAnimacion;
    SpriteRenderer Spriterender;
    public Sprite[] animacion;

    public int frame;



    // Start is called before the first frame update
    void Start()
    {
        Spriterender = GetComponent<SpriteRenderer>();
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
            for (int i = 0; i < animacion.Length; i++)
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
        if (DestruirAlFinalizar)
        {
            Destroy(gameObject);
        }
        
        yield return 0;
    }

  
    void Frameespecifico()
    {
        Spriterender.sprite = animacion[frame];
    }


}

