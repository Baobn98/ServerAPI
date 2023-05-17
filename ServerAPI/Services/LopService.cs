using Microsoft.EntityFrameworkCore;
using ServerAPI.Models.Context;
using ServerAPI.Models.HocSinh;
using ServerAPI.Services.Interface;

namespace ServerAPI.Services
{
    public class LopService : ILopService
    {
        private readonly HocSinhDBContext _hocSinhDBContext;

        public LopService(HocSinhDBContext hocSinhDBContext)
        {
            _hocSinhDBContext = hocSinhDBContext;
        }
        public async Task<bool> AddLop(Lop lop)
        {
            if (_hocSinhDBContext.Lops.Any(c => c.MaLop == lop.MaLop))
                return false;
            else
            {
                await _hocSinhDBContext.Lops.AddAsync(lop);
                await _hocSinhDBContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteLop(int maLop)
        {
            if (!_hocSinhDBContext.Lops.Any(c => c.MaLop == maLop))
                return false;
            else
            {
                _hocSinhDBContext.Remove(await _hocSinhDBContext.Lops.FirstOrDefaultAsync(c => c.MaLop == maLop));
                await _hocSinhDBContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Lop>> GetAllLop()
        {
            return await _hocSinhDBContext.Lops.ToListAsync();
        }

        public async Task<List<HocSinh>> GetHocSinhByMaLop(int maLop)
        {
            return await _hocSinhDBContext.HocSinhs.Where(s=> s.MaLop == maLop).ToListAsync();
        }

        public async Task<List<Lop>> TimKiemLop(string maTimkem)
        {
            return await _hocSinhDBContext.Lops.Where(c => c.TenLop.Contains(maTimkem)).Where(c=> c.MaLop.ToString().Contains(maTimkem)).ToListAsync();
        }

        public async Task<Lop> UpdateLop(int maLop, Lop lop)
        {
            if (!_hocSinhDBContext.Lops.Any(c => c.MaLop == maLop))
                return null;
            else
            {
                var oldLop = await _hocSinhDBContext.Lops.FirstOrDefaultAsync(c=> c.MaLop == maLop);
                oldLop.TenLop = lop.TenLop;
                oldLop.HocSinh = lop.HocSinh;
                await _hocSinhDBContext.SaveChangesAsync();
                return lop;
            }
        }
    }
}
