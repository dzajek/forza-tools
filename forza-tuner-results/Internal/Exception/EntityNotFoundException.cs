namespace ForzaTunerResults.Internal.Exception
{
    public class EntityNotFoundException(string entityName, long entityId) : System.Exception($"Entity '{entityName}' with ID '{entityId}' was not found.")
    {
    }
}
