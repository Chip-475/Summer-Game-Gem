using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;
using System.Runtime.CompilerServices;
public class dialoghiManager : MonoBehaviour
{
    [System.Serializable] 
    public class battuta
    {
        public string personaggio;
        public string testo;
        public string usaGenerica;
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
    [System.Serializable]
    public class battutaGenerica
    {
        public string categoria;
        public string testo;
    }
    [System.Serializable]
    public class genericheJson
    {
        public List<battutaGenerica> battuteGeneriche;
    }
    private genericheJson battGeneriche;
    private npc datiNpc;
    private conversazione convAttuale;
    private int indice = 0;
    private int indiceNPC = 0;//per capire a quale npc sta guardando
    //public TMP_Text testoPers;
    public TMP_Text testoBattuta;
    public cocktail barScript;
    public Button bottoneAvanti;
    
    void Start()
    {
        TextAsset file = Resources.Load<TextAsset>("dialoghiNPC");
        datiNpc = JsonUtility.FromJson<npc>(file.text);
        TextAsset fileGenerico = Resources.Load<TextAsset>("battuteGeneriche");
        battGeneriche = JsonUtility.FromJson<genericheJson>(fileGenerico.text);
        caricaNPC();
    }

    public void caricaNPC()
    {
        if(indiceNPC>=datiNpc.conversazioni.Count)
        {
            Debug.Log("Npc finitio ora ce leo");
            indiceNPC = 0;
            indice = 0;
            Debug.Log("ho resettato l'indice");
            mostraBattuta();
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
        if (!string.IsNullOrEmpty(b.usaGenerica)) testoBattuta.text = pescaBattGenerica(b.usaGenerica);
        testoBattuta.text = b.personaggio+"\n"+b.testo;
        if (b.comando == "ordina")
        {
            barScript.impostaOrdine(b.parametroComando, b.personaggio + "\n" + b.testo);
            bottoneAvanti.interactable = true;
        }
        else bottoneAvanti.interactable=false;

    }
    private string pescaBattGenerica(string categoria)
    {
        List<string> possibili = new List<string>();
        foreach(battutaGenerica bg in battGeneriche.battuteGeneriche)
        {
            if(bg.categoria==categoria)possibili.Add(bg.testo);

        }
        /*if (possibili.Count == 0)
        {
            Debug
        }*/
        int indice = RandomRange(0, possibili.Count);
        return possibili[indice];
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
