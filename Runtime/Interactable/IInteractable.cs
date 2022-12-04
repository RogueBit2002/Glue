using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.Glue
{
    public interface IInteractable
    {
        public InteractionGroup Group { get; }
        protected void ProcessInteractable();

        internal void ProcessInteractable_Internal() => ProcessInteractable();
    }
}
