using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string PercorsoFile => Path.Combine(Application.persistentDataPath, "salvataggio.json");

    public static void salva()
    {
        SaveData dati = gameData.CreaSnapshot();
        string json = JsonUtility.ToJson(dati, true);
        File.WriteAllText(PercorsoFile, json);
        Debug.Log("Partita salvata in: " + PercorsoFile);
        cocktail.apriScena("menu");
    }

    public static bool EsisteSalvataggio()
    {
        return File.Exists(PercorsoFile);
    }

    public static bool carica()
    {
        if (!EsisteSalvataggio())
        {
            Debug.Log("Nessun salvataggio trovato");
            return false;
        }
        string json = File.ReadAllText(PercorsoFile);
        SaveData dati = JsonUtility.FromJson<SaveData>(json);
        gameData.CaricaSnapshot(dati);
        return true;
    }

    public static void cancellaSalvataggio()
    {
        if (EsisteSalvataggio()) File.Delete(PercorsoFile);
    }
}