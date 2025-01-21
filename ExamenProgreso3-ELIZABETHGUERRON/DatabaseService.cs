using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProgreso3_ELIZABETHGUERRON
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Peliculas.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Pelicula>().Wait();
        }

        public Task<int> InsertPeliculaAsync(Pelicula pelicula)
        {
            return _database.InsertAsync(pelicula);
        }

        public Task<List<Pelicula>> GetPeliculasAsync()
        {
            return _database.Table<Pelicula>().ToListAsync();
        }

        public Task<int> DeletePeliculaAsync(Pelicula pelicula)
        {
            return _database.DeleteAsync(pelicula);
        }
    }
}
