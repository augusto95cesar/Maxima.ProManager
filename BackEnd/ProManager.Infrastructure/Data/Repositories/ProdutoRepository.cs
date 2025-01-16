using Dapper;
using ProManager.Domain.Entity;
using ProManager.Infrastructure.Data.Repositories.Interface;
using System.Data;

namespace ProManager.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProdutoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            bool status = true;
            const string sql = "SELECT  CODIGO, DESCRICAO, DEPARTAMENTO, PRECO, STATUS, ACOES FROM PRODUTOS WHERE STATUS = @status";
            return await _dbConnection.QueryAsync<Produto>(sql, new { status = status });
        }

        public async Task AddAsync(Produto produto)
        {
            const string sql = @"
                INSERT INTO PRODUTOS (id, codigo, descricao, departamento, preco, status, acoes)
                VALUES (@Id, @Codigo, @Descricao, @Departamento, @Preco, @Status, @Acoes)
                --RETURNING id;";
            //return await _dbConnection.ExecuteScalarAsync<int>(sql, produto);
            await _dbConnection.ExecuteScalarAsync(sql, produto);
        }

        public async Task<bool> UpdateAsync(Produto produto)
        {
            const string sql = @"
                UPDATE PRODUTOS 
                SET codigo = @Codigo, descricao = @Descricao, 
                    departamento = @Departamento, preco = @Preco, status = @Status, acoes = @Acoes
                WHERE codigo = @Codigo";
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, produto);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(string codigo)
        {
            bool status = false;
            //const string sql = "DELETE FROM PRODUTOS   WHERE codigo = @codigo";
            const string sql = "UPDATE PRODUTOS SET STATUS = @status WHERE CODIGO = @codigo";
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { status = status, codigo = codigo });
            return rowsAffected > 0;
        }

        public async Task<Produto?> GetByCodigoAsync(string codigo)
        {
            const string sql = "SELECT CODIGO, DESCRICAO, DEPARTAMENTO, PRECO, STATUS, ACOES FROM PRODUTOS WHERE codigo = @codigo";
            return await _dbConnection.QueryFirstOrDefaultAsync<Produto>(sql, new { codigo = codigo });
        }
    }
}
