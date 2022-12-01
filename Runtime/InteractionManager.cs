using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LaurensKruis.Glue
{
    public static class InteractionManager
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnLoad()
        {
            //Trick to call static constructor
        }

        static InteractionManager()
        {
            Initialize();
        }

        private static void Initialize()
        {
            Application.quitting += OnQuit;

            Application.onBeforeRender += OnUpdate;
        }


        private static void OnQuit()
        {
            Application.quitting -= OnQuit;
            Application.onBeforeRender -= OnUpdate;
        }


        private static void OnUpdate()
        {
            foreach (InteractionGroup group in InteractionGroup.Groups)
                group.Update();
        }

    }
}