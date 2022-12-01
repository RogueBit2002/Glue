using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LaurensKruis.Glue.Editor
{
    public class DebuggerWindow : EditorWindow
    {
        private static readonly Vector2 MinimumWindowSize = new Vector2(200, 300);

        private static Vector2 cachedWindowSize = MinimumWindowSize;

        private class Styles
        {
            public static readonly GUIStyle ActiveGroup = new GUIStyle(EditorStyles.label);

            static Styles()
            {
                ActiveGroup.normal.textColor = EditorStyles.linkLabel.normal.textColor;
            }
        }

        [MenuItem("LK/Glue/Debugger")]
        public static void OpenWindow()
        {
            DebuggerWindow window = GetWindow<DebuggerWindow>();
            window.Show();
        }

        private void CreateGUI()
        {
            titleContent = new GUIContent("Interaction Debugger", EditorGUIUtility.IconContent("RelativeJoint2D Icon").image);
            minSize = MinimumWindowSize;

            
            
            rootVisualElement.Add(CreateToolbar());

            TwoPaneSplitView splitView = new TwoPaneSplitView(0, cachedWindowSize.y / 2f, TwoPaneSplitViewOrientation.Vertical);
            splitView.Add(CreateGroupList());
            splitView.Add(new VisualElement());
            rootVisualElement.Add(splitView);
        }

        private void OnGUI()
        {
            cachedWindowSize = position.size;
        }
        
        private VisualElement CreateToolbar()
        {
            Toolbar toolbar = new Toolbar();
            ToolbarMenu fileMenu = new ToolbarMenu();
            fileMenu.text = "File";
            fileMenu.menu.AppendAction("A", action => { });
            fileMenu.menu.AppendAction("B", action => { });
            fileMenu.menu.AppendSeparator();
            fileMenu.menu.AppendAction("C", action => { });
            toolbar.Add(fileMenu);
            return toolbar;
        }

        private VisualElement CreateGroupList()
        {
            IMGUIContainer imgui = new IMGUIContainer();
            imgui.style.minHeight = new StyleLength(minSize.y / 2f);
            imgui.onGUIHandler = () => DrawGroupList(imgui.contentRect);
            return imgui;
        }



        private void DrawGroupList(Rect rect)
        {
            foreach(InteractionGroup group in InteractionGroup.Groups.OrderBy(g => g.name))
                EditorGUILayout.LabelField(group.name, group.HasMembers ? Styles.ActiveGroup : EditorStyles.label);
        }

    }
}
