using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.DataAccessLayer.DataAccess;
using GestionScolarite.ModelLayer;
using Microsoft.Data.SqlClient;


namespace GestionScolarite.DataAccessLayer.DAO.Implémentations
{
    internal class InscriptionDAO : IInscriptionDAO
    {
        private readonly IDao<Etudiant> etudiantDAO;
        private readonly IDao<Cours> coursDAO;

        public InscriptionDAO(IDao<Etudiant> etudiantDAO, IDao<Cours> coursDAO)
        {
            this.etudiantDAO = etudiantDAO;
            this.coursDAO = coursDAO;
        }

        public void Ajouter(Inscription inscription)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = @"INSERT INTO Inscriptions (EtudiantId, CoursId, Session, Note) 
                              VALUES (@EtudiantId, @CoursId, @Session, @Note)";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@EtudiantId", inscription.Etudiant.Id);
                    cmd.Parameters.AddWithValue("@CoursId", inscription.Cours.Id);
                    cmd.Parameters.AddWithValue("@Session", inscription.Session);
                    cmd.Parameters.AddWithValue("@Note", inscription.Note.HasValue ? inscription.Note.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Supprimer(Inscription inscription)
        {
            Supprimer(inscription.Etudiant.Id, inscription.Cours.Id, inscription.Session);
        }

        public List<Inscription> GetInscriptionsParEtudiant(int etudiantId)
        {
            var liste = new List<Inscription>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = @"SELECT i.EtudiantId, i.CoursId, i.Session, i.Note,
                                    e.Nom, e.Prenom, c.Titre, c.Code
                             FROM Inscriptions i
                             INNER JOIN Etudiants e ON i.EtudiantId = e.Id
                             INNER JOIN Cours c ON i.CoursId = c.Id
                             WHERE i.EtudiantId = @EtudiantId";
            
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@EtudiantId", etudiantId);
                
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var etudiant = new Etudiant(
                                (int)reader["EtudiantId"],
                                (string)reader["Nom"],
                                (string)reader["Prenom"]
                            );
                        
                            var cours = new Cours(
                                (int)reader["CoursId"],
                                (string)reader["Titre"],
                                (string)reader["Code"]
                            );
                        
                            int? note = reader["Note"] != DBNull.Value ? (int?)reader["Note"] : null;
                        
                            liste.Add(new Inscription(etudiant, cours, (string)reader["Session"], note));
                        }
                    }
                }
            }
            return liste;
        }

        public List<Inscription> GetInscriptionsParCours(int coursId)
        {
            var liste = new List<Inscription>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = @"SELECT i.EtudiantId, i.CoursId, i.Session, i.Note,
                                    e.Nom, e.Prenom, c.Titre, c.Code
                             FROM Inscriptions i
                             INNER JOIN Etudiants e ON i.EtudiantId = e.Id
                             INNER JOIN Cours c ON i.CoursId = c.Id
                             WHERE i.CoursId = @CoursId";
            
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@CoursId", coursId);
                
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var etudiant = new Etudiant(
                                (int)reader["EtudiantId"],
                                (string)reader["Nom"],
                                (string)reader["Prenom"]
                            );
                        
                            var cours = new Cours(
                                (int)reader["CoursId"],
                                (string)reader["Titre"],
                                (string)reader["Code"]
                            );
                        
                            int? note = reader["Note"] != DBNull.Value ? (int?)reader["Note"] : null;
                        
                            liste.Add(new Inscription(etudiant, cours, (string)reader["Session"], note));
                        }
                    }
                }
            }
            return liste;
        }

        public void Supprimer(int etudiantId, int coursId, string session)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = @"DELETE FROM Inscriptions 
                                 WHERE EtudiantId = @EtudiantId 
                                   AND CoursId = @CoursId 
                                   AND Session = @Session";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@EtudiantId", etudiantId);
                    cmd.Parameters.AddWithValue("@CoursId", coursId);
                    cmd.Parameters.AddWithValue("@Session", session);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}



