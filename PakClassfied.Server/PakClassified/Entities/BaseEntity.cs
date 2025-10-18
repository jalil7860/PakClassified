using Microsoft.EntityFrameworkCore;
using PakClassified.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakClassified.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        [AllowNull]
        public DateTime? DeletedDate { get; set; }
        [AllowNull]
        public int? DeletedBy { get; set; }
        [AllowNull]
        public int? ModifiedBy { get; set; }
        [AllowNull]
        public DateTime? ModifiedDate { get; set; }

    }
}
