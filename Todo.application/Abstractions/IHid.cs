namespace Todo.application.Abstractions
{
    public interface IHid
    {
        string Encode(int id);
        int Decode(string id);
    }
}
