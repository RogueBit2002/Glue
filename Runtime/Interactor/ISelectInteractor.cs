using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.Glue
{
    public interface ISelectInteractor : IInteractor
    {
        public ISelectInteractable SelectTarget { get; }

        public bool CanSelect(ISelectInteractable interactable);
        protected void OnSelectStart(ISelectInteractable interactable);
        protected void OnSelectEnd(ISelectInteractable interactable);

        internal void OnSelectStart_Internal(ISelectInteractable interactable) => OnSelectStart(interactable);
        internal void OnSelectEnd_Internal(ISelectInteractable interactable) => OnSelectEnd(interactable);
    }
}
