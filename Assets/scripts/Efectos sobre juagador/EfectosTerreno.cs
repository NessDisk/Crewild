using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectosTerreno : MonoBehaviour
{
    #region Members
    [Tooltip("Si esta activo funciona con las variables del jugador si esta False funciona con las de los Npc")]
    public bool PlayerOrNpc;
    public float velocidadpisadas;
    private  Animator Efectoanimation;
    private SpriteRenderer SpriteRender;
    private movimiento mov;
    private Npc_movimiento movNpc;
    private GameObject Pisada;
   
    private bool ActivaPisadas;
    /// <summary>
    /// arriba : 0, derecha : 1, abajo : 2, isquierda : 3
    /// </summary>
    private Transform[] TranfPisadas = new Transform[4];

    bool EstoyAnimando, activaEfecto, EstoySobreAgua , primeraEntradaAgua;



   [SerializeField] private LayerMask Layernavegation , LayerNormal;

    #endregion


    #region Arena 

    bool activaEfectoArena ;
    private GameObject PisadaArena;
    #endregion

    #region Funciones

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Agua Pantanosa")
        {
            SpriteRender.enabled = true;
            activaEfecto = true;

            CancelInvoke("InvokeExiteCollider");
        }

        else  if (col.tag == "Agua")
        {
            EstoySobreAgua = true;

            movimiento.SharedInstancia.Layermask = Layernavegation;

            mov.animt.SetBool("Agua", EstoySobreAgua);
            CancelInvoke("InvokeExitAguaCollider");
        }

        else if (col.tag == "Arena")
        {
            activaEfectoArena = true;
            CancelInvoke("InvokeExitArenaCollider");
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Agua Pantanosa" && activaEfecto == true)
        {
            Invoke("InvokeExiteCollider", 0.05f);
        }

       else if (col.tag == "Agua" && EstoySobreAgua == true)
        {
            Invoke("InvokeExitAguaCollider", 0.025f);
            movimiento.SharedInstancia.Layermask = LayerNormal;
        }
       else  if (col.tag == "Arena" && activaEfectoArena == true)
        {
            print("exitArena");
            Invoke("InvokeExitArenaCollider", 0.05f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerOrNpc == true)
            mov = GetComponent<movimiento>();
        else if (PlayerOrNpc == false)
            movNpc = GetComponent<Npc_movimiento>();

        Pisada = Resources.Load<Object>("Sprites/Terreno/Vfx Terreno/pisada en Agua/pisa Agua") as GameObject;

        PisadaArena = Resources.Load<Object>("Sprites/Terreno/Vfx Terreno/Arena/Arena") as GameObject;

        Animator[] animations = new Animator[gameObject.GetComponentsInChildren<Animator>().Length];
                    animations = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator I in animations)
        {
            if (I.transform.name == "EfectoTerreno")
            {
                Efectoanimation = I;
                SpriteRender = I.transform.GetComponent<SpriteRenderer>();
            }
        }
        Transform[] transfo = new Transform[gameObject.GetComponentsInChildren<Transform>().Length];
        transfo = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform I in transfo)
        {
            if (I.name == "arribaP")
                TranfPisadas[0] = I;
            else if (I.name == "derechaP")
                TranfPisadas[1] = I;
            else if (I.name == "abajoP")
                TranfPisadas[2] = I;
            else if (I.name == "isquierdaP")
                TranfPisadas[3] = I;
        }

     }

    // Update is called once per frame
    void LateUpdate()
    {

        if (activaEfecto == true && PlayerOrNpc == true)
        {
            Secuencia();
        }
        else if (activaEfecto == true && PlayerOrNpc == false)
        {
            SecuenciaNpc();

        }

        if (EstoySobreAgua == true && PlayerOrNpc == true)
        {
           SecuenciaPlayerAgua();
        }
        else if (EstoySobreAgua == true && PlayerOrNpc == false)
        {
        //    SecuenciaNpc();

        }

        if (activaEfectoArena == true && PlayerOrNpc == true)
        {
            SecuenciaNpcArena();
        }
        else if (activaEfectoArena == true && PlayerOrNpc == false)
        {
            

        }


    }


    void Secuencia()
    {
        if (mov.pos != transform.position && EstoyAnimando == false)
        {
            
            Efectoanimation.SetBool("Activar", true);
            EstoyAnimando = true;
        }
        else if (mov.pos == transform.position && EstoyAnimando == true)
        {
         
            Efectoanimation.SetBool("Activar", false);
            ActivaPisadas = false;
            EstoyAnimando = false;
        }

        if ( ActivaPisadas == false && EstoyAnimando)
        {
            StopAllCoroutines();    
            StartCoroutine(CargaPisadas());
        }

    }

    void SecuenciaNpc()
    {
        if (movNpc.pos != transform.position && EstoyAnimando == false)
        {
           
            Efectoanimation.SetBool("Activar", true);
            EstoyAnimando = true;
        }
        else if (movNpc.pos == transform.position && EstoyAnimando == true)
        {

            Efectoanimation.SetBool("Activar", false);
            ActivaPisadas = false;
            EstoyAnimando = false;
        }

        if (ActivaPisadas == false && EstoyAnimando)
        {
            StopAllCoroutines();
            StartCoroutine(CargaPisadas());
        }

    }

    void SecuenciaNpcArena()
    {
        if (mov.pos != transform.position && EstoyAnimando == false)
        {

            
            EstoyAnimando = true;
        }
        else if (mov.pos == transform.position && EstoyAnimando == true)
        {

          
            ActivaPisadas = false;
            EstoyAnimando = false;
        }

        if (ActivaPisadas == false && EstoyAnimando)
        {
            StopAllCoroutines();
            StartCoroutine(CargaPisadasArena());
        }

    }


    void SecuenciaPlayerAgua()
    {
        mov.animt.SetFloat("direccion", mov.face);
        if (primeraEntradaAgua == false)
        {
           
            StartCoroutine(PausaPersonaje());
            primeraEntradaAgua = true;
        }
    }

    void InvokeExiteCollider()
    {
        SpriteRender.enabled = false;
        activaEfecto = false;
        ActivaPisadas = false;
        StopCoroutine(CargaPisadas());
    }
    void InvokeExitAguaCollider()
    {
        EstoySobreAgua = false;
        mov.animt.SetBool("Agua", EstoySobreAgua);
        primeraEntradaAgua = false;
        StartCoroutine(PausaPersonaje());
        mov.animt.SetBool("walk", true);

    }

    void InvokeExitArenaCollider()
    {
        ActivaPisadas = false;
        activaEfectoArena = false;
        StopCoroutine(CargaPisadasArena());

    }

    IEnumerator PausaPersonaje()
    {
       
       
        mov.animt.SetBool("walk",false);
        mov.DisparadorEvento = true;
        yield return new WaitForSeconds(1f);
        mov.DisparadorEvento = false;
    }
    IEnumerator PausaPersonajeSux()
    {
        mov.DisparadorEvento = true;
        yield return new WaitForSeconds(1f);
        mov.DisparadorEvento = false;
    }
    IEnumerator CargaPisadas()
    {
        ActivaPisadas = true;
        while (ActivaPisadas == true)
        {
            GameObject  posiciona, posiciona2;

            posiciona = Instantiate(Pisada);
            //posicion primer pisada
            if (PlayerOrNpc == true)          
                posiciona.transform.position = posicionPisadas(mov.face, 0).position;     

            else if (PlayerOrNpc == false)                        
                posiciona.transform.position = posicionPisadas(movNpc.Face, 0).position;              

            yield return new WaitForSeconds(velocidadpisadas);

            posiciona2 = Instantiate(Pisada);

         
            //segunda Pisada posicion
            if (PlayerOrNpc == true)
                posiciona2.transform.position = posicionPisadas(mov.face, 1).position;

            else if (PlayerOrNpc == false) 
            {
                if (posicionPisadas(movNpc.Face, 1) == null)
                {
                    yield break;
                }
                posiciona2.transform.position = posicionPisadas(movNpc.Face, 1).position;               
            
            }

            yield return new WaitForSeconds(velocidadpisadas);
        }
        yield return 0;
    }

    IEnumerator CargaPisadasArena()
    {
        ActivaPisadas = true;
        while (ActivaPisadas == true)
        {
            GameObject posiciona, posiciona2;

            posiciona = Instantiate(PisadaArena);
            //posicion primer pisada
            if (PlayerOrNpc == true)
                posiciona.transform.position = posicionPisadas(mov.face, 0).position;

            else if (PlayerOrNpc == false)
                posiciona.transform.position = posicionPisadas(movNpc.Face, 0).position;

            //rota la posicion de la pisada
            if (mov.face == 2 || mov.face == 4)
            {
                posiciona.transform.eulerAngles = new Vector3(posiciona.transform.eulerAngles.x, posiciona.transform.eulerAngles.y,90);
            }

            yield return new WaitForSeconds(velocidadpisadas);

            posiciona2 = Instantiate(PisadaArena);
            //segunda Pisada posicion
            if (PlayerOrNpc == true)
                posiciona2.transform.position = posicionPisadas(mov.face, 1).position;

            else if (PlayerOrNpc == false)
                posiciona2.transform.position = posicionPisadas(movNpc.Face, 1).position;

            //rota la posicion de la pisada
            if (mov.face == 2 || mov.face == 4)
            {
                posiciona2.transform.eulerAngles = new Vector3(posiciona.transform.eulerAngles.x, posiciona.transform.eulerAngles.y, 90);
            }
            yield return new WaitForSeconds(velocidadpisadas);
        }
        yield return 0;
    }

    Transform posicionPisadas(int face, int Pie)
    {
        Transform TranfReturn = null;

        if (face == 1 || face == 3)
        {
            if (Pie == 0)
            {
                TranfReturn = TranfPisadas[1];
            }

            else if (Pie == 1)
            {
                TranfReturn = TranfPisadas[3];
            }

        }
        else if (face == 2 || face == 4)
        {
          
            if (Pie == 0)
            {
                TranfReturn = TranfPisadas[0];
            }
            
            else if (Pie == 1)
            {
                TranfReturn = TranfPisadas[2];
            }
           
        }

        return TranfReturn;
    }
    #endregion

   
}