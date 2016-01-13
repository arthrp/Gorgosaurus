using Gorgosaurus.DA.Managers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.IntegrationTests
{
    [TestFixture]
    public class GlobalSettingsTests
    {
        [SetUp]
        public void Setup()
        {
            DbConnector.Delete();
            DbConnector.Init();
        }

        [Test]
        public void CanLoadAndSaveGlobalSetting()
        {
            const string newForumName = "Updated forum name";
            var currentForumName = GlobalSettingsManager.Instance.Load(GlobalSettingsEnum.ForumName);

            Assert.IsNotNullOrEmpty(currentForumName);

            GlobalSettingsManager.Instance.Save(GlobalSettingsEnum.ForumName, newForumName);
            var updatedName = GlobalSettingsManager.Instance.Load(GlobalSettingsEnum.ForumName);
            Assert.IsNotNullOrEmpty(updatedName);
            Assert.True(updatedName.Equals(newForumName));
        }

        [Test]
        public void CanGetDefaultValues()
        {
            var defaultValues = GlobalSettingsManager.Instance.GetDefaultSettings();

            Assert.True(defaultValues.Count > 0);
        }
    }
}
