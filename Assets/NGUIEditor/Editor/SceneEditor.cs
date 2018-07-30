using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NGUIEditorPro
{
    public class SceneEditor
    {
		private static Object lastSelectObj = null;

		private static Object curSelectObj = null;

		[InitializeOnLoadMethod]
		private static void Init()
		{
			SceneView.onSceneGUIDelegate += OnSceneGUI;
		}

		private static void OnSceneGUI(SceneView sceneView)
		{
            Event e = Event.current;
            bool is_handled = false;
            if (Configure.IsEnableDragUIToScene && (Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragPerform))
            {
                //拉UI prefab或者图片入scene界面时帮它找到鼠标下的Canvas并挂在其上，若鼠标下没有画布就创建一个
                Object handleObj = DragAndDrop.objectReferences[0];
                if (!IsNeedHandleAsset(handleObj))
                {
                    //让系统自己处理
                    return;
                }
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                //当松开鼠标时
                if (Event.current.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (var item in DragAndDrop.objectReferences)
                    {
                        HandleDragAsset(sceneView, item);
                    }
                }
                is_handled = true;
            }
            else if (e.type == EventType.KeyDown && Configure.IsMoveNodeByArrowKey && ((e.modifiers & EventModifiers.Control) != 0))
            {
                //Ctrl + ↑↓←→
                //按上按下要移动节点，因为默认情况下只是移动Scene界面而已
                foreach (var item in Selection.transforms)
                {
                    Transform trans = item;
                    if (trans != null)
                    {
                        if (e.keyCode == KeyCode.UpArrow)
                        {
                            Vector3 newPos = new Vector3(trans.localPosition.x, trans.localPosition.y + 1, trans.localPosition.z);
                            trans.localPosition = newPos;
                            is_handled = true;
                        }
                        else if (e.keyCode == KeyCode.DownArrow)
                        {
                            Vector3 newPos = new Vector3(trans.localPosition.x, trans.localPosition.y - 1, trans.localPosition.z);
                            trans.localPosition = newPos;
                            is_handled = true;
                        }
                        else if (e.keyCode == KeyCode.LeftArrow)
                        {
                            Vector3 newPos = new Vector3(trans.localPosition.x - 1, trans.localPosition.y, trans.localPosition.z);
                            trans.localPosition = newPos;
                            is_handled = true;
                        }
                        else if (e.keyCode == KeyCode.RightArrow)
                        {
                            Vector3 newPos = new Vector3(trans.localPosition.x + 1, trans.localPosition.y, trans.localPosition.z);
                            trans.localPosition = newPos;
                            is_handled = true;
                        }
                    }
                }
            }
            else if (Event.current != null && Event.current.button == 1 && Event.current.type == EventType.MouseUp && Configure.IsShowSceneMenu)
            {
                ContextMenu.AddCommonItems(Selection.gameObjects);
                ContextMenu.Show();
                is_handled = true;
            }
            //else if (e.type == EventType.MouseMove)//show cur mouse pos
            //{
            //    Camera cam = sceneView.camera;
            //    Vector3 mouse_abs_pos = e.mousePosition;
            //    mouse_abs_pos.y = cam.pixelHeight - mouse_abs_pos.y;
            //    mouse_abs_pos = sceneView.camera.ScreenToWorldPoint(mouse_abs_pos);
            //    Debug.Log("mouse_abs_pos : " + mouse_abs_pos.ToString());
            //}
            if (is_handled)
                Event.current.Use();
		}

        private static bool IsNeedHandleAsset(Object obj)
        {
            return true;
        }

        private static void HandleDragAsset(SceneView sceneView,Object HandleObj)
        {
            
        }
    }
}
