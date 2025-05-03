namespace RestoManager_X.Models.RestosModel
{
    public class Restaurant
    {
        public int CodeResto { get; set; }
        public string NomResto { get; set; } = null!;
        public string Specialite { get; set; } = "tunnisienne"!;
        public string Ville { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public int Numprop { get; set; }

        public virtual Proprietaire? Leroprietaire { get; set; }

        public virtual ICollection<Avis>? LesAvis { get; set; }
    }
}
