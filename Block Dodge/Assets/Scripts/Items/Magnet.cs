using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Magnet : Item
    {
        public GameObject blockPrefab;

        public override void CopyFrom(Item item)
        {
            Magnet i = (Magnet)item;
            this.itemName = i.itemName;
            this.icon = i.icon;
            this.blockPrefab = i.blockPrefab;
        }
        public override void OnActivate(PlayerController caster)
        {
            //Alle anderen Spieler werden zu einem Block hingezogen und können sich erst bewegen, wenn sie am Block angekommen sind
        }
    }
}
