using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Collections;
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

    [System.Serializable]
    public class dialogoLeoJson
    {
        public List<battuta> battuta;
    }
    private dialogoLeoJson datiLeo;
    private bool inDialogo = false;  //scena speciale o no
    private genericheJson battGeneriche;
    private npc datiNpc;
    private conversazione convAttuale;
    private int indice = 0;
    private int indiceNPC = 0;//per capire a quale npc sta guardando
    //public TMP_Text testoPers;
    public TMP_Text testoBattuta;
    public cocktail barScript;
    public Button bottoneAvanti;
    public float delayNPC = 1.5f;
    
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
        bottoneAvanti.interactable = false;
        if (indiceNPC >= datiNpc.conversazioni.Count)
        {
            Debug.Log("Npc finitio ora ce leo");
            caricaLeo();
            return;
            //indiceNPC = 0;
            //Debug.Log("ho resettato l'indice");
        }
        StartCoroutine(caricaDelay());
    }

    public void caricaLeo()
    {
        TextAsset file = Resources.Load<TextAsset>("dialoghiLeo");
        datiLeo = JsonUtility.FromJson<dialogoLeoJson>(file.text);
        inDialogo = true;
        indice = 0;
        StartCoroutine(caricaLeoDelay());
    }
    private  IEnumerator caricaLeoDelay()
    {
        yield return new WaitForSeconds(delayNPC);
        battuta b = datiLeo.battuta[indice];
        testoBattuta.text = b.personaggio + "\n" + b.testo;
        if(b.comando=="ordina")
        {
            barScript.impostaOrdine(b.parametroComando,b.personaggio+"\n"+b.testo);
            bottoneAvanti.interactable=false;
        }
        else if(b.comando=="fine")
        {
            bottoneAvanti.interactable = false;
            Debug.Log("Fine gioco");
            //scena di fine game
        }
        else
        {
            bottoneAvanti.interactable = true;
        }
    }

    private IEnumerator caricaDelay()
    {
        yield return new WaitForSeconds(delayNPC);
        if (indiceNPC >= datiNpc.conversazioni.Count)
        {
            Debug.Log("indiceNpc troppo grande");
            indiceNPC = 0;
        }
        convAttuale = datiNpc.conversazioni[indiceNPC];
        indice = 0;
        indiceNPC++;
        mostraBattuta();
    }
    private void mostraBattuta()
    {
        battuta b = convAttuale.battuta[indice];
        string testoFin = b.testo;
        //Debug.Log(testoFin);
        //testoPers.text = b.personaggio;
        if (!string.IsNullOrEmpty(b.usaGenerica)) testoFin = pescaBattGenerica(b.usaGenerica);
        testoBattuta.text = b.personaggio+"\n"+testoFin;
        //Debug.Log(testoFin);
        if (b.comando == "ordina")
        {
            barScript.impostaOrdine(b.parametroComando, b.personaggio + "\n" + testoFin);
            bottoneAvanti.interactable = false;
           //Debug.Log(testoFin);
        }
        else
        {
            bottoneAvanti.interactable = true;
            //Debug.Log(testoFin);
        }

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
        int indice = Random.Range(0, possibili.Count);
        return possibili[indice];
    }
    public void prossBattuta()
    {
        bottoneAvanti.interactable=false;
        if(inDialogo)
        {
            indice++;
            if (indice >= datiLeo.battuta.Count) return;
            StartCoroutine(prossimaBattutaDelay());
            return;
        }
        indice++;
        if (indice >= convAttuale.battuta.Count)
        {
            caricaNPC();
            return;
        }
        StartCoroutine(prossimaBattutaDelay());
    }
    private IEnumerator prossimaBattutaDelay()
    {
        yield return new WaitForSeconds(delayNPC);
        mostraBattuta();
    }

}
