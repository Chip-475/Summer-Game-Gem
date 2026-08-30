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
    private float tempo = 600f; //10min =600s
    private float tempoRimasto;
    private float oraInizio = 20f;
    private float oraFine = 6f;

    public TMP_Text testoTempo;
    public GameObject panel;
    void Start()
    {
        tempoRimasto = tempo;
    }

    void Update()
    {
        tempoRimasto -= Time.deltaTime;
        float perc=1f-(tempoRimasto/tempo);
        float oraPass = (oraFine - oraInizio + 24f) * perc;
        float oraAtt = oraInizio + oraPass;
        if (oraAtt >= 24f) oraAtt -= 24f;
        int ore = (int)oraAtt;
        int min = (int)((oraAtt - ore) * 60);
        int sec = (int)tempoRimasto;
        testoTempo.text = "Giorno " + giornoAttuale + "  " +ore.ToString("00")+ ":" + min.ToString("00");
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
