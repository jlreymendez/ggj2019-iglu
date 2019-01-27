using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludilo;

public class Bastion : MonoBehaviour {

    #region Public
    public bool HasLight {
        get { return _homeEnergy > 0f; }
    }

    public bool HasFamily {
        get { return _hasFamily.Value; }
    }

    public bool AllowFamily {
        get { return _allowsFamily.Value; }
    }

    public bool FinalHome {
        get { return _finalHome.Value; }
    }

    public bool Emptied {
        get { return _emptied; }
    }

    public void Enter() {
        _onEnter.Invoke(this);
    }

    public void Exit() {
        _onExit.Invoke(this);
    }

    public void TurnLight() {
        if (HasLight) { return; }

        _homeEnergy = _maxEnergy.Value;

        _onTurnOn.Invoke();
    }

    public void TurnOff() {
        if (HasLight) { return; }

        _homeEnergy = 0f;

        _onTurnOff.Invoke();
    }

    public float GiveLight(float currentLight) {
        if (!HasFamily || !HasLight) {
            return 0f;
        }

        var reduction = _energyOutput * Time.deltaTime;
        reduction = Mathf.Min(reduction, Mathf.Min(1 - currentLight, _homeEnergy));
        ReduceLight(reduction);

        return reduction;
    }

    public void MoveIn() {
        _hasFamily.Value = true;
        _currentBastionEnergy.Value = _homeEnergy  / _maxEnergy;
        _onMoveIn.Invoke();
    }

    public void MoveOut() {
        _homeEnergy = 0f;
        _hasFamily.Value = false;
        _emptied = true;
        _onMoveOut.Invoke();
        _onFamilyMoved.Raise();
    }
    #endregion

    #region UnityAPI
    [SerializeField] FloatReference _maxEnergy;
    [SerializeField] FloatReference _energyOutput;
    [SerializeField] FloatReference _currentBastionEnergy;
    [SerializeField] BastionReference _currentBastion;
    [SerializeField] BoolReference _hasFamily;
    [SerializeField] BoolReference _allowsFamily;
    [SerializeField] BoolReference _finalHome;
    [SerializeField] GameEvent _onFamilyDied;
    [SerializeField] GameEvent _onFamilyMoved;
    [SerializeField] BastionEvent _onEnter;
    [SerializeField] BastionEvent _onExit;
    [SerializeField] UnityEvent _onTurnOn;
    [SerializeField] UnityEvent _onTurnOff;
    [SerializeField] UnityEvent _onMoveIn;
    [SerializeField] UnityEvent _onMoveOut;

    void Awake() {
        if (_hasFamily) {
            _currentBastion.Value = this;
            _currentBastionEnergy.Value = _homeEnergy;
        }
    }

    void OnDestroy() {
        _currentBastion.Value = null;
    }
    #endregion

    #region Private
    float _homeEnergy = 0f;
    bool _emptied = false;

    void ReduceLight(float amount) {
        _homeEnergy -= amount;
        _currentBastionEnergy.Value = _homeEnergy / _maxEnergy;

        if (_homeEnergy <= 0f) {
            _onTurnOff.Invoke();
            if (HasFamily) {
                _hasFamily.Value = false;
                _onFamilyDied.Raise();
            }
        }
    }
    #endregion
}
