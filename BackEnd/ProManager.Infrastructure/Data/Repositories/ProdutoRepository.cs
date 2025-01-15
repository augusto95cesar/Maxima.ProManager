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
            const string sql = "SELECT * FROM PRODUTOS";
            return await _dbConnection.QueryAsync<Produto>(sql);
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM PRODUTOS WHERE id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
        }

        public async Task AddAsync(Produto produto)
        {
            const string sql = @"
                INSERT INTO PRODUTOS (guild, codigo, descricao, departamento, preco, status, acoes)
                VALUES (@Guild, @Codigo, @Descricao, @Departamento, @Preco, @Status, @Acoes)
                --RETURNING id;";
            //return await _dbConnection.ExecuteScalarAsync<int>(sql, produto);
            await _dbConnection.ExecuteScalarAsync(sql, produto);
        }

        public async Task<bool> UpdateAsync(Produto produto)
        {
            const string sql = @"
                UPDATE PRODUTOS 
                SET guild = @Guild, codigo = @Codigo, descricao = @Descricao, 
                    departamento = @Departamento, preco = @Preco, status = @Status, acoes = @Acoes
                WHERE id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, produto);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM PRODUTOS WHERE id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
