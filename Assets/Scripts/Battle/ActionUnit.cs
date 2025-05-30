using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum ANIMATION
{
    IDLE,
    ATTACK,
    MOVE,
    CAST_SPELL

}

public class ActionUnit : SelectableUnit
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Stats stats;
    [SerializeField] private List<SpellSO> spells;
    [SerializeField] public Animator animator;
    [SerializeField] private InventoryHolder inventoryHolder;
    private UnitType unitType = UnitType.ACTION_UNIT;
    private int tileX, tileY;

    //Animations dictionary
    private Dictionary<ANIMATION, string> animationsDictionary = new Dictionary<ANIMATION, string>
    {
        { ANIMATION.IDLE,"idle"},
        { ANIMATION.ATTACK,"attack"},
        { ANIMATION.MOVE,"move"},
        { ANIMATION.CAST_SPELL,"spell"}

    };

    private Color initialColor;

    private void Start()
    {
        initialColor = spriteRenderer.color;
    }

    public void SetTilePosition(int x, int y)
    {
        tileX = x;
        tileY = y;
    }

    private void OnMouseEnter()
    {
        EnableHighlight();
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            spriteRenderer.color = initialColor;
        }
    }

    override protected void OnMouseDown()
    {
        isSelected = true;
        gridManager.UnitSelected(this.tileX,this.tileY,this.unitType);
    }

    public override void DisableHighlight()
    {
        spriteRenderer.color = initialColor;
    }

    override public void EnableHighlight()
    {
        spriteRenderer.color = highlightColor;
    }

    override public void deselect()
    {
        isSelected = false;
        DisableHighlight();
    }

    public void Select()
    {
        isSelected = true;
        EnableHighlight();
    }

    public void Move(Vector3 destination, int x, int y)
    {
        this.transform.position = destination;
        SetTilePosition(x, y);
    }

    public int GetTileX()
    {
        return tileX;
    }
    public int GetTileY() {
        return tileY;
    }
    public void Attack(ActionUnit target)
    {
        PlayAnimation(ANIMATION.ATTACK);
        StartCoroutine(TakeDamageAfterAnimation(target));
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public void Heal(int amount)
    {
        stats.Heal(amount);
    }

    public void ResetActions()
    {
        stats.ResetActions();
    }


    public int RollInitiative()
    {
        return DiceController.RollDice(DiceType.D20);
    }

    public int getMovement()
    {
        return stats.GetCurrentMovement();
    }

    public List<ItemSO> GetInventory()
    {
        return inventoryHolder.GetItems();
    }

    public int ItemUsed(ItemSO item)
    {
        return inventoryHolder.SubstractItemCount(item);
    }

    public void SubstractDistanceFromMovement(int distance)
    {
        stats.SubstractDistance(distance);
    }

    public void ResetResources()
    {
        stats.ResetResources();
    }

    public void PlayAnimation(ANIMATION animation)
    {
        animator.Play(animationsDictionary[animation]);
    }

    IEnumerator TakeDamageAfterAnimation(ActionUnit target)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length-0.1f);
        target.TakeDamage(stats.GetAttackPower());
    }

    public Stats GetStats()
    {
        return stats;
    }

    public int GetAttackRange()
    {
        return stats.GetAttackRange();
    }

}
