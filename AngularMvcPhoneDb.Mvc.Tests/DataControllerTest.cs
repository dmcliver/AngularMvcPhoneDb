using AngularMvcPhoneDb.Core.Domain;
using AngularMvcPhoneDb.Core.Repositories;
using AngularMvcPhoneDb.Mvc.Controllers;
using AngularMvcPhoneDb.Mvc.Models;
using Moq;
using NUnit.Framework;

namespace AngularMvcPhoneDb.Mvc.Tests
{
    [TestFixture]
    public class DataControllerTest
    {
        private string _manufacturer = "";

        [Test]
        public void Post_WithExistingManufacturer_DoesntCreateNewManufacturer()
        {
            const string manu = "Samsung";

            IManufacturerRepository manufacturerRepository = Mock.Of<IManufacturerRepository>();
            Mock.Get(manufacturerRepository).Setup(m => m.Any(manu)).Returns(true);

            ISmartPhoneBuilder smartPhoneBuilder = Mock.Of<ISmartPhoneBuilder>();
            IPhoneRepository phoneRepository = Mock.Of<IPhoneRepository>();


            DataController controller = new DataController(manufacturerRepository, smartPhoneBuilder, phoneRepository);
            controller.Post(new AddPhoneModel{manu = manu});

            Mock.Get(manufacturerRepository).Verify(m => m.LoadById(manu));
        }

        [Test]
        public void Post_WithNonExistingManufacturer_CreatesNewManufacturer()
        {
            const string manu = "Samsung";

            IManufacturerRepository manufacturerRepository = Mock.Of<IManufacturerRepository>();
            Mock.Get(manufacturerRepository).Setup(m => m.Any(manu)).Returns(false);

            ISmartPhoneBuilder smartPhoneBuilder = Mock.Of<ISmartPhoneBuilder>();
            SetupPhoneBuilder(smartPhoneBuilder);

            IPhoneRepository phoneRepository = Mock.Of<IPhoneRepository>();

            DataController controller = new DataController(manufacturerRepository, smartPhoneBuilder, phoneRepository);
            controller.Post(new AddPhoneModel { manu = manu });
            
            Mock.Get(manufacturerRepository).Verify(m => m.LoadById(manu), Times.Never);
            Assert.That(_manufacturer, Is.EqualTo(manu));
        }

        private void SetupPhoneBuilder(ISmartPhoneBuilder smartPhoneBuilder)
        {
            Mock.Get(smartPhoneBuilder)
                .Setup(pb => pb.BuildFromModel(It.IsAny<SmartPhone>(), It.IsAny<AddPhoneModel>(), It.IsAny<Manufacturer>()))
                .Returns(new SmartPhone())
                .Callback<AddPhoneModel, Manufacturer>((p, m) => _manufacturer = m.Name);
        }
    }
}
