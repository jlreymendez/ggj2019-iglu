using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludilo;

public class Energy : MonoBehaviour {

    #region Public
    public void StopDrain() {
        _draining = false;
    }


    public void StartDrain() {
        _draining = true;
    }

    public void Recover(Collider collider) {
        // Check the state of the base.
        var bastion = collider.GetComponentInParent<Bastion>();
        if (bastion.HasFamily) {
            _currentEnergy.Value = 1.0f;
        }
    }

    public void EnterBaseArea(Collider collider) {
        var bastion = collider.GetComponentInParent<Bastion>();
        if (bastion.HasLight) {
            _draining = false;
        }
    }

    #endregion

    #region UnityAPI
    [SerializeField] FloatReference _currentEnergy;
    [SerializeField] BoolReference _timeLoss;
    [SerializeField] FloatReference _drainSpeed;
    [SerializeField] StringReference _bastionLayer;
    [SerializeField] StringReference _bastionAreaLayer;
    [SerializeField] GameEvent _energyOutEvent;
    [SerializeField] GameEvent _resourceUsedEvent;

    void Awake() {
        _currentEnergy.Value = 1.0f;
        _oldPosition = transform.position;
    }

    void Update() {
        _deltaPosition = (transform.position - _oldPosition).magnitude;
        _oldPosition = transform.position;

        if (!_draining) { return; }

        if (_timeLoss) {
            TimeLoss();
        } else {
            MovementLoss();
        }

        if (_currentEnergy.Value <= 0) {
            _energyOutEvent.Raise();
        }
    }
    #endregion

    #region Private
    bool _draining = false;
    float _deltaPosition;
    Vector3 _oldPosition;

    void TimeLoss() {
        _currentEnergy.Value -= _drainSpeed.Value * Time.deltaTime;
    }

    void MovementLoss() {
        _currentEnergy.Value -= _drainSpeed.Value * _deltaPosition;
    }
    #endregion
}
