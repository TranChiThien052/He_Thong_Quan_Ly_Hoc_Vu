using QuanLyHocVu.Models;
using System.Collections.Generic;

namespace QuanLyHocVu.Services
{
    public interface IChuongTrinhDaoTaoService
    {
        List<ChuongTrinhDaoTao> GetAll();
        ChuongTrinhDaoTao GetById(string id);
        void Add(ChuongTrinhDaoTao ctdt);
        void Update(ChuongTrinhDaoTao ctdt);
        void Delete(string id);
    }
}
