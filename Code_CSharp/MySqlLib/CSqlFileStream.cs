using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Alivever.com.MySqlLib
{
    /// <summary>
    /// 提供一个SQL写入器。调用者需要不断地调用addValueStr（）或类似方法就可以了。
    /// 本对象负责自动把ValueStr按照指定长度写入文件。开发者不需要再关心文件切分的问题。
    /// 
    /// </summary>
    public class CSqlFileStream_Insert
    {
        #region public members

        public int In_MaxValuesInOneInsert = 300;

        public int In_MaxInsertInOneFile = 50;

        public Encoding FileEncoding = System.Text.Encoding.Default;

        /// <summary>
        /// 需要写入的文件名模板，默认带有一个{0}，用于本类不断分配新的真实文件。
        /// </summary>
        public string In_FileUrlTemplate;

        public string Sql_Header ;

        /// <summary>
        /// 执行的所有结果文件列表
        /// </summary>
        public List<string> Out_SqlFiles = new List<string>();
        #endregion /public members

        #region processing members
        List<string> ValueStrList = new List<string>();

        //MemoryStream MemStream;
        StringBuilder StrBuilder = new System.Text.StringBuilder();

        /// <summary>
        /// 当前写入的文件名。当一个文件内sql达到上限时，程序会自动分配一个新的文件名
        /// </summary>
        string CrrFileUrl;

        /// <summary>
        /// 当前文件中插入了多少条Insert语句了。 当达到this。In_MaxInsertInOneFile时将被清零
        /// </summary>
        int CrrInsertInOneFile = 0;

        int FileNumber = 0;
        #endregion //processing members

        public CSqlFileStream_Insert(string _In_FileUrlTemplate, string _Sql_Header)
        {
            if (!_In_FileUrlTemplate.Contains("{0}"))
                throw new Exception(string.Format("_In_FileUrlTemplate={0}\n中必须包含一个【0】参数,以便用于自动累加",_In_FileUrlTemplate));

            this.In_FileUrlTemplate = _In_FileUrlTemplate;
            this.Sql_Header = _Sql_Header;

            this.WriteFileWithMemStream(true);

        }//CBuilder_SqlFileStream_Insert()

        public void AddValueObj(IDbInsertIntoAble _iSqlable)
        {
            this.AddValueStr(_iSqlable.GetInsertValueStr());
        }

        public void AddValueObjList<T>(List<T> _iSqlableList,bool _isFinishLastFile) where T: IDbInsertIntoAble
        {
            for (int i=0; i < _iSqlableList.Count; i++)
            {
                IDbInsertIntoAble crrItem = _iSqlableList[i];
                this.AddValueStr(crrItem.GetInsertValueStr());
            }
            this.FinalWriteRestBuffer(_isFinishLastFile);
            //this.FinalWriteRestBuffer();

        }

        public void AddValueStr(string _valueStr)
        {
            lock (this)
            {
                this.ValueStrList.Add(_valueStr);

                if (this.ValueStrList.Count >= this.In_MaxValuesInOneInsert)
                {
                    this.WriteOnInsertIntoMemoryStreamAndClearBuffer();
                    this.CrrInsertInOneFile++;
                }
                if (CrrInsertInOneFile >= this.In_MaxInsertInOneFile)
                    WriteFileWithMemStream(true);
            }//lock (this)


        }//AddValueStr(string _valueStr)

        public void FinalWriteRestBuffer(bool _isFinishLastFile)
        {
            lock (this)
            {
                this.WriteOnInsertIntoMemoryStreamAndClearBuffer();
                this.CrrInsertInOneFile++;

                this.WriteFileWithMemStream(_isFinishLastFile);
            }
        }// FinalWriteRestBuffer()

        void WriteFileWithMemStream(bool _isGenerateNewCrrFileUrl_And_ClearCrrInsertInOnFile)
        {
            //lock (this)
            //{
            //if (this.MemStream != null)
            //{
            //    //byte[] bytes = MemStream.GetBuffer();
            //    //using (var stream = new FileStream(this.CrrFileUrl, FileMode.Append))
            //    //{
            //    //    stream.Write(bytes, 0, bytes.Length);
            //    //}                //File.WriteAllBytes(this.CrrFileUrl, );
            //    //MemStream.Close();
            //}

            //string rstStr = this.StrBuilder.ToString();
            if(this.StrBuilder.Length != 0)
                File.AppendAllText(this.CrrFileUrl, this.StrBuilder.ToString(), FileEncoding);

            //this.MemStream = new MemoryStream();
            this.StrBuilder.Clear();
            //this.StrBuilder = new StringBuilder();

            if (_isGenerateNewCrrFileUrl_And_ClearCrrInsertInOnFile)
            {
                if (CrrFileUrl != null && CrrFileUrl.Length !=0)
                    this.Out_SqlFiles.Add(this.CrrFileUrl);

                this.CrrInsertInOneFile = 0;
                this.FileNumber++;
                this.CrrFileUrl = string.Format(this.In_FileUrlTemplate,
                    string.Format("{0}_{1}", this.FileNumber, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
            }
            //}//lock (this)
        }

        void WriteOnInsertIntoMemoryStreamAndClearBuffer()
        {
            //lock (this)
            //{
            if (this.ValueStrList.Count == 0)
            {
                this.StrBuilder.AppendLine("-- Empty ValueStrList");
                return;
            }

            string midStr = ", \n\t ", lastStr = ";\n ";

            //MemStream.Write(System.Text.Encoding.Default.GetBytes(this.Sql_Header), 0, this.Sql_Header.Length);
            this.StrBuilder.Append(this.Sql_Header);

            for (int i = 0; i < this.ValueStrList.Count; i++)
            {
                string crrItem = this.ValueStrList[i];

                //MemStream.Write(System.Text.Encoding.Default.GetBytes(crrItem), 0, crrItem.Length);
                this.StrBuilder.Append(crrItem);

                if (i != this.ValueStrList.Count - 1)
                    //MemStream.Write(System.Text.Encoding.Default.GetBytes(midStr), 0, midStr.Length);
                    this.StrBuilder.Append(midStr);
                else
                    //MemStream.Write(System.Text.Encoding.Default.GetBytes(lastStr), 0, lastStr.Length);
                    this.StrBuilder.Append(lastStr);


            }

            this.ValueStrList.Clear();
            //}
        }//WriteFileAndClearBuffer()

    }//class CBuilder_SqlFileStream
}
