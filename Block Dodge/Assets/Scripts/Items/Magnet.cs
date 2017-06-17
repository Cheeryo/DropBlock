using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            PlayerController[] targets = GameObject.FindObjectsOfType<PlayerController>().Where(o => o != caster).ToArray();
            RaycastHit hit;
            Vector3 pos = caster.SpawnController.transform.position;
            if (Physics.Raycast(caster.SpawnController.transform.position + Vector3.up * 29, Vector3.down, out hit, 30))
            {
                pos = hit.point;
                pos.z = 0;
            }
            GameObject block = GameObject.Instantiate(blockPrefab, pos, Quaternion.identity, GameObject.Find("Gameplay/Cubes").transform);
            foreach (PlayerController p in targets)
            {
                p.ItemMagnet(block);
            }
        }
    }
}
