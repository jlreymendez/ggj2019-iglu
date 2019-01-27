using UnityEngine;

public class FamilyTeleport : Thought {

  protected override bool IsMet(Bastion bastion) {
    return bastion.HasFamily && bastion.HasLight && !bastion.FinalHome && _newHome.Value != null;
  }

}
