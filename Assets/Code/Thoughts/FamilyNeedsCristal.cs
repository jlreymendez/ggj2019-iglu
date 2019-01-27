using UnityEngine;

public class FamilyNeedsCristal : Thought {

  protected override bool IsMet(Bastion bastion) {
    return bastion.HasFamily && !bastion.HasLight;
  }

}
