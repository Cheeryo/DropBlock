using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Teleport : Item
    {
        public override void CopyFrom(Item item)
        {
            Teleport i = (Teleport)item;
            this.itemName = i.itemName;
            this.icon = i.icon;
        }
        public override void OnActivate(PlayerController caster)
        {
            //Spieler tauscht Platz mit anderem Spieler (der am nächsten ist)
        }
    }
}
