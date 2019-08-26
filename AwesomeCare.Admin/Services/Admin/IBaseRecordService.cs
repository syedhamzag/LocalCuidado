using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Admin
{
  public  interface IBaseRecordService
    {
        [Get("/BaseRecord")]
        Task<List<GetBaseRecord>> GetBaseRecord();
        [Get("/BaseRecord/{id}")]
        Task<GetBaseRecord> GetBaseRecord(int id);
        [Get("/BaseRecord/GetBaseRecordsWithItems")]
        Task<List<GetBaseRecordWithItems>> GetBaseRecordsWithItems();
        [Get("/BaseRecord/GetBaseRecordWithItems/{id}")]
        Task<GetBaseRecordWithItems> GetBaseRecordWithItems(int id);

        [Get("/BaseRecord/GetBaseRecordItemById/{baseRecordItemId}")]
        Task<GetBaseRecordItem> GetBaseRecordItemById(int baseRecordItemId);

        [Post("/BaseRecord")]
        Task<List<GetBaseRecord>> PostBaseRecordWithItems([Body]PostBaseRecord baseRecord);

        [Put("/BaseRecordItem")]
        Task<GetBaseRecordItem> UpdateBaseRecordItem([Body]PutBaseRecordItem baseRecordItem);
    }
}
