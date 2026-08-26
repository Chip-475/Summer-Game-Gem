using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuIniziale : MonoBehaviour
{
    [SerializeField] private Button bottoneContinua;
    [SerializeField] private string nomeSceneGioco = "incidente";

    void Start()
    {
        // il bottone Continua è cliccabile solo se esiste un salvataggio
        bottoneContinua.interactable = SaveManager.EsisteSalvataggio();
    }

    public void NuovaPartita()
    {
        gameData.ResetDati();
        SaveManager.cancellaSalvataggio(); 
        SceneManager.LoadScene(nomeSceneGioco);
    }

    public void Continua()
    {
        if (SaveManager.carica())
        {
            SceneManager.LoadScene("barGame");
        }
        else Debug.Log("Impossibile continuare: nessun salvataggio valido");
   
    }

    public void Esci()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}