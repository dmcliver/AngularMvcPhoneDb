using AngularMvcPhoneDb.Core.Domain;

namespace AngularMvcPhoneDb.Core.Repositories
{
    public interface IPhoneRepository
    {
        void Add(SmartPhone phone);

        SmartPhone LoadById(System.Guid id);
        SmartPhone Create();
    }
}