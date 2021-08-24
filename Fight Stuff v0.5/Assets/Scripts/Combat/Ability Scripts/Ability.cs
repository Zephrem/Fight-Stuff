using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public Sprite icon = null;

    new public string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Cast(Character_Stats myStats, Character_Stats targetStats) { }
}
