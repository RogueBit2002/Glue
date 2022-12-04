using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.Glue
{
    public interface IHoverInteractor : IInteractor
    {
        public IHoverInteractable HoverTarget { get; }

        public bool CanHover(IHoverInteractable interactable);
        protected void OnHoverStart(IHoverInteractable interactable);
        protected void OnHoverEnd(IHoverInteractable interactable);

        internal void OnHoverStart_Internal(IHoverInteractable interactable) => OnHoverStart(interactable);
        internal void OnHoverEnd_Internal(IHoverInteractable interactable) => OnHoverEnd(interactable);
    }
}
