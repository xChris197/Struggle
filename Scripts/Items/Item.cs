public class Item : BaseItem
{
    public override void Interact()
    {
        //Sets up the UI elements and displays the data
        CustomEvents.OnInteractWithItem?.Invoke(this);
    }    
}
