public interface IPlace
{
    
    int X { get; }
    int Y { get; }
    string Description { get; }
    string Name { get; }


    string GetDescriptorText();

    string GetName();

    World.PlaceType Type { get; }
}