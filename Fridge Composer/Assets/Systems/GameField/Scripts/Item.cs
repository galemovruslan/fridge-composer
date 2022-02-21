public class Item
{
    public Attribute Attribute { get => _attribute;  }

    private Attribute _attribute;

    public override string ToString()
    {
        return _attribute.ToString();
    }
}