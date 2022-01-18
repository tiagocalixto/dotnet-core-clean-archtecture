using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLittlePetShop.DataProvider.models
{
    [Table("pet")]
    public class PetDb : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int PetId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("breed")]
        public string Breed { get; set; }

        [Column("chip_id")]
        public string ChipId { get; set; }

        [Column("owner_id")]
        public virtual int? OwnerId { get; set; }
        public virtual OwnerDb Owner { get; set; }

        public bool IsDeleted { get; set; }
    }
}
