using GestionScolarite.ModelLayer;

namespace GestionScolarite.DataAccessLayer.DAO.Interfaces
{
    public interface IProfesseurDAO : IDao<Professeur>
    {
        Professeur? GetByNom(string nom);
        List<Professeur> GetByDepartement(string departement);
    }
}
