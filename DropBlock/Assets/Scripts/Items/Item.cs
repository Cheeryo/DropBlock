using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Programmiert von Maximilian Schöberl
namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        public string itemName;
        public Sprite icon;

        public abstract void CopyFrom(Item item);
        public abstract void OnActivate(PlayerController caster);
    }
}
