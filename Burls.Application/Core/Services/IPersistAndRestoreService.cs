namespace Burls.Application.Core.Services
{
    public interface IPersistAndRestoreService
    {
        void InitDataDirectory();

        void RestoreData();

        void PersistData();
    }
}
