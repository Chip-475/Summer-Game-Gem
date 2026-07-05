using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

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

    void Start()
    {
        TextAsset file = Resources.Load<TextAsset>("Ricette");
        ricettaFile = JsonUtility.FromJson<ricettaJson>(file.text);
       /* foreach (ricetta r in ricettaFile.ricette)
        {
            Debug.Log(r.nome + " " + string.Join(",", r.ingredienti));
        }*/
        nuovoOrdine();
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
    /*  dialoghi
    public void impostaOrdine(string nomeRicetta)
    {
        selected.Clear();
        aggTextSelect();
        ricetta ricetta=ricettaFile.ricette.Find(r=>r.nome == nomeRicetta);
        if (ricetta == null)
        {
            Debug.Log("ricetta sbagliata");
            return;
        }
        ordineNow = ricetta;
        testOrdine.text = "Il cliente vuole" + string.Join(",", ordineNow.nome);
    }
    */
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
        List<string> copiaSel = new List<string>(selected);
        List<string> copiaOra = new List<string>(ordineNow.ingredienti);
        if (copiaOra.Count != copiaSel.Count)
        {
            Debug.Log("male");
            nuovoOrdine();
            return;
        }
        copiaOra.Sort();
        copiaSel.Sort();
        for (int i = 0; i < copiaSel.Count; i++)
        {
            if (copiaSel[i] != copiaOra[i])
            {
                Debug.Log("Hai fatto male il drink cazzo");
                nuovoOrdine();
                return; 
            }
        }
        Debug.Log("bravo lo hai fatto giusto");
        nuovoOrdine();
    }

}
