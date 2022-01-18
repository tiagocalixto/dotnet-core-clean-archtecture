using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLittlePetShop.DataProvider.models
{
    [Table("contact")]
    public class ContactDb : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ContactId { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("owner_id")]
        public virtual int? OwnerId { get; set; }

        public virtual OwnerDb Owner { get; set; }

        [Column("deleted")]
        public bool IsDeleted { get; set; }
    }
}
