using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    public class GameManager
    {
        static GameManager _manger = null;

        #region Variables
        private BossScript m_currentBoss;
        public BossScript _currentBoss
        {
            get { return m_currentBoss; }
            set { m_currentBoss = value; }
        }
        private BossManager m_bossManager;
        public BossManager _bossManager
        {
            get { return m_bossManager; }
            set { m_bossManager = value; }
        }

        private PlayerController m_player;
        public PlayerController _player
        {
            get { return m_player; }
            set { m_player = value; }
        }

        private UIManager m_UIManager;
        public UIManager _UIManager
        {
            get { return m_UIManager; }
            set { m_UIManager = value; }
        }

        private LevelManager m_LevelManager;
        public LevelManager _LevelManager
        {
            get { return m_LevelManager; }
            set { m_LevelManager = value; }
        }

        #endregion

        public static GameManager GetManager()
        {
            if (_manger == null)
            {
                _manger = new GameManager(); // patern singleton
            }
            return _manger;
        }

    }
}
