using UnityEngine;
using Ludilo;

public class HomeDistance : MonoBehaviour {


  [SerializeField] FloatReference _distance;
  [SerializeField] FloatReference _capAt;
  [SerializeField] BastionReference _home;


  void Update() {
    if (_home.Value == null) { return; }

    _distance.Value = (transform.position - _home.Value.transform.position).magnitude;
    _distance.Value = Mathf.Clamp(_distance.Value / _capAt.Value, 0f, 1f);
  }
}