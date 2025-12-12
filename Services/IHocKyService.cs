using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface IHocKyService
    {
        List<HocKy> GetAll();
        HocKy GetById(string id);
        void Add(HocKy hocKy);
        void Update(HocKy hocKy);
        void Delete(string id);
    }
}
