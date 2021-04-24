using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hierbas : MonoBehaviour
{

    public Sprite[] hojas = new Sprite[2];

    SpriteRenderer Renderhojas;

   

    public DefinirCreaturasAllamar[] Criaturas;

    public static float ProbabilidadEncuentro = 1f;

    /// <summary>
    /// llama  a los metodos de Npcbraler
    /// </summary>
    public NpcBrauler modoBrauler;

    public AudioClip SonidoHierba;

    AudiosMenus Audios;

    
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer[] captadorRender = GetComponentsInChildren<SpriteRenderer>();

      // Debug.Log(Criaturas[0].criatura.NombreTaxonomico);

        //encapsula render
        foreach (SpriteRenderer Sprd in captadorRender)
        {
            if (Sprd.name == "hojas")
            {
                Renderhojas = Sprd;
            }

        }

        modoBrauler = new NpcBrauler();
        modoBrauler.inicializa();

        hierbas.ProbabilidadEncuentro = 5f;

        Audios = FindObjectOfType<AudiosMenus>();

        //Criaturas = GameObject.Find("Game Manager").GetComponent<ControlManager>().Criaturas;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            Renderhojas.sprite = hojas[1];
        }
       
        if (collision.tag == "Player")
        {
            Renderhojas.sprite = hojas[1];
            if (collision.transform.GetComponent < movimiento>().DisparadorEvento == false)
            {
                hierbas.ProbabilidadEncuentro += 1.5f;
                EncuentroRandon(hierbas.ProbabilidadEncuentro, collision.transform.GetComponent<movimiento>());
                Audios.AudioAuxiliar.PlayOneShot(SonidoHierba);
            }
          
           
         

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Renderhojas.sprite = hojas[0];
        if (collision.tag == "Player")
        {
            Renderhojas.sprite = hojas[0];
        }
       
    }
    /// <summary>
    /// mide la probabilidad  que haya un encuentro randon y el nivel de raresa
    /// </summary>
    /// <param name="Probabilidad"></param>
    /// <param name="Movplayer"></param>
    void EncuentroRandon(float Probabilidad, movimiento Movplayer)
    {
        Probabilidad = Probabilidad / 100;

        float ProbabilidadDeExito = Random.Range(0.01f,1.00f);
       // print("ProbabilidadDeExito: "+ ProbabilidadDeExito+ ", Probabilidad: "+Probabilidad);

        if (ProbabilidadDeExito < Probabilidad)
        {

            Criaturas = GameObject.Find("Game Manager").GetComponent<ControlManager>().Criaturas;

            Movplayer.DisparadorEvento = true;
            hierbas.ProbabilidadEncuentro = 1f;
            EjecutarBrawler(Probabilidad, Criaturas);
           
        }

       
    }


    


   
    void EjecutarBrawler(float RaresaDeEncuentro, DefinirCreaturasAllamar[] ArrayCall )
    {
        CrewildBase[] listaCriaturas;      


        int PisinaDeCriaturas = 0;

        for (int i = 0; i< ArrayCall.Length; i++)
        {
            //encapsular crewild base 
            ArrayCall[i].criatura = modoBrauler.RetornaCriatura(ArrayCall[i]);

            print(ArrayCall[i].criatura.NombreTaxonomico);         

            //agrega en una lista las criaturas dependendo del nivel de raresa
            if (ArrayCall[i].criatura.NivelRaresa <= RaresaDeEncuentro)
            {
                PisinaDeCriaturas++;
            }
        }


        int numAux = 0;
        //seleccion por defecto
        if (PisinaDeCriaturas == 0)
        {
            listaCriaturas = new CrewildBase[1];
            for (int i = 0; i < ArrayCall.Length; i++)
            {
                if (listaCriaturas[0] == null)
                {
                    listaCriaturas[0] = ArrayCall[i].criatura;

                }
                else if (listaCriaturas[0].NivelRaresa <= RaresaDeEncuentro)
                {
                    listaCriaturas[0] = ArrayCall[i].criatura;
                }
            }
        }
        ///seleccion por probabilidad raresa de encuentro
        else
        {
            listaCriaturas = new CrewildBase[PisinaDeCriaturas];
            for (int i = 0; i < ArrayCall.Length; i++)
            {
                if (ArrayCall[i].criatura.NivelRaresa <= RaresaDeEncuentro)
                {
                    listaCriaturas[numAux] = ArrayCall[i].criatura;
                    numAux++;
                }
            }


        }


       

    
        int CriaturaAllamar =  Random.Range(0, listaCriaturas.Length);
        print(listaCriaturas.Length);

     //   Debug.Log("Se llamo a esta critaura "+listaCriaturas[CriaturaAllamar].NombreTaxonomico);

        IniciaModoBrawlerEncuentroRandom(listaCriaturas[CriaturaAllamar]);

        }


    /// <summary>
    /// de comienzo  al transicion al modo Brauler en las cinematicas
    /// </summary>
    void IniciaModoBrawlerEncuentroRandom( CrewildBase crewild)
    {
        FindObjectOfType<animationScritpBatle>().randomEncounter = true;

        modoBrauler.InvokeEntraCinematica(crewild);
        StartCoroutine(modoBrauler.iniciaBrauler());
       
    }

}

  

