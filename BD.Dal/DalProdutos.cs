using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace BD.Dal
{
    public class DalProdutos : IDalProdutos
    {
        private IConnection _connection;
        public DalProdutos(IConnection connection)
        {
            _connection = connection;
        }


        public Produtos Insert(Produtos value)
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                string sql = "INSERT INTO Produtos(Nome, CodigoBarras, Preco, Imagem) VALUES(@Nome, @CodigoBarras, @Preco, @Imagem)";
                _command.CommandText = sql;
                _command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = value.Nome;
                _command.Parameters.Add("@CodigoBarras", SqlDbType.VarChar).Value = value.CodigoBarras;
                _command.Parameters.Add("@Preco", SqlDbType.Money).Value = value.Preco;
                _command.Parameters.Add("@Imagem", SqlDbType.VarChar).Value = value.Imagem;
                _command.ExecuteNonQuery();
            }
            return value;
        }


        public bool Update(Produtos value)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "UPDATE Produtos SET Nome=@Nome, Preco=@preco, Imagem=@Imagem WHERE CodigoBarras=@CodigoBarras";
                _command.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = value.Nome;
                _command.Parameters.Add("@Preco", SqlDbType.Money, 50).Value = value.Preco;
                _command.Parameters.Add("@Imagem", SqlDbType.Image, 50).Value = value.Imagem;
                _command.Parameters.Add("@CodigoBarras", SqlDbType.VarChar).Value = value.CodigoBarras;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }

        public Produtos FindByCodigoBarrasORNome(object codigoBarras, object nome)
        {
            Produtos brand = null;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Nome, CodigoBarras, Preco, Imagem FROM Produtos WHERE Nome=@Nome OR CodigoBarras=@CodigoBarras";
                _command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = nome;
                _command.Parameters.Add("@CodigoBarras", SqlDbType.VarChar).Value = codigoBarras;
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        brand = new Produtos(reader.GetString(0), reader.GetString(1), (double)reader.GetDecimal(2), (string)reader.GetValue(3));
                    }
                }
            }
            return brand;
        }

        public bool Delete(object codigoBarras)
        {
            bool ret = false;
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "DELETE FROM Produtos WHERE CodigoBarras=@CodigoBarras";
                _command.Parameters.Add("@CodigoBarras", SqlDbType.VarChar).Value = codigoBarras;
                ret = _command.ExecuteNonQuery() > 0;
            }
            return ret;
        }

        public bool Delete(Produtos value)
        {
            return Delete(value.CodigoBarras);
        }

        public IEnumerable<Produtos> List()
        {
            using (SqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT Nome, CodigoBarras, Preco, Imagem FROM Produtos ORDER BY CodigoBarras";
                using (SqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            yield return new Produtos(reader.GetString(0), reader.GetString(1), (double)reader.GetDecimal(2), (string)reader.GetValue(3));
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}