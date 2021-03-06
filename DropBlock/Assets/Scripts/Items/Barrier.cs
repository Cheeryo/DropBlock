﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Programmiert von Maximilian Schöberl
namespace Items
{
    public class Barrier : Item
    {
        public GameObject barrierPrefab;

        public override void CopyFrom(Item item)
        {
            Barrier i = (Barrier)item;
            this.itemName = i.itemName;
            this.icon = i.icon;
            this.barrierPrefab = i.barrierPrefab;
        }

        public override void OnActivate(PlayerController caster)
        {
            //4-Blöcke hohes Hindernis, das anderen Spielern schadet und nicht zum Abspringen benutzt werden kann
            BlockController block = GameObject.Instantiate(barrierPrefab, new Vector3(caster.SpawnController.transform.position.x, 34, 0), Quaternion.identity, GameObject.Find("Gameplay/Cubes").transform).GetComponent<BlockController>();
            block.Caster = caster;
        }
    }
}
