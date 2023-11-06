using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoConference.Controllers;
using VideoConference.Data;
using VideoConference.Entities;

namespace VideoConferenceTest
{
    public class DeviceTest
    {

        public static DbContextOptions<DeviceDbContext> options = new DbContextOptionsBuilder<DeviceDbContext>().UseSqlServer("Server=DESKTOP-QNIOH67;Database=Huddly;Trusted_Connection=True;TrustServerCertificate=true").Options;
        public static DeviceDbContext deviceDbContext = new DeviceDbContext(options);
        public DeviceController testDeviceController = new DeviceController(deviceDbContext);

        [SetUp]
        public void Setup()
        {
            // Test data

        }

        [TearDown]
        public void Cleanup()
        {
            
        }

        [Test]
        public async Task DeviceIdIsValid()
        {
            // Arrange
            string id = "aa0005";
            string model = "IQ";
            string room = "TestRoom";
            string organization = "TestOrg";

            // Act
            ActionResult<Device> deviceToAddResult = await testDeviceController.AddDevice(new Device
            {
                Id = id,
                Model = model,
                Room = room,
                Organization = organization
            });
            Device newDevice = deviceDbContext.Devices.Find(id);

            // Assert
            Assert.IsTrue(newDevice.Model == model && newDevice.Id.Length == 6 && newDevice.Room == room && newDevice.Organization == organization);
        }

    }
}
