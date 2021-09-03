using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntitiesController<MISAEntity> : ControllerBase
    {
        IBaseService<MISAEntity> _baseService;
        IBaseRepository<MISAEntity> _baseRepository;
        string _className;
        public BaseEntitiesController(IBaseService<MISAEntity> baseService, IBaseRepository<MISAEntity> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
            _className = typeof(MISAEntity).Name;
        }

        /// <summary>
        /// Lấy danh sách entity
        /// </summary>
        /// <returns>danh sách entity</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var serviceResult = this._baseService.GetAll();

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return BadRequest(serviceResult);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.AMIS.Core.Properties.Resources.Exception_default,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Lấy entity theo id
        /// </summary>
        /// <param name="id">entity</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var serviceResult = this._baseService.GetById(id);

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Message,
                        userMsg = serviceResult.Message,
                        errorCode = "misa-001",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.AMIS.Core.Properties.Resources.Exception_default,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Thêm entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Add(MISAEntity entity)
        {
            try
            {
                var serviceResult = this._baseService.Add(entity);

                if (serviceResult.IsValid == true)
                {
                    if ((int)serviceResult.Data > 0)
                    {
                        return StatusCode(201, serviceResult.Data);
                    }
                    else
                    {
                        var errObj = new
                        {
                            devMsg = "SQL command error: ADD but RowEffect = 0",
                            userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                            errorCode = "misa-001",
                            moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                            traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                        };

                        return StatusCode(500, errObj);
                    }
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Message,
                        userMsg = serviceResult.Message,
                        errorCode = "misa-001",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return StatusCode(400,errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.AMIS.Core.Properties.Resources.Exception_default,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }
        /// <summary>
        /// Sửa entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, MISAEntity entity)
        {
            try
            {
                var serviceResult = this._baseService.Update(id, entity);

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Message,
                        userMsg = serviceResult.Message,
                        errorCode = "misa-001",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                    };
                    return BadRequest(serviceResult.Data);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.AMIS.Core.Properties.Resources.Exception_default,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }
        /// <summary>
        /// Xóa entity theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var serviceResult = this._baseService.Delete(id);

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Message,
                        userMsg = serviceResult.Message,
                        errorCode = "misa-001",
                        moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                        traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3f8"
                    };
                    return BadRequest(serviceResult.Data);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.AMIS.Core.Properties.Resources.Exception_default,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = "ba9587fd-1a79-4ac5-a0ca-2c9f74dfd3fb"
                };

                return StatusCode(500, errObj);
            }
        }
    }
}
