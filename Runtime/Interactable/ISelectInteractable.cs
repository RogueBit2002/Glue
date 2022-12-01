using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.Glue
{
    public interface ISelectInteractable : IInteractable
    {
        public IEnumerable<ISelectInteractor> SelectTargets { get; }

        public bool CanSelect(ISelectInteractor interactor);
        protected void OnSelectStart(ISelectInteractor interactor);
        protected void OnSelectEnd(ISelectInteractor interactor);

        internal void OnSelectStart_Internal(ISelectInteractor interactor) => OnSelectStart(interactor);
        internal void OnSelectEnd_Internal(ISelectInteractor interactor) => OnSelectEnd(interactor);
    }
}
