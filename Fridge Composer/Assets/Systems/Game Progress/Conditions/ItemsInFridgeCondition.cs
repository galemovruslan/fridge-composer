using UnityEngine;

[CreateAssetMenu(fileName ="New Items in FridgeCondition", menuName ="Conditions/Items in Fridge")]
public class ItemsInFridgeCondition : Condition
{
    [SerializeField] private ItemGameEvent OnCreateItem;
    [SerializeField] private ItemGameEvent OnPlaceItem;
    [SerializeField] private ItemGameEvent OnRemoveItem;

    private FillProgress _progress;

    private void OnEnable()
    {
        _progress = new FillProgress();

        OnCreateItem.AddListener(_progress.HandleItemSpawn);
        OnPlaceItem.AddListener(_progress.HandlePlaceItem);
        OnRemoveItem.AddListener(_progress.HandleRemoveItem);
        _progress.AddListener(SatisfyCondition);
    }

    private void OnDisable()
    {
        OnCreateItem.RemoveListener(_progress.HandleItemSpawn);
        OnPlaceItem.RemoveListener(_progress.HandlePlaceItem);
        OnRemoveItem.RemoveListener(_progress.HandleRemoveItem);
        _progress.RemoveListener(SatisfyCondition);
    }

    public override void StartChecking(){}

    public override void PauseChecking()
    {
        _progress.TogglePause();
    }

    public override void Tick(float deltaTime){}

    private void SatisfyCondition()
    {
        ChangeStatus(this, ConditionStatus.satisfy);
    }

    
}
