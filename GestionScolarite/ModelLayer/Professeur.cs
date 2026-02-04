namespace GestionScolarite.ModelLayer
{
    // Classe représentant un professeur (style cours)
    public class Professeur
    {
        // Attributs privés
        private int id;
        private string nom;
        private string prenom;
        private string departement;

        // Propriétés publiques (style vu en classe)
        public int Id 
        { 
            get { return id; } 
            set { id = value; } 
        }
        
        public string Nom 
        { 
            get { return nom; } 
            set { nom = value; } 
        }
        
        public string Prenom 
        { 
            get { return prenom; } 
            set { prenom = value; } 
        }
        
        public string Departement 
        { 
            get { return departement; } 
            set { departement = value; } 
        }

        // Constructeur avec tous les paramètres
        public Professeur(int id, string nom, string prenom, string departement)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.departement = departement;
        }

        // Constructeur sans id (pour ajouter un nouveau prof dans la BD)
        public Professeur(string nom, string prenom, string departement)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.departement = departement;
        }

        // ToString pour afficher les infos
        public override string ToString()
        {
            return $"{prenom} {nom} - {departement}";
        }
    }
}
