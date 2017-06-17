using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    public class Chain : Item
    {
        public float movementModifier = .1f;
        public float jumpModifier = .1f;
        public int jumpCount = 5;

        public override void CopyFrom(Item item)
        {
            Chain i = (Chain)item;
            this.itemName = i.itemName;
            this.icon = i.icon;
            this.movementModifier = i.movementModifier;
            this.jumpModifier = i.jumpModifier;
            this.jumpCount = i.jumpCount;
        }

        public override void OnActivate(PlayerController caster)
        {
            //Andere Spieler werden an Stelle festgehalten und können sich nur sehr eingeschränkt bewegen.Mehrmals springen, um Fessel zu lösen
            PlayerController[] targets = GameObject.FindObjectsOfType<PlayerController>().Where(o => o != caster).ToArray();
            foreach (PlayerController p in targets)
            {
                p.ItemChain(movementModifier, jumpModifier, jumpCount);
            }
        }
    }
}
