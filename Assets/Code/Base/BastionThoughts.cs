using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastionThoughts : MonoBehaviour
{
    [SerializeField] List<Thought> _thoughts;
    [SerializeField] SpriteRenderer _iconTarget;
    [SerializeField] GameObject _target;


    public void Evaluate(Bastion bastion) {
        foreach(var thought in _thoughts) {
            var isMet = thought.Evaluate(bastion);
            if (thought.Evaluate(bastion)) {
                _iconTarget.sprite = thought.Icon;
                _target.SetActive(true);
            }
        }
    }

    public void Clean() {
        _target.SetActive(false);
    }
}
