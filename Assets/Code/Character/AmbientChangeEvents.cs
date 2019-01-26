using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludilo;

public class AmbientChangeEvents : MonoBehaviour
{
    #region Public
    public void EnterHouse(Collider collider)
    {
        var bastion = collider.GetComponentInParent<Bastion>();
        if (
          _currentBastion.Value != null &&
          bastion == _currentBastion.Value)
        {
            _playerEnterHouse.Raise();
        }
        else
        {
            _playerEnterBastion.Raise();
        }
    }

    public void LeaveHouse(Collider collider)
    {
        _playerLeaveHouse.Raise();
    }

    #endregion

    #region UnityAPI
    [SerializeField] BastionReference _currentBastion;
    [SerializeField] GameEvent _playerEnterHouse;
    [SerializeField] GameEvent _playerEnterBastion;
    [SerializeField] GameEvent _playerLeaveHouse;

    #endregion
}
