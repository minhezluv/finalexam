
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Attributes;
using System.Text.RegularExpressions;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>         
    {
        #region DECLARE
        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            this._baseRepository = baseRepository;
            this._serviceResult = new ServiceResult();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NQMINH(20/08/2021)
        public ServiceResult GetAll()
        {
            this._serviceResult.Data = this._baseRepository.GetAll();
            return this._serviceResult;
        }
        /// <summary>
        /// Lấy dữ liệu bằng ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///CreatedBy: NQMINH(20/08/2021)
        public ServiceResult GetById(Guid id)
        {
           
            this._serviceResult.Data = this._baseRepository.GetById(id);
            return this._serviceResult;
        }


        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">Dữ liệu truyền vào</param>
        /// <returns></returns>
        /// CreatedBy: NQMINH(20/08/2021)
        public virtual ServiceResult Add(MISAEntity entity)
        {
            //1. Validate du lieu va xu ly nghiep vu:
            if (!ValidateRequired(entity))
            {
                _serviceResult.IsValid = false;
                return _serviceResult;
            };
            if (!CheckExist(entity))
            {
                _serviceResult.IsValid = false;
                return _serviceResult;
            }
            if (!ValidateForm(entity))
            {
                _serviceResult.IsValid =false;
                return _serviceResult;
            }
            //2.Them moi
            this._serviceResult.Data = this._baseRepository.Add(entity);
            return this._serviceResult;
        }

        public virtual ServiceResult Update(Guid id, MISAEntity entity)
        {
            //1.validate du lieu va xu ly nghiep vu:
            if (!ValidateRequired(entity))
            { 
               
                _serviceResult.IsValid = ValidateRequired(entity);
                return _serviceResult;
            };
            if (!CheckExist(entity))
            {
                _serviceResult.IsValid = CheckExist(entity);
                return _serviceResult;
            }
            if (!ValidateForm(entity))
            {
                return _serviceResult;
            }
            //2.Cap nhat
            this._serviceResult.Data = this._baseRepository.Update(id, entity);

            return this._serviceResult;
        }

        public ServiceResult Delete(Guid id)
        {
            this._serviceResult.Data = this._baseRepository.Delete(id);

            return this._serviceResult;
        }
        #endregion

        #region ValidateMethods
        /// <summary>
        /// Xử lí validate dữ liêu chung
        /// </summary>
        /// <param name="entity">đối tượng muốn thưc hiện validate</param>
        /// <returns>true - dữ liệu hơp lệ, false - dữ liệu không hợp lệ</returns>
        /// CreatedBy: NQMINH(18/08/2021)
        private bool ValidateRequired(MISAEntity entity)
        {
            var isValid = true;

            // Thuc hien validate:
            // Bat buoc nhap:
            // 1. Lay thong tin cac properties:
            var properties = typeof(MISAEntity).GetProperties();

            //2. Xac dinh  viec validate dựa trên attribute: (MISARequired - bat buoc check thong tin, khong duoc phep null  hoac de trong)

            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(entity);

                var propName = prop.Name;

                //Kiem tra xem property hien tai co bat buoc nhap hay khong

                var propMISARequireds = prop.GetCustomAttributes(typeof(MISARequired), true);
                if (propMISARequireds.Length > 0)
                {
                    var message = (propMISARequireds[0] as MISARequired)._message;
                    if (prop.PropertyType == typeof(string) && (propValue == null || propValue.ToString() == string.Empty))
                    {
                        isValid = false;
                        _serviceResult.Message = message;
                        return isValid;
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Check dữ liêu trước khi thêm mới
        /// </summary>
        /// <param name="entity">Thực thể cần check</param>
        /// <returns></returns>
        /// CreatedBy: NQMinh(19/08/2021)
        protected virtual bool ValidateForm(MISAEntity entity)
        {
            var isValid = true;
            // Thuc hien validate:
            // Bat buoc nhap:
            // 1. Lay thong tin cac properties:
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var prop in properties)
            {
                //Gia tri cua field
                var propValue = prop.GetValue(entity);

                var propName = prop.Name;

                //Kiem tra xem property hien tai co dung dinh dang hay khong

                var propMISAFormats = prop.GetCustomAttributes(typeof(MISAFormat), true);
                if (propMISAFormats.Length > 0)
                {
                    var message = (propMISAFormats[0] as MISAFormat)._message;
                    var typeField = (propMISAFormats[0] as MISAFormat)._typeField;

                    switch (typeField)
                    {
                        case "Number":
                            var isMatchNumber = Regex.IsMatch((string)propValue, @"^[0-9]+$");
                            if (isMatchNumber == false)
                            {
                                isValid = false;
                                _serviceResult.Message = message;
                                return isValid;
                            }
                            break;
                        case "Email":
                            var emailFormat = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
                            var isMatch = Regex.IsMatch((string)propValue, emailFormat, RegexOptions.IgnoreCase);
                            if (isMatch == false)
                            {
                                isValid = false;
                                _serviceResult.Message = message;
                                return isValid;
                            }
                            break;
                        case "Alphabet":
                            var isMatchAlpha= Regex.IsMatch((string)propValue, @"^[a-zA-Z]+$", RegexOptions.IgnoreCase);
                            if (isMatchAlpha == true)
                            {
                                isValid = false;
                                _serviceResult.Message = message;
                                return isValid;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return isValid;
        }
        protected virtual bool CheckExist(MISAEntity entity)
        {
            var isValid = true;
            // Thuc hien validate:
            // Check ton tai:
            // 1. Lay thong tin cac properties:
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var prop in properties)
            {
                //Gia tri cua field
                var propValue = prop.GetValue(entity);

                var propName = prop.Name;

                //Kiem tra xem property hien tai co dung dinh dang hay khong

                var propMISAFormats = prop.GetCustomAttributes(typeof(MISACheckExist), true);
                if (propMISAFormats.Length > 0)
                {
                    var message = (propMISAFormats[0] as MISACheckExist)._message;
                    var typeField = (propMISAFormats[0] as MISACheckExist)._typeField;
                    var checkField = true;
                    switch (typeField)
                    {
                        case "EntityCode":
                            checkField = this._baseRepository.CheckCodeExists((string)propValue);
                           
                            break;
                        case "Email":
                            checkField = this._baseRepository.CheckEmailExists((string)propValue);
                            break;
                        case "MobilePhoneNumber":
                            checkField = this._baseRepository.CheckMobilePhoneNumber((string)propValue);
                            break;
                        default:
                            break;
                    }
                    if (checkField == false)
                    {
                        isValid = checkField;
                        _serviceResult.Message = message;
                        return isValid;
                    }
                }
            }

            return isValid;
        }
        #endregion

    }
}
