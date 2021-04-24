using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(Entrada)), CanEditMultipleObjects]
public class MyScriptEditor : Editor
{
    SerializedProperty NombreEntrada;
    SerializedProperty nombreSalida;
    bool AplicChange;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        NombreEntrada = serializedObject.FindProperty("NombreEntrada");
        nombreSalida = serializedObject.FindProperty("nombreSalida");
    }

    bool CambiaValorTodosLosObjetosselect => NombreEntrada.hasMultipleDifferentValues || nombreSalida.hasMultipleDifferentValues;
 


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var myScript = target as Entrada;

        myScript.modoDoor = (ModoDoor)EditorGUILayout.EnumPopup("My type", myScript.modoDoor);
              

        if (myScript.modoDoor == ModoDoor.nombre)
        {         

            myScript.NombreEntrada = EditorGUILayout.TextField("NombreEntrada", myScript.NombreEntrada);
            myScript.nombreSalida = EditorGUILayout.TextField("nombreSalida", myScript.nombreSalida);

            //  EditorGUILayout.IntSlider(NombreEntrada, 0, 100, new GUIContent("NombreEntrada"));

          
        

        }
        else if (myScript.modoDoor == ModoDoor.transfor)
        {
 
            myScript.Destino = EditorGUILayout.ObjectField("Destino", myScript.Destino, typeof(Transform), true) as Transform;
        }

        else if (myScript.modoDoor == ModoDoor.cargarScena)
        {

            myScript.NombreScenaAcargar = EditorGUILayout.TextField("NombreScenaAcargar", myScript.NombreScenaAcargar);
            myScript.posicionInstancia = EditorGUILayout.Vector2Field("posicionInstancia", myScript.posicionInstancia);

        }

        if (serializedObject.isEditingMultipleObjects)
        {
            base.OnInspectorGUI();


        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(myScript);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
[System.Serializable]
public enum ModoDoor
{
    [Tooltip("mueve al player dentro de la misma escena por las variables del script de salida Entrada ")]
    nombre,
    [Tooltip("mueve al player dentro de la misma escena por una posicion transfor ")]
    transfor,
    [Tooltip(" carga una nueva escena ")]
    cargarScena
}


public class Entrada : MonoBehaviour
{
 

    [Header("Cargar escena")]
    #region Carga escena 
    [Tooltip("Nombre de la scena a cargar")]
    public string NombreScenaAcargar;
    public Vector2 posicionInstancia;
    public static Vector2 posicionInstanciaStatic;
    #endregion
    [Header("cambia pos en scena actual")]
    #region cambia pos en scena actual



    public ModoDoor modoDoor = ModoDoor.nombre ;


    public string  NombreEntrada;
    public string nombreSalida;
    static public bool BoolSaltoDeScena;
    static public string StaticSalidaString;

    [Tooltip("define el destino a salida teniendo como referencia Transfor del destino si no es null se usa el nombre del destino")]
    public Transform Destino;

    #endregion
    private libreriaDeScrips LibresiaS;



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {

            col.transform.GetComponent<movimiento>().DisparadorEvento = true;

            GameObject.Find("transiciones").GetComponent<Canvas>().enabled = true;



            if (modoDoor != ModoDoor.cargarScena)
                StartCoroutine(Transicion());        

            else if (modoDoor == ModoDoor.cargarScena)
            {
                FindObjectOfType<informacionCrewild>().GuardaData();
                StartCoroutine(TransicionCargarescena());
            }
              
            // SceneManager.LoadScene(nombreDeLaScena);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LibresiaS = GameObject.Find("Game Manager").GetComponent<libreriaDeScrips>();
    }

    void  MoverJugador()
    {
        Entrada[] entradas = GameObject.FindObjectsOfType<Entrada>();
        Transform PlaYerTransf = GameObject.Find("personaje").transform;

        foreach (Entrada i in entradas)
        {           
            if (i.nombreSalida == NombreEntrada)
            {
                PlaYerTransf.transform.position             = i.transform.position;
                PlaYerTransf.GetComponent<movimiento>().pos = i.transform.position;
                informacionCrewild.PosActualJuador = i.transform.position;
                break;
            }
        }
    }

    void MoverJugador(Transform tranfPos)
    {
       
        Transform PlaYerTransf = GameObject.Find("personaje").transform;

   
                PlaYerTransf.transform.position = tranfPos.position;
                PlaYerTransf.GetComponent<movimiento>().pos = tranfPos.position;
                informacionCrewild.PosActualJuador = tranfPos.position;
         
       
    }

    public static void MoverJugador(string EntradaNombre)
    {
        Entrada[] entradas = GameObject.FindObjectsOfType<Entrada>();
        Transform PlaYerTransf = FindObjectOfType<movimiento>().transform;
        foreach (Entrada i in entradas)
        {

            //Debug.Log(i.NombreEntrada + "," + EntradaNombre);
            if (i.NombreEntrada == EntradaNombre)
            {
               
                PlaYerTransf.transform.position = i.transform.position;
                PlaYerTransf.GetComponent<movimiento>().pos = i.transform.position;
                break;
            }
        }
       
       
    }

    /// <summary>
    /// mueve al JUgador dependiendo  un vector de posicion
    /// </summary>
    /// <param name="Vectorpos"></param>
    public static void MoverJugador(Vector2 Vectorpos)
    {
        Transform PlaYerTransf = FindObjectOfType<movimiento>().transform;
        PlaYerTransf.transform.position = Vectorpos;
        PlaYerTransf.GetComponent<movimiento>().pos = Vectorpos ;
    }

    IEnumerator Transicion ()
    {
        Transform PlaYerTransf = GameObject.Find("personaje").transform;

        LibresiaS.ControlManag.TransicionHaciaNegro();
        yield return new WaitForSeconds(1);

        if (modoDoor ==  ModoDoor.transfor)
         MoverJugador(Destino);
        else if (modoDoor == ModoDoor.nombre)
            MoverJugador();

        LibresiaS.ControlManag.TransicionHaciaAlpha();
        yield return new WaitForSeconds(0.3f);

        PlaYerTransf.GetComponent<movimiento>().DisparadorEvento = false;
    }

    IEnumerator TransicionCargarescena()
    {
        LibresiaS.ControlManag.TransicionHaciaNegro();
        yield return new WaitForSeconds(1);
        BoolSaltoDeScena = true;
        //StaticSalidaString = nombreSalida;
        posicionInstanciaStatic = posicionInstancia;
        SceneManager.LoadScene(NombreScenaAcargar);

    }

}
