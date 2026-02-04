using GestionScolarite.ModelLayer;


namespace GestionScolarite.DataAccessLayer.DAO.Interfaces
{
    internal interface ICoursDAO : IDao<Cours>
    {
        // Ajouter ici des méthodes spécifiques si nécessaire
        Cours? GetByCode(string code); // Exemple
    }
}

