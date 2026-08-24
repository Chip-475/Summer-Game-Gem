using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class gameData : MonoBehaviour
{
    [System.Serializable]
    public struct bottMagaz
    {
        public string nome;
        public int livello;
    }

    [Header("sistema leo")]
    public static int clientiPassati = 0;
    public static int frequenzaLeo = 5;
    public static int drinkSbagliato = 0;
    public static bool inDialogo = false;

    public static List<bottMagaz> magazzino=new List<bottMagaz>();
    [Header("stats a runtime")]
    public static int indiceNPCAttuale=0;
    public static int indiceBattutaAttuale = 0;
   

    [Header("Soldi")]
    public static float monete = 50;

    public static string[] scaffaleAttivo = new string[10]
    {
        "Gin","Vodka","RUM","Tonica","Coca Cola","Lemon Soda",
        "Jägermeister","Jack Daniel's","Disaronno","Arancia"
    };

    [Header("Inventario bottiglie")]
    public static Dictionary<string, int> bottiglie = new Dictionary<string, int>
    {
        {"Gin",100},
        {"Vodka",100},
        {"RUM",100},
        {"Tonica",100},
        {"Coca Cola",100},
        {"Lemon Soda",100},
        {"Jägermeister",100},
        {"Jack Daniel's",100},
        {"Disaronno",100},
        {"Arancia",15}
    };
    public static string bottigliaAttiva = "Gin";
    public static Dictionary<string, float> prezziBottiglie = new Dictionary<string, float>
    {
        {"Gin",7.50f},
        {"Vodka",8f},
        {"RUM",11.50f},
        {"Tonica",1.50f},
        {"Coca Cola",2f},
        {"Lemon Soda",2f},
        {"Jägermeister",16.50f},
        {"Jack Daniel's",15f},
        {"Disaronno",17f},
        {"Energy drink",1f},
        {"Arancia",5f},
        {"Tequila",13.50f},
        {"Triple sec",21.50f},
        {"Whiskey",10.50f},
        {"Ginger ale",10.50f}
    };
    /*
     * "RUM","Vodka","Gin","Tonica","Lemon Soda","Coca Cola","Jack Daniel's",
        "Jägermeister","Disaronno","Energy drink","Arancia","Tequila",
        "Triple sec","Whiskey","Ginger ale"
     */
    public static Dictionary<string, int> prezziUsati = new Dictionary<string, int>
    {
        {"Gin",5},
        {"Vodka",7},
        {"RUM",4},
        {"Tonica",15},
        {"Coca Cola",17},
        {"Lemon Soda",13},
        {"Jägermeister",8},
        {"Jack Daniel's",7},
        {"Disaronno",7},
        {"Energy drink",15},
        {"Arancia",1},
        {"Tequila",7},
        {"Triple sec",10},
        {"Whiskey",5},
        {"Ginger ale",9}
    };

    //scambio della bottiglia dello scaffale
    public static void scambiaBottiglia(int posizione,string nomeBottiglia) //pos è il numero che mi da il bottone quindi il riferimento alla bottgilia di scaffale attivo
    {/*
        string vecchiaBottiglia = scaffaleAttivo[posizione];
        int i = -1;
        for (int j=0;j<scaffaleAttivo.Length;j++)
        {
            if(bottiglie[scaffaleAttivo[j]] <= 0||scaffaleAttivo[j] == nomeBottiglia)//&&!(bottiglie[vecchiaBottiglia]<=0))
            {
                Debug.Log("dentro primo if");
                i = j;
                break;
            }
        }
        if (i != -1)
        {
            scaffaleAttivo[i] = vecchiaBottiglia;
            scaffaleAttivo[posizione] = nomeBottiglia;
        }
        else
        {
            scaffaleAttivo[posizione] = nomeBottiglia;
            Debug.Log("nell'else");
        }*/
        scaffaleAttivo[posizione]=nomeBottiglia;
        Debug.Log("scambio fatto da gameData");
    }

    [Header("dimensioni per i bottoni")]
    public static Dictionary<string, Vector2> misureSprite = new Dictionary<string, Vector2>
    {
        {"Gin", new Vector2(100,220)},
        {"Vodka",new Vector2(100,200)},
        {"RUM",new Vector2(100,200)},
        {"Tonica",new Vector2(90,220)},
        {"Coca Cola",new Vector2(70,170)},
        {"Lemon Soda",new Vector2(70,170)},
        {"Jägermeister",new Vector2(110,230)},
        {"Jack Daniel's",new Vector2(100,250)},
        {"Disaronno",new Vector2(100,210)},
        {"Tequila", new Vector2(95,210)},
        {"Energy drink",new Vector2(60,160)},
        {"Triple sec",new Vector2(85,200)},
        {"Ginger ale",new Vector2(70,200)},
        {"Whiskey",new Vector2(95,220)},
        {"Arancia",new Vector2(80,150)}
    };

    public static int[] livelliScaffale = new int[10]
    {
        100,100,100,100,100,100,100,100,100,100
    };

    // crea uno snapshot dello stato attuale, pronto per essere salvato su file
    public static SaveData CreaSnapshot()
    {
        SaveData dati = new SaveData();
        dati.monete = monete;

        dati.bottiglieNomi.Clear();
        dati.bottiglieValori.Clear();
        foreach (var kv in bottiglie)
        {
            dati.bottiglieNomi.Add(kv.Key);
            dati.bottiglieValori.Add(kv.Value);
        }

        dati.scaffaleAttivo = (string[])scaffaleAttivo.Clone();
        dati.indiceNPCAttuale = indiceNPCAttuale;
        dati.indiceBattutaAttuale = indiceBattutaAttuale;
        dati.inDialogo = inDialogo;
        dati.clientiPassati = clientiPassati;
        dati.frequenzaLeo = frequenzaLeo;

        return dati;
    }

    // applica uno snapshot caricato da file allo stato di gioco attuale
    public static void CaricaSnapshot(SaveData dati)
    {
        monete = dati.monete;

        for (int i = 0; i < dati.bottiglieNomi.Count; i++)
        {
            bottiglie[dati.bottiglieNomi[i]] = dati.bottiglieValori[i];
        }

        scaffaleAttivo = (string[])dati.scaffaleAttivo.Clone();
        indiceNPCAttuale = dati.indiceNPCAttuale;
        indiceBattutaAttuale = dati.indiceBattutaAttuale;
        inDialogo = dati.inDialogo;
        clientiPassati = dati.clientiPassati;
        frequenzaLeo = dati.frequenzaLeo;
    }

    // riporta tutto ai valori di default, per una Nuova Partita
    public static void ResetDati()
    {
        monete = 100;

        bottiglie["Gin"] = 100;
        bottiglie["Vodka"] = 100;
        bottiglie["RUM"] = 100;
        bottiglie["Tonica"] = 100;
        bottiglie["Coca Cola"] = 100;
        bottiglie["Lemon Soda"] = 100;
        bottiglie["Jägermeister"] = 100;
        bottiglie["Jack Daniel's"] = 100;
        bottiglie["Disaronno"] = 100;

        scaffaleAttivo = new string[9]
        {
        "Gin","Vodka","RUM","Tonica","Coca Cola","Lemon Soda",
        "Jägermeister","Jack Daniel's","Disaronno"
        };

        indiceNPCAttuale = 0;
        indiceBattutaAttuale = 0;
        inDialogo = false;
        clientiPassati = 0;
        frequenzaLeo = 5;
    }

    private void OnApplicationQuit()
    {
        SaveManager.Salva();
    }
}

