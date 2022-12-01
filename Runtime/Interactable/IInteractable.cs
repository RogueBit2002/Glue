using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.Glue
{
    public interface IInteractable
    {
        protected void ProcessInteractable();

        internal void ProcessInteractable_Internal() => ProcessInteractable();
    }
}
