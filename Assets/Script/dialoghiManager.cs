using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
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
        public List<battuta> battuteNuove;
        public List<battuta> battuteSbagliato;
    }

    [System.Serializable]
    public class ricetta
    {
        public string nome;
        public List<string> ingredienti;
    }
    [System.Serializable]
    public class ricettaJson
    {
        public List<ricetta> ricette;
    }

    [System.Serializable]
    public class npcSprite
    {
        public string nome;
        public Sprite sprite;
    }

    [Header("NPC")]
    public TMP_Text testoBattuta;
    public Button bottoneAvanti;
    public Image imageNpc;
    public GameObject pannello;
    public npcSprite[] spriteNpc=new npcSprite[5];
    private Dictionary<string, Sprite> spriteDic;

    private dialogoLeoJson datiLeo;
    private bool inDialogo = false;  //scena speciale o no
    private genericheJson battGeneriche;
    private npc datiNpc;
    private conversazione convAttuale; // i dati del cliente che sta parlando ora
    private int indice = 0; // a che battuta della conv attuale
    private int indiceNPC = 0;//per capire a quale npc st0 guardando
    //public TMP_Text testoPers;
    public cocktail barScript;
    public float delayNPC;
    public float delay;
    private bool feed=false;
    private List<battuta> attualiLeo;
    private ricettaJson datiRicette;
    void Start()
    { 
        spriteDic=new Dictionary<string, Sprite>();
        foreach(var s in spriteNpc)
        {
            spriteDic[s.nome] = s.sprite;
        }
        indice = gameData.indiceBattutaAttuale;
        indiceNPC = gameData.indiceNPCAttuale;
        inDialogo = gameData.inDialogo;
    }

    void Awake()
    {
        TextAsset file = Resources.Load<TextAsset>("dialoghiNPC");
        datiNpc = JsonUtility.FromJson<npc>(file.text);
        mescolaClienti();

        TextAsset fileGenerico = Resources.Load<TextAsset>("battuteGeneriche");
        battGeneriche = JsonUtility.FromJson<genericheJson>(fileGenerico.text);

        TextAsset fileRicette = Resources.Load<TextAsset>("ricette");
        datiRicette = JsonUtility.FromJson<ricettaJson>(fileRicette.text);
        caricaNPC();
    }

    private void mescolaClienti()
    {
        for(int i=datiNpc.conversazioni.Count-1;i>0;i--)
        {
            int j = Random.Range(0, i + 1);
            conversazione temp = datiNpc.conversazioni[i];
            datiNpc.conversazioni[i] = datiNpc.conversazioni[j];
            datiNpc.conversazioni[j] = temp;
        }
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
        gameData.indiceNPCAttuale = indiceNPC;
        gameData.indiceBattutaAttuale = indice;
        gameData.inDialogo = inDialogo;
    }

    public void caricaLeo()
    {
        TextAsset file = Resources.Load<TextAsset>("dialoghiLeo");
        datiLeo = JsonUtility.FromJson<dialogoLeoJson>(file.text);
        inDialogo = true;
        //indice = Random.Range(0, datiLeo.battuta.Count);
        indice = 0;
        if (gameData.clientiPassati == 0) attualiLeo = datiLeo.battuta;
        else attualiLeo = datiLeo.battuteNuove;
        StartCoroutine(caricaLeoDelay());
    }
    private  IEnumerator caricaLeoDelay()
    {
        nascondiPers();
        yield return new WaitForSeconds(delayNPC);
        testoBattuta.text = "";
        yield return new WaitForSeconds(delay);
        mostraBattutaLeo();
    }

    private IEnumerator caricaDelay()
    {
        nascondiPers();
        yield return new WaitForSeconds(delayNPC);
        testoBattuta.text="";
        yield return new WaitForSeconds(delay);
        if (indiceNPC >= datiNpc.conversazioni.Count)
        {
            gameData.clientiPassati++;
            if(gameData.clientiPassati>=gameData.frequenzaLeo)
            {
                gameData.clientiPassati = 0;
                caricaLeo();
                if (gameData.frequenzaLeo == 5) gameData.frequenzaLeo = 10;
                yield break;
            }
            Debug.Log("indiceNpc troppo grande");
            indiceNPC = 0;
        }
        convAttuale = datiNpc.conversazioni[indiceNPC];
        indice = 0;
        indiceNPC++;
        gameData.indiceBattutaAttuale = indice;
        gameData.indiceNPCAttuale = indiceNPC;
        gameData.inDialogo = inDialogo;
        mostraBattuta();
    }
    private void mostraBattuta()
    {
        battuta b = convAttuale.battuta[indice];
        string testoFin = b.testo;
        mostraPersonaggio(b.personaggio);
        //Debug.Log(testoFin);
        //testoPers.text = b.personaggio;
        if (!string.IsNullOrEmpty(b.usaGenerica)) testoFin = pescaBattGenerica(b.usaGenerica);
        testoBattuta.text = b.personaggio+"\n"+testoFin;
        //Debug.Log(testoFin);
        if (b.comando == "ordina")
        {
            //ne riprendo una casuale
            ricetta ricettaCas = datiRicette.ricette[Random.Range(0, datiRicette.ricette.Count)];
            barScript.impostaOrdine(ricettaCas.nome, b.personaggio + "\n" + testoFin);
            bottoneAvanti.interactable = false;
            Debug.Log("ricetta casuale "+ricettaCas.nome);
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
        if (possibili.Count == 0)
        {
            Debug.Log("Nessuna battuta");
            return "";
        }
        int indice = Random.Range(0, possibili.Count);
        return possibili[indice];
    }

    private void mostraBattutaLeo() 
    {
        battuta b = attualiLeo[indice];
        mostraPersonaggio(b.personaggio);
        testoBattuta.text = b.personaggio + "\n" + b.testo;
        if (b.comando == "ordina")
        {
            barScript.impostaOrdine(b.parametroComando, b.personaggio + "\n" + b.testo);
            bottoneAvanti.interactable = false;
        }
        else if (b.comando == "fine")
        {
            bottoneAvanti.interactable = false;
            Debug.Log("Fine gioco");
            testoBattuta.text = "Fine gioco";//provvisorio
            //scena di fine gioco
        }
        else bottoneAvanti.interactable = true;
        if (gameData.clientiPassati != 0) attualiLeo = datiLeo.battuteNuove; 
        else attualiLeo = datiLeo.battuta;
    }

    public void prossBattuta(bool? drinkCorretto=null)
    {
        if(drinkCorretto.HasValue)
        {
            string categoria;
            if (drinkCorretto == true) categoria = "reazionePositiva";
            else categoria = "reazioneNegativa";
            string t= pescaBattGenerica(categoria);
            Debug.Log("battuta" + t);
            testoBattuta.text = t;
            StartCoroutine(avanzaFeed());
            return;
        }
        bottoneAvanti.interactable = false;
        indice++;
        int cont;
        if (inDialogo) cont = datiLeo.battuta.Count;
        else cont = convAttuale.battuta.Count;
        if (indice >= cont)
        {
            if (inDialogo)
            {
                inDialogo = false;
                indiceNPC = 0;
                gameData.clientiPassati = 0;
                caricaNPC();
            }
            else caricaNPC();
        }
        StartCoroutine(prossimaBattutaDelay());
    }
    private IEnumerator avanzaFeed()
    {
        yield return new WaitForSeconds(delayNPC);
        indice++;
        int cont;
        if (inDialogo) cont = datiLeo.battuta.Count;
        else cont = convAttuale.battuta.Count;
        if (indice >= cont) caricaNPC();
        else
        {
            if (inDialogo) mostraBattutaLeo();
            else mostraBattuta();
        }
    }
    private IEnumerator prossimaBattutaDelay()
    {
        yield return new WaitForSeconds(delayNPC);
        if (inDialogo) mostraBattutaLeo();
        else mostraBattuta();
    }
    public void battutaBottone()
    {
        prossBattuta();
    }

    private void mostraPersonaggio(string nome)
    {
        pannello.SetActive(true);
        if (spriteDic.TryGetValue(nome, out Sprite spr))
        {
            imageNpc.sprite = spr;
            imageNpc.enabled = true;
        }
        else Debug.Log("Sprite non trovato");
    }

    private void nascondiPers()
    {
        pannello.SetActive(false);
        imageNpc.enabled = false;
    }

}
