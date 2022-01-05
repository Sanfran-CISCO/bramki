using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;
using System.Collections.Generic;
using bramkominatorMobile.Exceptions;
using System.Collections;

namespace bramkominatorMobile.Services
{
    public class CircutsDbService : ICircutsDbService
    {
        private SQLiteAsyncConnection db;

        public CircutsDbService(SQLiteAsyncConnection con)
        {
            db = con;
        }

        public async Task Init()
        {
            if (db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bramkominatorDB.db");

            db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<LogicCircut>();
        }

        public async Task AddCircut(LogicCircut circut)
        {
            await Init();

            if (circut is null)
                throw new InvalidCircutException("Circut can't be null");

            var id = await db.InsertAsync(circut);
        }

        public async Task RemoveCircut(LogicCircut circut)
        {
            await Init();

            await db.DeleteAsync<LogicCircut>(circut.Id);
        }

        public async Task UpdateCircut(LogicCircut circut)
        {
            await Init();

            await db.UpdateAsync(circut);
        }

        public async Task<IEnumerable<LogicCircut>> GetAllCircuts()
        {
            await Init();

            var circuts = await db.Table<LogicCircut>().ToListAsync();

            return circuts;
        }

        public async Task<LogicCircut> GetCircut(int id)
        {
            await Init();

            var circut = await db.Table<LogicCircut>().FirstOrDefaultAsync(c => c.Id == id);

            return circut;
        }

        public IEnumerator<LogicCircut> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
