using System.Collections.Generic;
using UnityEngine;

public class gameData : MonoBehaviour
{
    [Header("Soldi")]
    public static int monete = 100;

    public static string[] scaffaleAttivo = new string[9]
    {
        "Gin","Vodka","RUM","Tonica","Coca Cola","Lemon Soda",
        "Jägermeister","Jack Daniel's","Disaronno"
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
        {"Disaronno",100}   
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
        {"Disaronno",17f}
    };
    public static Dictionary<string, int> prezziUsati = new Dictionary<string, int>
    {
        {"Gin",5},
        {"Vodka",7},
        {"RUM",4},
        {"Tonica",15},
        {"Coca Cola",17},
        {"Jägermeister",8},
        {"Jack Daniel's",7},
        {"Disaronno",7},
        {"Lemon Soda",13}
    };

    //scambio della bottiglia dello scaffale
    public static void scambiaBottiglia(int posizione,string nomeBottiglia)
    {
        string vecchiaBottiglia = scaffaleAttivo[posizione];
        scaffaleAttivo[posizione] = nomeBottiglia;
        Debug.Log("Scambio fatto");
    }
}

