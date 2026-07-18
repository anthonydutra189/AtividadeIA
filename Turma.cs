using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AtividadeIA
{
    public class Turma
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Periodo { get; set; }
        public string TempoAtivo { get; set; }
        public int? IdAlunos { get; set; }
        public int? IdCursos { get; set; }

        public Turma() { }

        public Turma(int id, int numero, string periodo, string tempoAtivo, int? idAlunos, int? idCursos)
        {
            Id = id;
            Numero = numero;
            Periodo = periodo;
            TempoAtivo = tempoAtivo;
            IdAlunos = idAlunos;
            IdCursos = idCursos;
        }

        public void Mostrar()
        {
            Console.WriteLine($"ID: {Id} | Número: {Numero} | Período: {Periodo} | Tempo Ativo: {TempoAtivo} | Alunos (ID): {IdAlunos} | Cursos (ID): {IdCursos}");
        }

        public static List<Turma> ListarTodos()
        {
            List<Turma> turmas = new List<Turma>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM turmas";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        turmas.Add(MapearDoBanco(reader));
                    }
                }
            }
            return turmas;
        }

        public static Turma? ListarPorId(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM turmas WHERE id = @id";
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
            if (ExisteNumeroTurma(Numero))
            {
                Console.WriteLine("Erro: Já existe uma turma com este número.");
                return;
            }

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO turmas (numero, periodo, tempo_ativo, id_alunos, id_cursos) 
                                 VALUES (@numero, @periodo, @tempo_ativo, @id_alunos, @id_cursos)";
                using (var command = new MySqlCommand(query, connection))
                {
                    PreencherParametros(command);
                    command.ExecuteNonQuery();
                    Id = (int)command.LastInsertedId;
                    Console.WriteLine("Turma adicionada com sucesso!");
                }
            }
        }

        public static void ExcluirPorId(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM turmas WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int afetados = command.ExecuteNonQuery();
                    if (afetados > 0)
                        Console.WriteLine("Turma excluída com sucesso!");
                    else
                        Console.WriteLine("Turma não encontrada.");
                }
            }
        }

        public void Atualizar()
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE turmas SET numero = @numero, periodo = @periodo, 
                                 tempo_ativo = @tempo_ativo, id_alunos = @id_alunos, 
                                 id_cursos = @id_cursos WHERE id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    PreencherParametros(command);
                    command.Parameters.AddWithValue("@id", Id);
                    int afetados = command.ExecuteNonQuery();
                    if (afetados > 0)
                        Console.WriteLine("Turma atualizada com sucesso!");
                    else
                        Console.WriteLine("Turma não encontrada para atualização.");
                }
            }
        }

        private bool ExisteNumeroTurma(int numero)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM turmas WHERE numero = @numero";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@numero", numero);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private static Turma MapearDoBanco(MySqlDataReader reader)
        {
            return new Turma
            {
                Id = reader.GetInt32("id"),
                Numero = reader.GetInt32("numero"),
                Periodo = reader.GetString("periodo"),
                TempoAtivo = reader.GetString("tempo_ativo"),
                IdAlunos = reader.IsDBNull(reader.GetOrdinal("id_alunos")) ? (int?)null : reader.GetInt32("id_alunos"),
                IdCursos = reader.IsDBNull(reader.GetOrdinal("id_cursos")) ? (int?)null : reader.GetInt32("id_cursos")
            };
        }

        private void PreencherParametros(MySqlCommand command)
        {
            command.Parameters.AddWithValue("@numero", Numero);
            command.Parameters.AddWithValue("@periodo", Periodo);
            command.Parameters.AddWithValue("@tempo_ativo", TempoAtivo);
            command.Parameters.AddWithValue("@id_alunos", (object)IdAlunos ?? DBNull.Value);
            command.Parameters.AddWithValue("@id_cursos", (object)IdCursos ?? DBNull.Value);
        }
    }
}
