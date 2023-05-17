using Microsoft.EntityFrameworkCore;
using ServerAPI.Models.Context;
using ServerAPI.Models.HocSinh;
using ServerAPI.Services.Interface;

namespace ServerAPI.Services
{
    public class HocSinhService : IHocSinhService
    {
        private readonly HocSinhDBContext _hocSinhContext;

        public HocSinhService(HocSinhDBContext hocSinhContext)
        {
            _hocSinhContext = hocSinhContext; 
        }
        public async Task<bool> AddHocSinh(HocSinh hocSinh)
        {
            if (_hocSinhContext.HocSinhs.Any(s => s.MaHS == hocSinh.MaHS))
                return false;
            else
            {
                await _hocSinhContext.HocSinhs.AddAsync(hocSinh);
                await _hocSinhContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteHocSinh(int maHS)
        {
            if (!_hocSinhContext.HocSinhs.Any(s => s.MaHS == maHS))
                return false;
            else
            {
                var oldHs = await _hocSinhContext.HocSinhs.FirstOrDefaultAsync(s => s.MaHS == maHS);
                _hocSinhContext.HocSinhs.Remove(oldHs);
                await _hocSinhContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<HocSinh>> GetAllHocSinh()
        {
            return await _hocSinhContext.HocSinhs.ToListAsync();
        }

        public async Task<HocSinh> GetHocSinhByMaHS(int maHS)
        {
            return await _hocSinhContext.HocSinhs.FirstOrDefaultAsync(s => s.MaHS == maHS);
        }

        public async Task<List<HocSinh>> GetHocSinhByTenHocSinh(string tenHS)
        {
            return await _hocSinhContext.HocSinhs.Where(s=> s.TenHS.Contains(tenHS)).ToListAsync();
        }

        public async Task<HocSinh> UpdateHocSinh(int maHS, HocSinh hocSinh)
        {
            if (!_hocSinhContext.HocSinhs.Any(s => s.MaHS == maHS))
                return null;
            else
            {
                var oldHS = await _hocSinhContext.HocSinhs.FirstOrDefaultAsync(s => s.MaHS == maHS);
                oldHS.DiaChi = hocSinh.DiaChi;
                oldHS.GhiChu = hocSinh.GhiChu;
                oldHS.MaLop = hocSinh.MaLop;
                oldHS.TenHS = hocSinh.TenHS;
                oldHS.TuoiHS = hocSinh.TuoiHS;
                await _hocSinhContext.SaveChangesAsync();
                return hocSinh;
            }
        }
    }
}
