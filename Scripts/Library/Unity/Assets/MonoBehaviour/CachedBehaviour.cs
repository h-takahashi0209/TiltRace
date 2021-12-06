using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Unity の MonoBehaviour を拡張したもの
    /// 基本的にこちらを継承させる
    /// </summary>
    public abstract class ExMonoBehaviour : MonoBehaviour
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// Transform のキャッシュ
        /// </summary>
        private Transform mTransformCache;

        /// <summary>
        /// RectTransform のキャッシュ
        /// </summary>
        private RectTransform mRectTransformCache;

        /// <summary>
        /// GameObject のキャッシュ
        /// </summary>
        private GameObject mGameObjectCache;


        //====================================
        //! プロパティ
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
