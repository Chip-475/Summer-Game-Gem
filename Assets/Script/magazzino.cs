using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net.Http.Headers;
using System;
public class magazzino : MonoBehaviour
{
    public Transform contenitore; // quello dello scroll view
    public GameObject prefabBottiglia;
    public Button bChiudi;
    public TMP_Text testoBottiglia;  //quello che si vede nello scaffale

    private int posizioneAttuale = -1;
    public scaffale script;
    void Start()
    {
        bChiudi.onClick.AddListener(chiudiMagazzino);
        //script = GetComponentInParent<Canvas>().GetComponentInChildren<scaffale>();
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
        /*
        //carica le bottiglie da scaffale e se ha comprato qualcosa
        foreach (var bottiglia in gameData.bottiglie)
        {
            string nome = bottiglia.Key;
            int livello = bottiglia.Value;
            //if (nome == bottAtt) continue;//coisi da non metterla due volte
           // if (nome == bottAtt) continue;
            GameObject item = Instantiate(prefabBottiglia, contenitore);
            TMP_Text[] testi = item.GetComponentsInChildren<TMP_Text>();
            Image immagine = item.GetComponentInChildren<Image>();
            Button btn = item.GetComponent<Button>();
            //testi[0].text = nome;
            testi[1].text = "" + livello;
            Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
            if (sprite != null && immagine != null)
            {
                //Debug.Log("immagine messa");
                immagine.sprite = sprite;
                if (gameData.misureSprite.TryGetValue(nome, out Vector2 misura)) immagine.rectTransform.sizeDelta = misura;
            }
            else if (sprite == null) Debug.Log("sprite null");
            else Debug.Log("immagine null");
            if (livello <= 0) testi[1].color = new Color(1, 0.3f, 0.3f);
            else if (livello <= 30) testi[1].color = new Color(1, 0.8f, 0.3f);
            else testi[1].color = new Color(0.3f, 1, 0.3f);
            btn.onClick.AddListener(() => scambiaBottiglia(nome));
        }*/
        for(int i=0;i<gameData.magazzino.Count;i++)
        {
            gameData.bottMagaz bottiglia = gameData.magazzino[i];
            string nome = bottiglia.nome;
            int livello=bottiglia.livello;
            //if (nome == bottAtt) continue;//coisi da non metterla due volte
            GameObject item = Instantiate(prefabBottiglia, contenitore);
            TMP_Text[] testi = item.GetComponentsInChildren<TMP_Text>();
            Image immagine = item.GetComponentInChildren<Image>();
            Button btn = item.GetComponent<Button>();
            //testi[0].text = nome;
            testi[1].text = "" + livello;
            Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
            if (sprite != null && immagine != null)
            {
               //Debug.Log("immagine messa");
               immagine.sprite = sprite;
                if (gameData.misureSprite.TryGetValue(nome, out Vector2 misura)) immagine.rectTransform.sizeDelta = misura;
            }
            else if (sprite == null) Debug.Log("sprite null");
            else Debug.Log("immagine null");
            if (livello <= 0) testi[1].color = new Color(1, 0.3f, 0.3f);
            else if (livello <= 30) testi[1].color = new Color(1, 0.8f, 0.3f);
            else testi[1].color = new Color(0.3f, 1, 0.3f);
            int indice = i;
            btn.onClick.AddListener(() => scambiaBottiglia(indice));
        }
    }

    private void scambiaBottiglia(string nome)
    {
        string vecchia = gameData.scaffaleAttivo[posizioneAttuale];
        gameData.magazzino.Add(new gameData.bottMagaz
        {
            nome = vecchia,
            livello = gameData.bottiglie[vecchia]
        });
        gameData.bottiglie[nome] = gameData.magazzino.Find(b => b.nome == nome).livello;
        gameData.magazzino.RemoveAll(b=>b.nome == nome);
        gameData.scambiaBottiglia(posizioneAttuale, nome);
        Debug.Log("bottiglia scambiata dalla seconda funzione");
        script.aggLivelli();
        caricaMagazzino(posizioneAttuale);
    }
   
    private void scambiaBottiglia(int indice)
    {
        gameData.bottMagaz bottgliaMag = gameData.magazzino[indice];
        /*string vecchia = gameData.scaffaleAttivo[posizioneAttuale];
        gameData.magazzino[vecchia] = gameData.bottiglie[vecchia];
        gameData.bottiglie[nome] = gameData.magazzino[nome];
        gameData.magazzino.Remove(nome);*/
        string nome = bottgliaMag.nome;
        int livelloMag=bottgliaMag.livello;
        string vecchia = gameData.scaffaleAttivo[posizioneAttuale];
        int livelloVecchia = gameData.livelliScaffale[posizioneAttuale];
        //gameData.bottiglie[nome] = livelloMag;
        gameData.livelliScaffale[posizioneAttuale] = livelloMag;
        gameData.magazzino.Add(new gameData.bottMagaz
        {
            nome = vecchia,
            livello = livelloVecchia
        });
        gameData.bottiglie[nome] = bottgliaMag.livello;
        gameData.magazzino.RemoveAt(indice);
        gameData.scambiaBottiglia(posizioneAttuale, nome);
        Debug.Log("Bottiglia Scambiata da magazzino");
        script.aggLivelli();
        caricaMagazzino(posizioneAttuale);
    }
}
