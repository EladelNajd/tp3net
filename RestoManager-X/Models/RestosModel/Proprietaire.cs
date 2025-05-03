namespace RestoManager_X.Models.RestosModel
{
    public class Proprietaire
    {
        public int Numéro { get; set; }
        public string Nom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gsm { get; set; }
        public virtual ICollection<Restaurant>? LesRestos { get; set; }
    }
}
