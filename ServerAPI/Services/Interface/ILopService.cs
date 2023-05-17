using ServerAPI.Models.HocSinh;

namespace ServerAPI.Services.Interface
{
    public interface ILopService
    {
        public Task<bool> AddLop(Lop lop);

        public Task<Lop> UpdateLop(int maLop, Lop lop);
        public Task<bool> DeleteLop(int maLop);
        public Task<List<HocSinh>> GetHocSinhByMaLop(int maLop);
        public Task<List<Lop>> GetAllLop();
        public Task<List<Lop>> TimKiemLop(string maTimkem);
    }
}
