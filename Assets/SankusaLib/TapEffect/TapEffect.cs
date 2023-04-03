using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace SankusaLib {
    public class TapEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem tapParticle;
        [SerializeField] private ParticleSystem dragParticle;
        [SerializeField] private float tapParticleLifeTime;
        [SerializeField] private float distanceFromCamera = 10f;
        private Vector2 inputPosition = Vector2.zero;
        private float tapParticleTimer = 0f;

        void Update() {
            //Vector2 screenPosition = Mouse.current.position.ReadValue();

            // タップでタップパーティクルタイマーに時間を充填
            foreach(var touch in Touchscreen.current.touches) {
                if(touch.press.wasPressedThisFrame) {
                    tapParticleTimer = tapParticleLifeTime;
                    break;
                }
            }

            // タップパーティクルタイマーによってタップパーティクルの再生/停止を管理
            if(tapParticle != null) {
                if(tapParticleTimer > 0 && !tapParticle.isPlaying) {
                    tapParticle.Play();
                }
                if(tapParticleTimer <= 0 && tapParticle.isPlaying) {
                    tapParticle.Stop();
                }
            }

            // ドラッグパーティクルの再生/停止を管理
            if(dragParticle != null) {
                if(Touchscreen.current.wasUpdatedThisFrame && !dragParticle.isPlaying) {
                    dragParticle.Play();
                }
                if(!Touchscreen.current.wasUpdatedThisFrame && dragParticle.isPlaying) {
                    dragParticle.Stop();
                }
            }

            // カメラ追従
            if(Touchscreen.current != null) {
                inputPosition = Touchscreen.current.position.ReadValue();
            }
            // 3D
            if(Camera.main.orthographic == false) {
                Vector3 inputPosition3D = new Vector3(inputPosition.x, inputPosition.y, distanceFromCamera);
                transform.position = Camera.main.ScreenToWorldPoint(inputPosition3D);
            // 2D
            } else {
                transform.position = Camera.main.ScreenToWorldPoint(inputPosition) + Camera.main.transform.forward * distanceFromCamera;
            }
            transform.rotation = Camera.main.transform.rotation;

            tapParticleTimer = Mathf.Max(tapParticleTimer - Time.deltaTime, 0);
        }
    }
}