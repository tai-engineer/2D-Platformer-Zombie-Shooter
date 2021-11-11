using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace PZS
{
    [CustomEditor(typeof(Damager))]
    public class DamagerEditor : Editor
    {
        SerializedProperty _damageProp;
        SerializedProperty _offsetProp;
        SerializedProperty _sizeProp;
        SerializedProperty _canHitTriggerProp;
        SerializedProperty _disableAfterHitProp;
        SerializedProperty _hittableLayerProp;
        SerializedProperty _OnDamageableHitProp;

        BoxBoundsHandle _boxBoundsHandle = new BoxBoundsHandle();
        Color _enableColor = Color.green + Color.gray;

        void OnEnable()
        {
            _damageProp = serializedObject.FindProperty("damage");
            _offsetProp = serializedObject.FindProperty("offset");
            _sizeProp = serializedObject.FindProperty("size");
            _canHitTriggerProp = serializedObject.FindProperty("canHitTrigger");
            _disableAfterHitProp = serializedObject.FindProperty("disableAfterHit");
            _hittableLayerProp = serializedObject.FindProperty("hittableLayer");
            _OnDamageableHitProp = serializedObject.FindProperty("onDamageableHit");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_damageProp);
            EditorGUILayout.PropertyField(_offsetProp);
            EditorGUILayout.PropertyField(_sizeProp);

            EditorGUILayout.PropertyField(_canHitTriggerProp);
            EditorGUILayout.PropertyField(_disableAfterHitProp);
            EditorGUILayout.PropertyField(_hittableLayerProp);
            EditorGUILayout.PropertyField(_OnDamageableHitProp);

            serializedObject.ApplyModifiedProperties();
        }

        void OnSceneGUI()
        {
            Damager damager = (Damager)target;
            if (!damager.enabled)
                return;

            Matrix4x4 handleMatrix = damager.transform.localToWorldMatrix;
            //handleMatrix.SetRow(0, Vector4.Scale(handleMatrix.GetRow(0), new Vector4(1f, 1f, 0f, 1f)));
            //handleMatrix.SetRow(1, Vector4.Scale(handleMatrix.GetRow(1), new Vector4(1f, 1f, 0f, 1f)));
            //handleMatrix.SetRow(2, new Vector4(0f, 0f, 1f, damager.transform.position.z));
            using (new Handles.DrawingScope(handleMatrix))
            {
                _boxBoundsHandle.center = damager.offset;
                _boxBoundsHandle.size = damager.size;
                _boxBoundsHandle.SetColor(_enableColor);

                EditorGUI.BeginChangeCheck();
                _boxBoundsHandle.DrawHandle();
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(damager, "Modify Damager");

                    damager.size = _boxBoundsHandle.size;
                    damager.offset = _boxBoundsHandle.center;
                }
            }
        }
    }
}
