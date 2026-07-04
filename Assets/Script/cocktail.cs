using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class cocktail : MonoBehaviour
{
    [System.Serializable]
    public class ricetta
    {
        public string nome;
        public List<string> ingredienti;
    }
    [System.Serializable]
    public class ricettaJson
    {
        public List<ricetta> ricette;
    }

    
   // ricettaJson ric = JsonUtility.FromJson<ricettaJson>(ricette.json); 

    //da completare...
}
