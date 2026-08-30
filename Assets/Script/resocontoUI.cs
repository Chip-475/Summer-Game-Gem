using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class resocontoUI : MonoBehaviour
{
    public TMP_Text testoGiorno;
    public TMP_Text testoGuad;
    public TMP_Text testoSpes;
    public TMP_Text testoCli;
    public TMP_Text testoTot;
    public Button avanti;
    
    public void mostraResoconto(int giorno,float guad,float spes,int cli)
    {
        testoGiorno.text = "Giorno: " + giorno;
        testoGuad.text = "Guadagni: " + guad;
        testoSpes.text = "Spese: " + spes;
        testoCli.text = "Clienti serviti: " + cli;
        float tot = guad - spes;
        testoTot.text = "Totale: " + tot;
        if(tot<0)testoTot.color= Color.red;
        else testoTot.color=Color.green;
    }
    
}
