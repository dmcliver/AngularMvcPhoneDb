using System;
using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Mvc.Models;

namespace AngularMvcPhoneDb.Mvc.Controllers
{
    public class SmartPhoneBuilder : ISmartPhoneBuilder
    {
        public SmartPhone BuildFromModel(SmartPhone phone, AddPhoneModel model, Manufacturer manufacturer)
        {
            phone.SmartPhoneId = Guid.NewGuid();
            phone.BatteryCapacity = model.battCap;
            phone.Cpu = model.cpu;
            phone.DisplayType = DisplayType.IPS;
            phone.Gpu = model.gpu;
            phone.Manufacturer = manufacturer;
            phone.Model = model.model;
            phone.ModelYear = DateTime.Now;
            phone.PixelHeight = model.pixelHeight;
            phone.PixelWidth = model.pixelWidth;
            
            return phone;
        }
    }
}