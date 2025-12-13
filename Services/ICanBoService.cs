using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface ICanBoService
    {
        List<CanBo> GetAll();
        CanBo GetById(string id);
        void Add(CanBo canBo);
        void Update(CanBo canBo);
        void Delete(string id);
        string GenerateCanBoId();
    }
}
