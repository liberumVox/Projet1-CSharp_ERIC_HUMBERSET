using GestionScolarite.DataAccessLayer.DAO.Interfaces;
using GestionScolarite.DataAccessLayer.DataAccess;
using GestionScolarite.ModelLayer;
using Microsoft.Data.SqlClient;

namespace GestionScolarite.DataAccessLayer.DAO.Implémentations
{
    public class ProfesseurDAO : IProfesseurDAO
    {
        public void Ajouter(Professeur professeur)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "INSERT INTO Professeurs (Nom, Prenom, Departement) VALUES (@Nom, @Prenom, @Departement)";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Nom", professeur.Nom);
                    cmd.Parameters.AddWithValue("@Prenom", professeur.Prenom);
                    cmd.Parameters.AddWithValue("@Departement", professeur.Departement);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Modifier(Professeur professeur)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "UPDATE Professeurs SET Nom = @Nom, Prenom = @Prenom, Departement = @Departement WHERE Id = @Id";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", professeur.Id);
                    cmd.Parameters.AddWithValue("@Nom", professeur.Nom);
                    cmd.Parameters.AddWithValue("@Prenom", professeur.Prenom);
                    cmd.Parameters.AddWithValue("@Departement", professeur.Departement);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Supprimer(int id)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "DELETE FROM Professeurs WHERE Id = @Id";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Professeur? GetById(int id)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "SELECT * FROM Professeurs WHERE Id = @Id";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Professeur(
                                (int)reader["Id"],
                                (string)reader["Nom"],
                                (string)reader["Prenom"],
                                (string)reader["Departement"]
                            );
                        }
                    }
                }
            }
            return null;
        }

        public List<Professeur> GetAll()
        {
            var liste = new List<Professeur>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "SELECT * FROM Professeurs";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            liste.Add(new Professeur(
                                (int)reader["Id"],
                                (string)reader["Nom"],
                                (string)reader["Prenom"],
                                (string)reader["Departement"]
                            ));
                        }
                    }
                }
            }
            return liste;
        }

        public Professeur? GetByNom(string nom)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "SELECT * FROM Professeurs WHERE Nom = @Nom";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Nom", nom);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Professeur(
                                (int)reader["Id"],
                                (string)reader["Nom"],
                                (string)reader["Prenom"],
                                (string)reader["Departement"]
                            );
                        }
                    }
                }
            }
            return null;
        }

        public List<Professeur> GetByDepartement(string departement)
        {
            var liste = new List<Professeur>();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var requete = "SELECT * FROM Professeurs WHERE Departement = @Departement";
                using (var cmd = new SqlCommand(requete, conn))
                {
                    cmd.Parameters.AddWithValue("@Departement", departement);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            liste.Add(new Professeur(
                                (int)reader["Id"],
                                (string)reader["Nom"],
                                (string)reader["Prenom"],
                                (string)reader["Departement"]
                            ));
                        }
                    }
                }
            }
            return liste;
        }
    }
}
