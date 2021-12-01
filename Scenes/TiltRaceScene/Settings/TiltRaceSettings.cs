using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceSettings))]
    public sealed class TiltRaceSettings : ScriptableObject
    {
        //====================================
        //! �ϐ��iprivate static�j
        //====================================

        /// <summary>
        /// �C���X�^���X
        /// </summary>
        private static TiltRaceSettings msInstance;


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �v���C���[�֘A
        /// </summary>
        [SerializeField] private TiltRacePlayerSettings mPlayer;

        /// <summary>
        /// �G�֘A
        /// </summary>
        [SerializeField] private TiltRaceEnemySettings mEnemy;

        /// <summary>
        /// �A�C�e���֘A
        /// </summary>
        [SerializeField] private TiltRaceItemSettings mItem;

        /// <summary>
        /// ���s�������Ƃ̃C�x���g�֘A
        /// </summary>
        [SerializeField] private TiltRaceDistanceEventSettings mDistanceEvent;

        /// <summary>
        /// �c�̈ړ��̈���
        /// </summary>
        [SerializeField] private float mHeightLimit;

        /// <summary>
        /// ���̈ړ��̈���
        /// </summary>
        [SerializeField] private float mWidthLimit;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �v���C���[�֘A
        /// </summary>
        public static TiltRacePlayerSettings Player => msInstance.mPlayer;

        /// <summary>
        /// �G�֘A
        /// </summary>
        public static TiltRaceEnemySettings Enemy => msInstance.mEnemy;

        /// <summary>
        /// �A�C�e���֘A
        /// </summary>
        public static TiltRaceItemSettings Item => msInstance.mItem;

        /// <summary>
        /// ���s�������Ƃ̃C�x���g�֘A
        /// </summary>
        public static TiltRaceDistanceEventSettings DistanceEvent => msInstance.mDistanceEvent;

        /// <summary>
        /// �c�̈ړ��̈���
        /// </summary>
        public static float HeightLimit => msInstance.mHeightLimit;

        /// <summary>
        /// ���̈ړ��̈���
        /// </summary>
        public static float WidthLimit => msInstance.mWidthLimit;


        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// �ǂݍ���
        /// </summary>
        public static void Load()
        {
            msInstance = Resources.Load<TiltRaceSettings>(Path.Scenes.TiltRaceScene.Settings);
        }

        /// <summary>
        /// �j��
        /// </summary>
        public static void Dispose()
        {
            msInstance = null;
        }
    }
}
