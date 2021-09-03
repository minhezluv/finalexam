using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISANotMap : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public string _fieldName = string.Empty;
        public string _message = string.Empty;

        public MISARequired(string fieldName)
        {
            _message = $"Thông tin {fieldName} không được để trống!";
            _fieldName = fieldName;

        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAFormat : Attribute
    {
        public string _fieldName = string.Empty;
        public string _message = string.Empty;
        public string _typeField = string.Empty;

        public MISAFormat(string typeField, string fieldName)
        {
            switch (typeField)
            {
                case "Number":
                    _message = $"Thông tin {fieldName} chỉ chứa chữ số!";
                  
                    break;
                case "Email":
                    _message = $"Thông tin {fieldName} không đúng định dạng!";
                  
                    break;
                case "Alphabet":
                    _message = $"Thông tin {fieldName} chỉ chứa chữ cái!";
                   
                    break;
                default:
                    break;
            }
            _typeField = typeField;
            _fieldName = fieldName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MISACheckExist : Attribute
    {
        public string _message = string.Empty;
        public string _typeField = string.Empty;
        public MISACheckExist(string typeField, string fieldName)
        {
            _message = $"Thông tin {fieldName} đã tồn tại!";
            _typeField = typeField;     
        }
    }


}
