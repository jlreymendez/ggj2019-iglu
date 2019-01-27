using UnityEngine;

public class PlayerNeedsCristal : Thought {

  protected override bool IsMet(Bastion bastion) {
    return !bastion.HasFamily && !bastion.HasLight;
  }

}
