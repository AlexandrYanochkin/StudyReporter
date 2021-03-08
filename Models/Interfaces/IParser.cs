namespace StudyReporter.Models.Interfaces
{
    public interface IParser<out T>
    {
        T Parse(string inputData);
    }
}
