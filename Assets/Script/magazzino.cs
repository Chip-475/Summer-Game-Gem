using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class magazzino : MonoBehaviour
{
    [SerializeField] private Transform contenitore; // quello dello scroll view
    [SerializeField] private GameObject prefabBottiglia;
    [SerializeField] private Button bChiudi;
    [SerializeField] private TMP_Text testoBottiglia;  //quello che si vede nello scaffale

    private int posizioneAttuale = -1;
    void Start()
    {
        bChiudi.onClick.AddListener(chiudiMagazzino);   
    }

    private void chiudiMagazzino()
    {
        gameObject.SetActive(false);
    }

    public void caricaMagazzino(int pos)
    {
        posizioneAttuale = pos;
        string bottAtt = gameData.scaffaleAttivo[pos];
        int livelloBott = gameData.bottiglie[bottAtt];
        testoBottiglia.text = "Sullo Scaffale c'è " + bottAtt + " con " + livelloBott;
        //toglie le bott precedenti
        foreach(Transform child in contenitore)
        {
            Destroy(child.gameObject);
        }
        //carica le bottiglie
        foreach (var bottiglia in gameData.bottiglie)
        {
            string nome = bottiglia.Key;
            int livello = bottiglia.Value;
            if (nome == bottAtt) continue;//coisi da non metterla due volte
            GameObject item = Instantiate(prefabBottiglia, contenitore);
            TMP_Text[] testi = item.GetComponentsInChildren<TMP_Text>();
            Button btn = item.GetComponent<Button>();
            testi[0].text = nome;
            testi[1].text = "" + livello;
            if (livello <= 0) testi[1].color = new Color(1, 0.3f, 0.3f);
            else if (livello <= 30) testi[1].color = new Color(1, 0.8f, 0.3f);
            else testi[1].color = new Color(0.3f, 1, 0.3f);
            btn.onClick.AddListener(() => scambiaBottiglia(nome));
        }
    }

    private void scambiaBottiglia(string nome)
    {
        gameData.scambiaBottiglia(posizioneAttuale, nome);
        Debug.Log("Bottiglia Scambiata");
        caricaMagazzino(posizioneAttuale);
    }
}
