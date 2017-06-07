using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class JumpBoost : Item
    {
        public float boostModifier = 2f;
        public float duration = 5f;

        public override void CopyFrom(Item item)
        {
            JumpBoost i = (JumpBoost)item;
            this.itemName = i.itemName;
            this.icon = i.icon;
            this.boostModifier = i.boostModifier;
            this.duration = i.duration;
        }

        public override void OnActivate(PlayerController caster)
        {
            // höher springen + keine Energiekosten
        }
    }
}
