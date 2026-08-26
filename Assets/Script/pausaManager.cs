using UnityEngine;
using UnityEngine.InputSystem;
public class pausaManager : MonoBehaviour
{
    public GameObject panel;
    private bool sem = false;
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!sem)
            {
                sem = true;
                panel.SetActive(true);
                Time.timeScale = 0f;
            }
            else continua();
            
        }
    }

    public void continua()
    {
        sem = false;
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

}
