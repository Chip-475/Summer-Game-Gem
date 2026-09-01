using UnityEngine;
using UnityEngine.Audio;
public class audioManager : MonoBehaviour
{
    public static audioManager Instance { get; private set; }
    [Header("audio mixer")]
    public AudioMixer mixer;
    public AudioSource sorg;
    public AudioSource clipEffe;

    [Header("parametri")]
    public string parmMaster = "MasterVolume";
    public string parmMusica = "MusicVolume";
    public string parmEffetti = "SFXVolume";

    public void suonaEffetto(AudioClip clip)
    {
        if (clip == null) return;
        clipEffe.PlayOneShot(clip);
    }
    private void Awake()
    {
        if(Instance!=null&&Instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        caricaVolumi();
    }

    public void caricaVolumi()
    {
        impostaVolume(parmMaster,PlayerPrefs.GetFloat(parmMaster,0.75f));
        impostaVolume(parmMusica, PlayerPrefs.GetFloat(parmMusica, 0.75f));
        impostaVolume(parmEffetti, PlayerPrefs.GetFloat(parmEffetti, 0.75f));
    }
    public void impostaVolume(string parm,float valore)
    {
        valore=Mathf.Clamp(valore,0.0001f, 1f);
        float deci = Mathf.Log10(valore) * 20f;
        bool ok=mixer.SetFloat(parm, deci);
        Debug.Log($"{parm} -> {deci:F1} dB, riuscito: {ok}");
        PlayerPrefs.SetFloat(parm, valore);
    }
       
    public float leggiVolume(string parm)
    {
        return PlayerPrefs.GetFloat(parm, 0.75f);
    }

    public void suona(AudioClip clip,bool loop=true)
    {
        if (sorg.clip == clip && sorg.isPlaying) return;
        sorg.clip= clip;
        sorg.loop= loop;
        sorg.Play();
    }
}
