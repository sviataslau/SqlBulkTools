﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace SqlBulkTools
{
    /// <summary>
    /// 
    /// </summary>
    public static class BulkOperationsUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        /// <exception cref="SqlBulkToolsException"></exception>
        public static SqlConnection GetSqlConnection(string connectionName)
        {
            if (connectionName != null)
            {
                var conn = new SqlConnection(ConfigurationManager
                    .ConnectionStrings[connectionName].ConnectionString);
                return conn;
            }

            throw new SqlBulkToolsException("SqlConnection requested could not be resolved. Please check the arguments supplied to GetSqlConnection");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /// <exception cref="SqlBulkToolsException"></exception>
        public static SqlConnection GetSqlConnection(string connectionName, SqlCredential credentials)
        {
            if (connectionName != null)
            {
                var conn = new SqlConnection(ConfigurationManager
                    .ConnectionStrings[connectionName].ConnectionString, credentials);
                return conn;
            }

            throw new SqlBulkToolsException("SqlConnection requested could not be resolved. Please check the arguments supplied to GetSqlConnection");
        }

        private static readonly Dictionary<Type, DbType> TypeMappings = new Dictionary<Type, DbType>()
        {
            { typeof(byte), DbType.Byte},
            { typeof(sbyte), DbType.Int16},
            { typeof(ushort), DbType.UInt16},
            { typeof(int), DbType.Int32},
            { typeof(uint), DbType.UInt32},
            { typeof(long), DbType.Int64},
            { typeof(ulong), DbType.UInt64 },
            { typeof(float), DbType.Single },
            { typeof(double), DbType.Double},
            { typeof(decimal), DbType.Decimal},
            { typeof(bool), DbType.Boolean},
            { typeof(string), DbType.String },
            { typeof(char), DbType.StringFixedLength},
            { typeof(Guid), DbType.Guid},
            { typeof(DateTime), DbType.DateTime},
            { typeof(DateTimeOffset), DbType.DateTimeOffset },
            { typeof(byte[]), DbType.Binary},
            { typeof(byte?), DbType.Byte},
            { typeof(sbyte?), DbType.SByte },
            { typeof(short?), DbType.Int16},
            { typeof(ushort?), DbType.UInt16},
            { typeof(int?), DbType.Int32},
            { typeof(uint?), DbType.UInt32},
            { typeof(long?), DbType.Int64},
            { typeof(ulong?), DbType.UInt64},
            { typeof(float?), DbType.Single},
            { typeof(double?), DbType.Double},
            { typeof(decimal?), DbType.Decimal},
            { typeof(bool?), DbType.Boolean},
            { typeof(char?), DbType.StringFixedLength},
            { typeof(Guid?), DbType.Guid},
            { typeof(DateTime?), DbType.DateTime },
            { typeof(DateTimeOffset?), DbType.DateTimeOffset},
            { typeof(Binary), DbType.Binary},
            { typeof(TimeSpan), DbType.Time },
            { typeof(TimeSpan?), DbType.Time },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static DbType GetSqlTypeFromDotNetType(Type type)
        {
            DbType dbType;

            if (TypeMappings.TryGetValue(type, out dbType))
            {
                return dbType;
            }

            throw new KeyNotFoundException($"The type {type} could not be found.");
        }
    }
}
