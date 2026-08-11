using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class scaffale : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textLivello=new TMP_Text[9];
    [SerializeField] private Button[] bottoniBottoglia = new Button[9];
    [SerializeField] private Image[] immaginiBottiglie = new Image[9]; // da allegare i bottoni

    private string[] nomiBottiglie = new string[14]
    {
        "RUM","Vodka","Gin","Tonica","Limone","Cola","Jack Daniel's",
        "Jägermeister","Disaronno","Energy drink","Arancia","Tequila",
        "Triple sec","Whiskey"
    };

    void Start()
    {
        for(int i=0;i<9;i++)
        {
            bottoniBottoglia[i].onClick.AddListener(() => apriNegozio(i));
        }
        aggLivelli();
    }
    void Update()
    {
        aggLivelli();
    }

    private void aggLivelli()
    {
        for(int i=0;i<9;i++)
        {
            string nome=nomiBottiglie[i];
            int livello = gameData.bottiglie[nome];
            textLivello[i].text =""+livello;
            if (livello <= 0) textLivello[i].color = new Color(1, 0.3f, 0.3f); //cioè di rosso
            else if (livello <= 30) textLivello[i].color = new Color(1, 0.8f, 0.3f);  //arancione
            else textLivello[i].color = new Color(0.3f, 1, 0.3f); //verde
        }
    }

    private void apriNegozio(int indice)
    {
        //salvo la bottiglia cosi nn la sovrasrivo
        PlayerPrefs.SetInt("BottigliaDaCambiare", indice);
        cocktail.apriScena("Negozio");
    }
}
