using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AtividadeIA
{
    public class Curso
    {
        public int Id { get; set; }
        public string NomeCurso { get; set; }
        public string Categoria { get; set; }
        public bool AbertoParaInscricao { get; set; }
        public int CargaHoraria { get; set; }
        public int? IdAlunos { get; set; }
        public int? IdTurmas { get; set; }

        public Curso() { }

        public Curso(int id, string nomeCurso, string categoria, bool abertoParaInscricao, int cargaHoraria, int? idAlunos, int? idTurmas)
        {
            Id = id;
            NomeCurso = nomeCurso;
            Categoria = categoria;
            AbertoParaInscricao = abertoParaInscricao;
            CargaHoraria = cargaHoraria;
            IdAlunos = idAlunos;
            IdTurmas = idTurmas;
        }

        public void Mostrar()
        {
            Console.WriteLine($"ID: {Id} | Curso: {NomeCurso} | Categoria: {Categoria} | Aberto para inscrição: {(AbertoParaInscricao ? "Sim" : "Não")} | Carga Horária: {CargaHoraria}h | Alunos (ID): {IdAlunos} | Turmas (ID): {IdTurmas}");
        }

        public static List<Curso> ListarTodos()
        {
            List<Curso> cursos = new List<Curso>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM cursos";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cursos.Add(MapearDoBanco(reader));
                    }
                }
            }
            return cursos;
        }

        public static Curso? ListarPorId(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM cursos WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearDoBanco(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Adicionar()
        {
            if (ExisteCursoDuplicado())
            {
                Console.WriteLine("Erro: Já existe um curso com as mesmas características de turma e período.");
                return;
            }

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO cursos (curso, categoria, aberto_para_incricao, carga_horaria, id_alunos, id_turmas) 
                                 VALUES (@curso, @categoria, @aberto_para_incricao, @carga_horaria, @id_alunos, @id_turmas)";
                using (var command = new MySqlCommand(query, connection))
                {
                    PreencherParametros(command);
                    command.ExecuteNonQuery();
                    Id = (int)command.LastInsertedId;
                    Console.WriteLine("Curso adicionado com sucesso!");
                }
            }
        }

        public static void ExcluirPorId(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM cursos WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int afetados = command.ExecuteNonQuery();
                    if (afetados > 0)
                        Console.WriteLine("Curso excluído com sucesso!");
                    else
                        Console.WriteLine("Curso não encontrado.");
                }
            }
        }

        public void Atualizar()
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE cursos SET curso = @curso, categoria = @categoria, 
                                 aberto_para_incricao = @aberto_para_incricao, carga_horaria = @carga_horaria, 
                                 id_alunos = @id_alunos, id_turmas = @id_turmas WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    PreencherParametros(command);
                    command.Parameters.AddWithValue("@id", Id);
                    int afetados = command.ExecuteNonQuery();
                    if (afetados > 0)
                        Console.WriteLine("Curso atualizado com sucesso!");
                    else
                        Console.WriteLine("Curso não encontrado para atualização.");
                }
            }
        }

        private bool ExisteCursoDuplicado()
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM cursos WHERE curso = @curso";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@curso", NomeCurso);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private static Curso MapearDoBanco(MySqlDataReader reader)
        {
            return new Curso
            {
                Id = reader.GetInt32("id"),
                NomeCurso = reader.GetString("curso"),
                Categoria = reader.GetString("categoria"),
                AbertoParaInscricao = reader.GetBoolean("aberto_para_incricao"),
                CargaHoraria = reader.GetInt32("carga_horaria"),
                IdAlunos = reader.IsDBNull(reader.GetOrdinal("id_alunos")) ? (int?)null : reader.GetInt32("id_alunos"),
                IdTurmas = reader.IsDBNull(reader.GetOrdinal("id_turmas")) ? (int?)null : reader.GetInt32("id_turmas")
            };
        }

        private void PreencherParametros(MySqlCommand command)
        {
            command.Parameters.AddWithValue("@curso", NomeCurso);
            command.Parameters.AddWithValue("@categoria", Categoria);
            command.Parameters.AddWithValue("@aberto_para_incricao", AbertoParaInscricao);
            command.Parameters.AddWithValue("@carga_horaria", CargaHoraria);
            command.Parameters.AddWithValue("@id_alunos", (object)IdAlunos ?? DBNull.Value);
            command.Parameters.AddWithValue("@id_turmas", (object)IdTurmas ?? DBNull.Value);
        }
    }
}
