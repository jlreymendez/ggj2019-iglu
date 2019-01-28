using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using Ludilo;

public class Fader : MonoBehaviour {

  public void StartFade() {
    _fading = true;
    Evaluate();
  }

  [SerializeField] AnimationCurve _fadeCurve;
  [SerializeField] FloatReference _variable;
  [SerializeField] BoolReference _invert;

  void Awake() {
    _targets = GetComponentsInChildren<Graphic>();
  }

  void Update() {
    if (!_fading) { return; }
    Evaluate();
  }

  public void Evaluate() {
    var alpha = _fadeCurve.Evaluate(_invert.Value ? 1 - _variable.Value : _variable.Value);
    foreach (var graphic in _targets) {
      var color = graphic.color;
      color.a = alpha;
      graphic.color = color;
    }
  }


  bool _fading = false;
  Graphic[] _targets;
}