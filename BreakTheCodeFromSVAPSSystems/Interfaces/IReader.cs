namespace BreakTheCodeFromSVAPSSystems.Interfaces
{
    public interface IReader<T>
    {
        T Read(string filename);
    }
}
