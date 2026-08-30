using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Tilemaps;
public class negozio : MonoBehaviour
{
    public Transform cont1;
    public Transform cont2;
    [SerializeField] private GameObject contenitore;
    [SerializeField] private GameObject prefabBott;
    [SerializeField] private GameObject prefabArancia;
    [SerializeField] private TMP_Text testoMonete;
    [SerializeField] private Button chiudi;
    public TMP_FontAsset font;
    private bool sem=false;
    private GameObject item;
    public scaffale script;

    void Start()
    {
        RectTransform rt=prefabArancia.GetComponent<RectTransform>();
        rt.sizeDelta=gameData.misureSprite["Arancia"];
    }

    public void apriChiudiNeg()
    {
        if(sem)
        {
            contenitore.SetActive(false);
            sem = false;
            Debug.Log("Dentro chiudi");
   
            Time.timeScale = 1f;
        }
        else
        {
            contenitore.SetActive(true);
            sem = true;
            caricaBott();
            Debug.Log("Dentro Apri");
            Time.timeScale = 1f;
        }
        testoMonete.text = "Monete: " + gameData.monete;
        
    }

    private void caricaBott()
    {
        foreach(Transform itm in cont1)
        {
            Destroy(itm.gameObject);
        }
        foreach(Transform itm in cont2)
        {
            Destroy(itm.gameObject);
        }
        string[] primiNove={"Gin","Vodka","RUM","Tonica","Coca Cola","Lemon Soda","Jagermeister","Jack Daniel's","Disaronno"};
        string[] nuoviBottoni={"Energy drink","Arancia","Tequila","Triple sec","Whiskey","Ginger ale"};
        foreach(string nome in primiNove)
        {
            if (gameData.prezziBottiglie.ContainsKey(nome)) caricaBottone(nome, cont1);
        }
        foreach(string nome in nuoviBottoni)
        {
            if (gameData.prezziBottiglie.ContainsKey(nome)) caricaBottone(nome, cont2);
        }
        script.aggLivelli();
        Debug.Log("fine funzione");
    }

    private void caricaBottone(string nome, Transform parent)
    {
        float prezzo = gameData.prezziBottiglie[nome];
        Debug.Log("negozio " + nome);
        if (gameData.misureSprite.TryGetValue(nome, out Vector2 misura))
        {
            if (nome == "Arancia")item = Instantiate(prefabArancia, parent);
            else item = Instantiate(prefabBott, parent);
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.sizeDelta = misura;
            TMP_Text[] testi = item.GetComponentsInChildren<TMP_Text>();
            Image immagine = item.GetComponent<Image>();
            Button btn = item.GetComponent<Button>();
            testi[0].text = prezzo + " €";
            testi[0].font = font;
            testi[0].rectTransform.anchoredPosition = gameData.misureText[nome];
            Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
            if (sprite != null && immagine != null) immagine.sprite = sprite;
            testi[0].color = new Color(0, 0, 0);
            btn.onClick.AddListener(() => compraBott(nome, prezzo));
        }
    }


    private void compraBott(string nome,float prezzo)
    {
        if (gameData.monete >= prezzo)
        {
            gameData.monete -= prezzo;
            dayManager.aggSpesa(prezzo);
            // gameData.bottiglie[nome] = 100;
            if (nome != "Arancia")
            {
                gameData.magazzino.Add(new gameData.bottMagaz
                {
                    nome = nome,
                    livello = 100
                });
            }
            else
            {
                gameData.magazzino.Add(new gameData.bottMagaz
                {
                    nome = nome,
                    livello = 15
                });
            }
            Debug.Log("Comprata bottiglia " + nome);
            foreach (var i in gameData.magazzino)
            {
                Debug.Log(i.nome+" "+ i.livello);
            }
            caricaBott();
        }
        else Debug.Log("non comparata bottglia " + nome);
    }
}


/*
 *  foreach (Transform item in contenitore)
        {
            Destroy(item.gameObject);
        }
        foreach(var bottiglia in gameData.prezziBottiglie)
        {
            string nome = bottiglia.Key;
            float prezzo = bottiglia.Value;
            if (gameData.misureSprite.TryGetValue(nome, out Vector2 misura))
            {
                RectTransform rt=prefabBott.GetComponent<RectTransform>();
                rt.sizeDelta = misura;
            }
            if (nome=="Arancia") item = Instantiate(prefabArancia, contenitore);
            else item = Instantiate(prefabBott, contenitore);
            TMP_Text[] testi = item.GetComponentsInChildren<TMP_Text>();
            Image immagine = item.GetComponent<Image>();
            Button btn = item.GetComponent<Button>();
            //testi[0].text = nome;
            testi[0].text = prezzo + " €";
            testi[0].font = font;
            Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
            if (sprite != null && immagine != null) immagine.sprite = sprite;
            else Debug.Log("immagine no dal negozio");
            testi[0].color = new Color(0,0,0);
            btn.onClick.AddListener(() => compraBott(nome, prezzo));
 * */