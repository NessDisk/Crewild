using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Npc_move : MonoBehaviour {


    public Animator animatorNpc;
    // posicion del playerobjet
    public Vector3 pos;
    
    public float speed;


    public int tiempoVariable;
    private  float TimeMove ;

    /// <summary>
    /// bool rutina move aleatore
    /// </summary>
    bool AccionMove;
       
    /// <summary>
    /// Ranger Border
    /// </summary>
 public   int Rb = 5;

    /// <summary>
    /// num de la posicion en el cuadro
    /// </summary>
    int Np= 0;

    /// <summary>
    /// lista esquinas
    /// </summary>
    List<int> l1N = new List<int>();

    /// <summary>
    /// centrales de los bordes
    /// </summary>
    List<int> l2N = new List<int>();
    
    /// <summary>
    /// centrales borde isquierdo
    /// </summary>
    List<int> l4N = new List<int>();

    /// <summary>
    ///  centrales borde dereho
    /// </summary>
    List<int> l5N = new List<int>();

    /// <summary>
    /// interior 
    /// </summary>
    List<int> l3N = new List<int>();
    
    public bool NPCStop , triggerEntracebattle;
     
    /// <summary>
    /// define en que posicion  inicia la rutina aleatoria de movimiento esta el objeto.
    /// </summary>
  public   int actualPos ;


    [Range(0, 2)]
    /// <summary>
    /// metodo de movimeinto de los npc;
    /// 0 = aletore move in Range border distance^2 (RB^2);
    /// 1 = loot move rutine;
    /// 2 = loot move ping pong
    /// </summary>
    public int metodoMove;

   private NPC_Dialoge NpcDialogue;

    [Range(0,3)]
    public int[] lootArrayMove;

    /// <summary>
    /// define the position in loot move npc ArrayMove
    /// </summary>
    int lootInter;

    /// <summary>
    /// direction move en metodo 1 loot move
    /// </summary>
    int dirloot = 1;

    bool decreases ;
    // Use this for initialization

    public BoxCollider2D box2d;


    public Transform playerTransf;

    public int AuxMoveObj;

    public Npc_BrawlerScritp NpcBrawlerScript;

    void Start ()
    {

        animatorNpc = GetComponent<Animator>();

        NpcDialogue = GetComponent<NPC_Dialoge>();

        actualPos = (int)Mathf.Pow((float)Rb, 2) / 2;

        NpcBrawlerScript = GetComponent<Npc_BrawlerScritp>();

        box2d = GetComponent<BoxCollider2D>();

        //Debug.Log((int)Mathf.Pow(5,2));
        pos = transform.position;

        lootInter = 0;

       AccionMove = true;

        //define los todos los puntos de un cuadro dependiendo del tamaño.
        for (int i = 1 ; i <=Rb; ++i)
        {
            for (int x = 1; x <= Rb; ++x)
            {
                Np += 1;
               if (Np == 1 
                    || Np == Rb
                    || Np == Rb * Rb
                    || Np == (Rb * Rb) - Rb+1
                   )
                {
                    l1N.Add(Np);
                }
                else if (Np > 1 && Np < Rb 
                    || i > 1 && i < Rb && x == 1
                    || x == Rb && i != 1 && i != Rb 
                    || Np > (Rb * Rb) - Rb && Np < Rb * Rb)
                {
                    l2N.Add(Np);
                }
                else if (i >1 
                         && x > 1
                         && i < Rb 
                         && x <Rb)
                {
                    l3N.Add(Np);
                }

              
                
            }
            
        }


       

        int numAux = 1;
        int numAux2 = 2;
        for (int i = 0; i < l2N.Count; i++)
        {
         
            if (l2N[i] > Rb && l2N[i] <= (Rb * Rb) - Rb)
            {

                
                if (l2N[i] == (Rb * numAux) + 1)
                {

                    l4N.Add(l2N[i]);
                    numAux++;
                }

                else if(l2N[i] == (Rb * numAux2))
               {
                   
                    l5N.Add(l2N[i]);
                    numAux2++;
                }

            }

            

        }

        playerTransf = GameObject.Find("personaje").GetComponent<Transform>();

       
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (NPCStop == true)
        {
            return;
        }
        
        //The Current Position = Move To (the current position to the new position by the speed * Time.DeltaTime)
        transform.position = Vector3.MoveTowards(transform.position, pos , speed * Time.deltaTime);    // Move there

        if (transform.position != pos)
        {
            animatorNpc.SetBool("walk", true);
        }
        else
        {
            box2d.offset = new Vector2(0, 0);
            animatorNpc.SetBool("walk", false);
        }



   if (NpcBrawlerScript.TriggerBrawler ==  false && AccionMove == true && NpcDialogue.ActDialogue == false)
        {
            if ( metodoMove == 0  )

            {
                TimeMove = (float)(int)Random.Range(1, tiempoVariable);
                StartCoroutine(MoveTime());
                AccionMove = false;
            }


            else if
                  (
                        metodoMove == 1
                     || metodoMove == 2
                  )
            {

                TimeMove = (float)(int)Random.Range(1, tiempoVariable);
                StartCoroutine(Moveloot());
                AccionMove = false;
            }

        }

    }


    IEnumerator MoveTime()
    {


        int direction = 0;

        // arrayActivate[0] = right
        // arrayActivate[1] = left
        // arrayActivate[2] = up 
        // arrayActivate[3] = down
        bool[] arrayActivate = new bool[] {true, true, true, true };

      

        bool definidepos = false;


        //esquinas
        

        if (actualPos == 1)
        {
            arrayActivate[1] = false;
            arrayActivate[2] = false;
          
        }
        else if (actualPos == Rb)
        {
            arrayActivate[0] = false;
            arrayActivate[2] = false;
        }
        else if (actualPos == Rb * Rb)
        {
            arrayActivate[0] = false;
            arrayActivate[3] = false;
        }
        else if (actualPos == (Rb * Rb) - Rb+1)
        {

            arrayActivate[1] = false;
            arrayActivate[3] = false;
        }

        else
        {
            if (definidepos == false)
            {
                //linea superior
                for (int i = 0; i < l2N.Count; i++)
                {
                    if (l2N[i] == actualPos)
                    {
                        if (actualPos > 1 && actualPos < Rb)
                        {
                            arrayActivate[2] = false;

                            definidepos = true;
                            break;
                        }
                    }
                }
            }

            if (definidepos == false)
            {
                //linea isquierda
                for (int i = 0; i < l4N.Count; i++)
                {
                    if (l4N[i] == actualPos)
                    {

                        arrayActivate[1] = false;

                        definidepos = true;
                        break;

                    }
                }
            }


            if (definidepos == false)
            {
                //linea derecha
                for (int i = 0; i < l5N.Count; i++)
                {
                    if (l5N[i] == actualPos)
                    {
                        arrayActivate[0] = false;

                        definidepos = true;
                        break;

                    }

                }
            }

            if (definidepos == false)
            {
                //linea inferior
                for (int i = 0; i < l2N.Count; i++)
                {
                    if (l2N[i] == actualPos)
                    {
                        if (actualPos > (Rb*Rb)-Rb+1 && actualPos < Rb*Rb)
                        {
                            arrayActivate[3] = false;

                            definidepos = true;
                            break;
                        }
                    }
                }
            }

        }
    /*    // relleno
        for (int i = 0; i < l3N.Count; i++)
        {
            if (l3N[i] == actualPos)
            {
              //  Debug.Log("estoy en posiactual;  " + l3N[i] );
            }
        }
       */

      direction = Random.Range(1, 5);
        animatorNpc.SetInteger("face", direction);

      //  Debug.Log(direction);
        if (direction == 4 && NpcDialogue.obstacle[0] == false)
        {
           

            if (arrayActivate[0] == true)
            {
                box2d.offset = new Vector2(-1, 0);
                actualPos += 1;
                pos += Vector3.right * 1;// Add 1 to pos.x
            }

        }

        else if (direction == 2 && NpcDialogue.obstacle[1] == false)
        {

        
            if (arrayActivate[1] == true)
            {
                box2d.offset = new Vector2(-1, 0);
                actualPos -= 1;
                pos += Vector3.left * 1;// Add 1 to pos.x
            }
        }
       
        else if (direction == 1 && NpcDialogue.obstacle[2] == false)
        {

        
            if (arrayActivate[3] == true)
            {
                box2d.offset = new Vector2(0,-1 );
                actualPos += Rb;
                pos += Vector3.down * 1;// Add 1 to pos.x
            }
        }


        else if (direction == 3 && NpcDialogue.obstacle[3] == false)
        {
    
            if (arrayActivate[2] == true)
            {
                box2d.offset = new Vector2(0, 1);
                actualPos -= Rb;
                pos += Vector3.up * 1;// Add 1 to pos.x
            }
        }

        //Debug.Log("estoy en posiactual;  " + actualPos);
        yield return 0;
        yield return new WaitForSeconds(TimeMove);

       

            AccionMove = true;
    }



    IEnumerator Moveloot()
    {




        /// <summary>
        /// bool define  incremet   lootInter++
        /// </summary>
        bool PauseIncreInterg = false;


       



        






        if (lootInter >= lootArrayMove.Length && metodoMove == 1)
        {

            lootInter = 0;

        }

        else if (lootInter >= lootArrayMove.Length && metodoMove == 2 && decreases == false)
        {
            --lootInter;
            decreases = true;
        }

        else if (lootInter <= -1 && metodoMove == 2 && decreases == true)
        {

            lootInter++;
            decreases = false;

        }



        // dir
        //  0 = down
        //  1 = left
        //  2 = up
        //  3 = right  
        dirloot = lootArrayMove[lootInter];


        if (
            
                dirloot == 0 && NpcDialogue.obstacle[2] == true  && decreases == false
             || dirloot == 1 && NpcDialogue.obstacle[1] == true && decreases == false
             || dirloot == 2 && NpcDialogue.obstacle[3] == true && decreases == false
             || dirloot == 3 && NpcDialogue.obstacle[0] == true && decreases == false

             || dirloot == 2 && NpcDialogue.obstacle[2] == true && decreases  == true
             || dirloot == 3 && NpcDialogue.obstacle[1] == true && decreases  == true
             || dirloot == 0 && NpcDialogue.obstacle[3] == true && decreases  == true
             || dirloot == 1 && NpcDialogue.obstacle[0] == true && decreases  == true

             )

        {
            PauseIncreInterg = true;
            
        }


        // if (PauseIncreInterg == false)
        // {

       

        if (dirloot == 3)
        {

            if (decreases == false || metodoMove == 1)
            {
                animatorNpc.SetInteger("face", 4);
            }

            else if(decreases == true)
            {
                animatorNpc.SetInteger("face", 2);
            }


            if (
                decreases == false && NpcDialogue.obstacle[0] == false 
                || metodoMove == 1 && NpcDialogue.obstacle[0] == false
               )
                    {

                box2d.offset = new Vector2(-1,0);
                        pos += Vector3.right * 1;// Add 1 to pos.x

                    }
                    else if (decreases == true && NpcDialogue.obstacle[1] == false)
                    {
                  
                        pos -= Vector3.right * 1;// Add 1 to pos.x
                box2d.offset = new Vector2(-1, 0);
            }
                

            }

            else if (dirloot == 1 )
            {

            if (decreases == false || metodoMove == 1)
            {
                animatorNpc.SetInteger("face", 2);
            }

            else if (decreases == true)
            {
                animatorNpc.SetInteger("face", 4);
            }

            if (
                decreases == false && NpcDialogue.obstacle[1] == false 
                || metodoMove == 1 && NpcDialogue.obstacle[1] == false
               )
                    {
                    box2d.offset = new Vector2(-1, 0);
                    pos += Vector3.left * 1;// Add 1 to pos.x
                    }

                    else if (decreases == true && NpcDialogue.obstacle[0] == false)
                    {
                     
                        pos -= Vector3.left * 1;// Add 1 to pos.x
                        box2d.offset = new Vector2(-1, 0);
            }
                

               
            }

            else if (dirloot == 0 )
            {

            if (decreases == false || metodoMove == 1)
            {
                animatorNpc.SetInteger("face", 1);
            }

            else if (decreases == true)
            {
                animatorNpc.SetInteger("face", 3);
            }

            if (
                decreases == false && NpcDialogue.obstacle[2] == false
                || metodoMove == 1 && NpcDialogue.obstacle[2] == false
                )
                    {
                     box2d.offset = new Vector2(0, -1);
                     pos += Vector3.down * 1;// Add 1 to pos.x
                    }

                    else if (decreases == true && NpcDialogue.obstacle[3] == false)
                    {
                box2d.offset = new Vector2(0, 1);
                pos -= Vector3.down * 1;// Add 1 to pos.x

                    }
                

                
            }


            else if (dirloot == 2 )
            {

            if (decreases == false || metodoMove == 1)
            {
                animatorNpc.SetInteger("face", 3);
            }

            else if (decreases == true)
            {
                animatorNpc.SetInteger("face", 1);
            }

            if (
                decreases == false && NpcDialogue.obstacle[3] == false 
                || metodoMove == 1 && NpcDialogue.obstacle[3] == false
                )
                    {
                     box2d.offset = new Vector2(0, 1);
                pos += Vector3.up * 1;// Add 1 to pos.x
                    }

                    else if (decreases == true && NpcDialogue.obstacle[2] == false)
                    {
                     box2d.offset = new Vector2(0, -1);
                    pos -= Vector3.up * 1;// Add 1 to pos.x

                    }
                

                
           // }
        }
        //Debug.Log("estoy en posiactual;  " + actualPos);
        yield return 0;
        yield return new WaitForSeconds(TimeMove);


        if (
            PauseIncreInterg == false && metodoMove == 1 
         || PauseIncreInterg == false && decreases == false && metodoMove == 2
           )
             {

            lootInter++;

             }

        else  if (PauseIncreInterg == false && decreases == true && metodoMove == 2)
        {
            lootInter--;

        }



        AccionMove = true;
    }


    public int ManhattanDistance(Vector2Int a, Vector2Int b)
    {
        checked
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }

    /// <summary>
    /// hace que el npc siga una direccion de movimiento hasta el obj player
    /// </summary>
    public void NpcMoveBrawlerPlayer()
    {
        AuxMoveObj =   ManhattanDistance(
                          new Vector2Int((int)transform.position.x, (int)transform.position.y),
                           new Vector2Int((int)playerTransf.position.x, (int)playerTransf.position.y)
                                        );
        StartCoroutine(walkPath());
    }


    IEnumerator walkPath()
    {

        for (int i = 0; i < AuxMoveObj-1; i++)
        {
            if (animatorNpc.GetInteger("face") == 1)
            {
                pos += Vector3.down * 1;// Add -1 to pos.y
             
            }

            else if (animatorNpc.GetInteger("face") == 2)
            {
                pos += Vector3.left * 1;// Add -1 to pos.x
               
            }
            else if (animatorNpc.GetInteger("face") == 3)
            {
                pos += Vector3.up * 1;// Add 1 to pos.y
               
            }
            else if (animatorNpc.GetInteger("face") == 4)
            {
                pos += Vector3.right * 1;// Add 1 to pos.x
     
            }
            yield return new WaitForSeconds(0.5f);
        }
        //trigger finish accion here
        triggerEntracebattle = true;
    }

}
