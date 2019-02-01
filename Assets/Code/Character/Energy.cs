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

    public void EnterBase(Collider collider) {
        // Check the state of the base.
        var bastion = collider.GetComponentInParent<Bastion>();
        _charging = true;
        _currentBastion = bastion;
        _draining = false;
    }

    public void ExitBase(Collider collider) {
        _charging = false;
        _currentBastion = null;
        var bastion = collider.GetComponentInParent<Bastion>();
        if (!bastion.HasLight) {
            _draining = true;
        }
    }

    public void EnterBaseArea(Collider collider) {
        var bastion = collider.GetComponentInParent<Bastion>();
        if (bastion.HasLight) {
            _draining = false;
        }
    }

    public void ExitBaseArea(Collider collider) {
        _draining = true;
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

        if (_charging && _currentBastion != null) {
            _currentEnergy.Value += _currentBastion.GiveLight(_currentEnergy.Value);
        }

        if (!_draining) { return; }

        if (_timeLoss) {
            TimeLoss();
        } else {
            MovementLoss();
        }

        if (_currentEnergy.Value <= 0) {
            _energyOutEvent.Raise();
            enabled = false;
        }
    }
    #endregion

    #region Private
    bool _draining = false;
    bool _charging = true;
    float _deltaPosition;
    Vector3 _oldPosition;
    Bastion _currentBastion;

    void TimeLoss() {
        _currentEnergy.Value -= _drainSpeed.Value * Time.deltaTime;
    }

    void MovementLoss() {
        _currentEnergy.Value -= _drainSpeed.Value * _deltaPosition;
    }
    #endregion
}
