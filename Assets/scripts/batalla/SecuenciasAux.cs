using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class  SecuenciasAux 
{

    
    public static  IEnumerator SecunciaDaño(Image Crewild)
    {
        for(int i = 0;i <7 ; i++ )
        {
            Debug.Log("pista secuencia");
            Crewild.color = new Color(1,1,1,0.5f);
            yield return new WaitForSeconds(0.05f);
            Crewild.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(0.05f);
        }


    }

    public static IEnumerator EnvenenamientoEfecto(Image Crewild)
    {

        for (int i = 0; i < 2; i++)
        {
          
            float Purpura = 1, velAnim = 2.5f;
            while (Purpura >= 0.2)
            {

                Purpura -= Time.deltaTime * velAnim;
                Crewild.color = new Color(1, Purpura, 1, 1f);
                yield return new WaitForSeconds(0.01f);
            }
            while (Purpura < 1)
            {
                Purpura += Time.deltaTime * velAnim;
                if (Purpura > 1)
                    Purpura = 1;

                Crewild.color = new Color(1, Purpura, 1, 1f);
                yield return new WaitForSeconds(0.01f);
            }

        }

        CalculoDaño.PausaEjecucionEvento = false;


    }


    public static void CambiaEstadoCrewild(string Atacante, EstadosEnum EstadoAlterado)
    {
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        if (Atacante == "Player")
        {
            libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].EstadosCrewild = EstadoAlterado;
            libreriaS.Batalla.EstadosBatleCuadro[1].SetActive(true);
            libreriaS.Batalla.EstadosBatleCuadro[1].GetComponentInChildren<Text>().text = InfoEstado(EstadoAlterado);
            libreriaS.Batalla.EstadosBatleCuadro[1].GetComponent<Image>().color = informacionCrewild.RetornAColor(EstadoAlterado);
        }

        else if (Atacante == "Enemy")
        {
            libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].EstadosCrewild = EstadoAlterado;
            libreriaS.Batalla.EstadosBatleCuadro[0].SetActive(true);
            libreriaS.Batalla.EstadosBatleCuadro[0].GetComponentInChildren<Text>().text = InfoEstado(EstadoAlterado);
            libreriaS.Batalla.EstadosBatleCuadro[0].GetComponent<Image>().color = informacionCrewild.RetornAColor(EstadoAlterado);


        }

    }

    public static void CambiaEstadoCrewild(DefiniteObject DefinePlayer, CrewildBase crewildBase)
    {
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        if (DefinePlayer == DefiniteObject.Enemy)
        {
            libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].EstadosCrewild = crewildBase.EstadosCrewild;
            libreriaS.Batalla.EstadosBatleCuadro[1].SetActive(true);
            libreriaS.Batalla.EstadosBatleCuadro[1].GetComponentInChildren<Text>().text = InfoEstado(crewildBase.EstadosCrewild);
            libreriaS.Batalla.EstadosBatleCuadro[1].GetComponent<Image>().color = informacionCrewild.RetornAColor(crewildBase.EstadosCrewild);
        }

        else if (DefinePlayer == DefiniteObject.Player)
        {
            libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].EstadosCrewild = crewildBase.EstadosCrewild;
            libreriaS.Batalla.EstadosBatleCuadro[0].SetActive(true);
            libreriaS.Batalla.EstadosBatleCuadro[0].GetComponentInChildren<Text>().text = InfoEstado(crewildBase.EstadosCrewild);
            libreriaS.Batalla.EstadosBatleCuadro[0].GetComponent<Image>().color = informacionCrewild.RetornAColor(crewildBase.EstadosCrewild);


        }

    }

    public static void EstadoNormal(DefiniteObject DefinePlayer, CrewildBase crewildBase)
    {
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        if (DefinePlayer == DefiniteObject.Enemy)
        {
            libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].EstadosCrewild = crewildBase.EstadosCrewild;
           
            libreriaS.Batalla.EstadosBatleCuadro[1].GetComponentInChildren<Text>().text = InfoEstado(crewildBase.EstadosCrewild);
            libreriaS.Batalla.EstadosBatleCuadro[1].GetComponent<Image>().color = informacionCrewild.RetornAColor(crewildBase.EstadosCrewild);
            libreriaS.Batalla.EstadosBatleCuadro[1].SetActive(false);
        }

        else if (DefinePlayer == DefiniteObject.Player)
        {
            libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].EstadosCrewild = crewildBase.EstadosCrewild;           
            libreriaS.Batalla.EstadosBatleCuadro[0].GetComponentInChildren<Text>().text = InfoEstado(crewildBase.EstadosCrewild);
            libreriaS.Batalla.EstadosBatleCuadro[0].GetComponent<Image>().color = informacionCrewild.RetornAColor(crewildBase.EstadosCrewild);
            libreriaS.Batalla.EstadosBatleCuadro[0].SetActive(false);
        }
    }

        public static string InfoEstado(EstadosEnum Estado)
    {
        string DiminutivoAretornar = "";

        switch (Estado)
        {
            case EstadosEnum.poison:
                DiminutivoAretornar = "Ven";
                break;
            case EstadosEnum.Paralize:
                DiminutivoAretornar = "Par";
                break;
        }
        
        return DiminutivoAretornar;
    }


   

        /// <summary>
        /// devuelve los sprite iniciales de los personajes
        /// </summary>
        /// <param name="Atacante"></param>
    public static void DevuelveSpriteBase(string Atacante)
    {
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        if (Atacante == "Player")
        {
            libreriaS.Batalla.imagenePlayer.sprite = libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[0];
        }

        else if (Atacante == "Enemy")
        {

            libreriaS.Batalla.ImagenEnemigo.sprite = libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[0];
        }

    }

    public static Image RetornarObjImageBattle(bool TwovsTwo, string QuienAtaca)
    {
        UnityEngine.UI.Image ImagenPersonaje = null;
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        if (TwovsTwo == false)
        {
            //ataca de espalda
            if (QuienAtaca == "Player")
            {
                ImagenPersonaje = libreriaS.Batalla.GetComponent<animationScritpBatle>().ImagenEnemigo;
                libreriaS.Batalla.imagenePlayer.sprite = libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].animaCrewildEspalda[2];
            }
            //ataca de frente
            else if (QuienAtaca == "Enemy")
            {
                //   animationBrawler.AddClip(this.animaBattle[1], "Attack");
                ImagenPersonaje = libreriaS.Batalla.GetComponent<animationScritpBatle>().imagenePlayer;
                libreriaS.Batalla.ImagenEnemigo.sprite = libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].animaCrewildFrentre[2];             
            }
          
        }

        else if (TwovsTwo == true)
        {
            // sin implementaciontodavia
        }
        return ImagenPersonaje;
    }

    public static Image RetornarObjImageBattleInversa(bool TwovsTwo, string QuienAtaca)
    {
        UnityEngine.UI.Image ImagenPersonaje = null;
        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        if (TwovsTwo == false)
        {
            //ataca de espalda
            if (QuienAtaca == "Enemy")
            {
                ImagenPersonaje = libreriaS.Batalla.GetComponent<animationScritpBatle>().ImagenEnemigo;
              
            }
            //ataca de frente
            else if (QuienAtaca == "Player")
            {
                //   animationBrawler.AddClip(this.animaBattle[1], "Attack");
                ImagenPersonaje = libreriaS.Batalla.GetComponent<animationScritpBatle>().imagenePlayer;
              
            }

        }

        else if (TwovsTwo == true)
        {
            // sin implementaciontodavia
        }
        return ImagenPersonaje;
    }

    public static bool RetornaSitieneEstadoAlterado(bool TwovsTwo, string QuienAtaca)
    {
        bool valorARetonar = false;

        libreriaDeScrips libreriaS = GameObject.FindObjectOfType<libreriaDeScrips>();
        if (TwovsTwo == false)
        {
            //ataca de espalda
            if (QuienAtaca == "Enemy" && libreriaS.informacionCrewild.CrewillInstancia[libreriaS.Batalla.ActualSelNumPlayer].EstadosCrewild == EstadosEnum.None)
                valorARetonar = true;

            //ataca de frente
            else if (QuienAtaca == "Player" && libreriaS.Batalla.EnemigosBatalla[libreriaS.Batalla.ActualSeNumEnemy].EstadosCrewild == EstadosEnum.None)
                valorARetonar = true;
        }

        else if (TwovsTwo == true)
        {
            // sin implementaciontodavia
        }

        return valorARetonar;

    }
}

