using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private float moveSpeed = 3f;
    private float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Horizontal;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private int x, y;
    private float xAux, yAux;
    private float factor; 

    private int prioridad = 0;

   public bool boolpath;

    List<int> ruteNum = new List<int>();

    public Vector2Int player;
    public Vector2Int movevector;
    public Vector2Int NewPosicion;

    void Start()
    {

        //     movevector = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        movevector = new Vector2Int((int)GameObject.Find("targer").GetComponent<Transform>().position.x, (int)GameObject.Find("targer").GetComponent<Transform>().position.y);
        player = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    public void FixedUpdate()
    {
       // targer = new Vector2Int((int)GameObject.Find("targer").GetComponent<Transform>().position.x, (int)GameObject.Find("targer").GetComponent<Transform>().position.y);
       // Debug.Log(targer);
       // Debug.Log(ManhattanDistance(new Vector2Int((int)transform.position.x, (int)transform.position.y), movevector));

        if (boolpath == true)
        {
            movevector = new Vector2Int((int)GameObject.Find("targer").GetComponent<Transform>().position.x, (int)GameObject.Find("targer").GetComponent<Transform>().position.y);
            player = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            ruteNum.Clear();
           // Debug.Log(ManhattanDistance(player , movevector)); 
             path();
        }

        if (x == 1 && y == 0 )
        {
            Debug.Log("derecha");
        }
        else if (x == -1 && y == 0)
        {
            Debug.Log("isquierda");
        }
        else if (x == 0 && y == 1)
        {
            Debug.Log("arriba ");
        }
        else if (x == 0 && y == -1)
        {
            Debug.Log("abajo");
        }
       
    }

    IEnumerator MoveSimpleGrif()
    {
        yield return new WaitForSeconds(1f);
    }

    void path()
    {
       

      
        List<int> directions = new List<int>();
        // 
       // int prueba = 0;
        while (ManhattanDistance(movevector, player) != 0)
        {
           
            for (int i = 0; i<= 3; i++)
            {
                if (i == 0)
                {
                    directions.Add(ManhattanDistance(movevector, player + Vector2Int.down));
                }
                else if (i == 1)
                {
                    directions.Add(ManhattanDistance(movevector , player + Vector2Int.left));
                }

                else if (i == 2)
                {
                    directions.Add(ManhattanDistance(movevector , player + Vector2Int.up));
                }
                else if (i == 3)
                {
                    directions.Add(ManhattanDistance(movevector , player + Vector2Int.right));
                }
            }

            int auxRevision = -1;
            int numDir = 0;
            for (int i = 0 ; i < directions.Count; i++ )
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


            if (numDir == 0)
            {
                player = player + Vector2Int.down;
            }
            else if (numDir == 1)
            {
                player = player + Vector2Int.left;
            }

            else if (numDir == 2)
            {
                player = player + Vector2Int.up;
            }
            else if (numDir == 3)
            {
                player = player + Vector2Int.right;
            }

          //  Debug.Log(movevector);

            ruteNum.Add(numDir);
            //prueba++;
           
            directions.Clear();

        }
      /* numeros a recorrer.
        for (int i = 0 ; i < ruteNum.Count ; i++)
        {
            Debug.Log(ruteNum[i]);
        } */  
       
     
        boolpath = false;
    }

    // in Vector2Int
    public static int ManhattanDistance(Vector2Int a, Vector2Int b)
    {
        checked
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}