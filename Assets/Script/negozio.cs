using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class negozio : MonoBehaviour
{
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
        foreach (Transform item in contenitore)
        {
            Destroy(item.gameObject);
        }
        foreach(var bottiglia in gameData.prezziBottiglie)
        {
            string nome = bottiglia.Key;
            float prezzo = bottiglia.Value;
            if(nome=="Arancia") item = Instantiate(prefabArancia, contenitore);
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
            LayoutElement layout = item.GetComponent<LayoutElement>();
            if (gameData.misureSprite.TryGetValue(nome, out Vector2 misura))
            {
                layout.preferredHeight = misura.y;
                layout.preferredWidth = misura.x;
            }
            testi[0].color = new Color(0,0,0);
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
