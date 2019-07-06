using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/HuntWaterDecision")]
public class LookDecision : Decision {
    public override bool decide(StateController controller) {
        return true;
    }
}