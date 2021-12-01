using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Component �g�����\�b�h��`�p
    /// </summary>
    public static class ComponentExtensions
    {
        //====================================
        //! �֐��ipublic static�j
        //====================================

        #region active

        /// <summary>
        /// �A�N�e�B�u����
        /// </summary>
        /// <param name="isActive"> �A�N�e�B�u�� </param>
        public static void SetActive(this Component self, bool isActive)
        {
            self.gameObject.SetActive(isActive);
        }

        /// <summary>
        /// �A�N�e�B�u��
        /// </summary>
        public static bool IsActiveInHierarchy(this Component self)
        {
            return self.gameObject.activeInHierarchy;
        }

        #endregion

        #region localPosition

        /// <summary>
        /// ���[�J�����W�擾
        /// </summary>
        public static Vector3 GetLocalPosition(this Component self)
        {
            return self.transform.localPosition;
        }

        /// <summary>
        /// ���[�J�� X ���W�擾
        /// </summary>
        public static float GetLocalPositionX(this Component self)
        {
            return self.transform.localPosition.x;
        }

        /// <summary>
        /// ���[�J�� Y ���W�擾
        /// </summary>
        public static float GetLocalPositionY(this Component self)
        {
            return self.transform.localPosition.y;
        }

        /// <summary>
        /// ���[�J�� Z ���W�擾
        /// </summary>
        public static float GetLocalPositionZ(this Component self)
        {
            return self.transform.localPosition.z;
        }

        /// <summary>
        /// ���[�J�����W�ݒ�
        /// </summary>
        /// <param name="localPosition"> ���[�J�����W </param>
        public static void SetLocalPosition(this Component self, Vector3 localPosition)
        {
            self.transform.localPosition = localPosition;
        }

        /// <summary>
        /// ���[�J�� X ���W�ݒ�
        /// </summary>
        /// <param name="localPositionX"> ���[�J�� X ���W </param>
        public static void SetLocalPositionX(this Component self, float localPositionX)
        {
            self.transform.localPosition = new Vector3(localPositionX, self.transform.localPosition.y, self.transform.localPosition.z);
        }

        /// <summary>
        /// ���[�J�� Y ���W�ݒ�
        /// </summary>
        /// <param name="localPositionY"> ���[�J�� Y ���W </param>
        public static void SetLocalPositionY(this Component self, float localPositionY)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, localPositionY, self.transform.localPosition.z);
        }

        /// <summary>
        /// ���[�J�� Z ���W�ݒ�
        /// </summary>
        /// <param name="localPositionZ"> ���[�J�� Z ���W </param>
        public static void SetLocalPositionZ(this Component self, float localPositionZ)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, self.transform.localPosition.y, localPositionZ);
        }

        /// <summary>
        /// ���[�J�����W���Z
        /// </summary>
        /// <param name="localPosition"> ���[�J�����W </param>
        public static void AddLocalPosition(this Component self, Vector3 localPosition)
        {
            self.transform.localPosition += localPosition;
        }

        /// <summary>
        /// ���[�J�� X ���W���Z
        /// </summary>
        /// <param name="localPositionX"> ���[�J�� X ���W </param>
        public static void AddLocalPositionX(this Component self, float localPositionX)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x + localPositionX, self.transform.localPosition.y, self.transform.localPosition.z);
        }

        /// <summary>
        /// ���[�J�� Y ���W���Z
        /// </summary>
        /// <param name="localPositionY"> ���[�J�� Y ���W </param>
        public static void AddLocalPositionY(this Component self, float localPositionY)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, self.transform.localPosition.y + localPositionY, self.transform.localPosition.z);
        }

        /// <summary>
        /// ���[�J�� Z ���W���Z
        /// </summary>
        /// <param name="localPositionZ"> ���[�J�� Z ���W </param>
        public static void AddLocalPositionZ(this Component self, float localPositionZ)
        {
            self.transform.localPosition = new Vector3(self.transform.localPosition.x, self.transform.localPosition.y, self.transform.localPosition.z + localPositionZ);
        }

        /// <summary>
        /// ���[�J�����W���Z�b�g
        /// </summary>
        public static void ResetLocalPosition(this Component self)
        {
            self.transform.localPosition = Vector3.zero;
        }

        #endregion

        #region position

        /// <summary>
        /// ���[���h���W�擾
        /// </summary>
        public static Vector3 GetWorldPosition(this Component self)
        {
            return self.transform.position;
        }

        /// <summary>
        /// ���[���h X ���W�擾
        /// </summary>
        public static float GetWorldPositionX(this Component self)
        {
            return self.transform.position.x;
        }

        /// <summary>
        /// ���[���h Y ���W�擾
        /// </summary>
        public static float GetWorldPositionY(this Component self)
        {
            return self.transform.position.y;
        }

        /// <summary>
        /// ���[���h Z ���W�擾
        /// </summary>
        public static float GetWorldPositionZ(this Component self)
        {
            return self.transform.position.z;
        }

        /// <summary>
        /// ���[���h���W�ݒ�
        /// </summary>
        /// <param name="position"> ���[���h���W </param>
        public static void SetWorldPosition(this Component self, Vector3 position)
        {
            self.transform.position = position;
        }

        /// <summary>
        /// ���[���h X ���W�ݒ�
        /// </summary>
        /// <param name="positionX"> ���[���h X ���W </param>
        public static void SetWorldPositionX(this Component self, float positionX)
        {
            self.transform.position = new Vector3(positionX, self.transform.position.y, self.transform.position.z);
        }

        /// <summary>
        /// ���[���h Y ���W�ݒ�
        /// </summary>
        /// <param name="positionY"> ���[���h Y ���W </param>
        public static void SetWorldPositionY(this Component self, float positionY)
        {
            self.transform.position = new Vector3(self.transform.position.x, positionY, self.transform.position.z);
        }

        /// <summary>
        /// ���[���h Z ���W�ݒ�
        /// </summary>
        /// <param name="positionZ"> ���[���h Z ���W </param>
        public static void SetWorldPositionZ(this Component self, float positionZ)
        {
            self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, positionZ);
        }

        /// <summary>
        /// ���[���h���W���Z
        /// </summary>
        /// <param name="position"> ���[���h���W </param>
        public static void AddWorldPosition(this Component self, Vector3 position)
        {
            self.transform.position += position;
        }

        /// <summary>
        /// ���[���h X ���W���Z
        /// </summary>
        /// <param name="positionX"> ���[���h X ���W </param>
        public static void AddWorldPositionX(this Component self, float positionX)
        {
            self.transform.position = new Vector3(self.transform.position.x + positionX, self.transform.position.y, self.transform.position.z);
        }

        /// <summary>
        /// ���[���h Y ���W���Z
        /// </summary>
        /// <param name="positionY"> ���[���h Y ���W </param>
        public static void AddWorldPositionY(this Component self, float positionY)
        {
            self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y + positionY, self.transform.position.z);
        }

        /// <summary>
        /// ���[���h Z ���W���Z
        /// </summary>
        /// <param name="positionZ"> ���[���h Z ���W </param>
        public static void AddWorldPositionZ(this Component self, float positionZ)
        {
            self.transform.position = new Vector3(self.transform.position.x, self.transform.position.y, self.transform.position.z + positionZ);
        }

        /// <summary>
        /// ���[���h���W���Z�b�g
        /// </summary>
        public static void ResetWorldPosition(this Component self)
        {
            self.transform.position = Vector3.zero;
        }

        #endregion

        #region localScale

        /// <summary>
        /// �X�P�[���擾
        /// </summary>
        public static Vector3 GetLocalScale(this Component self)
        {
            return self.transform.localScale;
        }

        /// <summary>
        /// X �X�P�[���擾
        /// </summary>
        public static float GetLocalScaleX(this Component self)
        {
            return self.transform.localScale.x;
        }

        /// <summary>
        /// Y �X�P�[���擾
        /// </summary>
        public static float GetLocalScaleY(this Component self)
        {
            return self.transform.localScale.y;
        }

        /// <summary>
        /// Z �X�P�[���擾
        /// </summary>
        public static float GetLocalScaleZ(this Component self)
        {
            return self.transform.localScale.z;
        }

        /// <summary>
        /// �X�P�[���ݒ�
        /// </summary>
        /// <param name="localScale"> �X�P�[�� </param>
        public static void SetLocalScale(this Component self, Vector3 localScale)
        {
            self.transform.localScale = localScale;
        }

        /// <summary>
        /// X �X�P�[���ݒ�
        /// </summary>
        /// <param name="localScaleX"> ���[�J�� X �X�P�[�� </param>
        public static void SetLocalScaleX(this Component self, float localScaleX)
        {
            self.transform.localScale = new Vector3(localScaleX, self.transform.localScale.y, self.transform.localScale.z);
        }

        /// <summary>
        /// Y �X�P�[���ݒ�
        /// </summary>
        /// <param name="localScaleY"> ���[�J�� Y �X�P�[�� </param>
        public static void SetLocalScaleY(this Component self, float localScaleY)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, localScaleY, self.transform.localScale.z);
        }

        /// <summary>
        /// Z �X�P�[���ݒ�
        /// </summary>
        /// <param name="localScaleZ"> ���[�J�� Z �X�P�[�� </param>
        public static void SetLocalScaleZ(this Component self, float localScaleZ)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y, localScaleZ);
        }

        /// <summary>
        /// ���[�J���X�P�[�����Z
        /// </summary>
        /// <param name="localScale"> ���[�J���X�P�[�� </param>
        public static void AddLocalScale(this Component self, Vector3 localScale)
        {
            self.transform.localScale += localScale;
        }

        /// <summary>
        /// ���[�J�� X �X�P�[�����Z
        /// </summary>
        /// <param name="localScaleX"> ���[�J�� X �X�P�[�� </param>
        public static void AddLocalScaleX(this Component self, float localScaleX)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x + localScaleX, self.transform.localScale.y, self.transform.localScale.z);
        }

        /// <summary>
        /// ���[�J�� Y �X�P�[�����Z
        /// </summary>
        /// <param name="localScaleY"> ���[�J�� Y �X�P�[�� </param>
        public static void AddLocalScaleY(this Component self, float localScaleY)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y + localScaleY, self.transform.localScale.z);
        }

        /// <summary>
        /// ���[�J�� Z �X�P�[�����Z
        /// </summary>
        /// <param name="localScaleZ"> ���[�J�� Z �X�P�[�� </param>
        public static void AddLocalScaleZ(this Component self, float localScaleZ)
        {
            self.transform.localScale = new Vector3(self.transform.localScale.x, self.transform.localScale.y, self.transform.localScale.z + localScaleZ);
        }

        /// <summary>
        /// �X�P�[�����Z�b�g
        /// </summary>
        public static void ResetLocalScale(this Component self)
        {
            self.transform.localScale = Vector3.one;
        }

        #endregion

        #region eulerAngles

        /// <summary>
        /// �I�C���[�p�擾
        /// </summary>
        public static Vector3 GetEulerAngles(this Component self)
        {
            return self.transform.eulerAngles;
        }

        /// <summary>
        /// �I�C���[ X �p�擾
        /// </summary>
        public static float GetEulerAnglesX(this Component self)
        {
            return self.transform.eulerAngles.x;
        }

        /// <summary>
        /// �I�C���[ Y �p�擾
        /// </summary>
        public static float GetEulerAnglesY(this Component self)
        {
            return self.transform.eulerAngles.y;
        }

        /// <summary>
        /// �I�C���[ Z �p�擾
        /// </summary>
        public static float GetEulerAnglesZ(this Component self)
        {
            return self.transform.eulerAngles.z;
        }

        /// <summary>
        /// �I�C���[�p�ݒ�
        /// </summary>
        /// <param name="eulerAngles"> �I�C���[�p </param>
        public static void SetEulerAngles(this Component self, Vector3 eulerAngles)
        {
            self.transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// �I�C���[ X �p�ݒ�
        /// </summary>
        /// <param name="eulerAnglesX"> �I�C���[ X �p </param>
        public static void SetEulerAnglesX(this Component self, float eulerAnglesX)
        {
            self.transform.eulerAngles = new Vector3(eulerAnglesX, self.transform.eulerAngles.y, self.transform.eulerAngles.z);
        }

        /// <summary>
        /// �I�C���[ Y �p�ݒ�
        /// </summary>
        /// <param name="eulerAnglesY"> �I�C���[ Y �p </param>
        public static void SetEulerAnglesY(this Component self, float eulerAnglesY)
        {
            self.transform.eulerAngles = new Vector3(self.transform.eulerAngles.x, eulerAnglesY, self.transform.eulerAngles.z);
        }

        /// <summary>
        /// �I�C���[ Z �p�ݒ�
        /// </summary>
        /// <param name="eulerAnglesZ"> �I�C���[ Z �p </param>
        public static void SetEulerAnglesZ(this Component self, float eulerAnglesZ)
        {
            self.transform.eulerAngles = new Vector3(self.transform.eulerAngles.x, self.transform.eulerAngles.y, eulerAnglesZ);
        }

        #endregion

        #region transform

        /// <summary>
        /// �e�ݒ�
        /// </summary>
        /// <param name="parent"> �e </param>
        public static void SetParent(this Component self, Transform parent)
        {
            self.transform.SetParent(parent);
        }

        /// <summary>
        /// ���[�g�擾
        /// </summary>
        public static Transform GetRoot(this Component self)
        {
            return self.transform.root;
        }

        #endregion
    }
}
