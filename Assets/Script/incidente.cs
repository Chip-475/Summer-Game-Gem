using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
public class incidente : MonoBehaviour
{
    public TMP_Text testoPanel;
    public float velocita = 0.05f;
    public float delay = 1f;
    public AudioClip suono;
    private string[] scena = new string[]
    {
        "Era una notte come tante altre...",
        "Mara guidava sulla strada di ritorno dal lavoro",
        "Improvvisamente una macchina sbanda dalla corsia opposta",
        "Lo scontro è violento",
        "Quando Mara si sveglia è sola",
        "Leo, suo amico non ce l'ha fatta",
        "Ora lavora al suo bar, cercando di andare avanti",
        "Ma Leo continua a tormentarla..."
    };
    private int indice=0;
    private bool click=false;

    void Start()
    {
        audioManager.Instance.suonaEffetto(suono);
        mostraBattuta();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame&& click)prosBattuta();
    }

    private void mostraBattuta()
    {
        if(indice<scena.Length)
        {
            click = false;
            StartCoroutine(TypewriterEffect(scena[indice]));
        }
        else SceneManager.LoadScene("barGame");
    }
    
    IEnumerator TypewriterEffect(string testo)
    {
        testoPanel.text = "";
        foreach(char lettera in testo)
        {
            testoPanel.text += lettera;
            yield return new WaitForSeconds(velocita);
        }
        yield return new WaitForSeconds(delay);
        click = true;
    }

    private void prosBattuta()
    {
        indice++;
        mostraBattuta();
    }
}
