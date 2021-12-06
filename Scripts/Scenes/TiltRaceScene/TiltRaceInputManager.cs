using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - ���͊Ǘ�
    /// </summary>
    public static class TiltRaceInputManager
    {
        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ���̓x�N�g���擾
        /// </summary>
        /// <param name="speed"> ���x </param>
        public static Vector3 GetInputVec(float speed)
        {
            var retVec = Vector3.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
            if (InputManager.IsKeyPress(KeyCode.UpArrow))
            {
                retVec += Vector3.up * speed * TimeManager.DeltaTime;
            }

            if (InputManager.IsKeyPress(KeyCode.DownArrow))
            {
                retVec += Vector3.down * speed * TimeManager.DeltaTime;
            }

            if (InputManager.IsKeyPress(KeyCode.LeftArrow))
            {
                retVec += Vector3.left * speed * TimeManager.DeltaTime;
            }

            if (InputManager.IsKeyPress(KeyCode.RightArrow))
            {
                retVec += Vector3.right * speed * TimeManager.DeltaTime;
            }

            return retVec;
#else
            retVec = Input.acceleration * speed * TimeManager.DeltaTime;
            retVec.z = 0f;

            return retVec;
#endif
        }
    }
}
