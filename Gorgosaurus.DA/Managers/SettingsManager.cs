using Dapper;
using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Managers
{
    public class SettingsManager
    {
        public static readonly SettingsManager Instance = new SettingsManager();

        public Dictionary<string, string> GetDefaultSettings()
        {
            var res = new Dictionary<string, string>()
            {
                { Enum.GetName(typeof(GlobalSettingsEnum), GlobalSettingsEnum.ForumName), "First forum" }
            };

            return res;
        }

        public void Save(GlobalSettingsEnum setting, string value)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                conn.Execute(String.Format("Update {0} set {1} = :value", typeof(GlobalSetting).Name, Enum.GetName(typeof(GlobalSettingsEnum), setting)), new { value });
            }
        }

        public string Load(GlobalSettingsEnum setting)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var res = conn.ExecuteScalar<string>(String.Format("select Value from {0} where Name = :name", typeof(GlobalSetting).Name),
                    new { name = Enum.GetName(typeof(GlobalSettingsEnum), setting) });

                return res;
            }
        }
    }
}
