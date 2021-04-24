using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcionesAnimation : MonoBehaviour
{
    Animation animation;
    public string NombreAnimacion;

    public bool Disparador;
    public static bool eventoTrigger;
    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    public OpcionesAnimation(Animation anim)
    {
        animation = anim;

    }

    public void pause()
    {
        animation[NombreAnimacion].speed = 0;
        
    }

    public void play()
    {
        animation[NombreAnimacion].speed = 1;
    }

    public void SalirAnimacion()
    {
        animation.Stop();
    }

    public void DisparadorEvento()
    {
        Disparador = true;
        eventoTrigger = true; 

    }

    public void SalirAnimatorClip(string NombreClipAnimatior)
    {
        GetComponent<Animator>().SetBool(NombreClipAnimatior,false);
        transform.localScale = new Vector2(1, 1);
        FindObjectOfType<movimiento>().DisparadorEvento = false;
        FindObjectOfType<movimiento>().pos = FindObjectOfType<movimiento>().transform.position; 
    }

}
