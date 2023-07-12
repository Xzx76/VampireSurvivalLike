using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace VampireSLike
{
    /// <summary>
    /// 主界面
    /// </summary>
    public class BattlePanel : BasePanel
    {
        //CanvasGroup组件可用于统一控制该面板与其子物体能否交互。取消Blocks Raycasts时该面板和其子物体都不能再接收点击。
        private CanvasGroup _canvasGroup;
        private Slider _expSlider;
        private Slider _hpSlider;
        private Slider _energyCdSlider;

        private Text _timeText;
        private Text _coinText;
        private Text _levelText;

        private void Start()
        {
            _expSlider = UnityHelper.GetComponent<Slider>(gameObject, "ExperienceBar");
            _hpSlider = UnityHelper.GetComponent<Slider>(gameObject, "HealthBar");
            _energyCdSlider = UnityHelper.GetComponent<Slider>(gameObject, "EnergyBar");
            _timeText = UnityHelper.GetComponent<Text>(gameObject, "Time/num");
            _coinText = UnityHelper.GetComponent<Text>(gameObject, "Coins/num");
            _levelText = UnityHelper.GetComponent<Text>(gameObject, "ExperienceBar/LevelText/num");
            _canvasGroup = GetComponent<CanvasGroup>();
            AddMsgListener();
        }
        public override void OnPause()
        {
            _canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候,不再可交互
        }
        public override void OnResume()
        {
            this.gameObject.SetActive(true);
            _canvasGroup.blocksRaycasts = true;
        }
        public override void OnExit()
        {
            this.gameObject.SetActive(false);
        }

        private void AddMsgListener()
        {
            MsgSystem.Instance.AddListener<float, int>(Constants.Msg_BattleExpChange, (sliderValue,level) =>
              {
                  _expSlider.value = sliderValue;
                  _levelText.text = level.ToString();
              });
            MsgSystem.Instance.AddListener(Constants.Msg_BattleCoinChange,()=>
            {
                _coinText.text =CoinController.instance.currentCoins.ToString();
            });
            MsgSystem.Instance.AddListener<float>(Constants.Msg_BattleTimeChange, (time) =>
             {
                 float minutes = Mathf.FloorToInt(time / 60f);
                 float seconds = Mathf.FloorToInt(time % 60);
                 _timeText.text =minutes + ":" + seconds.ToString("00");
             });
            MsgSystem.Instance.AddListener<float>(Constants.Msg_BattleHpChange, (hpSliderValue) =>
            {
                _hpSlider.value = hpSliderValue;
            });
        }
        /*/// <summary>
        /// 将某面板显示出来
        /// </summary>
        /// <param name="panelTypeString">要显示的面板的名称</param>
        public void OnPushPanel(string panelTypeString)
        {
            //先通过字符串得到枚举类型
            UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
            //显示该类型的面板
            //UIManager.Instance.PushPanel(panelType);
        }*/

    }
}

