using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cocktail : MonoBehaviour
{
    //serve solamente a scrivere come è fatto il file json
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
    private ricettaJson ricettaFile;

    [Header("UI per ordinare")]
    public TMP_Text testOrdine;
    public TMP_Text testoSelect;
    public TMP_Text monete;

    private ricetta ordineNow;
    private List<string> selected = new List<string>();

    [Header("dialoghi")]
    public dialoghiManager dialoghiManager;

    public scaffale scaff;
    public Button conferma;
    public GameObject ricettario;
    private bool sem=false;
    public GameObject[] banc=new GameObject[4];

    void Awake()
    {
        //Debug.Log("Cartella Asset/Resourse ce ? " + System.IO.Directory.Exists("Assets/Resources"));
        //Debug.Log("file ricette json " + System.IO.File.Exists("Assets/Resources/ricette.json"));
        TextAsset file = Resources.Load<TextAsset>("ricette");
        if (file == null)
        {
            Debug.Log("file non trovato");
            return;
        }
        //Debug.Log("Primo carattere: " + ((int)file.text[0]));
        //Debug.Log("Lunghezza " + file.text.Length);
        //Debug.Log("Contenuto " + file.text);
        try
        {
            ricettaFile = JsonUtility.FromJson<ricettaJson>(file.text);
            if (ricettaFile == null)
            {
                Debug.LogError("ERRORE: JSON è NULL dopo il parsing!");
                return;
            }
            Debug.Log("Ricette caricate: " + ricettaFile.ricette.Count);
        }
        catch (System.Exception e)
        {
            Debug.LogError("ERRORE nel parsing JSON: " + e.Message);
        }
        monete.text = "Monete " + gameData.monete;
    }


    void Start()
    {
       // TextAsset file = Resources.Load<TextAsset>("ricette");
        //ricettaFile = JsonUtility.FromJson<ricettaJson>(file.text);
        /* foreach (ricetta r in ricettaFile.ricette)
         {
             Debug.Log(r.nome + " " + string.Join(",", r.ingredienti));
         }*/
        //nuovoOrdine();
    }
    // ricettaJson ric = JsonUtility.FromJson<ricettaJson>(ricette.json); 

    //da completare...
    
    public void nuovoOrdine()
    {
        //funzione vecchia
        selected.Clear();
        aggTextSelect();
        int index = Random.Range(0, ricettaFile.ricette.Count);
        ordineNow = ricettaFile.ricette[index];
        testOrdine.text = "Il cliente vuole " + string.Join(",", ordineNow.ingredienti);
    }
    // dialoghi
    public void impostaOrdine(string nomeRicetta,string testo)
    {
        //Debug.Log("imposta Ordine su " + GetInstanceID());
        selected.Clear();
        aggTextSelect();
        ricetta ricTrovata = null;
        //Debug.Log("Sto cercando la ricetta: [" + nomeRicetta + "]");
        foreach (ricetta r in ricettaFile.ricette)
        {
            bool nome = r.nome.Trim().Equals(nomeRicetta.Trim(),System.StringComparison.OrdinalIgnoreCase);
            Debug.Log(r.nome);
            if (nome)
            {
                ricTrovata = r;
                break;
            }
            //Debug.Log("Ricetta disponibile: [" + r.nome + "]");
        }
        //Debug.Log("Suca");
        //ricetta ricetta=ricettaFile.ricette.Find(r=>r.nome == nomeRicetta);
        if (ricTrovata == null)
        {
            Debug.Log("ricetta sbagliata");
            return;
        }
        Debug.Log(ricTrovata.nome);
        ordineNow = ricTrovata;
        testOrdine.text = testo+ordineNow.nome;
        conferma.interactable = true;
    }

    public void AddIngredienti(string ingre,int posizione)
    {
        if (gameData.prezziUsati.ContainsKey(ingre))
        {
            int consumo = gameData.prezziUsati[ingre];
            gameData.livelliScaffale[posizione] -= consumo;
            if(gameData.livelliScaffale[posizione]<=0) gameData.livelliScaffale[posizione] = 0;
                /*
                if (gameData.bottiglie[ingre] < 0)
                {
                    gameData.bottiglie[ingre] = 0;
                    Debug.Log("Consumato: " + ingre + "rimane " + gameData.bottiglie[ingre]);
                    aggTextSelect();
                    scaff.aggLivelli();
                    return;
                }*/
            Debug.Log("Consumato: " + ingre + "rimane " + gameData.bottiglie[ingre]);
            selected.Add(ingre);
            aggTextSelect();
            scaff.aggLivelli();    
            foreach(GameObject bott in banc)
            {

                Image img = bott.GetComponentInChildren<Image>();
                if (img.sprite == null)
                {
                    Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{ingre}");
                    img.sprite = sprite;
                    if (gameData.misureSprite.TryGetValue(ingre, out Vector2 misura)) img.rectTransform.sizeDelta = misura;
                    scaff.bottoniBottoglia[posizione].gameObject.SetActive(false);
                    bott.gameObject.SetActive(true);
                    Debug.Log("bancone");
                    break;
                }
                else Debug.Log("bancone no");
            } 
        }
    }

    private void bottBancone()
    {
        foreach(Button bott in scaff.bottoniBottoglia)
        {
            bott.gameObject.SetActive(true);
        }
        foreach(GameObject bott in banc)
        {
            Image img = bott.GetComponentInChildren<Image>();
            img.sprite = null;
            bott.gameObject.SetActive(false);
        }
    }

    private void aggTextSelect()
    {
        if (selected.Count == 0) testoSelect.text = "Selezionati: ";
        else testoSelect.text ="Selezionati "+string.Join(",", selected);
    }

    public void confermaOrdine()
    {
        //Debug.Log("conferma ordine su " + GetInstanceID());
        if (conferma.interactable == false) return;
        conferma.interactable = false;
        if(ordineNow==null)
        {
            Debug.Log("nessun ordine");
            //dialoghiManager.prossBattuta();
            bottBancone();
            aggTextSelect();
            selected.Clear();
            return;
        }
        List<string> copiaSel = new List<string>(selected);
        List<string> copiaOra = new List<string>(ordineNow.ingredienti);
        if (copiaOra.Count != copiaSel.Count)
        {
            Debug.Log("male");
            //nuovoOrdine();
            if(gameData.inDialogo)
            {
                gameData.drinkSbagliato++;
                if(gameData.drinkSbagliato>=2)
                {
                    gameData.frequenzaLeo = Mathf.Max(5, gameData.frequenzaLeo - 2);
                    gameData.drinkSbagliato = 0;
                }
            }    
            dialoghiManager.prossBattuta(false);
            gameData.monete += 1;
            bottBancone();
            aggTextSelect();
            selected.Clear();
            return;
        }
        copiaOra.Sort();
        copiaSel.Sort();
        for (int i = 0; i < copiaSel.Count; i++)
        {
            if (copiaSel[i] != copiaOra[i])
            {
                Debug.Log("Hai fatto male il drink cazzo");
                //nuovoOrdine();
                dialoghiManager.prossBattuta(false);
                gameData.monete += 1;
                monete.text = "Monete " + gameData.monete;
                selected.Clear();
                aggTextSelect();
                bottBancone();
                return; 
            }
        }
        Debug.Log("bravo lo hai fatto giusto");
        if (gameData.inDialogo) gameData.drinkSbagliato = 0;
        //nuovoOrdine();
        gameData.monete += 2.5f;
        monete.text="Monete "+gameData.monete;
        dialoghiManager.prossBattuta(true);
        selected.Clear();
        aggTextSelect();
        ordineNow= null;
        bottBancone();
        //Debug.Log("fine");
    }

    public static void apriScena(string nome)
    {
        if(Time.timeScale==0)Time.timeScale=1;
        else Time.timeScale=0;
        SceneManager.LoadScene(nome);
    }

    public void rifai()
    {
        selected.Clear();
        aggTextSelect();
        bottBancone();
    }

    public void apri_chiudiRic()
    {
        if (sem)
        {
            ricettario.SetActive(false);//chiude
            sem = false;
        }
        else
        {
            ricettario.SetActive(true); //apre
            sem =true;
        }
    }
}
