using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal.Internal;
public class magazzino : MonoBehaviour
{
    public Transform contenitore; // il content dello scroll view
    public Transform cont2;
    public GameObject prefabBottiglia;
    public GameObject prefabArancia;
    public TMP_Text testoBottiglia;  //quello che si vede nello scaffale
    public TMP_FontAsset font;
    private int posizioneAttuale = -1;
    public scaffale script;
    private GameObject item;
    public GameObject panel;
    public const int MAX= 19;
    public void chiudiMagazzino()
    {
        panel.SetActive(false);
    }
    
    public void caricaMagazzino(int pos)
    {
        if (gameData.magazzino.Count > MAX) return;
        posizioneAttuale = pos;
        string bottAtt = gameData.scaffaleAttivo[pos];
        int livelloBott = gameData.livelliScaffale[pos];
        testoBottiglia.text = "Sullo Scaffale c'è " + bottAtt + " con " + livelloBott;
        //toglie le bott precedenti
        foreach(Transform child in contenitore)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child in cont2)
        {
            Destroy(child.gameObject);
        }
        string[] primiNove = { "Gin", "Vodka", "RUM", "Tonica", "Coca Cola", "Lemon Soda", "Jagermeister", "Jack Daniel's", "Disaronno" };
        string[] nuoviBottoni = { "Energy drink", "Arancia", "Tequila", "Triple sec", "Whiskey", "Ginger ale" };
        foreach(string nome in primiNove)
        {
            bool trov = false;
            foreach(gameData.bottMagaz bott in gameData.magazzino)
            {
                if(bott.nome==nome)
                {
                    caricaBottone(bott, contenitore);
                    trov = true;
                    break;
                }
            }
            if (!trov) Debug.Log("no ce "+nome);
        }
        foreach(string nome in nuoviBottoni)
        {
            bool trov = false;
            foreach(gameData.bottMagaz bott in gameData.magazzino)
            {
                if(bott.nome==nome)
                {
                    caricaBottone(bott, cont2);
                    trov = true;
                    break;
                }
            }
            if (!trov) Debug.Log("non ce " + nome);
        }
    }
   
    private void caricaBottone(gameData.bottMagaz bott,Transform parent)
    {
        string nome = bott.nome;
        int livello = bott.livello;
        if (nome == "Arancia") item = Instantiate(prefabArancia, contenitore);
        else item = Instantiate(prefabBottiglia, contenitore);
        TMP_Text[] testi = item.GetComponentsInChildren<TMP_Text>();
        Image immagine = item.GetComponentInChildren<Image>();
        Button btn = item.GetComponent<Button>();
        testi[0].text = "" + livello;
        testi[0].font = font;
        testi[0].rectTransform.anchoredPosition = new Vector2(0, -150);
        LayoutElement layout = item.GetComponent<LayoutElement>();
        Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
        if (sprite != null && immagine != null)
        {
            //Debug.Log("immagine messa");
            immagine.sprite = sprite;
            if (gameData.misureSprite.TryGetValue(nome, out Vector2 misura))
            {
                RectTransform rt = item.GetComponent<RectTransform>();
                rt.sizeDelta = misura;
            }
        }
        else if (sprite == null) Debug.Log("sprite null");
        else Debug.Log("immagine null");
        if (livello <= 0) testi[0].color = new Color(1, 0.3f, 0.3f); //cioè di rosso
        else if (livello <= 30) testi[0].color = new Color(1, 0.8f, 0.3f);  //arancione
        else if (nome == "Arancia" && livello >= 10) testi[0].color = new Color(0.3f, 1, 0.3f);
        else if (nome == "Arancia" && livello >= 5 && livello < 10) testi[0].color = new Color(1, 0.8f, 0.3f);
        else if (nome == "Arancione" && livello < 5) testi[0].color = new Color(1, 0.3f, 0.3f);
        else testi[0].color = new Color(0.3f, 1, 0.3f); //verde
        btn.onClick.AddListener(() => scambiaBottiglia(nome));
    }
   
    private void scambiaBottiglia(string nome)
    {
        string vecchia = gameData.scaffaleAttivo[posizioneAttuale];
        int livelloVec = gameData.livelliScaffale[posizioneAttuale];
        gameData.bottMagaz vecchiaBott=new gameData.bottMagaz();
        vecchiaBott.nome = vecchia;
        vecchiaBott.livello = livelloVec;
        gameData.magazzino.Add(vecchiaBott);
        gameData.bottMagaz bottNuova=new gameData.bottMagaz();
        int cont = -1;
        for(int i=0;i<gameData.magazzino.Count;i++)
        {
            if (gameData.magazzino[i].nome==nome)
            {
                bottNuova=gameData.magazzino[i];
                cont = i;
                break;
            }
        }
        if(cont!=-1)
        {
            gameData.livelliScaffale[posizioneAttuale] = bottNuova.livello;
            gameData.magazzino.RemoveAt(cont);
        }
        gameData.scambiaBottiglia(posizioneAttuale, nome);
        Debug.Log("bottiglia scambiata");
        script.aggLivelli();
        caricaMagazzino(posizioneAttuale);
    }
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

/*
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
   }*/
/*
 * gameData.bottMagaz bottgliaMag = gameData.magazzino[indice];
        /*string vecchia = gameData.scaffaleAttivo[posizioneAttuale];
        gameData.magazzino[vecchia] = gameData.bottiglie[vecchia];
        gameData.bottiglie[nome] = gameData.magazzino[nome];
        gameData.magazzino.Remove(nome);///
string nome = bottgliaMag.nome;
int livelloMag = bottgliaMag.livello;
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
* */