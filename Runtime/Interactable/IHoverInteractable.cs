using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.Glue
{
    public interface IHoverInteractable : IInteractable
    {
        public IEnumerable<IHoverInteractor> HoverTargets { get; }

        public bool CanHover(IHoverInteractor interactor);
        protected void OnHoverStart(IHoverInteractor interactor);
        protected void OnHoverEnd(IHoverInteractor interactor);

        internal void OnHoverStart_Internal(IHoverInteractor interactor) => OnHoverStart(interactor);
        internal void OnHoverEnd_Internal(IHoverInteractor interactor) => OnHoverEnd(interactor);
    }
}
