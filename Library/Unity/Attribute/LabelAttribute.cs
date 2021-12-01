using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace TakahashiH
{
    /// <summary>
    /// �C���X�y�N�^�̃��x�������w�肵��������Œu��������A�g���r���[�g
    /// </summary>
    public sealed class LabelAttribute : PropertyAttribute
    {
        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �\�����镶����
        /// </summary>
        public string Label;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="label"> �\�����镶���� </param>
        public LabelAttribute(string label)
        {
            Label = label;
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// LabelAttribute �`��p�N���X
    /// </summary>
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public sealed class LabelDrawer : PropertyDrawer
    {
        //====================================
        //! �֐��iPropertyDrawer�j
        //====================================

        /// <summary>
        /// �`��
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var labelAttribute = attribute as LabelAttribute;

            EditorGUI.PropertyField(position, property, new GUIContent(labelAttribute.Label));

            EditorGUI.EndProperty();
        }
    }

#endif

}