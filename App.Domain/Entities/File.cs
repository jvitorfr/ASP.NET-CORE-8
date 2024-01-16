using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities
{
    public  class File : Base
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "blob")]
        public byte[] Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }

    }

}
