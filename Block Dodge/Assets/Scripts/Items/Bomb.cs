using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Programmiert von Maximilian Schöberl
namespace Items
{
    public class Bomb : Item
    {
        public GameObject bombPrefab;
        public float delay;
        public float radius;

        public override void CopyFrom(Item item)
        {
            Bomb i = (Bomb)item;
            this.itemName = i.itemName;
            this.icon = i.icon;
            this.bombPrefab = i.bombPrefab;
            this.delay = i.delay;
            this.radius = i.radius;
        }

        public override void OnActivate(PlayerController caster)
        {
            //Man lässt eine Bombe beim Spawner fallen, die bei Kollision nach x Sekunden explodiert, Blöcke zerstört und anderen Spielern schadet
            BombController bomb = GameObject.Instantiate(bombPrefab, new Vector3(caster.SpawnController.transform.position.x, 34, 0), Quaternion.identity, GameObject.Find("Gameplay/Items").transform).GetComponent<BombController>();
            bomb.Init(delay, radius);
        }
    }
}
