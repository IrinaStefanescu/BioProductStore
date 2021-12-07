using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Models.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; } //Specific .net String
        DateTime? DateCreated { get; set; } //we use ? if we also want to be null
        DateTime? DateModified { get; set; }
    }
}
