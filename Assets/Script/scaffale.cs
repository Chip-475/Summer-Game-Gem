//10:39
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class scaffale : MonoBehaviour
{
    public TMP_Text[] textLivello=new TMP_Text[10];
    public Button[] bottoniBottoglia = new Button[10];
    public Image[] immaginiBottiglie = new Image[10]; // da allegare i bottoni
    public Button[] bottiniMagaz=new Button[9];
    public GameObject panelMagazzino;
    public TMP_Text monete;
    public cocktail cock;
    public magazzino script;
    private string[] nomiBottiglie = new string[15]
    {
        "RUM","Vodka","Gin","Tonica","Lemon Soda","Coca Cola","Jack Daniel's",
        "Jagermeister","Disaronno","Energy drink","Arancia","Tequila",
        "Triple sec","Whiskey","Ginger ale"
    };

    void Start()
    {
        for(int i=0;i<immaginiBottiglie.Length;i++)
        {
            int indice = i;
            //bottoniBottoglia[i].onClick.AddListener(() => apriNegozio(indice));
            bottoniBottoglia[i].onClick.RemoveAllListeners();
            bottoniBottoglia[i].onClick.AddListener(() => cock.AddIngredienti(gameData.scaffaleAttivo[indice],indice));
        }
        aggLivelli();
        monete.text = "Monete: " + gameData.monete;
    }
    void Update()
    {
        
    }

    public void aggLivelli()
    {
        for(int i=0;i<immaginiBottiglie.Length;i++)
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
            else if (nome == "Arancia"&&livello>=10) textLivello[i].color = new Color(0.3f, 1, 0.3f);
            else if(nome=="Arancia"&&livello>=5&&livello<10) textLivello[i].color = new Color(1, 0.8f, 0.3f);
            else if(nome=="Arancione"&&livello<5) textLivello[i].color = new Color(1, 0.3f, 0.3f);
            else textLivello[i].color = new Color(0.3f, 1, 0.3f); //verde
        }
        monete.text = "Monete: " + gameData.monete;
    }

    public void apriMagazzino(int posizione)
    {
        panelMagazzino.SetActive(true);
        //con le bottiglie disponibili
        //GetComponentInParent<Canvas>().GetComponentInChildren<magazzino>().caricaMagazzino(posizione);  // dallo script del magazino della pos che cambia
        script.caricaMagazzino(posizione);
    }

    private void apriNegozio(int indice)
    {
        //salvo la bottiglia cosi nn la sovrasrivo
        PlayerPrefs.SetInt("BottigliaDaCambiare", indice);
        //cocktail.apriScena("Negozio");
    }
}
