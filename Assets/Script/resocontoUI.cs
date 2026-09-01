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

/*
 * >> risolto    >>? per meta
 
bug fix:
-leo con la stessa battuta,   >>?
-vedere se la meccanica di almeno una selezione va bene
-sistemare quando compro delle stesse bottiglie che si bugga con che non crea dei doppioni?!?!

ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index controllare questa eccezione
battuta b = convAttuale.battuta[indice]; in mostra battuta

 */
