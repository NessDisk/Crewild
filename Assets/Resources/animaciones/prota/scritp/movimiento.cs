using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class movimiento : MonoBehaviour
{

    public static movimiento SharedInstancia = new movimiento();
    public Animator animt;


    // posicion del player
    public Vector3 pos;

    //
    public float speed = 0.1f;

    // define las capas de deteccion
    public LayerMask Layermask;

    /// <summary>
    /// define el rumbo que esta tomando el jugador
    /// </summary>
    public Vector3 RUMBO;


    /// <summary>
    /// define la cara que tiene en la interfae
    /// </summary>
    public int face;


    /// <summary>
    /// define el la pausa entrada a la pausa entre cada movimiento 
    /// </summary>
    bool[] beginmove = new bool[3];



    /// <summary>
    /// tiempo de espera entre que se le da movimiento al jugador
    /// </summary>
    public float timeWait;




    /// <summary>
    /// define si el jugador esta realizando una accion por lo que se le quita el control.
    /// </summary>
    public bool StopPlayer, DisparadorEvento;

    /// <summary>
    /// value  move en axis X and Y
    /// </summary>
    private int x, y;

    /// <summary>
    /// rango en que va   cambiar de direccion
    /// </summary>
    private int prioridad = 0;

    /// <summary>
    /// no se usa
    /// </summary>
    public bool triggerPath;

    public Transform TargerPath;

    public pathfinderGrid path;

    /// <summary>
    /// define el alcancecomo la distancia(tambien para el Raycasts) de recorre el personaje
    /// </summary>
    public float DistanciaRecorrida;

    /// <summary>
    /// define el alcance que tengra el Raycast
    /// </summary>
    public float RaycastAlcance;

    /// <summary>
    /// 0= arriba , 1= derecha, 2= abajo, 3=isquierda
    /// </summary>
    BoxCollider2D[] BoxColiderRadios;

    /// <summary>
    /// raycast  de jugador
    /// </summary>
 [HideInInspector] public   RaycastHit2D hitArrriba = new RaycastHit2D(),
                 hitAbajo = new RaycastHit2D(),
                 hitIsquierda = new RaycastHit2D(),
                 hitDerecha = new RaycastHit2D(),
                 hitRaycast = new RaycastHit2D();

    /// <summary>
    /// disparador de entrada y salida en el metodo para activar las animaciones para el movimiento del personaje
    /// </summary>
    public bool AuxValidador;

    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Salto" && DisparadorEvento == false )
        {
            animt.SetBool("jump", true);
            DisparadorEvento = true;
            pos += RUMBO * DistanciaRecorrida*2;
            print("funciona");
        }      
    }*/

    EfectoFueraDeBatalla EfectoEstado = new EfectoFueraDeBatalla();

    void Start()
    {
        pos = transform.position; // Take the current position

        SharedInstancia = this;

         animt = GetComponent<Animator>();
        //animt = transform.Find("SpritePlayer").GetComponent<Animator>();
         path = new pathfinderGrid();


        beginmove[0] = false;

        beginmove[1] = true;
        beginmove[2] = false;


        BoxColiderRadios = new BoxCollider2D[4];

        Transform[] auxobj = GetComponentsInChildren<Transform>();

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

        path = new pathfinderGrid();
        DesactivaRadioBoxCollider();
    }
    void FixedUpdate()
    {

        if (StopPlayer == true)
        {
            return;
        }
        //====RayCasts====//
        raycastPlayer();      


        AnimacionMovimiento();


        //dibuja una linea que fedine el punto vision del jugador.
        Debug.DrawLine(transform.position, transform.position + (RUMBO * RaycastAlcance));


        //The Current Position = Move To (the current position to the new position by the speed * Time.DeltaTime)
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);    // Move there

        if (DisparadorEvento == true)
        {
            return;
        }


        if (hitRaycast.collider != null && Input.GetKeyUp(KeyCode.Space) && transform.position == pos)
        {
            Dectecta_Npc_Hablar();
        }



        // movimientos
        x = (int)Input.GetAxis("Horizontal");
        y = (int)Input.GetAxis("Vertical");



        if (
       Input.GetKeyUp(KeyCode.A)
     || Input.GetKeyUp(KeyCode.W)
     || Input.GetKeyUp(KeyCode.S)
     || Input.GetKeyUp(KeyCode.D)

            )
        {

            prioridad = 0;
        }

        if (prioridad == 0 && x != 0)
        {
            prioridad = 1;

        }
        else if (prioridad == 0 && y != 0)
        {
            prioridad = 2;
        }

        else if (x == 0 && y == 0)
        {
            prioridad = 0;
        }



        if (prioridad == 1 && x != 0)
        {
            if (y != 0)
            {

                x = 0;

            }
        }

        else if (prioridad == 2 && y != 0)
        {
            if (x != 0)
            {
                y = 0;

            }
        }



        //==Inputs==//
        if (x == -1 && y == 0 && transform.position == pos   && hitIsquierda.collider == null
         || x == -1 && y == 0 && transform.position == pos && hitIsquierda.transform.gameObject.layer == 11
            )
        {

            RUMBO = Vector3.left.normalized;
            animt.SetInteger("face", 2);

            face = 2;


            if (beginmove[1] == true)
            {

                StartCoroutine(FaceTime());


            }


            //(-1,0)

            if (beginmove[0] == true)
            {
                pos += Vector3.left * DistanciaRecorrida;// Add -1 to pos.x

                verificadorEstado();
            }


            return;

        }
        // isquierda
        else if (Input.GetKey(KeyCode.A) && transform.position == pos)
        {
            RUMBO = Vector3.left.normalized;
            animt.SetInteger("face", 2);

            face = 2;
        }






        if (x == 1 && y == 0 && transform.position == pos && hitDerecha.collider == null
         || x == 1 && y == 0 && transform.position == pos && hitDerecha.transform.gameObject.layer == 11
            )
        {           //(1,0)


            RUMBO = Vector3.right.normalized;
            animt.SetInteger("face", 4);

            face = 4;

            if (beginmove[1] == true)
            {
                StartCoroutine(FaceTime());

            }

            if (beginmove[0] == true)
            {

                pos += Vector3.right * DistanciaRecorrida;// Add 1 to pos.x

                verificadorEstado();
            }




            return;


        }

        // derecha
        else if (Input.GetKey(KeyCode.D) && transform.position == pos)
        {
            RUMBO = Vector3.right.normalized;
            animt.SetInteger("face", 4);

            face = 4;
        }



        if (x == 0 && y == 1 && transform.position == pos && hitArrriba.collider == null
          ||x == 0 && y == 1 && transform.position == pos && hitArrriba.transform.gameObject.layer == 11
         )
        {


            RUMBO = Vector3.up.normalized;
            animt.SetInteger("face", 3);

            face = 3;


            if (beginmove[1] == true)
            {
                StartCoroutine(FaceTime());

            }

            if (beginmove[0] == true)
            {

                pos += Vector3.up * DistanciaRecorrida; // Add 1 to pos.y

                verificadorEstado();
            }

            return;

        }


        //arriba
        else if (Input.GetKey(KeyCode.W) && transform.position == pos)
        {
            RUMBO = Vector3.up.normalized;
            animt.SetInteger("face", 3);

            face = 3;
        }


        if (x == 0 && y == -1 && transform.position == pos && hitAbajo.collider == null
         || x == 0 && y == -1 && transform.position == pos && hitAbajo.transform.gameObject.layer == 11
            )
        {           //(0,-1
            RUMBO = Vector3.down.normalized;
            animt.SetInteger("face", 1);

            face = 1;


            if (beginmove[1] == true)
            {
                StartCoroutine(FaceTime());
            }
            if (beginmove[0] == true)
            {

                pos += Vector3.down * DistanciaRecorrida;// Add -1 to pos.y

                verificadorEstado();


            }
            return;
        }

        //Abajo
        else if (Input.GetKey(KeyCode.S) && transform.position == pos)
        {
            RUMBO = Vector3.down.normalized;
            animt.SetInteger("face", 1);

            face = 1;
        }


        if (
               Input.GetKeyUp(KeyCode.S)
            || Input.GetKeyUp(KeyCode.A)
            || Input.GetKeyUp(KeyCode.W)
            || Input.GetKeyUp(KeyCode.D))
        {


            beginmove[1] = true;
            beginmove[0] = false;

          //  StopAllCoroutines();




        }





    }

    /// <summary>
    /// Raycas para todas las direcciones del player
    /// </summary>
    void raycastPlayer()
    {
        hitArrriba = Physics2D.Raycast(transform.position, Vector2.up, RaycastAlcance, Layermask);
        hitAbajo = Physics2D.Raycast(transform.position, Vector2.down, RaycastAlcance, Layermask);
        hitDerecha = Physics2D.Raycast(transform.position, Vector2.right, RaycastAlcance, Layermask);
        hitIsquierda = Physics2D.Raycast(transform.position, Vector2.left, RaycastAlcance, Layermask);

        hitRaycast = Physics2D.Raycast(transform.position, RUMBO, RaycastAlcance, Layermask);

    }


   public void MovDirection()
    {
        switch (face)
        {
           
            //abajo
            case 1:
                pos += Vector3.down * DistanciaRecorrida;// Add -1 to pos.y
                break;
                //isquierda
            case 2:
                pos += Vector3.left * DistanciaRecorrida;// Add -1 to pos.y
                break;

            //arriba
            case 3:
                pos += Vector3.up * DistanciaRecorrida;// Add -1 to pos.y
                break;
                //derecha
            case 4:
                pos += Vector3.right * DistanciaRecorrida;// Add -1 to pos.y
                break;

    }

    }

   

    void verificadorEstado()
    {

        // verifica que se cumpla los efectos de estados negativos de un Crewild
        if (EfectoFueraDeBatalla.VerificadorBool == true)
           StartCoroutine(EfectoEstado.ContadorDepasos());
    } 

    /// <summary>
    /// define la animacion de movimiento
    /// </summary>
    void AnimacionMovimiento()
    {

        if (transform.position != pos)
        {
            AuxValidador = true;
            HabilitadorBoxCollider(face);
            animt.SetBool("walk", true);
        }

        else if (transform.position == pos && AuxValidador)
        {
            AuxValidador = false;
            DesactivaRadioBoxCollider();
            animt.SetBool("walk", false);
        }
    }


   


    /// <summary>
    /// cuando dectecta al Npc permite la opcion para habalr pero solo cuando los 2 estan quieto
    /// </summary>
    void Dectecta_Npc_Hablar()
    {

       
        //ejecuta Dialogo Npc
         if (hitRaycast.collider.tag == "NPC"
            && hitRaycast.transform.position == hitRaycast.transform.GetComponent<Npc_movimiento>().pos
            && hitRaycast.transform.GetComponent<NPC_Dialogo>().Desactivador == false
            && hitRaycast.transform.GetComponent<Npc_Tienda>().desactivador == true
            && hitRaycast.transform.GetComponent<NpcBrauler>().desactivador == true
            )
        {
            hitRaycast.transform.GetComponent<NPC_Dialogo>().DisparadorDialogo = true;
            hitRaycast.transform.GetComponent<Npc_movimiento>().StopAllCoroutines();
            hitRaycast.transform.GetComponent<Npc_movimiento>().animatorNpc.SetInteger("face", face);
            hitRaycast.transform.GetComponent<Npc_movimiento>().validador = true;
            DisparadorEvento = true;
        }
        // modo Brawler solo  voleta la face del personaje
        else if (hitRaycast.collider.tag == "NPC"
            && hitRaycast.transform.position == hitRaycast.transform.GetComponent<Npc_movimiento>().pos
            && hitRaycast.transform.GetComponent<NPC_Dialogo>().Desactivador == true
            && hitRaycast.transform.GetComponent<Npc_Tienda>().desactivador == true
            )
        {

            hitRaycast.transform.GetComponent<Npc_movimiento>().Face = face;
            hitRaycast.transform.GetComponent<Npc_movimiento>().animatorNpc.SetInteger("face", face);
            hitRaycast.transform.GetComponent<NpcBrauler>().DisparadorEntradaBrauler = true;
            DisparadorEvento = true;
        }

        //Disparador tienda
        else if (hitRaycast.collider.tag == "NPC"

            && hitRaycast.transform.position == hitRaycast.transform.GetComponent<Npc_movimiento>().pos
            && hitRaycast.transform.GetComponent<NPC_Dialogo>().Desactivador == true
            && hitRaycast.transform.GetComponent<Npc_Tienda>().desactivador == false)
        {

            hitRaycast.transform.GetComponent<Npc_movimiento>().Face = face;
            hitRaycast.transform.GetComponent<Npc_movimiento>().animatorNpc.SetInteger("face", face);
            hitRaycast.transform.GetComponent<Npc_Tienda>().DisparadorEvento = true;

            DisparadorEvento = true;

        }

        //ejecuta entregar Itens
        else if (hitRaycast.collider.tag == "Itens" && hitRaycast.transform.GetComponent<BaulItenScript>().Desactivador == false)
        {
            hitRaycast.transform.GetComponent<BaulItenScript>().DisparadorBaul = true;
            StopPlayer = true;
        }

        //ejecuta entregar Itens
        else if (hitRaycast.collider.tag == "Crewild" && hitRaycast.transform.GetComponent<selecionInicialCrewild>().Desactivador == false)
        {
            hitRaycast.transform.GetComponent<selecionInicialCrewild>().Disparador = true;
            hitRaycast.transform.GetComponent<selecionInicialCrewild>().ActivaObjetos();
            DisparadorEvento = true;
        }

        //recupera criatura en el lista
        if (
             hitRaycast.collider.tag == "Medico"
           && hitRaycast.transform.GetComponent<Npc_Medico>().DisparadorEventos == false
           )
        {
    
            hitRaycast.transform.GetComponent<Npc_Medico>().DisparadorEventos = true;
            DisparadorEvento = true;
        }


        if (hitRaycast.collider.tag == "Pc"
            && hitRaycast.transform.GetComponent<PcAbrir>().Activador == false)
        {
            hitRaycast.transform.GetComponent<PcAbrir>().Activador = true;
            hitRaycast.transform.GetComponent<PcAbrir>().abrirpanelopciones();
            DisparadorEvento = true;
        }

    }  


    /// <summary>
    /// activa el box collider del radio al rededor del personaje
    /// </summary>
    void HabilitadorBoxCollider(int face)
    {

        DesactivaRadioBoxCollider();
        if (face == 1)
        {
            BoxColiderRadios[2].enabled = true;
        }


        else if (face == 2)
        {
            BoxColiderRadios[3].enabled = true;
        }


        else if (face == 3)
        {
            BoxColiderRadios[0].enabled = true;
        }


        else if (face == 4)
        {
            BoxColiderRadios[1].enabled = true;
        }
            
    }
    #region Salto
    public void jumpStop()
    {
        animt.SetBool("jump", false);
        DisparadorEvento = false;
       // Invoke("invokeSalidaSalto",0.3f);
        //StartCoroutine();
    }

    public IEnumerator corector()
    {
        transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitWhile(() => transform.localScale != Vector3.zero);
        DisparadorEvento = false;
        yield return 0;
    }

   void invokeSalidaSalto()
    {
        print("llegue a salida");
        transform.localScale = new Vector2(1, 1);
        DisparadorEvento = false;

    }
    /// <summary>
    /// define el tiempo de espera del jugador antes de arrancar carrera
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    /// 


    public void JumpFuncion() 
    {
             StartCoroutine(RutinaSalto());
    }

    public IEnumerator RutinaSalto()
    {
        DisparadorEvento = true;
        GetComponent<SortingGroup>().sortingOrder = 1;
        float VarTimeJump = 0.19f;
        float TimeJump = VarTimeJump;
        float Speejump =2;
        while (TimeJump > 0) 
        {
            float AuxVar = Time.deltaTime * Speejump;
            transform.localScale += new Vector3(AuxVar, AuxVar, 0);
            TimeJump -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        TimeJump = VarTimeJump;
        while (TimeJump > 0)
        {
            float AuxVar = Time.deltaTime * Speejump;
            transform.localScale -= new Vector3(AuxVar, AuxVar, 0);
            TimeJump -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.localScale = Vector3.one;
        DisparadorEvento = false;
        GetComponent<SortingGroup>().sortingOrder = 0;
        yield return 0;
    }

    #endregion

    IEnumerator FaceTime()
    {
        beginmove[1] = false;
        yield return new WaitForSeconds(timeWait);
        beginmove[0] = true;
    }

    /// <summary>
    /// desactiva el radio que inactividad para movimiento
    /// </summary>
    void DesactivaRadioBoxCollider()
    {
        for (int i = 0; i < 4; i++)
        {
            BoxColiderRadios[i].enabled = false;
        }
    }

   

    /// <summary>
    /// sigue la una ruta simple sin evitar obstaculos para Objeto
    /// </summary>
    /// <param name="FallowPath"></param>
    /// <param name="end"></param>
    public void followpath(Vector2 origen, Vector2 destino, float Alcance)
    {
       
       
        //
        path.path(
                   origen
                 , destino
                 , Alcance);
       
        StartCoroutine(walkPath());
    }
     
    IEnumerator walkPath()
    {
     
        for (int i = 0 ; i < path.ruteNum.Count  ;  i++)
        {
            //
            if (path.ruteNum[i] == 0)
            {
                pos += Vector3.up * DistanciaRecorrida;// Add -1 to pos.y
                animt.SetInteger("face", 3);
            }

            else if (path.ruteNum[i] == 1)
            {
                pos += Vector3.right * DistanciaRecorrida;// Add -1 to pos.x
                animt.SetInteger("face", 4);
            }
            else if (path.ruteNum[i] == 2)
            {
                pos += Vector3.down * DistanciaRecorrida;// Add 1 to pos.y
                animt.SetInteger("face", 1);
            }
            else if (path.ruteNum[i] == 3)
            {
                pos += Vector3.left * DistanciaRecorrida;// Add 1 to pos.x
               animt.SetInteger("face", 2);
            }
            yield return new WaitWhile(() =>  transform.position != pos);
        }
        path.ruteNum.Clear();

        //llegue Ruta
    }

}

/// <summary>
/// simple path o ruta solo marca una ruta que sugue el  jugador
/// </summary>
public class pathfinderGrid : MonoBehaviour
{
    public Vector2Int player;
    public Vector2Int movevector;
    public bool boolpath;

    /// <summary>
    /// litsa de ruta a seguir por el personaje
    /// </summary>
   public List<int> ruteNum = new List<int>();

    /// <summary>
    /// define cual la ruta a seguir  una direccion  para cada eje
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="rute"></param>
    /// <param name="DistaciaRecorrida"></param>
  public  void path(Vector2 origin, Vector2 rute, float DistaciaRecorrida)
    {


        ruteNum.Clear();
        List<float> directions = new List<float>();
        // 
        // int prueba = 0;
        //---->reacer
        while (ManhattanDistance(origin, rute) != 0)
        {
            //define cual es el paso mas corto en depeniendo cual distancia es menor usano le metodo ma ManhattanDistance
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    directions.Add(ManhattanDistance(origin + (Vector2.up * DistaciaRecorrida), rute));
                }
                else if (i == 1)
                {
                    directions.Add(ManhattanDistance(origin + (Vector2.right * DistaciaRecorrida), rute));
                }

                else if (i == 2)
                {
                    directions.Add(ManhattanDistance(origin +(Vector2.down * DistaciaRecorrida), rute));
                }
                else if (i == 3)
                {
                    directions.Add(ManhattanDistance(origin + (Vector2.left * DistaciaRecorrida), rute));
                }
            }


            //ordena los numeros de menor a mayor
            float auxRevision = -1;
            int numDir = 0;
            for (int i = 0; i < directions.Count; i++)
            {
                if (auxRevision == -1)
                {
                    auxRevision = directions[i];
                    numDir = i;
                }

                else if (auxRevision > directions[i])

                {
                    auxRevision = directions[i];
                    numDir = i;
                }
            }


            // añade la rua  seguir
            if (numDir == 0)
            {
                origin = origin + Vector2.up* DistaciaRecorrida;
            }
            else if (numDir == 1)
            {
                origin = origin + Vector2.right * DistaciaRecorrida;
            }

            else if (numDir == 2)
            {
                origin = origin + Vector2.down * DistaciaRecorrida;
            }
            else if (numDir == 3)
            {
                origin = origin + Vector2.left * DistaciaRecorrida;
            }


            ruteNum.Add(numDir);
           

            directions.Clear();

        }
        // numeros a recorrer. esto es un verificador
          for (int i = 0 ; i < ruteNum.Count ; i++)
          {
              Debug.Log(ruteNum[i]);
          }


     
    }

    /// <summary>
    /// Arroja la distancia en Float  de cual la ditancia entre  la suma de dos sus catetos contiguo y opuesto
    /// </summary>
    /// <param name="a">puede ser el punto final o inicial</param>
    /// <param name="b">puede ser el punto inicial o final</param>
    /// <returns></returns>
    public static float ManhattanDistance(Vector2 a, Vector2 b)
    {
        checked
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }


}