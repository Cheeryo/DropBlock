using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Programmiert von Maximilian Schöberl
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
            Vector3 casterPos = caster.transform.position;
            PlayerController target = GameObject.FindObjectsOfType<PlayerController>().Where(o => o != caster).OrderByDescending(o => Vector3.Distance(o.transform.position, caster.transform.position)).ToArray()[0];
            caster.transform.position = target.transform.position;
            target.transform.position = casterPos;
        }
    }
}
