using Gorgosaurus.BO.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Entities
{
    public class ForumUser : BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [NotColumn]
        public bool IsUserAdmin
        {
            get { return IsAdmin == 1; }
            set { IsAdmin = (value) ? 1 : 0; }
        }

        public int IsAdmin { get; private set; }
    }
}
