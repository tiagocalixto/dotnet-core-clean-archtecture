using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLittlePetShop.DataProvider.models
{
    [Table("owner")]
    public class OwnerDb : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int OwnerId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public virtual IList<ContactDb> Contacts { get; set; } = new List<ContactDb>();
        public virtual IList<PetDb> Pets { get; set; } = new List<PetDb>();

        public bool IsDeleted { get; set; }
    }
}
