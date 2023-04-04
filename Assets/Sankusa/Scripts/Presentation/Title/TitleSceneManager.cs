using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Runtime.UIManager.Components;
using SankusaLib;
using SankusaLib.SoundLib;
using SankusaLib.SceneManagementLib;
using Sankusa.BitTyping.Domain;
using Zenject;

namespace Sankusa.BitTyping.Presentation
{
    public class TitleSceneManager : MonoBehaviour
    {
        [SerializeField, SoundId] private string bgmId;
        [SerializeField] private UIButton startButton;
        [Inject] private SceneArgStore sceneArgStore;

        void Start()
        {
            startButton.AddListenerToPointerClick(() =>
            {
                StartTimeAttackMode();
            });

            SoundManager.Instance.CrossFadeBgm(bgmId);
        }

        public void StartTimeAttackMode()
        {
            sceneArgStore.Set(new InGameArg(60f, 1));

            LoadInGameScene();
        }

        private void LoadInGameScene()
        {
            Blackout.Instance.PlayBlackout(1, () => SceneManager.LoadScene(GameConst.SCENE_NAME_INGAME));
        }
    }
}