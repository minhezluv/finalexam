using Dapper;
using MISA.AMIS.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
       // protected readonly IConfiguration _configuration;
        protected IDbConnection _dbConnection;
        protected const string connectString = "Host = 47.241.69.179;" +
                    "Database =WEB07.TEST.MF932.NQMINH;" +
                    "User Id = dev;" +
                    "Password = 12345678";
        private readonly string _className;

        public BaseRepository()
        {
            this._dbConnection = new MySqlConnection(connectString);
            this._className = typeof(MISAEntity).Name;
        //    _configuration = configuration;
        }
        /// <summary>
        /// Thêm mới entity vào database
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns>
        /// 1 - thêm thành công
        /// </returns>
        /// CreatedBy: NQMINH(29/8/2021)
        public int Add(MISAEntity entity)
        {
            var dynamicParam = new DynamicParameters();

            ////3. Them du lieu vao db:

            ////Doc tung prop cua obj:
            var properties = entity.GetType().GetProperties();

            ////Duyet tung prop:
            foreach (var prop in properties)
            {
                //lay ten cua prop
                var propName = prop.Name;

                //lay val cu prop
                var propValue = prop.GetValue(entity);

                //Them param tuong ung voi moi prop
                dynamicParam.Add($"@{propName}", propValue);
            }
            var rowsEffect = _dbConnection.Execute($"Proc_Insert{_className}", param: dynamicParam, commandType: CommandType.StoredProcedure);

            return rowsEffect;
        }
        /// <summary>
        /// Xóa 1 entity theo id trong database
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>
        /// 1 - xóa thành công
        /// </returns>
        /// CreatedBy: NQMINH (29/8/2021)
        public int Delete(Guid entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@dynamicId", entityId);
            var sqlCommand = $"DELETE FROM {_className} WHERE {_className}Id = @dynamicId";
            var rowEffect = _dbConnection.Execute(sqlCommand, param: parameters);
            return rowEffect;
        }
        /// <summary>
        /// Lấy tất cả entity
        /// </summary>
        /// <returns>
        /// </returns>
        /// CreatedBy: NQMINH (29/8/2021)
        //Để virtual vào hàm có thể override
        public virtual List<MISAEntity> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
           
            //trycatch
            var sqlCommand = $"SELECT * From View_{_className} ";
            var entities = _dbConnection.Query<MISAEntity>(sqlCommand, param: parameters);
            return entities.ToList();
        }
        /// <summary>
        /// Lấy thông tin entity theo id từ database
        /// </summary>
        /// <param name="id">id entity</param>
        /// <returns></returns>
        /// CreatedBy: NQMINH (29/8/2021)
        public MISAEntity GetById(Guid entityId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EntityIdParam", entityId);
            var sqlCommand = $"SELECT * FROM {_className} WHERE {_className}Id = @EntityIdParam";
            var entity = _dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: parameters);
            return entity;
        }
        /// <summary>
        /// Cập nhật entity theo id
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <param name="entity">thông tin entity</param>
        /// <returns>
        /// 1 - thêm thành công
        /// </returns>
        /// CreatedBy: NQMINH (29/8/2021)
        public int Update(Guid id, MISAEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            var transaction = _dbConnection.BeginTransaction();
            //  var columsName = string.Empty;
            var dynamicParam = new DynamicParameters();

            ////3. Them du lieu vao db:

            ////Doc tung prop cua obj:
            var properties = entity.GetType().GetProperties();

            ////Duyet tung prop:
            foreach (var prop in properties)
            {
                //lay ten cua prop
                var propName = prop.Name;

                //lay val cu prop
                var propValue = prop.GetValue(entity);

                //Them param tuong ung voi moi prop
                dynamicParam.Add($"@{propName}", propValue);

            }
            var rowEffect = _dbConnection.Execute($"Proc_Update{_className}", transaction: transaction, param: parameters, commandType: CommandType.StoredProcedure);
            transaction.Commit();
            return rowEffect;           
        }

        public bool CheckCodeExists(string entityCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Code", entityCode);
            //var rowEffect = _dbConnection.Execute($"Proc_Check{_className}CodeExist", param: parameters, commandType: CommandType.StoredProcedure);
            var sqlCommand= $"SELECT EXISTS(SELECT 1 FROM {_className} WHERE {_className}.{_className}Code = @Code)";
            var rowEffect = _dbConnection.Execute(sqlCommand, param: parameters);
            //Mã đã tồn tại
            if(rowEffect != 0)
            {
                return false;
            }
            return true;
        }

        public bool CheckEmailExists(string email)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            int rowEffect = _dbConnection.Execute($"Proc_Check{_className}EmailExist", param: parameters, commandType: CommandType.StoredProcedure);
            //Email đã tồn tại
            if (rowEffect == 1)
            {
                return false;
            }
            return true;
        }

        public bool CheckMobilePhoneNumber(string mobilePhoneNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MobilePhoneNumber", mobilePhoneNumber);
            int rowEffect = _dbConnection.Execute($"Proc_Check{_className}MobilePhoneNumberExist", param: parameters, commandType: CommandType.StoredProcedure);
            //Số điện thoại đã tồn tại
            if (rowEffect == 1)
            {
                return false;
            }
            return true;
        }
    }
}
