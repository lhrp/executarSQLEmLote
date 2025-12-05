using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace executarSQLEmLote.Funcoes
{
    public class BancoDados
    {
        public string IpServidor { get; set; }
        public string BaseDados { get; set; }

        private string connectionString;

        public BancoDados(string ipServidor, string baseDados)
        {
            IpServidor = ipServidor;
            BaseDados = baseDados;

            connectionString =
                $"Server={IpServidor};Database={BaseDados};Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public SqlConnection AbrirConexao()
        {
            SqlConnection conexao = new SqlConnection(connectionString);
            conexao.Open();
            return conexao;
        }

        public int ExecutarComando(string sql)
        {
            using (var conexao = AbrirConexao())
            using (var comando = new SqlCommand(sql, conexao))
            {
                return comando.ExecuteNonQuery();
            }
        }

        public DataTable Consultar(string sql)
        {
            using (var conexao = AbrirConexao())
            using (var comando = new SqlCommand(sql, conexao))
            using (var adapter = new SqlDataAdapter(comando))
            {
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
        }

        /// <summary>
        /// Executa uma Stored Procedure com ou sem parâmetros e retorna DataTable.
        /// </summary>
        public DataTable ExecutarProcedure(string nomeProcedure, Dictionary<string, object> parametros = null)
        {
            using (var conexao = AbrirConexao())
            using (var comando = new SqlCommand(nomeProcedure, conexao))
            {
                comando.CommandType = CommandType.StoredProcedure;

                // Se houver parâmetros, adiciona ao comando
                if (parametros != null)
                {
                    foreach (var prm in parametros)
                    {
                        comando.Parameters.AddWithValue(prm.Key, prm.Value ?? DBNull.Value);
                    }
                }

                using (var adapter = new SqlDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    return tabela;
                }
            }
        }
    }
}
