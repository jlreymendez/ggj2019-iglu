using UnityEngine;
using UnityEngine.Animations;
using Ludilo;

[RequireComponent(typeof(AudioSource))]
public class VolumeCurve : MonoBehaviour {

  [SerializeField] AnimationCurve _volumeCurve;
  [SerializeField] FloatReference _variable;

  void Awake() {
    _source = GetComponent<AudioSource>();
  }

  void Update() {
    Evaluate();
  }

  public void Evaluate() {
    _source.volume = _volumeCurve.Evaluate(_variable.Value);
  }

  AudioSource _source;
}