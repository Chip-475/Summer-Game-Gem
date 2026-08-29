using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
public class dayManager : MonoBehaviour
{
    public static int giornoAttuale = 1;
    public static float guadagno;
    public static float spesa;
    public static int clientiServ;
    private float tempo = 60f; //10min
    private float tempoRimasto;

    public TMP_Text testoTempo;
    public GameObject panel;
    void Start()
    {
        tempoRimasto = tempo;
    }

    void Update()
    {
        tempoRimasto -= Time.deltaTime;
        int min = (int)(tempoRimasto / 60);
        int sec = (int)(tempoRimasto % 60);
        testoTempo.text = "Giorno " + giornoAttuale + " - " + min + ":" + sec.ToString("00");
        if (tempoRimasto <= 0) fineGiorno();
    }
    private void fineGiorno()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
        panel.GetComponent<resocontoUI>().mostraResoconto(giornoAttuale,guadagno,spesa,clientiServ);
        giornoAttuale++;
        guadagno = 0;
        spesa = 0;
        clientiServ = 0;
        tempoRimasto = tempo;
    }
    public static void aggGuad(float imp)
    {
        guadagno += imp;
        gameData.monete += imp;
    }
    public static void aggSpesa(float imp)
    {
        spesa += imp;
    }
    public static void aggCli()
    {
        clientiServ++;
    }
    public void chiudiRes()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
