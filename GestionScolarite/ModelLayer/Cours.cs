using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ModelLayer
{
    internal class Cours
    {
        private int id;  // correspond au champs Id Identity dans la BD
        private string titre;
        private string code;
        private List<Etudiant> etudiantsInscrits;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Titre
        {
            get => titre;
            set => titre = value;
        }

        public string Code
        {
            get => code;
            set => code = value;
        }

        public List<Etudiant> EtudiantsInscrits
        {
            get => etudiantsInscrits;
            set => etudiantsInscrits = value;
        }

        // Constructeurs
        public Cours(int id, string titre, string code)
        {
            this.id = id;
            this.titre = titre;
            this.code = code;
            this.etudiantsInscrits = new List<Etudiant>();
        }

        //pour la création d'un cours à inserer dans la BD, le champs id est créé automatiquement
        public Cours(string titre, string code)
        {
            this.titre = titre;
            this.code = code;
            etudiantsInscrits = new List<Etudiant>();
        }
    }

}
