using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float monete;

    // dictionary bottiglie -> due liste parallele
    public List<string> bottiglieNomi = new List<string>();
    public List<int> bottiglieValori = new List<int>();

    // scaffaleAttivo (array di 9 stringhe)
    public string[] scaffaleAttivo;

    public int indiceNPCAttuale;
    public int indiceBattutaAttuale;
    public bool inDialogo;
    public int clientiPassati;
    public int frequenzaLeo;

}