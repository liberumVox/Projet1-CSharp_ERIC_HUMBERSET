using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ModelLayer
{
    internal class Etudiant
    {
        private int id;  // correspond au champs Id Identity dans la BD
        private string nom;
        private string prenom;
        private List<Cours> coursSuivis;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Nom
        {
            get => nom;
            set => nom = value;
        }

        public string Prenom
        {
            get => prenom;
            set => prenom = value;
        }

        public List<Cours> CoursSuivis
        {
            get => coursSuivis;
            set => coursSuivis = value;
        }

        // Constructeurs
        //Pour créer un étudiant à ajouter à la BD.le champs id 
        public Etudiant(string nom, string prenom)
        {
            //le champs id est crée automatiquement lors de l'insertion dans la BD.
            this.nom = nom;
            this.prenom = prenom;
            CoursSuivis = new List<Cours>();
        }

        //pour créer un étudiant récupéré de la BD
        public Etudiant(int id, string nom, string prenom)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            CoursSuivis = new List<Cours>();
        }
    }
}

