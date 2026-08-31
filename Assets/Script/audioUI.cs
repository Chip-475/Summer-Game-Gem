using UnityEngine;
using UnityEngine.UI;
public class audioUI : MonoBehaviour
{
    public Slider sliderMaster;
    public Slider sliderMusica;
    public Slider sliderEffetti;

    private void OnEnable()
    {
        sliderMaster.SetValueWithoutNotify(audioManager.Instance.leggiVolume(audioManager.Instance.parmMaster));
        sliderMusica.SetValueWithoutNotify(audioManager.Instance.leggiVolume(audioManager.Instance.parmMusica));
        sliderEffetti.SetValueWithoutNotify(audioManager.Instance.leggiVolume(audioManager.Instance.parmEffetti));
        sliderMaster.onValueChanged.AddListener(OnMasterChanged);
        sliderMusica.onValueChanged.AddListener(OnMusicaChanged);
        sliderEffetti.onValueChanged.AddListener(OnEffettiChanged);
    }
    private void OnDisable()
    {
        sliderEffetti.onValueChanged.RemoveListener(OnEffettiChanged);
        sliderMaster.onValueChanged.RemoveListener(OnMasterChanged);
        sliderMusica.onValueChanged.RemoveListener(OnMusicaChanged);  
    }

    private void OnMasterChanged(float v) => audioManager.Instance.impostaVolume(audioManager.Instance.parmMaster, v);
    private void OnMusicaChanged(float v) => audioManager.Instance.impostaVolume(audioManager.Instance.parmMusica, v);
    private void OnEffettiChanged(float v) => audioManager.Instance.impostaVolume(audioManager.Instance.parmEffetti, v);
}
