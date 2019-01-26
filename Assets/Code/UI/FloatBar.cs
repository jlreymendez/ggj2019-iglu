using UnityEngine;
using UnityEngine.UI;
using Ludilo;

public class FloatBar : MonoBehaviour {

  [SerializeField] FloatReference _variable;
  [SerializeField] Image _target;

  void Update() {
    _target.fillAmount = _variable.Value;
  }
}