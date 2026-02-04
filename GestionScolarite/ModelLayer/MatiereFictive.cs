namespace GestionScolarite.ModelLayer
{
    // Classe pour tester le mocking - n'existe PAS dans la BD
    public class MatiereFictive
    {
        // Attributs privés
        private int id;
        private string code;
        private string nom;
        private int nombreHeures;

        // Propriétés publiques (style TD8)
        public int Id 
        { 
            get { return id; } 
            set { id = value; } 
        }
        
        public string Code 
        { 
            get { return code; } 
            set { code = value; } 
        }
        
        public string Nom 
        { 
            get { return nom; } 
            set { nom = value; } 
        }
        
        public int NombreHeures 
        { 
            get { return nombreHeures; } 
            set { nombreHeures = value; } 
        }

        // Constructeur complet
        public MatiereFictive(int id, string code, string nom, int nombreHeures)
        {
            this.id = id;
            this.code = code;
            this.nom = nom;
            this.nombreHeures = nombreHeures;
        }

        // ToString simple
        public override string ToString()
        {
            return $"{code} - {nom} ({nombreHeures}h)";
        }
    }
}
