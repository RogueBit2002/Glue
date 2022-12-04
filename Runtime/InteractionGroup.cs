using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaurensKruis.Glue
{
    [CreateAssetMenu(fileName = "New Interaction Group", menuName = Constants.AssetMenuPrefix + "Interaction Group")]
    public class InteractionGroup : ScriptableObject
    {
        private static HashSet<InteractionGroup> groups = new HashSet<InteractionGroup>();

        public static IReadOnlyCollection<InteractionGroup> Groups
        {
            get
            {
                FilterNulls(); //Required because OnDisable isn't always called on ScriptableObjects(????)
                return groups.ToList().AsReadOnly();
            }
        }

        internal static void FilterNulls() => groups.Where(g => g == null).ToList().ForEach(g => groups.Remove(g));

        private HashSet<IInteractable> interactables = new HashSet<IInteractable>();
        private HashSet<IInteractor> interactors = new HashSet<IInteractor>();

        public IReadOnlyCollection<IInteractable> Interactables => interactables.ToList().AsReadOnly();
        public IReadOnlyCollection<IInteractor> Interactors => interactors.ToList().AsReadOnly();


        private void OnEnable()
        {
            groups.Add(this);
        }

        private void OnDisable()
        {
            groups.Remove(this);    
        }

        internal void Update()
        {
            foreach (IInteractor interactor in interactors)
                interactor.ProcessInteractor_Internal();

            foreach (IInteractable interactable in interactables)
                interactable.ProcessInteractable_Internal();
        }

        #region Register/Unregister

        #region Interactable
        public void RegisterInteractable(ISelectInteractable interactable) => RegisterInteractable((IInteractable) interactable);
        public void RegisterInteractable(IHoverInteractable interactable) => RegisterInteractable((IInteractable) interactable);
        private void RegisterInteractable(IInteractable interactable)
        {
            if (interactables.Contains(interactable))
                throw new ArgumentException("Interactable is already registered.");

            interactables.Add(interactable);
        }

        public bool UnregisterInteractable(IInteractable interactable) => interactables.Remove(interactable);
        #endregion

        #region Interactor
        public void RegisterInteractor(ISelectInteractor interactor) => RegisterInteractor((IInteractor) interactor);
        public void RegisterInteractor(IHoverInteractor interactor) => RegisterInteractor((IInteractor)interactor);
        private void RegisterInteractor(IInteractor interactor)
        {
            if (interactors.Contains(interactor))
                throw new ArgumentException("Interactor is already registered.");

            interactors.Add(interactor);
        }

        public bool UnregisterInteractor(IInteractor interactor) => interactors.Remove(interactor);
        #endregion

        #endregion

        #region Requests

        #region Select
        public bool RequestSelection(ISelectInteractor interactor, ISelectInteractable interactable)
        {
            if (!interactables.Contains(interactable))
                throw new ArgumentException("Interactable isn't registered.");

            if (!interactors.Contains(interactor))
                throw new ArgumentException("Interactor isn't registered.");

            if (interactor.SelectTarget == interactable)
                return false;

            if (!interactor.CanSelect(interactable) || !interactable.CanSelect(interactor))
                return false;

            interactor.OnSelectStart_Internal(interactable);
            interactable.OnSelectStart_Internal(interactor);

            return true;
        }

        public void RequestSelectionEnd(ISelectInteractor interactor)
        {
            if (!interactors.Contains(interactor))
                throw new ArgumentException("Interactor isn't registered.");

            if (interactor.SelectTarget == null)
                return;

            ISelectInteractable interactable = interactor.SelectTarget;

            interactor.OnSelectEnd_Internal(interactable);
            interactable.OnSelectEnd_Internal(interactor);
        }

        #endregion

        #region Hover

        public bool RequestHover(IHoverInteractor interactor, IHoverInteractable interactable)
        {
            if (!interactables.Contains(interactable))
                throw new ArgumentException("Interactable isn't registered.");

            if (!interactors.Contains(interactor))
                throw new ArgumentException("Interactor isn't registered.");

            if (interactor.HoverTarget == interactable)
                return false;

            if (!interactor.CanHover(interactable) || !interactable.CanHover(interactor))
                return false;

            interactor.OnHoverStart_Internal(interactable);
            interactable.OnHoverStart_Internal(interactor);

            return true;
        }

        public void RequestHoverEnd(IHoverInteractor interactor)
        {
            if (!interactors.Contains(interactor))
                throw new ArgumentException("Interactor isn't registered.");

            if (interactor.HoverTarget == null)
                return;

            IHoverInteractable interactable = interactor.HoverTarget;

            interactor.OnHoverEnd_Internal(interactable);
            interactable.OnHoverEnd_Internal(interactor);
        }

        #endregion
        #endregion
    }
}
