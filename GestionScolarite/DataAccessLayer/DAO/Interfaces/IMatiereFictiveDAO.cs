using GestionScolarite.ModelLayer;
using System.Collections.Generic;

namespace GestionScolarite.DataAccessLayer.DAO.Interfaces
{
    // Interface pour DAO inexistant - utilisée uniquement pour tests
    public interface IMatiereFictiveDAO
    {
        List<MatiereFictive> GetAll();
        MatiereFictive GetById(int id);
    }
}
