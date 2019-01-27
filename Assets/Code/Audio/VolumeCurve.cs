using UnityEngine;
using UnityEngine.Animations;
using Ludilo;

[RequireComponent(typeof(AudioSource))]
public class VolumeCurve : MonoBehaviour {

  [SerializeField] AnimationCurve _volumeCurve;
  [SerializeField] FloatReference _variable;
  [SerializeField] BoolReference _invert;

  void Awake() {
    _source = GetComponent<AudioSource>();
  }

  void Update() {
    Evaluate();
  }

  public void Evaluate() {
    _source.volume = _volumeCurve.Evaluate(_invert.Value ? 1 - _variable.Value : _variable.Value);
  }

  AudioSource _source;
}