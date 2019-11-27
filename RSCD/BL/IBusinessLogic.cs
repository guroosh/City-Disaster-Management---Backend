using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSCD.BLL
{
    public interface IBusinessLogic
    {
        Task<bool> CreateAsync(object request);
        Task<bool> UpdateDocumentAsync(object request);
        Task<bool> DeleteDocumentAsync(object request);
        Task<object> GetDocumentAsync(object request);
        Task<object> GetAllDocumentsAsync(object request = null);
    }
}
