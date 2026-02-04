using GestionScolarite.ControlLayer;
using GestionScolarite.DataAccessLayer.DAO.Implémentations;
using GestionScolarite.ViewLayer;
using Microsoft.Data.SqlClient;

try
{
    var etudiantDAO = new EtudiantDAO();
    var coursDAO = new CoursDAO();
    var inscriptionDAO = new InscriptionDAO(etudiantDAO, coursDAO);

    var etudiantView = new EtudiantView();
    var coursView = new CoursView();
    var inscriptionView = new InscriptionView();
    var principalView = new PrincipalView();

    var etudiantCtrl = new EtudiantControlleur(etudiantDAO, etudiantView);
    var coursCtrl = new CoursControlleur(coursDAO, coursView);
    var inscriptionCtrl = new InscriptionControlleur(inscriptionDAO, inscriptionView, etudiantDAO, coursDAO);

    var appCtrl = new ApplicationControlleur(etudiantCtrl, coursCtrl, inscriptionCtrl, principalView);

    appCtrl.Demarrer();
}
catch (SqlException sql)
{
    Console.WriteLine($"Erreur SQL : {sql.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Exception : {ex.Message}");
}
