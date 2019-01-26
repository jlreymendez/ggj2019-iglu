using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludilo;

public class BaseEnter : MonoBehaviour {

    #region Public
    public void EnterBase(Collider collider) {
      var bastion = collider.GetComponentInParent<Bastion>();
      bastion.Enter();
    }

    public void ExitBase(Collider collider) {
      var bastion = collider.GetComponentInParent<Bastion>();
      bastion.Exit();
    }
    #endregion
}
