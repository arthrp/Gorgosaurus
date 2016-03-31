using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Interfaces
{
    public interface IHasPagedCollection
    {
        int TotalRecords { get; set; }
    }
}
