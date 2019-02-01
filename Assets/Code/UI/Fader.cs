using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using Ludilo;

public class Fader : MonoBehaviour {

  public void StartFade() {
    Debug.LogFormat("{0}: Start fader", name, _fading);
    _fading = true;
  }

  [SerializeField] AnimationCurve _fadeCurve;
  [SerializeField] FloatReference _variable;
  [SerializeField] BoolReference _invert;

  void Awake() {
    var targets = GetComponentsInChildren<Graphic>();
    _targets = new FadeTarget[targets.Length];
    for (var i = 0; i < targets.Length; i++) {
      _targets[i] = new FadeTarget { graphic = targets[i], alpha = targets[i].color.a };
    }
  }

  void Update() {
    Debug.LogFormat("{0}: Fader is fading {1}", name, _fading);
    if (!_fading) { return; }
    Evaluate();
  }

  public void Evaluate() {
    var alpha = _fadeCurve.Evaluate(_invert.Value ? 1 - _variable.Value : _variable.Value);
    foreach (var target in _targets) {
      var color = target.graphic.color;
      color.a = alpha * target.alpha;
      target.graphic.color = color;
    }
  }


  bool _fading = false;
  FadeTarget[] _targets;

  public struct FadeTarget {
    public Graphic graphic;
    public float alpha;
  }
}