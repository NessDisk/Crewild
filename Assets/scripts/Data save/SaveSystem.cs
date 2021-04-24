using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // llama al metodo para guardar los datos
    public static void SavedataCrewildAMano(CrewildBase DataCrewild, int Numlist)
    {
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + "/CrewildAMano"+ Numlist+ ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveDataCrewild data = new SaveDataCrewild(DataCrewild);
        formater.Serialize(stream, data);
        stream.Close();
    }


    // llama al metodo para guardar los datos
    public static void SavedataCrewildAGuardada(CrewildBase DataCrewild, int Numlist)
    {
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + "/CrewildAGuardado" + Numlist + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveDataCrewild data = new SaveDataCrewild(DataCrewild);
        formater.Serialize(stream, data);
        stream.Close();
    }

    // llama al metodo para guardar los datos
    public static void SaveDataItens(BaseItem DataIten, int Numlist)
    {
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Iten" + Numlist + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveDataIten data = new SaveDataIten(DataIten);
        formater.Serialize(stream, data);
        stream.Close();
    }

    // llama al metodo para guardar los datos
    public static void SaveDataInfoGloval(SaveDataInfoGloval DataInfoGloval)
    {
        BinaryFormatter formater = new BinaryFormatter();
        string path = Application.persistentDataPath + "/InfoGloval.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveDataInfoGloval data = new SaveDataInfoGloval(DataInfoGloval);
        formater.Serialize(stream, data);
        stream.Close();
    }



    /// <summary>
    /// limpia la data
    /// </summary>
    /// <param name="Numlist"></param>
    public static void ClearData()
    {
      for(int Numlist = 0; Numlist < 6 ; Numlist++ )
        {
            string path = Application.persistentDataPath + "/CrewildAMano" + Numlist + ".data";
            if (File.Exists(path))
            {
                File.Delete(path);

            }
        }

        for (int Numlist = 0; Numlist < 144; Numlist++)
        {
            string path = Application.persistentDataPath + "/CrewildAGuardado" + Numlist + ".data";
            if (File.Exists(path))
            {
                File.Delete(path);

            }
        }

        for (int Numlist = 0; Numlist < 100; Numlist++)
        {
            string path = Application.persistentDataPath + "/Iten" + Numlist + ".data";
            if (File.Exists(path))
            {
                File.Delete(path);

            }
        }


        string pathGloval = Application.persistentDataPath + "/InfoGloval.data";
        if (File.Exists(pathGloval))
        {
            File.Delete(pathGloval);

        }


    }


    public static SaveDataCrewild loadDataCrewildAmano(int Numlist)
    {
      string path = Application.persistentDataPath + "/CrewildAMano" + Numlist + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            

            SaveDataCrewild data = formater.Deserialize(stream) as SaveDataCrewild;

            stream.Close();

            return data;
        }
        else
        {
            //   Debug.LogError("No se escontro el archo en esta ruta "+ path);           
            SaveDataCrewild data = null;
            return data;
            return null;
        }
    }


    public static SaveDataCrewild loadDataCrewildAGuardada(int Numlist)
    {
        string path = Application.persistentDataPath + "/CrewildAGuardado" + Numlist + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveDataCrewild data = formater.Deserialize(stream) as SaveDataCrewild;

            stream.Close();

            return data;
        }
        else
        {
            //   Debug.LogError("No se escontro el archo en esta ruta "+ path);           
            SaveDataCrewild data = null;
            return data;
        }
    }

    public static SaveDataIten loadDataItem(int Numlist)
    {
        string path = Application.persistentDataPath + "/Iten" + Numlist + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveDataIten data = formater.Deserialize(stream) as SaveDataIten;

            stream.Close();

            return data;
        }
        else
        {
            //   Debug.LogError("No se escontro el archo en esta ruta "+ path);           
            SaveDataIten data = null;
            return data;

        }
    }

    public static SaveDataInfoGloval loadDataDatosGlovales()
    {
        string path = Application.persistentDataPath + "/InfoGloval.data";
        if (File.Exists(path))
        {
            BinaryFormatter formater = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveDataInfoGloval data = formater.Deserialize(stream) as SaveDataInfoGloval;

            stream.Close();

            return data;
        }
        else
        {
            //   Debug.LogError("No se escontro el archo en esta ruta "+ path);           
            SaveDataInfoGloval data = null;
            return data;

        }
    }
}
