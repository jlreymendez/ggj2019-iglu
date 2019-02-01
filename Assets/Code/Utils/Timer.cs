using UnityEngine;
using Ludilo;

public class Timer : MonoBehaviour {

  public void Reset() {
    _counting = false;
    _percentVariable.Value = 0f;
    _timerVariable.Value = 0f;
  }

  public void StartTimer() {
    _counting = true;
    _timerVariable.Value = _timeToTrigger.Value;
    _percentVariable.Value = 1.0f;
  }

  [SerializeField] GameEvent _delayedEvent;
  [SerializeField] FloatReference _timerVariable;
  [SerializeField] FloatReference _percentVariable;
  [SerializeField] FloatReference _timeToTrigger;


  void Update() {
    if (!_counting) { return; }

    _timerVariable.Value = Mathf.Max(_timerVariable - Time.deltaTime, 0);
    _percentVariable.Value = 1 - _timerVariable / _timeToTrigger;

    if (_timerVariable.Value < 0) {
      _counting = false;

      if (_delayedEvent != null) {
        _delayedEvent.Raise();
      }
    }
  }

  bool _counting;
}