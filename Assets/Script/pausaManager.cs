using UnityEngine;
using UnityEngine.InputSystem;
public class pausaManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject panelImpo;
    private bool sem = false;
    private bool sem2 = false;
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!sem&&!sem2)
            {
                sem = true;
                panel.SetActive(true);
                Time.timeScale = 0f;
            }
            else continua();
            if (sem2)
            {
                panelImpo.SetActive(false);
                sem2 = false;
            }
            
        }
    }
    public void continua()
    {
        sem = false;
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void impo()
    {
        sem2= true;
        panelImpo.SetActive(true);
    }
    public void chiudi()
    {
        panelImpo.SetActive(false); 
    }

}
