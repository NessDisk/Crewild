using System.Collections;
using UnityEngine;


public enum ModoMovimiento
{
    quieto,
    loot,
    pinpon,
    Rutina,
    Rotar
}
public class Npc_movimiento : MonoBehaviour
{
    public bool PausaMovimiento;
    public Animator animatorNpc;



    public ModoMovimiento modoMovimiento;



    public enum Direcciones
    {
        arriba,
        derecha,
        abajo,
        isquierda
    }

    /// <summary>
    /// Cuando es true  INCREMENTA, cuando es false DECREMENTA la posicion del Array
    /// </summary>
    bool IncrementaDecrementa = true;

    /// <summary>
    /// Es contador de la posicion del array
    /// </summary>
    int numArrayPosicion = 0;

    /// <summary>
    /// direcciones en las que se mueve el  npc en el Enum
    /// </summary>
    public Direcciones[] direcciones;

    // posicion a la que se dirige el perosnaje
    public Vector3 pos;

    /// <summary>
    /// velocidad de movimiento 
    /// </summary>
    public float speed;

    /// <summary>
    /// distancia recorrida
    /// </summary>
    public float distacia;

    /// <summary>
    /// rango  al cual el  npc se puede mover rdmxrdm
    /// </summary>
    public int RangoDeMovimiento;

    /// <summary>
    /// define cual es la posicion en el  rango de movimiento
    /// </summary>
    public int PosicionActual;

    //tiempo aleatorio en el que  se le da tiempo de espera antes de moverse
    public float tiempoVariable;

    /// <summary>
    /// validador la entrada, en movimiento del npc 
    /// </summary>
    public bool validador = true;

    /// <summary>
    /// capas con las que va interactuar
    /// </summary>
    public LayerMask Layermask;

    public float DistanciaRayCast, posicionY;

    /// <summary>
    /// 0= arriba , 1= derecha, 2= abajo, 3=isquierda
    /// </summary>
    BoxCollider2D[] BoxColiderRadios;

    public RaycastHit2D hitArriba = new RaycastHit2D(),
                        HitAbajo = new RaycastHit2D(),
                        HitIsquierda = new RaycastHit2D(),
                        HitDerecha = new RaycastHit2D();


    /// <summary>
    /// llama al script propio dentro del objeto  NPC_Dialogo
    /// </summary>
    private NPC_Dialogo npc_Dialogo;

    /// <summary>
    /// Define cuando npc en brauler puede dispara el modoBrawler
    /// </summary>
    private NpcBrauler npc_Brauler;

    /// <summary>
    /// Direccion en la que mire el NPC
    /// </summary>
    public int Face = 0;

    public static bool AllnpcStop;


    // Start is called before the first frame update
    void Start()
    {
        animatorNpc = GetComponent<Animator>();

        PosicionActual = ((RangoDeMovimiento * RangoDeMovimiento) / 2);

        pos = transform.position;


        BoxColiderRadios = new BoxCollider2D[4];

        Transform[] auxobj = GetComponentsInChildren<Transform>();

        animatorNpc.SetInteger("face", Face);

        //otorga valores box Collider.
        foreach (Transform obj in auxobj)
        {
            if (obj.name == "arriba")
            {
                BoxColiderRadios[0] = obj.GetComponent<BoxCollider2D>();

            }
            else if (obj.name == "derecha")
            {
                BoxColiderRadios[1] = obj.GetComponent<BoxCollider2D>();

            }
            else if (obj.name == "abajo")
            {
                BoxColiderRadios[2] = obj.GetComponent<BoxCollider2D>();

            }
            else if (obj.name == "Isquierda")
            {
                BoxColiderRadios[3] = obj.GetComponent<BoxCollider2D>();

            }
        }

        npc_Dialogo = GetComponent<NPC_Dialogo>();

        npc_Brauler = GetComponent<NpcBrauler>();

        DesactivaRadioBoxCollider();


        //ajuste de face al iniciar
        Face = ((int)direcciones[0]) + 1;
        animatorNpc.SetInteger("face", Face);

    }




    // Update is called once per frame
    void Update()
    {

        // pausa de todos los parametros
        if (PausaMovimiento == true || npc_Dialogo.DisparadorDialogo == true || AllnpcStop == true)
        {
            return;
        }



        RaycastNpc(transform, Layermask, DistanciaRayCast, posicionY);

        //define el movimiento del npc
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);    // Move there

        animacionMovimiento();

        if (npc_Brauler.validadorDeteccion == true)
        {
            return;
        }


        if (pos == transform.position && validador == true)
        {


            DesactivaRadioBoxCollider();
            if (modoMovimiento == ModoMovimiento.loot)
            {
                StartCoroutine(MovAleatorio());

            }
            else if (modoMovimiento == ModoMovimiento.pinpon)
            {

                StartCoroutine(MovPinpon());
            }
            else if (modoMovimiento == ModoMovimiento.Rutina)
            {
                StartCoroutine(MovCiclo());
            }
            else if (modoMovimiento == ModoMovimiento.Rotar)
            {
                StartCoroutine(Rotacion());
            }

        }






    }

    /// <summary>
    /// define el movimiento del personaje 
    /// </summary>
    /// <returns></returns>
    IEnumerator MovAleatorio()
    {
        // Face = 0;
        validador = false;

        bool ValidadorDeMovimiento = false;

        yield return new WaitForSeconds(tiempoVariable);



        Face = Random.Range(1, 5);
        animatorNpc.SetInteger("face", Face);

        ValidadorDeMovimiento = ValidadorDireccion(Face);

        if (ValidadorDeMovimiento == true)
        {
            MovimientoPos(Face);
        }


        validador = true;

    }

    /// <summary>
    /// define el movimiento en la corrutina en modo pinpon
    /// </summary>
    /// <returns></returns>
    IEnumerator MovPinpon()
    {

        validador = false;
        Face = 0;

        yield return new WaitForSeconds(tiempoVariable);

        // cuando el array llega al limite cambia la direccion del movimiento
        if (numArrayPosicion == direcciones.Length)
        {
            //cuando llega  aun extremo del recorrido hace una pasusa mas larga 
            yield return new WaitForSeconds(tiempoVariable * 3);
            IncrementaDecrementa = false;
            numArrayPosicion -= 1;
        }
        else if (numArrayPosicion == -1)
        {
            //cuando llega  aun extremo del recorrido hace una pasusa mas larga 
            yield return new WaitForSeconds(tiempoVariable * 3);
            IncrementaDecrementa = true;
            numArrayPosicion += 1;
        }




        Face = ((int)direcciones[numArrayPosicion]) + 1;

        //incremenmta o decrementa el el numArrayPosicion
        numArrayPosicion = RecorridoArray(numArrayPosicion, direcciones.Length);

        if (IncrementaDecrementa == false)
        {
            Face = correctorPinPon(Face);
        }


        animatorNpc.SetInteger("face", Face);


        yield return new WaitWhile(() => ValidadorPinPon(Face) == false);


        MovimientoPos(Face);

        validador = true;


        yield return 0;
    }

    /// <summary>
    /// define el movimiento en ciclos que se repiten
    /// </summary>
    /// <returns></returns>
    IEnumerator MovCiclo()
    {
        validador = false;

        Face = 0;
        yield return new WaitForSeconds(tiempoVariable);

        // reinicia el recorrido del array
        if (numArrayPosicion == direcciones.Length)
        {

            numArrayPosicion = 0;
        }



        Face = ((int)direcciones[numArrayPosicion]) + 1;

        //incremenmta o decrementa el el numArrayPosicion
        numArrayPosicion = RecorridoArray(numArrayPosicion, direcciones.Length);


        animatorNpc.SetInteger("face", Face);


        yield return new WaitWhile(() => ValidadorPinPon(Face) == false);


        MovimientoPos(Face);

        validador = true;


        yield return 0;

    }

    /// <summary>
    /// solo rota el cuerpo una direccion o otra
    /// </summary>
    /// <returns></returns>
    IEnumerator Rotacion()
    {
        validador = false;


        yield return new WaitForSeconds(tiempoVariable * 3);

        // reinicia el recorrido del array
        if (numArrayPosicion == direcciones.Length)
        {
            numArrayPosicion = 0;
        }



        Face = ((int)direcciones[numArrayPosicion]) + 1;

        //incremenmta o decrementa el el numArrayPosicion
        numArrayPosicion = RecorridoArray(numArrayPosicion, direcciones.Length);


        animatorNpc.SetInteger("face", Face);

        validador = true;


        yield return 0;

    }

    void animacionMovimiento()
    {
        if (transform.position != pos)
        {
            animatorNpc.SetBool("walk", true);
        }
        else
        {
            animatorNpc.SetBool("walk", false);
        }
    }

    /// <summary>
    /// desactiva el radio que inactividad para movimiento
    /// </summary>
    public void DesactivaRadioBoxCollider()
    {
        for (int i = 0; i < 4; i++)
        {
            BoxColiderRadios[i].enabled = false;
        }
    }

    public void  StopallCourritne()
    {
        StopAllCoroutines();
    }
    //funciones modo pin pon y Rutina

    /// <summary>
    /// define si la direccion es  valida para moverse
    /// </summary>
    /// <returns></returns>
    bool ValidadorPinPon(int Direccion)
    {
        //arriba
        bool ValorARetornar = false;
        //arriba
        if (Direccion == 1)
        {
            if (hitArriba.collider == null)
            {
                ValorARetornar = true;
            }          
        }
        //derecha
        else if (Direccion == 2)
        {
            if (HitDerecha.collider == null)
            {
                ValorARetornar = true;
            }      
        }
        //abajo
        else if (Direccion == 3)
        {

            if (HitAbajo.collider == null)
            {
                ValorARetornar = true;
            }    
        }
        //isquierda
        else if (Direccion == 4)
        {
            if (HitIsquierda.collider == null)
            {
                ValorARetornar = true;
            }        
        }


        return ValorARetornar;
    }

    /// <summary>
    /// corrige en numero cuando el rrecorrido en el array es decendente en el modo de PinPon
    /// </summary>
    /// <param name="NumCorrector">numero que se va corregir</param>
    /// <returns></returns>
    public int correctorPinPon(int NumCorrector)
    {
        if (NumCorrector == 1)
        {
            NumCorrector =3;
        }
        else if (NumCorrector== 2)
        {
            NumCorrector = 4;
        }
        else if (NumCorrector == 3)
        {
            NumCorrector = 1;
        }
        else if (NumCorrector == 4)
        {
            NumCorrector = 2;
        }

        return NumCorrector;
    }

    /// <summary>
    /// toma  los elementos del array he incrementa y decrementa el array en forma de Pinpon
    /// </summary>
    /// <param name="NumActualArray"> posicion actual en la que se encuentra el array</param>
    /// <param name="TamañoArray">tamañop total del array</param>
    int RecorridoArray(int NumActualArray, int TamañoArray)
    {
        int NumARetornar = 0;
        if (IncrementaDecrementa == true)
        {          
             if (NumActualArray < TamañoArray)
            {
                NumARetornar = incrementa(NumActualArray);
            }         

        }
        else  if (IncrementaDecrementa == false)
        {
            if (NumActualArray > -1 )
            {
                NumARetornar = decrementa(NumActualArray);             
            }       
        }

        return NumARetornar;
    }



    /// <summary>
    /// Icrementa en uno un numero del array
    /// </summary>
    /// <param name="NumActualArray"></param>
    /// <returns></returns>
    int incrementa(int NumActualArray)
    {
        int NumARetornar = NumActualArray + 1;
        return  NumARetornar;
    }

    /// <summary>
    /// Decrementa  un numero del array
    /// </summary>
    /// <param name="NumActualArray"></param>
    /// <returns></returns>
    int decrementa(int NumActualArray)
    {
        int NumARetornar = NumActualArray - 1;
        return NumARetornar;
    }




    // ----------- funciones  modo loot ---------------

    /// <summary>
    /// valida si la direccion a la que va el Npc es permitida
    /// </summary>
    /// <param name="Direccion"></param>
    /// <returns></returns>
    bool ValidadorDireccion(int Direccion)
    {
        
        bool ValorARetornar = false;

        //arriba
        if (Direccion == 1)
        {
            if (hitArriba.collider == null)
            {
                ValorARetornar = LimiteSuperior();
            }
        //    else { Debug.Log(hitArriba.collider.name); }

        }
        //derecha
        else if (Direccion == 2)
        {
            if (HitDerecha.collider == null)
            {
                ValorARetornar = LimiteDerecho();
            }
        //    else { Debug.Log(HitDerecha.collider.name); }
        }
        //abajo
        else if (Direccion == 3)
        {

            if (HitAbajo.collider == null)
            {
                ValorARetornar = LimiteInferior();
            }
        //    else { Debug.Log(HitAbajo.collider.name); }
        }
        //isquierda
        else if (Direccion == 4)
        {

            if (HitIsquierda.collider == null)
            {
                ValorARetornar = LimiteIsquierdo();
            }
         //   else {Debug.Log(HitIsquierda.collider.name);}
        }

        return ValorARetornar;
    }

    bool ValidadorDireccionBrawlwe(int Direccion)
    {

        bool ValorARetornar = false;

        //arriba
        if (Direccion == 1)
        {
            if (hitArriba.collider == null)
            {
                ValorARetornar = LimiteSuperior();
            }
            else { Debug.Log(hitArriba.collider.name); }

        }
        //derecha
        else if (Direccion == 2)
        {
            if (HitDerecha.collider == null)
            {
                ValorARetornar = LimiteDerecho();
            }
            else { Debug.Log(HitDerecha.collider.name); }
        }
        //abajo
        else if (Direccion == 3)
        {

            if (HitAbajo.collider == null)
            {
                ValorARetornar = LimiteInferior();
            }
            else { Debug.Log(HitAbajo.collider.name); }
        }
        //isquierda
        else if (Direccion == 4)
        {

            if (HitIsquierda.collider == null)
            {
                ValorARetornar = LimiteIsquierdo();
            }
            else
            {
                Debug.Log(HitIsquierda.collider.name);
            }
        }

        return ValorARetornar;
    }

    //devuelve si es permitido el movimiento en dependiendo el numero de la poisicon del NPC en el rango de movimiento
    bool LimiteSuperior()
    {
        bool ValorARetornar = true;

        for (int i = 0; i < RangoDeMovimiento; i++)
        {
            if (PosicionActual == i+1)
            {
                ValorARetornar = false;
                break;
            }
        }    

        return ValorARetornar;
    }

    //devuelve si es permitido el movimiento en dependiendo el numero de la poisicon del NPC en el rango de movimiento
    bool LimiteDerecho()
    {
        bool ValorARetornar = true;
        for (int i = 0; i < RangoDeMovimiento; i++)
        {

            if (PosicionActual  == (i+1)* RangoDeMovimiento)
            {
                ValorARetornar = false;
                break;
            }
        }
        return ValorARetornar;
    }
    
    //devuelve si es permitido el movimiento en dependiendo el numero de la poisicon del NPC en el rango de movimiento
    bool LimiteInferior()
    {
        bool ValorARetornar = true;
        for (int i = 0; i < RangoDeMovimiento; i++)
        {          
            if (PosicionActual == ((RangoDeMovimiento * RangoDeMovimiento) - RangoDeMovimiento) + (i + 1))
            {
                ValorARetornar = false;
                break;
            }
        }
        return ValorARetornar;
    }

    //devuelve si es permitido el movimiento en dependiendo el numero de la poisicon del NPC en el rango de movimiento
    bool LimiteIsquierdo()
    {
        bool ValorARetornar = true;
        for (int i = 0; i < RangoDeMovimiento; i++)
        {

            if (PosicionActual == 1)
            {
                ValorARetornar = false;
                break;
            }
           else  if (PosicionActual == (RangoDeMovimiento * (i + 1)) +1)
            {
                ValorARetornar = false;
                break;
            }
        }

        return ValorARetornar;
    }





    // ----- funcion de movimiento para todos los modos ------

    /// <summary>
    /// 
    ///deinde el la direccion en que la posicion se va  dirigir
    /// </summary>
    /// <param name="Direccion"></param>
    public void MovimientoPos(int Direccion)
        {
        
        BoxColiderRadios[Direccion-1].enabled = true;
        //arriba
            if (Direccion == 1)
            {
            //activa un boxCollider  en el caso de que el enemigo se mueva en direccion
          
            if(modoMovimiento == ModoMovimiento.loot)
            PosicionActual -= RangoDeMovimiento;

            pos += Vector3.up * distacia;
           
            }
            //derecha
           else if (Direccion == 2)
            {
            if (modoMovimiento == ModoMovimiento.loot)
                PosicionActual += 1;

            pos += Vector3.right * distacia;
            }
            //abajo
          else  if (Direccion == 3)
            {

            if (modoMovimiento == ModoMovimiento.loot)
                PosicionActual += RangoDeMovimiento;

            pos += Vector3.down * distacia;
            }
            //Isquierda
           else if (Direccion == 4)
            {

            if (modoMovimiento == ModoMovimiento.loot)
                PosicionActual -= 1;

            pos += Vector3.left * distacia;
            }

        }

    /// <summary>
    /// Raycas que vordean todo el Npc
    /// </summary>
    /// <param name="transF">posicion inical por la que el Ray cast inicia</param>
    /// <param name="Layer">capas sobre las que va afectar el Raycast</param>
    /// <param name="Distancia"> alcance del Raycastas en el Editor </param>
    /// <param name="DistanciaEnY"></param>
    void RaycastNpc(Transform transF, LayerMask Layer, float Distancia, float DistanciaEnY)
    {
        hitArriba = Physics2D.Raycast(new Vector2(transF.position.x, transF.position.y), Vector2.up, Distancia, Layer);
        HitDerecha = Physics2D.Raycast(new Vector2(transF.position.x, transF.position.y), Vector2.right, Distancia, Layer);
        HitAbajo = Physics2D.Raycast(new Vector2(transF.position.x, transF.position.y), Vector2.down, Distancia, Layer);
        HitIsquierda = Physics2D.Raycast(new Vector2(transF.position.x, transF.position.y), Vector2.left, Distancia, Layer);



        Debug.DrawLine(new Vector2(transF.position.x, transF.position.y), new Vector3(transF.position.x, transF.position.y) + Vector3.up * Distancia);
        Debug.DrawLine(new Vector2(transF.position.x, transF.position.y), new Vector3(transF.position.x, transF.position.y) + Vector3.right * Distancia);
        Debug.DrawLine(new Vector2(transF.position.x, transF.position.y), new Vector3(transF.position.x, transF.position.y) + Vector3.down * Distancia);
        Debug.DrawLine(new Vector2(transF.position.x, transF.position.y), new Vector3(transF.position.x, transF.position.y) + Vector3.left * Distancia);
    }

    /// <summary>
    /// define el numero de cara  a la que tiene que mirar el npc si este entra en modo  dialogo
    /// </summary>
    /// <returns></returns>
    int definirFaceJugador()
    {
        int face = 0;
        if      (hitArriba.collider.name == "personaje")
        {
            face = 1;
        }
        else if (HitDerecha.collider.name == "personaje")
        {
            face = 2;
        }
        else if (HitAbajo.collider.name == "personaje")
        {
            face = 3;
        }
        else if (HitIsquierda.collider.name == "personaje")
        {
            face = 4;
        }
        return face;

    }
    /// <summary>
    /// detecta el jugador si esta el su rango
    /// </summary>
    /// <returns></returns>
    public bool DetectarJugador()
    {

        bool detectarJugador = false;
        if (hitArriba.collider != null)
        {
            if (hitArriba.collider.name == "personaje")
            {
                detectarJugador = true;
                Face = 1;
            }
        }

       else  if (HitDerecha.collider != null)
        {
            if (HitDerecha.collider.name == "personaje")
            {
                detectarJugador = true;
                Face = 2;
            }
        }
        else if (HitAbajo.collider != null)
        {
            if (HitAbajo.collider.name == "personaje")
            {
                detectarJugador = true;
                Face = 3;
            }
        }
        else if (HitIsquierda.collider != null)
        {
            if (HitIsquierda.collider.name == "personaje")
            {
                detectarJugador = true;
                Face = 4;
            }
        }

        return detectarJugador;

    }

    public bool DetectarJugador(LayerMask layers, RaycastHit2D hit2D)
    {
      
        bool detectarJugador = false;
        Vector3 Directionface = Vector3.zero;
        // raycast
        if (Face == 1)
        {
            Directionface = Vector3.up;
        }
        else if (Face == 2)
        {
            Directionface = Vector3.right;
        }
        else if (Face == 3)
        {
            Directionface = Vector3.down;
        }
        else if (Face == 4)
        {
            Directionface = Vector3.left;
        }


        hit2D = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Directionface, DistanciaRayCast, layers);
       
        if (hit2D.collider != null)
        {
            if (hit2D.collider.name == "personaje")
            {
                detectarJugador = true;
                Face = 1;
            }
        }      

        return detectarJugador;

    }
}



