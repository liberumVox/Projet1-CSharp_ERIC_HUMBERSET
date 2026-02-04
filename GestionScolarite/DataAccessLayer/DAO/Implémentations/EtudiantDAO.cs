using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.DataAccessLayer.DataAccess;
using GestionScolarite.ModelLayer;
using Microsoft.Data.SqlClient;

namespace GestionScolarite.DataAccessLayer.DAO.Implémentations
{
    internal class EtudiantDAO : IEtudiantDAO
    {
        public void Ajouter(Etudiant etudiant)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "INSERT INTO Etudiants (Nom, Prenom) VALUES (@Nom, @Prenom)";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Nom", etudiant.Nom);
                    cmd.Parameters.AddWithValue("@Prenom", etudiant.Prenom);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Modifier(Etudiant etudiant)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "UPDATE Etudiants SET Nom = @Nom, Prenom = @Prenom WHERE Id = @Id";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Nom", etudiant.Nom);
                    cmd.Parameters.AddWithValue("@Prenom", etudiant.Prenom);
                    cmd.Parameters.AddWithValue("@Id", etudiant.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Supprimer(int id)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "DELETE FROM Etudiants WHERE Id = @Id";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Etudiant? GetById(int id)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "SELECT * FROM Etudiants WHERE Id = @Id";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Etudiant(
                                (int)reader["Id"],
                                (string)reader["Nom"],
                                (string)reader["Prenom"]
                            );
                        }
                    }
                }
            }
            return null;
        }

        public List<Etudiant> GetAll()
        {
            var liste = new List<Etudiant>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "SELECT * FROM Etudiants";
                using (var cmd = new SqlCommand(requete, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        liste.Add(new Etudiant(
                            (int)reader["Id"],
                            (string)reader["Nom"],
                            (string)reader["Prenom"]
                        ));
                    }
                }
            }
            return liste;
        }
    }
}



