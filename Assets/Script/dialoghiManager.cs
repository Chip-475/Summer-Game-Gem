using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
public class dialoghiManager : MonoBehaviour
{
    [System.Serializable] 
    public class battuta
    {
        public string personaggio;
        public string testo;
        public string comando;
        public string parametroComando;
    }
    [System.Serializable]
    public class conversazione
    {
        public string nomeCliente;
        public List<battuta> battuta;
    }
    [System.Serializable]
    public class npc
    {
        public List<conversazione> conversazioni;
    }
    private npc datiNpc;
    private conversazione convAttuale;
    private int indice = 0;
    private int indiceNPC = 0;//per capire a quale npc sta guardando
    //public TMP_Text testoPers;
    public TMP_Text testoBattuta;
    public cocktail barScript;

    void Start()
    {
        TextAsset file = Resources.Load<TextAsset>("dialoghiNPC");
        datiNpc = JsonUtility.FromJson<npc>(file.text);
        caricaNPC();
    }

    public void caricaNPC()
    {
        if(indiceNPC>=datiNpc.conversazioni.Count)
        {
            Debug.Log("Npc finitio ora ce leo");
            return;
        }
        convAttuale=datiNpc.conversazioni[indiceNPC];
        indice = 0;
        indiceNPC++;
        mostraBattuta();
    }

    private void mostraBattuta()
    {
        battuta b = convAttuale.battuta[indice];
        //testoPers.text = b.personaggio;
        testoBattuta.text = b.personaggio+"\n"+b.testo;    
        if (b.comando == "ordina") barScript.impostaOrdine(b.parametroComando);
    }
    public void prossBattuta()
    {
        indice++;
        if(indice>=convAttuale.battuta.Count)
        {
            caricaNPC();
            return;
        }
        mostraBattuta();
    }

}
