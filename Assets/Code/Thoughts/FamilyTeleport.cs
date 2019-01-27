using UnityEngine;

public class FamilyTeleport : Thought {

  protected override bool IsMet(Bastion bastion) {
    Debug.Log(_newHome.Value);
    return bastion.HasFamily && bastion.HasLight && !bastion.FinalHome && _newHome.Value != null;
  }

}
