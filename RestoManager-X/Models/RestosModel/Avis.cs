using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoManager_X.Models.RestosModel
{
    public class Avis
    {
        [Key]
        public int CodeAvis { get; set; }

        [Required]
        [StringLength(30)]
        public string NomPersonne { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Note { get; set; }

        [StringLength(256)]
        public string? Commentaire { get; set; }

        [ForeignKey("LeResto")]
        public int NumResto { get; set; }

        public virtual Restaurant? LeResto { get; set; }
    }
}
