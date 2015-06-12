﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Hxj.Tools.EntityDesign.Model;
using System.Xml;
using System.Windows.Forms;

namespace Hxj.Tools.EntityDesign
{
    public class EntityBuilder
    {
        private List<Model.ColumnInfo> _columns = new List<Hxj.Tools.EntityDesign.Model.ColumnInfo>();

        private string _tableName;

        private string _nameSpace = "Dos.Model";

        private string _className;

        private bool _isView = false;

        private bool _isSZMDX = false;

        public EntityBuilder(string tableName, string nameSpace, string className, List<Model.ColumnInfo> columns, bool isView)
            : this(tableName, nameSpace, className, columns, isView, false)
        {

        }

        public EntityBuilder(string tableName, string nameSpace, string className, List<Model.ColumnInfo> columns, bool isView, bool isSZMDX)
        {
            _isSZMDX = isSZMDX;
            _className = Utils.ReplaceSpace(className);
            _nameSpace = Utils.ReplaceSpace(nameSpace);
            _tableName = tableName;
            if (_isSZMDX)
            {
                _className = Utils.ToUpperFirstword(_className);
            }
            _isView = isView;



            foreach (Model.ColumnInfo col in columns)
            {
                col.ColumnName = Utils.ReplaceSpace(col.ColumnName);
                if (_isSZMDX)
                {
                    col.ColumnName = Utils.ToUpperFirstword(col.ColumnName);
                }
                
                col.DeText = Utils.ReplaceSpace(col.DeText);
                _columns.Add(col);
            }

        }

        public List<Model.ColumnInfo> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        public string NameSpace
        {
            get { return _nameSpace; }
            set { _nameSpace = value; }
        }
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        public bool IsView
        {
            get { return _isView; }
            set { _isView = value; }
        }

        public string Builder()
        {
            Columns = DbToCS.DbtoCSColumns(Columns);

            StringPlus plus = new StringPlus();
            plus.AppendLine("//------------------------------------------------------------------------------");
            plus.AppendLine("// <auto-generated>");
            plus.AppendLine("//     此代码由工具生成。");
            plus.AppendLine("//     运行时版本:" + Environment.Version.ToString());
            plus.AppendLine("//     Support: http://www.cnblogs.com/huxj");
            plus.AppendLine("//     Website: http://www.ITdos.com/");
            plus.AppendLine("//     对此文件的更改可能会导致不正确的行为，并且如果");
            plus.AppendLine("//     重新生成代码，这些更改将会丢失。");
            plus.AppendLine("// </auto-generated>");
            plus.AppendLine("//------------------------------------------------------------------------------");
            plus.AppendLine();
            plus.AppendLine();
            plus.AppendLine("using System;");
            plus.AppendLine("using System.Data;");
            plus.AppendLine("using System.Data.Common;");
            plus.AppendLine("using Dos.ORM;");
            plus.AppendLine("using Dos.ORM.Common;");
            plus.AppendLine();
            plus.AppendLine("namespace " + NameSpace);
            plus.AppendLine("{");
            plus.AppendLine();
            plus.AppendSpaceLine(1, "/// <summary>");
            plus.AppendSpaceLine(1, "/// 实体类" + ClassName + " 。(属性说明自动提取数据库字段的描述信息)");
            plus.AppendSpaceLine(1, "/// </summary>");
            plus.AppendSpaceLine(1, "[Serializable]");
            plus.AppendSpaceLine(1, "public class " + ClassName + " : Entity ");
            plus.AppendSpaceLine(1, "{");
            plus.AppendSpaceLine(2, "public " + ClassName + "():base(\"" + TableName + "\") {}");
            plus.AppendLine();
            plus.AppendLine(BuilderModel());
            plus.AppendLine(BuilderMethod());
            plus.AppendSpaceLine(1, "}");
            plus.AppendLine("}");
            plus.AppendLine("");
            return plus.ToString();
        }

        private string BuilderModel()
        {
            StringPlus plus = new StringPlus();
            StringPlus plus2 = new StringPlus();
            StringPlus plus3 = new StringPlus();
            plus.AppendSpaceLine(2, "#region Model");
            foreach (ColumnInfo column in Columns)
            {
                plus2.AppendSpaceLine(2, "private " + column.TypeName + " _" + column.ColumnName + ";");
                plus3.AppendSpaceLine(2, "/// <summary>");
                plus3.AppendSpaceLine(2, "/// " + column.DeText);
                plus3.AppendSpaceLine(2, "/// </summary>");
                plus3.AppendSpaceLine(2, "public " + column.TypeName + " " + column.ColumnName);
                plus3.AppendSpaceLine(2, "{");
                plus3.AppendSpaceLine(3, "get{ return _" + column.ColumnName + "; }");
                plus3.AppendSpaceLine(3, "set");
                plus3.AppendSpaceLine(3, "{");
                plus3.AppendSpaceLine(4, "this.OnPropertyValueChange(_." + column.ColumnName + ",_" + column.ColumnName + ",value);");
                plus3.AppendSpaceLine(4, "this._" + column.ColumnName + "=value;");
                plus3.AppendSpaceLine(3, "}");
                plus3.AppendSpaceLine(2, "}");
            }
            plus.Append(plus2.Value);
            plus.Append(plus3.Value);
            plus.AppendSpaceLine(2, "#endregion");

            return plus.ToString();


        }



        private string BuilderMethod()
        {
            StringPlus plus = new StringPlus();


            plus.AppendSpaceLine(2, "#region Method");


            //只读
            if (IsView)
            {
                plus.AppendSpaceLine(2, "/// <summary>");
                plus.AppendSpaceLine(2, "/// 是否只读");
                plus.AppendSpaceLine(2, "/// </summary>");
                plus.AppendSpaceLine(2, "public override bool IsReadOnly()");
                plus.AppendSpaceLine(2, "{");
                plus.AppendSpaceLine(3, "return true;");
                plus.AppendSpaceLine(2, "}");
            }

            Model.ColumnInfo identityColumn = Columns.Find(delegate(Model.ColumnInfo col) { return col.IsIdentity; });
            if (null != identityColumn)
            {
                plus.AppendSpaceLine(2, "/// <summary>");
                plus.AppendSpaceLine(2, "/// 获取实体中的标识列");
                plus.AppendSpaceLine(2, "/// </summary>");
                plus.AppendSpaceLine(2, "public override Field GetIdentityField()");
                plus.AppendSpaceLine(2, "{");
                plus.AppendSpaceLine(3, "return _." + identityColumn.ColumnName + ";");
                plus.AppendSpaceLine(2, "}");
            }

            List<Model.ColumnInfo> primarykeyColumns = Columns.FindAll(delegate(Model.ColumnInfo col) { return col.IsPK; });
            if (null != primarykeyColumns && primarykeyColumns.Count > 0)
            {
                plus.AppendSpaceLine(2, "/// <summary>");
                plus.AppendSpaceLine(2, "/// 获取实体中的主键列");
                plus.AppendSpaceLine(2, "/// </summary>");
                plus.AppendSpaceLine(2, "public override Field[] GetPrimaryKeyFields()");
                plus.AppendSpaceLine(2, "{");
                plus.AppendSpaceLine(3, "return new Field[] {");
                StringPlus plus2 = new StringPlus();
                foreach (Model.ColumnInfo col in primarykeyColumns)
                {
                    plus2.AppendSpaceLine(4, "_." + col.ColumnName + ",");
                }
                plus.Append(plus2.ToString().TrimEnd().Substring(0, plus2.ToString().TrimEnd().Length - 1));
                plus.AppendLine("};");
                plus.AppendSpaceLine(2, "}");
            }



            plus.AppendSpaceLine(2, "/// <summary>");
            plus.AppendSpaceLine(2, "/// 获取列信息");
            plus.AppendSpaceLine(2, "/// </summary>");
            plus.AppendSpaceLine(2, "public override Field[] GetFields()");
            plus.AppendSpaceLine(2, "{");
            plus.AppendSpaceLine(3, "return new Field[] {");
            StringPlus plus3 = new StringPlus();
            foreach (ColumnInfo col in Columns)
            {
                plus3.AppendSpaceLine(4, "_." + col.ColumnName + ",");
            }
            plus.Append(plus3.ToString().TrimEnd().Substring(0, plus3.ToString().TrimEnd().Length - 1));
            plus.AppendLine("};");
            plus.AppendSpaceLine(2, "}");


            plus.AppendSpaceLine(2, "/// <summary>");
            plus.AppendSpaceLine(2, "/// 获取值信息");
            plus.AppendSpaceLine(2, "/// </summary>");
            plus.AppendSpaceLine(2, "public override object[] GetValues()");
            plus.AppendSpaceLine(2, "{");
            plus.AppendSpaceLine(3, "return new object[] {");
            StringPlus plus4 = new StringPlus();
            foreach (ColumnInfo col in Columns)
            {
                plus4.AppendSpaceLine(4, "this._" + col.ColumnName + ",");
            }
            plus.Append(plus4.ToString().TrimEnd().Substring(0, plus4.ToString().TrimEnd().Length - 1));
            plus.AppendLine("};");
            plus.AppendSpaceLine(2, "}");


            plus.AppendSpaceLine(2, "/// <summary>");
            plus.AppendSpaceLine(2, "/// 给当前实体赋值");
            plus.AppendSpaceLine(2, "/// </summary>");
            plus.AppendSpaceLine(2, "public override void SetPropertyValues(IDataReader reader)");
            plus.AppendSpaceLine(2, "{");
            foreach (ColumnInfo col in Columns)
            {
                plus.AppendSpaceLine(3, "this._" + col.ColumnName + " = DataUtils.ConvertValue<" + col.TypeName + ">(reader[\"" + col.ColumnNameRealName + "\"]);");
            }
            plus.AppendSpaceLine(2, "}");


            plus.AppendSpaceLine(2, "/// <summary>");
            plus.AppendSpaceLine(2, "/// 给当前实体赋值");
            plus.AppendSpaceLine(2, "/// </summary>");
            plus.AppendSpaceLine(2, "public override void SetPropertyValues(DataRow row)");
            plus.AppendSpaceLine(2, "{");
            foreach (ColumnInfo col in Columns)
            {
                plus.AppendSpaceLine(3, "this._" + col.ColumnName + " = DataUtils.ConvertValue<" + col.TypeName + ">(row[\"" + col.ColumnNameRealName + "\"]);");
            }
            plus.AppendSpaceLine(2, "}");


            plus.AppendSpaceLine(2, "#endregion");
            plus.AppendLine();



            plus.AppendSpaceLine(2, "#region _Field");
            plus.AppendSpaceLine(2, "/// <summary>");
            plus.AppendSpaceLine(2, "/// 字段信息");
            plus.AppendSpaceLine(2, "/// </summary>");
            plus.AppendSpaceLine(2, "public class _");
            plus.AppendSpaceLine(2, "{");
            plus.AppendSpaceLine(3, "/// <summary>");
            plus.AppendSpaceLine(3, "/// * ");
            plus.AppendSpaceLine(3, "/// </summary>");
            plus.AppendSpaceLine(3, "public readonly static Field All = new Field(\"*\",\"" + TableName + "\");");
            foreach (ColumnInfo col in Columns)
            {
                plus.AppendSpaceLine(3, "/// <summary>");
                plus.AppendSpaceLine(3, "/// " + col.DeText);
                plus.AppendSpaceLine(3, "/// </summary>");
                plus.AppendSpaceLine(3, "public readonly static Field " + col.ColumnName + " = new Field(\"" + col.ColumnNameRealName + "\",\"" + TableName + "\",\"" + (string.IsNullOrEmpty(col.DeText) ? col.ColumnNameRealName : col.DeText) + "\");");
            }
            plus.AppendSpaceLine(2, "}");
            plus.AppendSpaceLine(2, "#endregion");
            plus.AppendLine();

            return plus.ToString();


        }




    }

    public class DbToCS
    {

        /// <summary>
        /// 类型配置文件
        /// </summary>
        public static readonly string DbTypePath = Application.StartupPath + "/dbtype.xml";


        private const string cachekeystring = "_dbtype_cache_";



        static Dictionary<string, string> loadType()
        {

            Dictionary<string, string> types = Dos.ORM.Cache.Default.GetCache(cachekeystring) as Dictionary<string, string>;

            if (null == types)
            {

                types = new Dictionary<string, string>();

                XmlDocument doc = new XmlDocument();

                doc.Load(DbTypePath);

                XmlNodeList nodes = doc.SelectNodes("//type");

                if (null != nodes && nodes.Count > 0)
                {
                    foreach (XmlNode node in nodes)
                    {
                        XmlAttribute att = node.Attributes["dbtype"];
                        if (null != att)
                        {
                            string dbtypeStr = att.Value.Trim().ToLower();
                            if (!types.ContainsKey(dbtypeStr))
                            {
                                XmlAttribute attcstype = node.Attributes["cstype"];
                                if (null != attcstype)
                                {
                                    types.Add(dbtypeStr, attcstype.Value);
                                }
                            }
                        }
                    }
                }

                Dos.ORM.Cache.Default.AddCacheFilesDependency(cachekeystring, types, DbTypePath);

            }

            return types;
        }


        /// <summary>
        /// 修改TypeName
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static List<Model.ColumnInfo> DbtoCSColumns(List<Model.ColumnInfo> columns)
        {
            Dictionary<string, string> types = loadType();

            foreach (ColumnInfo column in columns)
            {
                try
                {
                    if (column.TypeName.Trim().ToLower() == "char" && column.Length == "36")
                    {
                        column.TypeName = types["uniqueidentifier"];
                    }
                    else
                    {
                        column.TypeName = types[column.TypeName.Trim().ToLower()];
                    }
                }
                catch
                {
                    column.TypeName = "object";
                }
                if (!column.IsIdentity && !column.IsPK && column.cisNull)
                {
                    if (!column.TypeName.Equals("string") && !column.TypeName.Equals("object") && !column.TypeName.Equals("byte[]"))
                        column.TypeName += "?";
                }
            }

            return columns;
        }





    }
}
