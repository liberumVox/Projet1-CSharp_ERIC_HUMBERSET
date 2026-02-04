using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.ModelLayer;
using System.Collections.Generic;

namespace GestionScolarite.ControlLayer
{
    // Controlleur pour tester le mocking
    public class MatiereFictiveControlleur
    {
        // Le DAO qui sera mocké
        private IMatiereFictiveDAO dao;

        // Constructeur avec injection de dépendance
        public MatiereFictiveControlleur(IMatiereFictiveDAO dao)
        {
            this.dao = dao;
        }

        // Méthode pour obtenir toutes les matières
        public List<MatiereFictive> ObtenirToutesMatieres()
        {
            // Appel simple au DAO
            List<MatiereFictive> matieres = dao.GetAll();
            return matieres;
        }

        // Méthode pour obtenir une matière par id
        public MatiereFictive ObtenirMatiere(int id)
        {
            // Appel simple au DAO
            MatiereFictive matiere = dao.GetById(id);
            return matiere;
        }
    }
}
