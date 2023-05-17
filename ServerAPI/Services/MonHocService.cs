using Microsoft.EntityFrameworkCore;
using ServerAPI.Models.Context;
using ServerAPI.Models.HocSinh;
using ServerAPI.Services.Interface;

namespace ServerAPI.Services
{
    public class MonHocService : IMonHocService
    {
        private readonly HocSinhDBContext _hocSinhDBContext;

        public MonHocService(HocSinhDBContext hocSinhDBContext)
        {
            _hocSinhDBContext = hocSinhDBContext;
        }
        public async Task<bool> AddMonHoc(MonHoc monHoc)
        {
            if (_hocSinhDBContext.MonHocs.Any(s => s.MaMH == monHoc.MaMH))
                return false;
            else
            {
                await _hocSinhDBContext.MonHocs.AddAsync(monHoc);
                await _hocSinhDBContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> RemoveMonHoc(int maMH)
        {
            if (!_hocSinhDBContext.MonHocs.Any(s => s.MaMH == maMH))
                return false;
            else
            {
                _hocSinhDBContext.Remove(await _hocSinhDBContext.MonHocs.FirstOrDefaultAsync(s => s.MaMH == maMH));
                await _hocSinhDBContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
