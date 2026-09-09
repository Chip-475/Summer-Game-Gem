using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float monete;

    public List<string> bottiglieNomi = new List<string>();
    public List<int> bottiglieValori = new List<int>();

    public string[] scaffaleAttivo;

    public int[] livelliScaffale;

    public List<string> magazzzinoNomi = new List<string>();
    public List<int> magazzinioLivelli = new List<int>();

    public int indiceNPCAttuale;
    public int indiceBattutaAttuale;
    public bool inDialogo;
    public int clientiPassati;
    public int frequenzaLeo;

    public int giornoAttuale;
    public float guadagniGiorno;
    public float speseGiorno;
    public int clientiServiti;
    public float tempoRimasto;

    public int leoIndice;
    public int drinkSbagliatiLeo;

    public int contatoreOrdineRipetuto;
    public string ultimoOrdine;

}