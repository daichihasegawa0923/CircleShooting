using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DiamondGames.CicleShooting.UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private Image _sceneChangeBlackFadeOut;
        [SerializeField]
        private float _fadeoutSpeed = 1.0f;

        /// <summary>
        /// シーン読み込みのコルーチン処理を開始する
        /// </summary>
        /// <param name="sceneName">遷移先のシーン名</param>
        public void LoadSceneWithFadeout(string sceneName)
        {
            StartCoroutine("Fadeout", sceneName);
        }

        /// <summary>
        /// シーン遷移のコルーチン処理
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <returns></returns>
        private IEnumerator Fadeout(string sceneName)
        {
            while(_sceneChangeBlackFadeOut.color.a < 1.0f)
            {
                var color = _sceneChangeBlackFadeOut.color;
                color.a += 0.001f * this._fadeoutSpeed;
                _sceneChangeBlackFadeOut.color = color;
                yield return new WaitForSeconds(0.001f);
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}
