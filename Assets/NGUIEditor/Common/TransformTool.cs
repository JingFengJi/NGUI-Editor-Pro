using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NGUIEditorPro
{
    public class TransformTool
    {
		public static void ResetPosition()
		{
			if(Selection.activeTransform == null)
				return;
			Selection.activeTransform.localPosition = Vector3.zero;
		}

		public static void ResetRotation()
		{
			if(Selection.activeTransform == null)
				return;
			Selection.activeTransform.localRotation = Quaternion.identity;
		}

		public static void ResetScale()
		{
			if(Selection.activeTransform == null)
				return;
			Selection.activeTransform.localScale = Vector3.one;
		}

		public static void ShowAllSelection()
		{
			if(Selection.gameObjects != null && Selection.gameObjects.Length > 0)
			{
				foreach (var item in Selection.gameObjects)
				{
					item.SetActive(true);
				}
			}
		}

		public static void HideAllSelection()
		{
            if (Selection.gameObjects != null && Selection.gameObjects.Length > 0)
            {
                foreach (var item in Selection.gameObjects)
                {
                    item.SetActive(false);
                }
            }
		}

		public static void Unlock()
		{
			if(Selection.gameObjects.Length > 0)
			{
				Selection.gameObjects[0].hideFlags = HideFlags.None;
			}
		}

		public static void Lock()
		{
			if(Selection.gameObjects.Length > 0)
			{
				Selection.gameObjects[0].hideFlags = HideFlags.NotEditable;
			}
		}
    }
}