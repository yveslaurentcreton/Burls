namespace Burls.Persistence
{
    public interface IPersistAndRestoreService
    {
        void RestoreData();

        void PersistData();
    }
}
