using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludilo;

public class BaseEnter : MonoBehaviour {

    #region Public
    public void EnterBase(Collider collider) {
      var bastion = collider.GetComponentInParent<Bastion>();
      bastion.Enter();

      if (bastion.HasFamily && bastion.FinalHome) {
        _gameOverEvent.Raise();
      }

      _onEnter.Invoke(bastion);
    }

    public void ExitBase(Collider collider) {
      var bastion = collider.GetComponentInParent<Bastion>();
      bastion.Exit();
      _onExit.Invoke(bastion);
    }
    #endregion

    #region UnityAPI
    [SerializeField] BastionEvent _onEnter;
    [SerializeField] BastionEvent _onExit;

    [SerializeField] GameEvent _gameOverEvent;
    #endregion
}

[System.Serializable]
public class BastionEvent : UnityEvent<Bastion> {}
