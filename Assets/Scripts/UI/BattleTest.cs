using UnityEngine;
using System.Collections;
using TocClient;
using UnityEngine.UI;
/// <summary>
/// 商城
/// </summary>
public class BattleTest : BasePanel {

    private CanvasGroup canvasGroup;
    private Button beginTurn;
    private Button endTurn;
    private Button dropFirstCard;
    private Button getOneCard;
    private Slider healthBar;
    private Text healthText;
    private void Start()
    {
        //MsgSystem.Instance.AddListener<int,int>(Constants.Msg_PlayerHealthChange, PlayerHealthChange);
        //if (canvasGroup == null)
        //{
        //    canvasGroup = GetComponent<CanvasGroup>();
        //}
        //fightManager = UnityHelper.GetComponent<FightManager>(this.gameObject, "FightManager");
        //beginTurn = UnityHelper.GetComponent<Button>(this.gameObject, "BeginTurn");
        //endTurn = UnityHelper.GetComponent<Button>(this.gameObject, "EndTurn");
        //dropFirstCard = UnityHelper.GetComponent<Button>(this.gameObject, "DropFirstCard");
        //getOneCard = UnityHelper.GetComponent<Button>(this.gameObject, "GetOneCard");
        //playerRayCaster = UnityHelper.GetComponent<SpriteRaycasterFollow>(this.gameObject, "player");
        //healthBar = UnityHelper.GetComponent<Slider>(this.gameObject, "HealthBar");
        //healthText = UnityHelper.GetComponent<Text>(this.gameObject, "HealthBar/HealthNum");

        RegisterAllLisenter();
    }
    public void SetPlayer(Transform player)
    {
    }
    private void PlayerHealthChange(int currencyHp,int maxHp)
    {
        healthBar.value = (float)currencyHp / maxHp;
        healthText.text = currencyHp + "/" + maxHp;
    }
    private void RegisterAllLisenter()
    {
    }
    public override void OnEnter()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        //透明度与能否点击
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    /// <summary>
    /// 处理页面关闭
    /// </summary>
    public override void OnExit()
    {
        
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    /// <summary>
    /// 点叉
    /// </summary>
    public void OnClosePanel()
    {
        //UIManager.Instance.PopPanel();
    }
}
