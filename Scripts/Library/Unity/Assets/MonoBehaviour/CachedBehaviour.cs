using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Unity �� MonoBehaviour ���g����������
    /// ��{�I�ɂ�������p��������
    /// </summary>
    public abstract class ExMonoBehaviour : MonoBehaviour
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// Transform �̃L���b�V��
        /// </summary>
        private Transform mTransformCache;

        /// <summary>
        /// RectTransform �̃L���b�V��
        /// </summary>
        private RectTransform mRectTransformCache;

        /// <summary>
        /// GameObject �̃L���b�V��
        /// </summary>
        private GameObject mGameObjectCache;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// Transform
        /// </summary>
        public new Transform transform
        {
            get
            {
                if (mTransformCache == null)
                {
                    mTransformCache = GetComponent<Transform>();
                }

                return mTransformCache;
            }
        }

        /// <summary>
        /// RectTransform
        /// </summary>
        public RectTransform rectTransform
        {
            get
            {
                if (mRectTransformCache == null)
                {
                    mRectTransformCache = GetComponent<RectTransform>();
                }

                return mRectTransformCache;
            }
        }

        /// <summary>
        /// GameObject
        /// </summary>
        public new GameObject gameObject
        {
            get
            {
                if (mGameObjectCache == null)
                {
                    mGameObjectCache = GetComponent<GameObject>();
                }

                return mGameObjectCache;
            }
        }
    }
}
