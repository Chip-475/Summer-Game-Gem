using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class negozio : MonoBehaviour
{
    [SerializeField] private Transform contenitore;
    [SerializeField] private GameObject prefabBott;
    [SerializeField] private TMP_Text testoMonete;
    [SerializeField] private Button chiudi;
    [SerializeField] private TMP_Text monete;
    public TMP_FontAsset font;
    private bool sem=false;

    void Start()
    {
        chiudi.onClick.AddListener(apriChiudiNeg);    
    }

    void Update()
    {
        testoMonete.text = "Monete: " + gameData.monete;    
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
        monete.text = "Monete: " + gameData.monete + " €";
    }

    private void caricaBott()
    {
        foreach (Transform item in contenitore)
        {
            Destroy(item.gameObject);
        }
        foreach(var bottiglia in gameData.prezziBottiglie)
        {
            string nome = bottiglia.Key;
            float prezzo = bottiglia.Value;
            GameObject item = Instantiate(prefabBott, contenitore);
            TMP_Text[] testi = item.GetComponentsInChildren<TMP_Text>();
            Image immagine = item.GetComponent<Image>();
            Button btn = item.GetComponent<Button>();
            //testi[0].text = nome;
            testi[1].text = prezzo + " €";
            testi[1].font = font;
            Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
            if (sprite != null && immagine != null) immagine.sprite = sprite;
            else Debug.Log("immagine no dal negozio");
            if (gameData.misureSprite.TryGetValue(nome, out Vector2 misura))
            {
                RectTransform rt = immagine.rectTransform;
                rt.sizeDelta = misura;
            }
            testi[1].color = new Color(0,0,0);
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
