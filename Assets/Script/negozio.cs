using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Tilemaps;
public class negozio : MonoBehaviour
{
    public Transform cont1;
    public Transform cont2;
    [SerializeField] private Transform contenitore;
    [SerializeField] private GameObject prefabBott;
    [SerializeField] private GameObject prefabArancia;
    [SerializeField] private TMP_Text testoMonete;
    [SerializeField] private Button chiudi;
    public TMP_FontAsset font;
    private bool sem=false;
    private GameObject item;

    void Start()
    {
        chiudi.onClick.AddListener(apriChiudiNeg);
        RectTransform rt=prefabArancia.GetComponent<RectTransform>();
        rt.sizeDelta=gameData.misureSprite["Arancia"];
    }

    public void apriChiudiNeg()
    {
        if(sem)
        {
            gameObject.SetActive(false);
            sem = false;
        }
        else
        {
            gameObject.SetActive(true);
            sem = true;
            caricaBott();
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
        string[] primiNove={"Gin","Vodka","RUM","Tonica","Coca Cola","Lemon Soda","Jägermeister","Jack Daniel's","Disaronno"};
        string[] nuoviBottoni={"Energy drink","Arancia","Tequila","Triple sec","Whiskey","Ginger ale"};
        foreach(string nome in primiNove)
        {
            if (gameData.prezziBottiglie.ContainsKey(nome)) caricaBottone(nome, cont1);
        }
        foreach(string nome in nuoviBottoni)
        {
            if (gameData.prezziBottiglie.ContainsKey(nome)) caricaBottone(nome, cont2);
        }
    }

    private void caricaBottone(string nome, Transform parent)
    {
        float prezzo = gameData.prezziBottiglie[nome];

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
            // gameData.bottiglie[nome] = 100;
            gameData.magazzino.Add(new gameData.bottMagaz
            {
                nome = nome,
                livello = 100
            });
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