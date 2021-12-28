﻿using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;
using System.Collections.Generic;
using bramkominatorMobile.Models;
using bramkominatorMobile.Exceptions;

namespace bramkominatorMobile.Services
{
    public static class GatewaysDbService
    {
        private static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bramkominatorDB.db");

            db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<LogicGateway>();

            List<LogicGateway> basicGates = new List<LogicGateway>
            {
                new LogicGateway(GatewayType.Not, "Not"),
                new LogicGateway(GatewayType.And, "And"),
                new LogicGateway(GatewayType.Or, "Or"),
                new LogicGateway(GatewayType.Nor, "Nor"),
                new LogicGateway(GatewayType.Xor, "Xor"),
                new LogicGateway(GatewayType.Xnor, "Xnor"),
                new LogicGateway(GatewayType.Nand, "Nand"),
            };

            foreach (LogicGateway gate in basicGates)
            {
                await db.InsertAsync(gate);
            }
        }

        public static async Task AddGateway(LogicGateway gate)
        {
            await Init();

            if (gate.Type != GatewayType.Custom)
                throw new BadGatewayTypeException("Unable to add one of basic gateways again");

            var id = await db.InsertAsync(gate);
        }

        public static async Task RemoveGateway(LogicGateway gate)
        {
            await Init();

            if (gate.Type != GatewayType.Custom)
                throw new BadGatewayTypeException("Unable to remove one of basic gateways");

            await db.DeleteAsync<LogicGateway>(gate.Id);
        }

        public static async Task<IEnumerable<LogicGateway>> GetAllGates()
        {
            await Init();

            var gates = await db.Table<LogicGateway>().ToListAsync();

            return gates;
        }

        public static async Task<IEnumerable<LogicGateway>> GetBasicGates()
        {
            await Init();

            var basicGates = await db.Table<LogicGateway>().Where(g => g.Type != GatewayType.Custom).ToListAsync();

            return basicGates;
        }

        public static async Task<IEnumerable<LogicGateway>> GetCustomGates()
        {
            await Init();

            var customGates = await db.Table<LogicGateway>().Where(g => g.Type == GatewayType.Custom).ToListAsync();

            return customGates;
        }
    }
}
