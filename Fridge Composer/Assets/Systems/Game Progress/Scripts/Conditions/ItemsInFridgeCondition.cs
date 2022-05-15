using UnityEngine;

[CreateAssetMenu(fileName ="New Items in FridgeCondition", menuName ="Conditions/Items in Fridge")]
public class ItemsInFridgeCondition : Condition
{
    [SerializeField] private ItemGameEvent OnCreateItem;
    [SerializeField] private ItemGameEvent OnPlaceItem;
    [SerializeField] private ItemGameEvent OnRemoveItem;

    private FillProgress _progress;
    private bool _checking;

    private void OnEnable()
    {
        _progress = new FillProgress();
        _checking = false;

        OnCreateItem.AddListener(HandleItemSpawn);
        OnPlaceItem.AddListener(HandlePlaceItem);
        OnRemoveItem.AddListener(HandleRemoveItem);
        _progress.AddListener(SatisfyCondition);
    }

    private void OnDisable()
    {
        OnCreateItem.RemoveListener(HandleItemSpawn);
        OnPlaceItem.RemoveListener(HandlePlaceItem);
        OnRemoveItem.RemoveListener(HandleRemoveItem);
        _progress.RemoveListener(SatisfyCondition);
    }

    public override void StartChecking()
    {
        _checking = true;
    }

    public override void PauseChecking(bool isPaused)
    {
        _progress.TogglePause(isPaused);
    }

    public override void Tick(float deltaTime){}

    private void SatisfyCondition()
    {
        ChangeStatus(this, ConditionStatus.satisfy);
    }

    private void HandleItemSpawn(Item item)
    {
        if (_checking)
        {
            _progress.HandleItemSpawn(item);
        }
    }

    private void HandlePlaceItem(Item item)
    {
        if (_checking)
        {
            _progress.HandlePlaceItem(item);
        }
    }

    private void HandleRemoveItem(Item item)
    {
        if (_checking)
        {
            _progress.HandleRemoveItem(item);
        }
    }

}
