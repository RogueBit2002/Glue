using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.Glue
{
    public interface IInteractor
    {
        protected void ProcessInteractor();

        internal void ProcessInteractor_Internal() => ProcessInteractor();
    }
}
