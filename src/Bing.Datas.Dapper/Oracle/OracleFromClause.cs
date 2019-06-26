﻿using Bing.Datas.Sql;
using Bing.Datas.Sql.Builders;
using Bing.Datas.Sql.Builders.Clauses;
using Bing.Datas.Sql.Builders.Core;
using Bing.Datas.Sql.Matedatas;

namespace Bing.Datas.Dapper.Oracle
{
    /// <summary>
    /// Oracle From子句
    /// </summary>
    public class OracleFromClause : FromClause
    {
        /// <summary>
        /// 初始化一个<see cref="OracleFromClause"/>类型的实例
        /// </summary>
        /// <param name="builder">Sql生成器</param>
        /// <param name="dialect">Sql方言</param>
        /// <param name="resolver">实体解析器</param>
        /// <param name="register">实体别名注册器</param>
        /// <param name="tableDatabase">表数据库</param>
        /// <param name="table">表</param>
        public OracleFromClause(ISqlBuilder builder, IDialect dialect, IEntityResolver resolver,
            IEntityAliasRegister register, ITableDatabase tableDatabase, SqlItem table = null) : base(builder, dialect,
            resolver, register, tableDatabase, table)
        {
        }

        /// <summary>
        /// 创建Sql项
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="schema">架构名</param>
        /// <param name="alias">别名</param>
        protected override SqlItem CreateSqlItem(string table, string schema, string alias)
        {
            return new OracleSqlItem(table, schema, alias);
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <param name="builder">Sql生成器</param>
        /// <param name="register">实体别名注册器</param>
        public override IFromClause Clone(ISqlBuilder builder, IEntityAliasRegister register)
        {
            if (register != null)
                register.FromType = Register.FromType;
            return new OracleFromClause(builder, Dialect, Resolver, register, TableDatabase, Table);
        }
    }
}
