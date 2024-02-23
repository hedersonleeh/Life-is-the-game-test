using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AnimationCanvasButtonController : MonoBehaviour
{
    [SerializeField] private Button _btnPrefab; 
    [SerializeField] private Animator _targetAnimator;
    [SerializeField] private List<string> _stateAnimationsName;
    private void Awake()
    {
        var content = transform.Find("Content");
        foreach (var animStateName in _stateAnimationsName)
        {
        var btn =Instantiate(_btnPrefab, content);
        var display =btn.GetComponentInChildren<TextMeshProUGUI>();
            display.text = animStateName;
            btn.onClick.AddListener(() =>
            {
                _targetAnimator.Play(animStateName);
            });
        }
        var nextBtn = Instantiate(_btnPrefab, content);
        var nextBtnDisplay = nextBtn.GetComponentInChildren<TextMeshProUGUI>();
        nextBtnDisplay.text = "next scene";
        nextBtn.onClick.AddListener(() =>
        {
            _targetAnimator.GetComponentInParent<DacingCharacterController>().ActiveCamera();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1,LoadSceneMode.Single);
        });

    }

}
