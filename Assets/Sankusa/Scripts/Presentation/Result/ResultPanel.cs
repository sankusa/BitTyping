using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SankusaLib;
using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.Components;
using Sankusa.BitTyping.Domain;

namespace Sankusa.BitTyping.Presentation
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private UIView uiView;
        [SerializeField] private UIButton returnButton;

        void Start()
        {
            returnButton.AddListenerToPointerClick(() =>
            {
                Blackout.Instance.PlayBlackout(1, () => SceneManager.LoadScene(GameConst.SCENE_NAME_TITLE));
            });
        }

        public void Show()
        {
            uiView.Show();
        }

        public void Hide()
        {
            uiView.Hide();
        }
    }
}