//10:39
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class scaffale : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textLivello=new TMP_Text[10];
    [SerializeField] private Button[] bottoniBottoglia = new Button[10];
    [SerializeField] private Image[] immaginiBottiglie = new Image[10]; // da allegare i bottoni
    [SerializeField] private Button[] bottiniMagaz=new Button[9];
    [SerializeField] private GameObject panelMagazzino;
    private int posSelezionata=-1; //quale pos sta cambiando
    public TMP_Text monete;
    public cocktail cock;
    private string[] nomiBottiglie = new string[14]
    {
        "RUM","Vodka","Gin","Tonica","Lemon Soda","Coca Cola","Jack Daniel's",
        "Jägermeister","Disaronno","Energy drink","Arancia","Tequila",
        "Triple sec","Whiskey"
    };

    void Start()
    {
        for(int i=0;i<9;i++)
        {
            int indice = i;
            //bottoniBottoglia[i].onClick.AddListener(() => apriNegozio(indice));
            bottoniBottoglia[i].onClick.RemoveAllListeners();
            bottoniBottoglia[i].onClick.AddListener(() => cock.AddIngredienti(gameData.scaffaleAttivo[indice]));
        }
        aggLivelli();
        monete.text = "Monete: " + gameData.monete + " €";
    }
    void Update()
    {
        
    }

    public void aggLivelli()
    {
        for(int i=0;i<9;i++)
        {
            string nome=gameData.scaffaleAttivo[i];
            int livello = gameData.livelliScaffale[i];
            textLivello[i].text =""+livello;
            Sprite sprite=Resources.Load<Sprite>($"sprite/bottiglie/{nome}");
            if (sprite != null)
            {
                immaginiBottiglie[i].sprite = sprite;
                if(gameData.misureSprite.TryGetValue(nome,out Vector2 misura))
                {
                    RectTransform rt = immaginiBottiglie[i].rectTransform;
                    rt.sizeDelta=misura;
                }
            }
            else Debug.Log("sprite null dallo scaffale");
            if (livello <= 0) textLivello[i].color = new Color(1, 0.3f, 0.3f); //cioè di rosso
            else if (livello <= 30) textLivello[i].color = new Color(1, 0.8f, 0.3f);  //arancione
            else textLivello[i].color = new Color(0.3f, 1, 0.3f); //verde
        }
        monete.text = "Monete: " + gameData.monete + " €";
    }

    public void apriMagazzino(int posizione)
    {
        posSelezionata = posizione;
        panelMagazzino.SetActive(true);
        //con le bottiglie disponibili
        GetComponentInParent<Canvas>().GetComponentInChildren<magazzino>().caricaMagazzino(posizione);  // dallo script del magazino
    }

    private void apriNegozio(int indice)
    {
        //salvo la bottiglia cosi nn la sovrasrivo
        PlayerPrefs.SetInt("BottigliaDaCambiare", indice);
        //cocktail.apriScena("Negozio");
    }
}
