using System;
using System.Collections.Generic;
using System.Web.Http;
using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Core.DomainDto;
using AngularMvcPhoneDb.Core.Repositories;
using AngularMvcPhoneDb.Mvc.Models;

namespace AngularMvcPhoneDb.Mvc.Controllers
{
    public class DataController : ApiController
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ISmartPhoneBuilder _smartPhoneBuilder;
        private readonly IPhoneRepository _phoneRepository;

        public DataController(IManufacturerRepository manufacturerRepository, ISmartPhoneBuilder smartPhoneBuilder, IPhoneRepository phoneRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _smartPhoneBuilder = smartPhoneBuilder;
            _phoneRepository = phoneRepository;
        }

        public DataController() : this(new ManufacturerRepository(), new SmartPhoneBuilder(), new PhoneRepository())
        {
        }

        // GET api/data
        public IEnumerable<PhoneManufacturerDto> Get()
        {
            return _manufacturerRepository.LoadAllWithPhones();
        }

        public dynamic Get(Guid id)
        {
            SmartPhone phone = _phoneRepository.LoadById(id);
            
            return new
            {
                phone.Model, 
                phone.BatteryCapacity,
                phone.Cpu, 
                phone.Gpu, 
                phone.PixelWidth,
                phone.PixelHeight
            };
        }

        // POST api/data
        public Guid Post([FromBody]AddPhoneModel model)
        {
            bool anyManufacturers = _manufacturerRepository.Any(model.manu);
            Manufacturer manufacturer = !anyManufacturers? 
                                        new Manufacturer {Name = model.manu} : 
                                        _manufacturerRepository.LoadById(model.manu);

            if (!anyManufacturers)
                _manufacturerRepository.Save(manufacturer);

            var phone = _phoneRepository.Create();
            _smartPhoneBuilder.BuildFromModel(phone, model, manufacturer);
            return _phoneRepository.Add(phone);
        }
    }
}
