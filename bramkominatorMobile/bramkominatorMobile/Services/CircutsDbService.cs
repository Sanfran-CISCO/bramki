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
    public static class CircutsDbService
    {
        private static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bramkominator_CircutsDB.db");

            db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<LogicCircut>();
        }

        public static async Task AddCircut(LogicCircut circut)
        {
            await Init();

            if (circut is null)
                throw new InvalidCircutException("Circut can't be null");

            var id = await db.InsertAsync(circut);
        }

        public static async Task RemoveCircut(LogicCircut circut)
        {
            await Init();

            await db.DeleteAsync<LogicCircut>(circut.Id);
        }

        public static async Task UpdateCircut(LogicCircut circut)
        {
            await Init();

            await db.UpdateAsync(circut);
        }

        public static async Task<IEnumerable<LogicCircut>> GetAllCircuts()
        {
            await Init();

            var circuts = await db.Table<LogicCircut>().ToListAsync();

            return circuts;
        }

        public static async Task<LogicCircut> GetCircut(int id)
        {
            await Init();

            var circut = await db.Table<LogicCircut>().FirstOrDefaultAsync(c => c.Id == id);

            return circut;
        }

        public static async Task<IEnumerable<LogicCircut>> GetAllCircutsSample()
        {
            List<LogicCircut> circuts = new List<LogicCircut>
            {
                new LogicCircut(),
                new LogicCircut(),
                new LogicCircut(),
                new LogicCircut(),
                new LogicCircut(),
            };

            for (int i = 0; i < 5; i++)
            {
                circuts[i].Name = $"Circut {i}";
            }

            await Task.Delay(100);

            return circuts;
        }
    }
}
