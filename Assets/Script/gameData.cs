using System.Collections.Generic;
using UnityEngine;

public class gameData : MonoBehaviour
{
    [Header("Soldi")]
    public static int monete = 100;
    [Header("Inventario Bottiglie")]
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
        {"Jack Daniel's",10},
        {"Disaronno",7},
    };
}

