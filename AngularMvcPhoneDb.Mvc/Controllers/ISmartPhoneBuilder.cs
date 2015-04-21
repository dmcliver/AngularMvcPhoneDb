using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Mvc.Models;

namespace AngularMvcPhoneDb.Mvc.Controllers
{
    public interface ISmartPhoneBuilder
    {
        SmartPhone BuildFromModel(SmartPhone create, AddPhoneModel model, Manufacturer manufacturer);
    }
}