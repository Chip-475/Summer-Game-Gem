using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class negozio : MonoBehaviour
{
    [SerializeField] private Transform contenitore;
    [SerializeField] private GameObject prefabBott;
    [SerializeField] private TMP_Text testoMonete;
    [SerializeField] private Button chiudi;
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
            Sprite sprite = Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
            if(sprite!=null&&immagine!=null) immagine.sprite = sprite;
            testi[1].color = new Color(0,0,0);
            btn.onClick.AddListener(() => compraBott(nome, prezzo));
        }
    }

    private void compraBott(string nome,float prezzo)
    {
        if (gameData.monete >= prezzo)
        {
            gameData.monete -= prezzo;
            gameData.bottiglie[nome] = 100;
            Debug.Log("Comprata bottiglia " + nome);
            caricaBott();
        }
        else Debug.Log("non comparata bottglia " + nome);
    }
}
