public interface IPlace
{
    int x { get; }
    int y { get; }
    string description { get; }
    string name { get; }


    string GetDescriptorText();

    string GetName();
}