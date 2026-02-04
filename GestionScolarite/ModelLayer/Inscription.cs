using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionScolarite.ModelLayer
{
    internal class Inscription
    {
        private Etudiant etudiant;
        private Cours cours;
        private string session;
        private int? note; //veut dire que note est un int mais peut avoir la valeur null

        public Etudiant Etudiant
        {
            get => etudiant;
            set => etudiant = value;
        }

        public Cours Cours
        {
            get => cours;
            set => cours = value;
        }

        public string Session
        {
            get => session;
            set => session = value;
        }

        public int? Note
        {
            get => note;
            set => note = value;
        }

        // Constructeur
        public Inscription(Etudiant etudiant, Cours cours, string session, int? note = null)
        {
            this.etudiant = etudiant;
            this.cours = cours;
            this.session = session;
            this.note = note;
        }
    }

}
