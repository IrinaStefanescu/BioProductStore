using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Models.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key] //for primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //to generate Id when we insert new row
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? DateCreated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] //it generates a new value when a new row is added or updated
        public DateTime? DateModified { get; set; }
    }
}
