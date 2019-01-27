using UnityEngine;

public class PlayerBringFamily : Thought {

  protected override bool IsMet(Bastion bastion) {
    return !bastion.HasFamily && bastion.HasLight && bastion.AllowFamily;
  }

}
