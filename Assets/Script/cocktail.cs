using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

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

    private ricetta ordineNow;
    private List<string> selected = new List<string>();

    [Header("dialoghi")]
    public dialoghiManager dialoghiManager;

    void Awake()
    {
        TextAsset file = Resources.Load<TextAsset>("ricette");
        if (file == null)
        {
            Debug.Log("file non trovato");
            return;
        }
        ricettaFile = JsonUtility.FromJson<ricettaJson>(file.text);
        if (ricettaFile == null)
        {
            Debug.Log("ricette non trovate");
            return;
        }
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
        selected.Clear();
        aggTextSelect();
        int index = Random.Range(0, ricettaFile.ricette.Count);
        ordineNow = ricettaFile.ricette[index];
        testOrdine.text = "Il cliente vuole " + string.Join(",", ordineNow.ingredienti);
    }
    // dialoghi
    public void impostaOrdine(string nomeRicetta,string testo)
    {
        selected.Clear();
        aggTextSelect();
        ricetta ricTrovata = null;
        //Debug.Log("Sto cercando la ricetta: [" + nomeRicetta + "]");
        foreach (ricetta r in ricettaFile.ricette)
        {
            bool nome = r.nome.Trim().Equals(nomeRicetta.Trim(),System.StringComparison.OrdinalIgnoreCase);
            if (nome)
            {
                ricTrovata = r;
                break;
            }
            //Debug.Log("Ricetta disponibile: [" + r.nome + "]");
        }
        //ricetta ricetta=ricettaFile.ricette.Find(r=>r.nome == nomeRicetta);
        if (ricTrovata == null)
        {
            Debug.Log("ricetta sbagliata");
            return;
        }
        ordineNow = ricTrovata;
        testOrdine.text = testo;
    }

    public void AddIngredienti(string ingre)
    {
        selected.Add(ingre);
        aggTextSelect();
        Debug.Log(ingre);
    }

    private void aggTextSelect()
    {
        if (selected.Count == 0) testoSelect.text = "Selezionati: nessuno";
        else testoSelect.text ="Selezionati "+string.Join(",", selected);
    }

    public void confermaOrdine()
    {
        if(ordineNow==null)
        {
            Debug.Log("nessun ordine");
            dialoghiManager.prossBattuta();
            return;
        }
        List<string> copiaSel = new List<string>(selected);
        List<string> copiaOra = new List<string>(ordineNow.ingredienti);
        if (copiaOra.Count != copiaSel.Count)
        {
            Debug.Log("male");
            //nuovoOrdine();
            dialoghiManager.prossBattuta(false);
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
                return; 
            }
        }
        Debug.Log("bravo lo hai fatto giusto");
        //nuovoOrdine();
        dialoghiManager.prossBattuta(true);
        //Debug.Log("fine");
    }

    public void apriScena(string nome)
    {
        if(Time.timeScale==0)Time.timeScale=1;
        else Time.timeScale=0;
        SceneManager.LoadScene(nome);
    }
}
