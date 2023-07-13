using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace VampireSLike
{
    /// <summary>
    /// 战斗升级界面
    /// </summary>
    public class BattleLvUpPanel : BasePanel
    {
        //CanvasGroup组件可用于统一控制该面板与其子物体能否交互。取消Blocks Raycasts时该面板和其子物体都不能再接收点击。
        private CanvasGroup _canvasGroup;
        private Button _skipLvUp;

        private Transform[] _lvUpItems = new Transform[3];
        private Text _timeText;
        private Text _coinText;
        private Text _levelText;

        private void Awake()
        {
            _skipLvUp = UnityHelper.GetComponent<Button>(gameObject, "Bg/Window/Skip");
            for (int i = 0; i < 3; i++)
                _lvUpItems[i] = UnityHelper.GetComponent<Transform>(gameObject, "Bg/Window/LvUpArea/LvUp"+i);
            _timeText = UnityHelper.GetComponent<Text>(gameObject, "Time/num");
            _coinText = UnityHelper.GetComponent<Text>(gameObject, "Coins/num");
            _levelText = UnityHelper.GetComponent<Text>(gameObject, "ExperienceBar/LevelText/num");
            _canvasGroup = GetComponent<CanvasGroup>();
            AddAllListener();
        }
        public override void OnPause()
        {
            _canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候,不再可交互
        }
        public override void OnResume()
        {
            _canvasGroup.blocksRaycasts = true;
        }
        public override void OnExit()
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        public override void OnEnter()
        {
            SetLvUpItems(ExperienceLevelController.instance.weaponsToUpgrade);
            this.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        private void SetLvUpItems(List<Weapon> weapons)
        {
            for (int i = 0; i < _lvUpItems.Length; i++)
            {
                int idx = i;
                if (idx >= weapons.Count)
                    _lvUpItems[idx].gameObject.SetActive(false);
                else
                {
                    _lvUpItems[idx].gameObject.SetActive(true);
                    Button click = _lvUpItems[i].gameObject.GetComponent<Button>();
                    Text nameLevel = UnityHelper.GetComponent<Text>(_lvUpItems[i].gameObject, "nameLevel");
                    Text introduce = UnityHelper.GetComponent<Text>(_lvUpItems[i].gameObject, "introduce");
                    Image Icon = UnityHelper.GetComponent<Image>(_lvUpItems[i].gameObject, "Image");
                    nameLevel.text = weapons[idx].WeaponName + " - Lvl " + weapons[idx].weaponLevel;
                    introduce.text = weapons[idx].stats[weapons[idx].weaponLevel].upgradeText;
                    Icon.sprite = weapons[idx].icon;
                    click.onClick.RemoveAllListeners();
                    click.onClick.AddListener(()=>
                    {
                        if (weapons[idx].IsEquiped)
                            weapons[idx].LevelUp();
                        else
                            BattleManager.Instance.PlayerCtrl.AddWeapon(weapons[idx]);
                        UIManager.Instance.PopPanel();
                    });
                }
            }
        }
        private void AddAllListener()
        {
            _skipLvUp.onClick.AddListener(()=>
            {
                Time.timeScale = 0f;
                UIController.instance.SkipLevelUp();
            });
/*            MsgSystem.Instance.AddListener(Constants.Msg_BattleCoinChange,()=>
            {
                _coinText.text =CoinController.instance.currentCoins.ToString();
            });*/
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

